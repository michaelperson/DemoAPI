using ApiMiddleware;
using BLL.Interface;
using BLL.Models;
using BLL.Services;
using DAL.Interface;
using DAL.Services;
using DemoAPI.Hubs;
using DemoAPI.Models;
using DemoAPI.Tools.Logging.Interfaces;
using DemoAPI.Tools.Logging.NLog;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DemoAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            //Chargement de la configuration à partir de notre fichier de config
            LogManager.LoadConfiguration($"{Directory.GetCurrentDirectory()}/NLog.config");
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public string policyName = "LocalHostPolicy";
        public string policyName2 = "NoOriginPolicy";
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //DI de notre logger
            services.AddScoped<ILoggerManager, LoggerManager>();

            //CORS
            services.AddCustomCorsPolicy(policyName2, new List<string>() { "http://127.0.0.1:5500'" });
            services.AddCustomCorsPolicy(policyName, new List<string>() { "https://*.mondomaine.com" });


            //SignalR
            services.AddSignalR(options =>
            {
                options.EnableDetailedErrors = true;
          
            });



            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(
                 options =>
                 {
                     options.SaveToken = true;
                     options.TokenValidationParameters = new TokenValidationParameters()
                     {
                         ClockSkew = new TimeSpan(),
                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["jwt:key"])),
                         ValidateIssuerSigningKey = true,
                         ValidateLifetime = true,
                         ValidIssuer = Configuration["jwt:issuer"],
                         ValidAudience = Configuration["jwt:audience"],
                         ValidateAudience = true,
                         ValidateIssuer = true
                     };
                 }

                ); 

            services.AddSwaggerGen(c =>
            {
                services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(
                 options =>
                 {
                     options.SaveToken = true;
                     options.TokenValidationParameters = new TokenValidationParameters()
                     {
                         ClockSkew = new TimeSpan(),
                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["jwt:key"])),
                         ValidateIssuerSigningKey = true,
                         ValidateLifetime = true,
                         ValidIssuer = Configuration["jwt:issuer"],
                         ValidAudience = Configuration["jwt:audience"],
                         ValidateAudience = true,
                         ValidateIssuer = true
                     };
                 }

                );


                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TechniFutur - Contact Api", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer", //!!!!!LOWERCASE!!!!!
                    BearerFormat = "JWT", //!!!UPPERCASE!!!!
                    In = ParameterLocation.Header, //Dans le Header Http

                    Description = "JWT Bearer : \r\n Enter  Token"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                      {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = JwtBearerDefaults.AuthenticationScheme
                                }
                            },
                            new string[] {}

                    }

                });
            });
            services.AddControllers();

            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IContactRepoLibrary, ContactWithLibray>();
            services.AddScoped<IContactBusiness<BusinessContact>, BusinessContactService>();
            services.AddScoped<IUserBusiness<BusinessUser>, BusinessUserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DemoAPI v1"));
                
            } 

            //Global error Handling
            app.UseExceptionHandler
                (
                  MidErrors =>
                  {
                      LoggerManager manager = new LoggerManager();
                      MidErrors.Run(async context =>
                     {
                         //JE gère l'erreur suivant son context 
                         manager.LogCritic($"[Global Handler] {context.Response.StatusCode}");
                         IExceptionHandlerFeature lerreur = context.Features.Get<IExceptionHandlerFeature>();
                         if (lerreur != null)
                         {
                             manager.LogCritic($"[Global Handler - IExceptionHandlerFeature] {lerreur.Error}");
                         }
                         context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                         context.Response.ContentType = "application/json";
                         ErrorDetails error = new ErrorDetails()
                         {
                             StatusCode = context.Response.StatusCode,
                             Message = "Internal Error - Global catch"
                         };

                         await context.Response.WriteAsync(error.ToString());


                     });

                  }
                
                );
            

            app.UseHttpsRedirection();
           
            if (env.IsDevelopment())
            {
                app.UseCors(policyName2);
            }
            else
            {
                app.UseCors(policyName);
            }
            app.UseRouting();
                app.UseAuthentication(); 
            app.UseAuthorization();
          
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<MessageHub>("/messagehub");
            });
        }
    }
}
