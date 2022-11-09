using JuleBeer.DB.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NSwag;
using NSwag.Generation.Processors.Security;
using System.Security.Claims;

namespace JuleBeer.Extensions;

public static class ServiceCollectionExtensions
{
    public static void ConfigureDataContext(this IServiceCollection services, IConfiguration configuration)
    {
        if (services != null)
        {
            JuleBeerContext.ConnectionString = configuration.GetConnectionString("BeerDB");
            services.AddDbContextFactory<JuleBeerContext>();
            services.AddDbContext<JuleBeerContext>();
            using var dbContext = new JuleBeerContext();
            dbContext.Database.Migrate();
        }
    }

    public static void ConfigureControllers(this IServiceCollection services)
    {
        services.AddControllers(
            opt =>
            {
                opt.Filters.Add(new ProducesAttribute("application/json"));
            });
    }

    public static void ConfigureSwagger(this IServiceCollection services, AppSettings appSettings)
    {
        services.AddOpenApiDocument(options =>
        {
            options.AddSecurity("Bearer", Enumerable.Empty<string>(),
             new OpenApiSecurityScheme
             {
                 Name = "Authorization",
                 Type = OpenApiSecuritySchemeType.ApiKey,
                 Scheme = "Bearer",
                 BearerFormat = "JWT",
                 In = OpenApiSecurityApiKeyLocation.Header,
                 Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
             });
            options.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("Bearer"));

            var apiSettings = appSettings.API;
            
            options.PostProcess = (openApiDocument) =>
            {
                var info = new OpenApiInfo()
                {
                    Title = $"{apiSettings.Title} {2}",
                    Version = "2",
                    Description = apiSettings?.Description,
                    Contact = (apiSettings != null && apiSettings.Contact != null) ? new OpenApiContact { Name = apiSettings.Contact.Name, Email = apiSettings.Contact.Email, Url = apiSettings.Contact.Url } : null,
                    License = (apiSettings != null && apiSettings.License != null) ? new OpenApiLicense { Name = apiSettings.License.Name, Url = apiSettings.License.Url } : null,
                    TermsOfService = !string.IsNullOrEmpty(apiSettings?.TermsOfServiceUrl) ? apiSettings.TermsOfServiceUrl : null
                };
                openApiDocument.Info = info;
            };

        });
    }




    public static void ConfigureJwt(this IServiceCollection services, IConfiguration config)
    {
        if (services != null)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(JwtBearerDefaults.AuthenticationScheme, policy =>
                {
                    policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
                    policy.RequireClaim(ClaimTypes.PrimarySid);     // id
                    policy.RequireClaim(ClaimTypes.NameIdentifier); // username
                });
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = TokenSettings.GetTokenValidationParameters(config["JWT:Secret"], config["JWT:ValidAudience"], config["JWT:ValidIssuer"]);
                });
        }
    }

}
