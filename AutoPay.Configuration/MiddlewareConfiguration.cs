using System;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AutoPay.DataLayer;
using AutoPay.Entities;
using AutoPay.Infrastructure.DataLayer;
using AutoPay.Infrastructure.Managers;
using AutoPay.Infrastructure.Services;
using AutoPay.Managers;
using AutoPay.Mappers;
using AutoPay.Services;

namespace AutoPay.Configuration
{
    public class MiddlewareConfiguration
    {
        public static void ConfigureEf(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));
        }

        public static void ConfigureIdentity(IServiceCollection services)
        {
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<DataContext>()
                .AddDefaultTokenProviders();

            //configure identity options
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
            });
            //configure auth cookie
            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/access-denied";
                options.Cookie.Name = ".ASPAUTHCC";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.LoginPath = "/account/login";
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                options.SlidingExpiration = true;
            });
        }

        public static void ConfigureUoW(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void ConfigureRepositories(IServiceCollection services)
        {
            services.AddScoped<IDapperRepository, DapperRepository>();

            services.AddScoped<IRepository<Country>, Repository<Country>>();
            services.AddScoped<IRepository<RemoteDbConfig>, Repository<RemoteDbConfig>>();
            services.AddScoped<IRepository<Batch>, Repository<Batch>>();
            services.AddScoped<IRepository<BatchCustomer>, Repository<BatchCustomer>>();
            services.AddScoped<IRepository<BatchCustomerDueDetail>, Repository<BatchCustomerDueDetail>>();
            services.AddScoped<IRepository<Customer>, Repository<Customer>>();
            services.AddScoped<IRepository<Payment>, Repository<Payment>>();
            services.AddScoped<IRepository<PaymentError>, Repository<PaymentError>>();
        }

        public static void ConfigureManagers(IServiceCollection services)
        {
            services.AddScoped<IDataSeedManager, DataSeedManager>();
            services.AddScoped<IMasterDataManager, MasterDataManager>();
            services.AddScoped<IRemoteDbConfigManager, RemoteDbConfigManager>();
            services.AddScoped<IRemoteDbManager, RemoteDbManager>();
            services.AddScoped<IBatchManager, BatchManager>();
            services.AddScoped<IBatchCustomerManager, BatchCustomerManager>();
            services.AddScoped<ICustomerManager, CustomerManager>();
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ICryptographyService, CryptographyService>();
            services.AddScoped<IPaymentService, PaymentService>();
        }

        public static void ConfigureAutoMapper(IServiceCollection services)
        {

            var mappingConfig = new MapperConfiguration(options =>
            {
                options.AddProfile(new MappingProfile());
            });

            var mapper = mappingConfig.CreateMapper();

            services.AddSingleton(mapper);

            services.AddAutoMapper();
        }
    }
}
