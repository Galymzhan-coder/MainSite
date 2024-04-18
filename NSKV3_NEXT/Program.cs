/*var builder = WebApplication.CreateBuilder(args);

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
/*
using Microsoft.AspNetCore.Hosting.Server;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSpaStaticFiles(configuration =>
{
    //configuration.RootPath = "ClientApp/dist";
    configuration.RootPath = "ClientApp/.next/server/app";
    
});



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

app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

// Serve static files from the ClientApp/dist directory
app.UseSpaStaticFiles();

app.UseRouting();

// Start the Vue.js development server in Development environment
if (app.Environment.IsDevelopment())
{
    var env = app.Services.GetRequiredService<IWebHostEnvironment>();

    //
    //var process = new Process
    //{
    //StartInfo = new ProcessStartInfo
    //{
    //FileName = "npm",
    //Arguments = "run serve --prefix \"ClientApp\"",
    //WorkingDirectory = Path.Combine(env.ContentRootPath, "ClientApp"),
    //UseShellExecute = false,
    //CreateNoWindow = true
    //}
    //};
    //

    //var spaScript = @"pnpm next dev --prefix ""ClientApp""";
    var spaScript = @"pnpm next dev";
    var process = new Process
    {
        StartInfo = new ProcessStartInfo
        {
            FileName = "cmd",
            RedirectStandardInput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        }
    };

    process.Start();
    process.StandardInput.WriteLine(spaScript);
    process.StandardInput.Close();

    //process.Start();

    // Open the browser with the SPA URL
    var spaUrl = "https://localhost:3000"; // Update with the correct port of your Vue.js dev server
    Process.Start(new ProcessStartInfo { FileName = spaUrl, UseShellExecute = true });

    app.UseSpa(spa =>
    {
        spa.Options.SourcePath = "ClientApp";
    });

}

app.Run();
*/
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSKV3_NEXT.Services;
using Yarp.ReverseProxy.Forwarder;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);



// ��������� YARP
builder.Services.AddHttpForwarder();

// ������������ ������ Next.js Development Server
builder.Services.AddHostedService<NextJsDevelopmentServer>();

builder.Services.AddControllers(); // �������� ��� ������, ���� � ��� ���
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    var forwarder = app.Services.GetRequiredService<IHttpForwarder>();
    var httpClient = new HttpMessageInvoker(new SocketsHttpHandler());
    var requestOptions = new ForwarderRequestConfig { ActivityTimeout = TimeSpan.FromSeconds(100) };
    var transformer = new CustomTransformer(); // ���������� ��� ��������� �����������
    
    app.UseSwagger();
    
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty; // Swagger UI �������� �� ����� ���-�����
    });
    
    app.UseRouting();

    app.UseEndpoints(endpoints =>
    {
        endpoints.Map("/{**catch-all}", async context =>
        {
            
            if (context.Request.Path.StartsWithSegments("/swagger"))
            {
                // ������ � Swagger, ��������� ASP.NET Core ���������� ��� ������� �������
                await context.Response.WriteAsync("Swagger ������� �������������� ASP.NET Core");
                //return;
            }
            
            var error = await forwarder.SendAsync(context, "http://localhost:3000", httpClient, requestOptions, transformer);
            if (error != ForwarderError.None)
            {
                var errorFeature = context.Features.Get<IForwarderErrorFeature>();
                context.Response.StatusCode = 502; // Bad Gateway
                await context.Response.WriteAsync($"������ ������ �������: {errorFeature.Error}");
            }
        });
    });
    
    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

    app.MapControllers();
    
}


app.Run();



