using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation;
using TeleWeb.DI;
using TeleWeb.Presentation.AppStartExtensions;
using TeleWeb.Domain.Models;
using TeleWeb.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TeleWeb.Validation;


var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new ModuleDI(builder.Configuration)));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCustomAutoMapper();
builder.Services.AddDbContext<IdentityContext>(option => option.UseSqlServer(
    builder.Configuration.GetConnectionString("IdentityConnection")
    ));
builder.Services.AddIdentity<UserIdentity, IdentityRole>(options =>
    {
        
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 6;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
    } ).AddEntityFrameworkStores<IdentityContext>()
    .AddTokenProvider<DataProtectorTokenProvider<UserIdentity>>(TokenOptions.DefaultProvider);//AddDefaultTokenProviders();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromDays(30);
});
/*builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["AuthSettings:Audience"],
        ValidIssuer = builder.Configuration["AuthSettings:Issuer"],
        RequireExpirationTime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AuthSettings:Key"])),
        ValidateIssuerSigningKey = true
    };
});*/

/*builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(options =>
{
    options = new CookieAuthenticationOptions()
    {
        LoginPath = "/Account/Login",
        AccessDeniedPath = "/Account/AccessDenied",
        ReturnUrlParameter = "ReturnUrl",
        ExpireTimeSpan = TimeSpan.FromMinutes(5),
    };
});*/
builder.Services.AddHttpContextAccessor();
builder.Services.AddValidatorsFromAssemblyContaining<IAssemblyMarker>();
builder.Services.AddCors(co=>
{
    co.AddPolicy("Policy", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("Policy");
//app.UseAuthentication();
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
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();
