using Blogger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration config)
    {
        Configuration = config;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddRazorPages()
                .AddRazorPagesOptions(opts =>
                {
                    //opts.Conventions.AddPageRoute("/Index", "/page/{pageno:int}");
                    //opts.Conventions.AddPageRoute("/Category", "/{category}/page/{pageno:int}");
                });
        services.Configure<RouteOptions>(options =>
        {
            options.AppendTrailingSlash = true;
            options.LowercaseUrls = true;
            options.LowercaseQueryStrings = true;
        });
        services.AddScoped<ArticleService>();
        var connString = Configuration.GetConnectionString("DefaultConnection");
        //Console.WriteLine(connString);
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 21));
        services.AddDbContext<AppDbContext>(options => options.UseMySql(connString, serverVersion));
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }
        app.UseStatusCodePagesWithReExecute("/{0}");
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapRazorPages();
        });
    }
}
