using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSKV3_NEXT.Services;
using Yarp.ReverseProxy.Forwarder;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddHttpForwarder();

builder.Services.AddHostedService<NextJsDevelopmentServer>();

builder.Services.AddControllers(); 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    var forwarder = app.Services.GetRequiredService<IHttpForwarder>();
    var httpClient = new HttpMessageInvoker(new SocketsHttpHandler());
    var requestOptions = new ForwarderRequestConfig { ActivityTimeout = TimeSpan.FromSeconds(100) };
    var transformer = new CustomTransformer();
    
    app.UseSwagger();
    
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty; 
    });
    
    app.UseRouting();

    app.UseEndpoints(endpoints =>
    {
        endpoints.Map("/{**catch-all}", async context =>
        {
            
            if (context.Request.Path.StartsWithSegments("/swagger"))
            {
                await context.Response.WriteAsync("Swagger запросы обрабатываются ASP.NET Core");
            }
            
            var error = await forwarder.SendAsync(context, "http://localhost:3000", httpClient, requestOptions, transformer);
            if (error != ForwarderError.None)
            {
                var errorFeature = context.Features.Get<IForwarderErrorFeature>();
                context.Response.StatusCode = 502; 
                await context.Response.WriteAsync($"Ошибка прокси сервера: {errorFeature.Error}");
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



