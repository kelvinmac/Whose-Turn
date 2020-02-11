using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Whose_Turn.ConfigModels;
using Whose_Turn.Context;
using Whose_Turn.Context.Entities;
using Whose_Turn.Extensions;
using Whose_Turn.Managers;
using Whose_Turn.Services;

namespace Whose_Turn
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
            var reactConfig = new ReactConfigModel();
            Configuration.Bind("React", reactConfig);

            services.AddCors(options =>
            {
                options.AddPolicy("React",
                builder =>
                {
                    builder
                       .WithOrigins(reactConfig.Uri)
                       .AllowAnyHeader()
                       .AllowAnyMethod()
                       .AllowCredentials();
                });

            });

            services.AddEntityFrameworkSqlite()
                .AddDbContext<DatabaseContext>();

            var jwtConfig = new JwtTokenConfig();
            Configuration.Bind("JwtTokens", jwtConfig);

            services.AddTransient(services =>
            {
                Configuration.Bind("JwtTokens", jwtConfig);
                return jwtConfig;
            });

            services.AddTransient<PasswordHashing>();
            services.AddScoped<Usermanager>();

            services.AddTransient(s =>
            {
                var config = new SendGridConfiguration();
                Configuration.Bind("SendGrid", config);

                return config;
            });

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtConfig.Secret)),
                    ValidIssuer = jwtConfig.Issuer,
                    ValidAudience = jwtConfig.Audience,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                };
            });

            services.AddControllers().AddNewtonsoftJson(options =>
                                    {
                                        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                                        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                                    });

            // services.AddServiceBus(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("React");

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication(); // this one first
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}