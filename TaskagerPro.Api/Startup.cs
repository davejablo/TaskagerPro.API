using System;
using AutoMapper;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using TaskagerPro.Core.Identities;
using TaskagerPro.Core.Models;
using TaskagerPro.DAL;
using TaskagerPro.Services.Interfaces;
using TaskagerPro.Services.Repositories;
using Microsoft.OpenApi.Models;

namespace TaskagerPro.Api
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
            // Return Not Acceptable status code.
            services.AddControllers(setupAction =>
            {
                setupAction.ReturnHttpNotAcceptable = true;
            }).AddXmlDataContractSerializerFormatters(); // Return XML format.

            // Configure DbContext and Identity
            services.AddDbContext<TaskagerProContext>(options => options.UseSqlServer(Configuration.GetConnectionString("TaskagerPro")));
            services.AddIdentity<ApplicationUser, IdentityRole<Guid>>().AddEntityFrameworkStores<TaskagerProContext>();

            // Configure JWT Settings and regster it.
            var jwtSettingsConfiguration = Configuration.GetSection("JwtSettings");
            var jwtSettings = jwtSettingsConfiguration.Get<JwtSettingsModel>();
            var key = Encoding.ASCII.GetBytes(jwtSettings.SecretKey);
            services.AddSingleton(jwtSettings);

            // Configure JWT Authentication
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience
                };
            });
            // Configure Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Taskager Pro - API",
                    Description = "Endpoints documentation for Taskager Pro - API with ASP.NET Core 3.1",
                    Contact = new OpenApiContact
                    {
                        Email = "davejab97@gmail.com",
                    },
                    License = new OpenApiLicense
                    {
                        Name = "No license here :)",
                    }
                });
            });

            // Add AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Dependency injection container.

            services.AddTransient<IAccountRepository, AccountRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // Prevent client to receive stack trace at 500 status in production.
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("An unexpected error occured. Please try again later :C.");
                    });
                });
            }
            app.UseCors(x =>
            {
                x.AllowAnyOrigin();
                x.AllowAnyHeader();
                x.AllowAnyMethod();
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Swagger service register
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Trainer-Pro API V1");
            });
        }
    }
}
