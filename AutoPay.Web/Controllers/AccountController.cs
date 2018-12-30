using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using AutoPay.Infrastructure.Services;
using AutoPay.Utilities;
using AutoPay.ViewModels.Account;
using AutoPay.Web.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Memory;

namespace AutoPay.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMemoryCache _cache;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ICryptographyService _cryptographyService;

        public AccountController(IMemoryCache cache,
            ICryptographyService cryptoService,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ICryptographyService cryptographyService)
        {
            _cache = cache;
            _userManager = userManager;
            _signInManager = signInManager;
            _cryptographyService = cryptographyService;
        }

        public async Task<IActionResult> Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _signInManager.SignOutAsync();
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm]LoginVm model)
        {
            if (!ModelState.IsValid)
                return View(model);

            //get user
            var user = await _userManager.FindByNameAsync(model.Username);
            //register if user not already exists
            if (user == null)
            {
                //initiate user instance
                user = new IdentityUser
                {
                    UserName = model.Username,
                    Email = string.Empty,
                    PhoneNumber = string.Empty
                };
                //create user
                var identityResult = await _userManager.CreateAsync(user, model.Password);
                //return if user creation failed
                if (!identityResult.Succeeded)
                {
                    this.SetResponse(ResponseType.Error, identityResult.Errors.First().Description);
                    return View(model);
                }
                //assign role to user
                identityResult = await _userManager.AddToRoleAsync(user, UserType.User);
                //delete user and return if role assignment get failed
                if (!identityResult.Succeeded)
                {
                    await _userManager.DeleteAsync(user);
                    this.SetResponse(ResponseType.Error, identityResult.Errors.First().Description);
                    return View(model);
                }
                //add encryption key validator claim
                var claim = new Claim(ClaimTypes.Thumbprint, _cryptographyService.Encrypt(model.Secret, model.Secret));
                await _userManager.AddClaimAsync(user, claim);
            }
            //validate encryption key
            var claims = await _userManager.GetClaimsAsync(user);
            try
            {
                _cryptographyService.Decrypt(claims.Single(x => x.Type.Equals(ClaimTypes.Thumbprint)).Value,
                    model.Secret);
            }
            catch
            {
                this.SetResponse(ResponseType.Error, "Invalid encryption key. Please enter correct encryption key.");
                return View(model);
            }
            //do login
            var signInResult = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
            //redirect to home if success
            if (signInResult.Succeeded)
            {
                //store secret key to cache
                _cache.Set(Utility.GetEncryptionKeyName(user.Id), model.Secret);
                //redirect to home
                return RedirectToAction("index", "home");
            }

            if (signInResult.IsLockedOut)
            {
                this.SetResponse(ResponseType.Error, "Your account has been locked out. Please contact to administrator.");
                return View(model);
            }

            if (signInResult.IsNotAllowed)
            {
                this.SetResponse(ResponseType.Error, "Your account has been disabled. Please contact to administrator.");
                return View(model);
            }

            this.SetResponse(ResponseType.Error, "Invalid username or password.");

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            _cache.Remove(Utility.GetEncryptionKeyName(User.GetUserId()));

            await _signInManager.SignOutAsync();

            return RedirectToAction("login", "account");
        }

        [Authorize]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword([FromForm]ChangePasswordVm model)
        {
            if (!ModelState.IsValid) return View(model);
            //get user
            var user = await _userManager.FindByIdAsync(User.GetUserId());
            //change password
            var identityResult = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            //set response
            if (identityResult.Succeeded)
            {
                this.SetResponse(ResponseType.Success, "Password has been changed successfully.");
            }
            else
            {
                this.SetResponse(ResponseType.Error, identityResult.Errors.First().Description);
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ResetThumbprint([FromQuery]string username, [FromQuery]string encKey)
        {
            var user = await _userManager.FindByNameAsync(username);
            var claims = await _userManager.GetClaimsAsync(user);
            if (claims.Any(x => x.Type.Equals(ClaimTypes.Thumbprint)))
                return Ok("Thumbprint claim already exists");
            //add encryption key validator claim
            var claim = new Claim(ClaimTypes.Thumbprint, _cryptographyService.Encrypt(encKey, encKey));
            await _userManager.AddClaimAsync(user, claim);
            return Ok("Success!!");
        }
    }
}