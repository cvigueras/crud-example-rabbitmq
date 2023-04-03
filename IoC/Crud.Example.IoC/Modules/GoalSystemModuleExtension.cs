using System.Text;
using Crud.Example.Domain.Core.Services;
using Crud.Example.Domain.Repositories;
using Crud.Example.Domain.Services;
using Crud.Example.Infrastructure.Auth.Interfaces;
using Crud.Example.Infrastructure.Auth.Services;
using Crud.Example.Infrastructure.Data.Context;
using Crud.Example.Infrastructure.Data.Repositories;
using Crud.Example.Infrastructure.Messaging.Models;
using Crud.Example.Infrastructure.Messaging.Services;
using Crud.Example.Main.Auth.Interfaces;
using Crud.Example.Main.Auth.Services;
using Crud.Example.Main.Interfaces;
using Crud.Example.Main.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Crud.Example.IoC.Modules
{
    public static class ModuleExtension
    {
        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("ConnStr")));
        }

        public static void AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IGetDealerService, GetDealerService>();
            services.AddScoped<IDealerRepository, EFDealerRepository>();
            services.AddScoped<IShopRepository, EFShopRepository>();
            services.AddScoped<IOrderRepository, EFOrderRepository>();
            services.AddScoped<IFoodRepository, EFFoodRepository>();
            services.AddScoped<IDealerService, DealerService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ICreateDealerService, CreateDealerService>();
            services.AddScoped<ICreateOrderService, CreateOrderService>();
            services.AddScoped<IGetShopService, GetShopService>();
            services.AddScoped<IGetOrderService, GetOrderService>();
            services.AddScoped<ICreateShopService, CreateShopService>();
            services.AddScoped<IShopService, ShopService>();
            services.AddScoped<IRemoveShopService, RemoveShopService>();
            services.AddScoped<ICheckShopExpiresServices, CheckShopExpiresServices>();
            services.AddSingleton<AmpqService>();
            services.Configure<AmpqInfo>(configuration.GetSection("amqp"));
        }

        public static void ConfigureIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
        }

        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            // Adding Jwt Bearer
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = configuration["JWT:ValidAudience"],
                    ValidIssuer = configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
                };
            });
        }

        public static void AddSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new List<string>()
                    }
                });
            });
        }
    }
}