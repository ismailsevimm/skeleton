using Skeleton.Core.Infrastructure;
using Skeleton.Core.Utilities;
using Skeleton.Data.Context;
using Skeleton.Models.Entities;
using Skeleton.Services.Configuration;
using Skeleton.Services.Interfaces;
using Skeleton.Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Skeleton.Services.Module
{
    public class ServiceModule : IDrawModule
    {
        private readonly IConfiguration Configuration;

        public ServiceModule(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
        }

        public void Load(IServiceCollection services)
        {
            services.AddScoped(typeof(IServiceGeneric<,>), typeof(ServiceGeneric<,>));

            services.AddScoped<AuthService>();

            services.AddScoped<TokenService>();

            services.Configure<CustomTokenOption>(Configuration.GetSection("TokenOptions"));

            services.Configure<List<ClientOption>>(Configuration.GetSection("Clients"));


            services.AddIdentity<User, IdentityRole>(Opt =>
            {
                Opt.User.RequireUniqueEmail = true;
                Opt.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<DrawDbContext>().AddDefaultTokenProviders();



            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opts =>
            {
                var tokenOptions = Configuration.GetSection("TokenOptions").Get<CustomTokenOption>();

                opts.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidIssuer = tokenOptions.Issuer,
                    ValidAudience = tokenOptions.Audience[0],
                    IssuerSigningKey = SecurityService.GetSymmetricSecurityKey(tokenOptions.SecurityKey),

                    ValidateIssuerSigningKey = true,
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

        }
    }
}
