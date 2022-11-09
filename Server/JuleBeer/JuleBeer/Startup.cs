using JuleBeer.Extensions;
using JuleBeer.Middleware;

namespace JuleBeer;

public class Startup
{
    private readonly IConfiguration Configuration;
    private readonly IConfigurationSection _appsettingsConfigurationSection;
    private readonly AppSettings _appSettings;

    public Startup(IConfiguration configuration, IWebHostEnvironment env)
    {
        Configuration = configuration;

        // AppSettings
        _appsettingsConfigurationSection = Configuration.GetSection(nameof(AppSettings));
        if (_appsettingsConfigurationSection == null)
        {
            throw new Exception("No appsettings has been found");
        }
        _appSettings = _appsettingsConfigurationSection.Get<AppSettings>();
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.Configure<AppSettings>(_appsettingsConfigurationSection);
        services.ConfigureDataContext(Configuration);
        services.ConfigureJwt(Configuration);
        services.ConfigureControllers();
        services.AddCors();

        //SWAGGER
        if (_appSettings.Swagger.Enabled)
        {
            services.ConfigureSwagger(_appSettings);
        }
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseMiddleware<ErrorHandlingMiddleware>();
        app.UseHsts();

        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
        );

        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });


        if (_appSettings.Swagger.Enabled)
        {
            app.UseOpenApi();
            app.UseSwaggerUi3(c =>
            {
                c.PersistAuthorization = true;
                c.OperationsSorter = "alpha";
                c.TagsSorter = "alpha";
            });
        }
    }
}
