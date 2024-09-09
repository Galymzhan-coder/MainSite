using Administration.Configs.Image;
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
builder.Services.AddScoped<ILanguagesService, LanguagesService>();
builder.Services.AddScoped<IPagesService, PagesService>();
builder.Services.AddScoped<ITopManagementService, TopManagementService>();
builder.Services.AddScoped<IBlogCategoriesService, BlogCategoriesService>();
builder.Services.AddScoped<IFaqCategoriesService, FaqCategoriesService>();


builder.Services.AddScoped<IBlogsService, BlogsService>();


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
    .SetBasePath(builder.Environment.ContentRootPath) // Этот вызов на самом деле не требуется в новых версиях
    .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true); // Пример добавления дополнительного файла конфигурации


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Core Service", Version = "v1" });
    c.DescribeAllParametersInCamelCase();
}
);
builder.Logging.AddConsole();


builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection("ConnectionStrings"));

// SaveImage Config
builder.Services.Configure<ImageConfig>(builder.Configuration.GetSection("ImageConfig"));

var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
builder.Logging.ClearProviders();
builder.Host.UseNLog();


var app = builder.Build();


// Configure the HTTP request pipeline.
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
    c.RoutePrefix = ""; // Оставляем пустой префикс для Swagger UI
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Administration API V1");
    c.ConfigObject.DefaultModelExpandDepth = 2;
    c.ConfigObject.DefaultModelRendering = ModelRendering.Model;
    c.ConfigObject.DefaultModelsExpandDepth = 2;
    c.ConfigObject.DisplayRequestDuration = true;
});



app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");


// Start the Vue.js development server in Development environment
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


    // Open the browser with the SPA URL
    var spaUrl = "http://localhost:8084"; // Update with the correct port of your Vue.js dev server
    Process.Start(new ProcessStartInfo { FileName = spaUrl, UseShellExecute = true });
}

app.Run();