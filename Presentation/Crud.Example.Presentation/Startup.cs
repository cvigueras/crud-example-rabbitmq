using Crud.Example.Api.Filters;
using Crud.Example.Api.Services;
using Crud.Example.Domain.Automapper;
using Crud.Example.Domain.Events;
using Crud.Example.Domain.Handler;
using Crud.Example.IoC.Modules;

namespace Crud.Example.Api
{
    public class Startup
    {
        public IConfiguration _configRoot
        {
            get;
        }
        public Startup(IConfiguration configuration)
        {
            _configRoot = configuration;
        }
        public void ConfigureServices(WebApplicationBuilder builder)
        {
            //Dependency injection.
            builder.Services.AddTransient<BearerAuthenticationFilterAttribute>();
            builder.Services.AddDependencyInjection(_configRoot);

            builder.Services.ConfigureIdentity(_configRoot);
            ModuleExtension.ConfigureJWT(builder.Services, _configRoot);
            builder.Services.AddDbContext(_configRoot);
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwagger(_configRoot);
            builder.Services.AddAutoMapper(typeof(Startup));
            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<AutomapperProfile>();
            });

            DomainEvent.serviceProvider = builder.Services.AddScoped<IDomainHandler<ShopRemovedEvent>, ShopRemovedHandler>().BuildServiceProvider();
            DomainEvent.serviceProvider = builder.Services.AddScoped<IDomainHandler<ShopExpiredEvent>, ShopExpiredHandler>().BuildServiceProvider();
            builder.Services.AddHostedService<TimedHostedService>();
        }
        public void Configure(WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}