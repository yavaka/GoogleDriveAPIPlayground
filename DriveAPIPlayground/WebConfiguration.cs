using DriveAPIPlayground.ServiceModels;
using DriveAPIPlayground.Services;
using Google.Apis.Auth.AspNetCore3;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace DriveAPIPlayground
{
    public static class WebConfiguration
    {
        public static IServiceCollection AddWeb(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllersWithViews();

            services.AddTransient(s =>
            {
                var credential = GoogleCredential.FromFile("in-code-auth.json")
                    .CreateScoped(new[] { DriveService.Scope.Drive });

                var service = new DriveService(new BaseClientService.Initializer
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "In code auth"
                });

                return service;
            });

            services.AddTransient<IGoogleDriveService, GoogleDriveService>();

            return services;
        }
    }
}
