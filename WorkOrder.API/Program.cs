#region BuilderServices

using WorkOrder.API.Attributes;
using WorkOrder.API.Handlers;
using WorkOrder.Domain.Interfaces.Repositories;
using WorkOrder.Domain.Interfaces.Services;
using WorkOrder.Domain.Services;
using WorkOrder.Domain.Settings;
using WorkOrder.Infrastructure.Contexts;
using WorkOrder.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddHttpContextAccessor();

#region Filters

builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(ExceptionAttribute));
    options.Filters.Add(typeof(ValidationAttribute));
});

#endregion

#region HttpContext

builder.Services.AddHttpContextAccessor();

#endregion

#region Swagger

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

#endregion

#region AppSettings

var appSetting = builder.Configuration.GetSection(nameof(AppSetting)).Get<AppSetting>();
builder.Services.AddSingleton(appSetting);

#endregion

#region Mapper

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

#endregion

#region Services    

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<IFinalizationService, FinalizationService>();
builder.Services.AddScoped<IWorkOrderService, WorkOrderService>();
builder.Services.AddScoped<IWorkService, WorkService>();
builder.Services.AddScoped<IProfileService, ProfileService>();

#endregion

#region Repositories    

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IFinalizationRepository, FinalizationRepository>();
builder.Services.AddScoped<IWorkOrderRepository, WorkOrderRepository>();
builder.Services.AddScoped<IWorkRepository, WorkRepository>();
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();

#endregion

#region Dbcontext

builder.Services.AddDbContext<WorkOrderContext>(options =>
{
    options.UseSqlServer(appSetting.SqlServerConnection);
    options.UseLazyLoadingProxies();
});

#endregion

#region Jwt

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSetting.JwtSecurityKey)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

#endregion

#region Authorization

builder.Services.AddAuthorization(options =>
{
    options.InvokeHandlersAfterFailure = true;
}).AddSingleton<IAuthorizationMiddlewareResultHandler, AuthorizationHandler>();

#endregion

#endregion

#region AppConfiguration

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

#endregion
