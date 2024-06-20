/*
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
*/

//using Administration.Factories;
//using Administration.Factories.Interfaces;
using Administration.Helpers;
using Administration.Interfaces;
using Administration.Services;
using LoggingService;
using Microsoft.OpenApi.Models;
//using Models.DTO.Interfaces;
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

// Add services to the container.
//builder.Services.AddRazorPages();

/*
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Core Service", Version = "v1" });
    c.DescribeAllParametersInCamelCase();
}
);
*/

builder.Services.AddControllersWithViews();
//builder.Services.AddDbContext<DbNskContext>();
builder.Services.AddScoped<IUserInterface, UserServices>();
builder.Services.AddScoped<ODDANP>();
//builder.Services.AddScoped<CategoriesService>();
//builder.Services.AddScoped<IDto>();
builder.Services.AddScoped<CategoryDTO>();
//builder.Services.AddScoped<IDBService<IDto>>();
//builder.Services.AddScoped(typeof(IDBService<IDto>), typeof(CategoriesService));
builder.Services.AddScoped<ICategoriesService, CategoriesService>();
builder.Services.AddScoped<IPagesService, PagesService>();
builder.Services.AddScoped<IContentService, ContentService>();
//builder.Services.AddScoped<IDBService<CategoryDTO>, CategoriesService>();
builder.Services.AddScoped<IServiceFactory, ServiceFactory>();


builder.Services.AddScoped<ISQLReadCommands, CPgReadCommands>();
builder.Services.AddScoped<ISQLUpdateCommands, CPgUpdateCommands>();
builder.Services.AddScoped<ISQLCreateCommands, CPgCreateCommands>();
builder.Services.AddScoped<ISQLDeleteCommands, CPgDeleteCommands>();
builder.Services.AddScoped<ILogService, LogService>();
builder.Services.AddScoped<ILanguagesService, LanguagesService>();
builder.Services.AddScoped<IPagesService, PagesService>();

//DBService<CategoryDTO>, ICategoriesService
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
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true); // Пример добавления дополнительного файла конфигурации


/*--------------------------
builder.Services.AddSpaStaticFiles(configuration =>
{
    configuration.RootPath = "ClientApp/dist";
});
*/
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Core Service", Version = "v1" });
    c.DescribeAllParametersInCamelCase();
}
);
builder.Logging.AddConsole();

//builder.Services.AddSwaggerGen();

builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection("ConnectionStrings"));

var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
builder.Logging.ClearProviders();
builder.Host.UseNLog();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
    //c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
    c.RoutePrefix = ""; // Оставляем пустой префикс для Swagger UI
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Administration API V1");
    //c.IndexStream = () => typeof(Program).Assembly.GetManifestResourceStream("WebApplicationMVC.index.html"); // Замените "YourProjectName" на имя вашего проекта
    //c.HeadContent = @"<meta http-equiv=""refresh"" content=""0;URL=/swagger"" />"; // Автоматическое перенаправление на Swagger UI
    c.ConfigObject.DefaultModelExpandDepth = 2;
    c.ConfigObject.DefaultModelRendering = ModelRendering.Model;
    c.ConfigObject.DefaultModelsExpandDepth = 2;
    c.ConfigObject.DisplayRequestDuration = true;
});


//app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

// Serve static files from the ClientApp/dist directory
//app.UseSpaStaticFiles();----------------------

// Start the Vue.js development server in Development environment
if (app.Environment.IsDevelopment())
{
    var env = app.Services.GetRequiredService<IWebHostEnvironment>();

    /*
    var process = new Process
    {
        StartInfo = new ProcessStartInfo
        {
            FileName = "npm",
            Arguments = "run serve --prefix \"ClientApp\"",
            WorkingDirectory = Path.Combine(env.ContentRootPath, "ClientApp"),
            UseShellExecute = false,
            CreateNoWindow = true
        }
    };
    */

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

    //process.Start();

    // Open the browser with the SPA URL
    var spaUrl = "http://localhost:8084"; // Update with the correct port of your Vue.js dev server
    Process.Start(new ProcessStartInfo { FileName = spaUrl, UseShellExecute = true });
    /* ----------------------------------
    app.UseSpa(spa =>
    {
        spa.Options.SourcePath = "ClientApp";
    });
    */
}

app.Run();