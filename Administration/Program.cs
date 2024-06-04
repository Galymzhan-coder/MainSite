using Administration.Helpers;
using Administration.Interfaces;
using Administration.Services;
using LoggingService;
using Microsoft.OpenApi.Models;
using Models.Entities;
using Models.FFIFND;
using NLog.Web;
using Services.Factory;
using Services.Factory.Interfaces;
using Services.FND;
using Services.FND.Interfaces;
using Services.SQLCommandBuilder.Interfaces;
using Services.SQLCommandBuilder.PgSQLCommands;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IUserInterface, UserServices>();
builder.Services.AddScoped<ODDANP>();
builder.Services.AddScoped<CategoryDTO>();
builder.Services.AddScoped<ICategoriesService, CategoriesService>();
builder.Services.AddScoped<IPagesService, PagesService>();
builder.Services.AddScoped<IContentService, ContentService>();
builder.Services.AddScoped<IServiceFactory, ServiceFactory>();


builder.Services.AddScoped<ISQLReadCommands, CPgReadCommands>();
builder.Services.AddScoped<ISQLUpdateCommands, CPgUpdateCommands>();
builder.Services.AddScoped<ISQLCreateCommands, CPgCreateCommands>();
builder.Services.AddScoped<ISQLDeleteCommands, CPgDeleteCommands>();
builder.Services.AddScoped<ILogService, LogService>();

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
builder.Services.AddControllers()
           .AddNewtonsoftJson(options =>
               options.SerializerSettings.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto);

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath) 
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true); 


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Core Service", Version = "v1" });
    c.DescribeAllParametersInCamelCase();
}
);
builder.Logging.AddConsole();

builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection("ConnectionStrings"));

var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
builder.Logging.ClearProviders();
builder.Host.UseNLog();


var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseCors();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.RoutePrefix = ""; 
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Administration API V1");
    c.ConfigObject.DefaultModelExpandDepth = 2;
    c.ConfigObject.DefaultModelRendering = ModelRendering.Model;
    c.ConfigObject.DefaultModelsExpandDepth = 2;
    c.ConfigObject.DisplayRequestDuration = true;
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

if (app.Environment.IsDevelopment())
{
    var env = app.Services.GetRequiredService<IWebHostEnvironment>();

    var spaScript = @"npm run serve --prefix ""ClientApp""";
    var process = new Process
    {
        StartInfo = new ProcessStartInfo
        {
            FileName = "cmd",
            RedirectStandardInput = true,
            UseShellExecute = false,
            CreateNoWindow = false
        }
    };

    process.Start();
    process.StandardInput.WriteLine(spaScript);
    process.StandardInput.Close();
	
    var spaUrl = "http://localhost:8084"; // Update with the correct port of your Vue.js dev server
    Process.Start(new ProcessStartInfo { FileName = spaUrl, UseShellExecute = true });
}

app.Run();