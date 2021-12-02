using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace ApiMiddleware
{
    public static class ServiceExtension
    {

        #region Swagger

        #endregion

        #region Cors
         public static void AddCustomCorsPolicy(this IServiceCollection services, string policyName, List<string> origin=null)
        {
            services.AddCors(
                
                options => 
                { 
                  if(origin!=null)
                    {
                        options.AddPolicy(policyName, policy =>
                        {
                        policy.WithOrigins(origin.ToArray())
                            .AllowAnyMethod()  
                            .AllowCredentials()
                            .SetIsOriginAllowed((host) => true)
                            .AllowAnyHeader();
                        });
                    }
                  else
                    {
                        options.AddPolicy(policyName, policy =>
                        {
                            policy.AllowAnyOrigin()
                                  .AllowAnyMethod()
                                  .SetIsOriginAllowed((host) => true)
                                  .AllowAnyHeader();
                        });
                        
                    }
                
                
                }
                
                
                );
        }
        #endregion

        #region DI

        #endregion

        #region Security

        #endregion
    }
}
