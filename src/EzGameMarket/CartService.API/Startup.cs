using CartService.API.Data;
using CartService.API.Services;
using EventBus.MockedTest;
using EventBus.Shared.Abstraction;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace CartService.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            AddJWT(services);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });

            services.AddDbContext<CartDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<ICartRepository, CartRepository>();

            services.AddSingleton<IEventBusRepository, MagicBus>();
        }

        private void AddJWT(IServiceCollection services)
        {
            var key = Encoding.UTF8.GetBytes(Configuration.GetValue<string>("Secret"));

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    AuthenticationType = "Identity.Application"
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseRouting();

            app.Use(async (context, next) =>
            {
                var authenticationCookieName = "access_token";
                var cookie = context.Request.Cookies[authenticationCookieName];
                if (cookie != null)
                {
                    if (context.Request.Headers.ContainsKey("Authorization") == false)
                    {
                        context.Request.Headers.Add("Authorization", "Bearer " + cookie);
                    }
                }

                await next();
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}