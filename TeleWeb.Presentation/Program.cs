using Autofac;
using Autofac.Extensions.DependencyInjection;
using TeleWeb.DI;
using TeleWeb.Presentation.AppStartExtensions;
using Microsoft.EntityFrameworkCore.Design;
using TeleWeb.Domain.Models;
using TeleWeb.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new ModuleDI(builder.Configuration)));

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCustomAutoMapper();

builder.Services.AddDbContext<IdentityContext>(option => option.UseSqlServer(
    builder.Configuration.GetConnectionString("IdentityConnection")
    ));

builder.Services.AddIdentity<UserIdentity, IdentityRole>()
    .AddEntityFrameworkStores<IdentityContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();


app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = context =>
    {
        // Get the requested file path
        var filePath = context.File.PhysicalPath;

        // Check if the file path contains the "secret" folder
        if (filePath != null && filePath.Contains("/secret/"))
        {
            // If so, return a 404 status code
            context.Context.Response.StatusCode = 404;
            context.Context.Response.ContentLength = 0;
            context.Context.Response.Body = Stream.Null;
        }
    }
});

app.UseAuthentication();    
app.UseRouting();

app.UseAuthorization();


app.MapControllers();

app.Run();
