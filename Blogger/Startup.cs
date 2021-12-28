﻿using Blogger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

public class Startup
{
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
        //services.AddScoped<AppDbContext>();
        string connString = "server=localhost;database=Articles;user=root;password=imgmongouser";
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 27, 1));
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
