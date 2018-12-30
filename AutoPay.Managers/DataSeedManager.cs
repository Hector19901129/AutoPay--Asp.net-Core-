using System;
using System.Threading.Tasks;
using AutoPay.Infrastructure.Managers;
using AutoPay.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AutoPay.Managers
{
    public class DataSeedManager : IDataSeedManager
    {
        private readonly ILogger _logger;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DataSeedManager(ILogger<DataSeedManager> logger,
            RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task InitializeAsync()
        {
            await SeedRolesAsync();
        }

        private async Task SeedRolesAsync()
        {
            try
            {
                if (await _roleManager.Roles.AnyAsync())
                    return;

                var identityResult = await _roleManager.CreateAsync(new IdentityRole(UserType.Admin));
                _logger.LogInformation($"Role ({UserType.Admin}) seed result: {identityResult}");

                identityResult = await _roleManager.CreateAsync(new IdentityRole(UserType.User));
                _logger.LogInformation($"Role ({UserType.User}) seed result: {identityResult}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in Role seeding {ex}");
            }
        }
    }
}
