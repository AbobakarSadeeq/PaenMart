using Business_Core.Entities.Identity;
using Business_Core.IServices;
using Business_Core.IUnitOfWork;
using Data_Access.DataContext_Class;
using Data_Access.Extensions;
using Data_Access.UnitOfWork;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PaenMart.SignalR;
using Presentation.AppSettingClasses;
using Presentation.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// --------Configure services method of startup class--------
 


var builder = WebApplication.CreateBuilder(args);

// Injecting AppSetting
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));
builder.Services.Configure<ApplicationSettings>(builder.Configuration.GetSection("ApplicationSettings"));

builder.Services.AddIdentity<CustomIdentity, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<DataContext>()
    .AddTokenProvider<DataProtectorTokenProvider<CustomIdentity>>(TokenOptions.DefaultProvider);

 




// database connection
builder.Services.AddDbContextPool<DataContext>(options =>
 options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// JWT Token schema and it is used for to check the given token is valid or not. without it authorize attribute cannot work.
var key = Encoding.UTF8.GetBytes(builder.Configuration["ApplicationSettings:JWT_Secret"].ToString());
builder.Services.AddAuthentication(a =>
{
    a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    a.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x => {
    x.RequireHttpsMetadata = false;
    x.SaveToken = false;
    x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };
});

 

// Validation for Password
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 5;
    options.Password.RequiredUniqueChars = 3;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.SignIn.RequireConfirmedEmail = false;
});

builder.Services.AddHangfire(configuration =>
{
    configuration.UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection")); // hangfire need a storage to store its own data so give your sql database or anything you want to give.
});


// AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMappers));

// Add services to the container.
builder.Services.AddScoped<IUnitofWork, UnitofWork>();
builder.Services.ConfigureServiceLayer();

// Nested Include Error.
builder.Services.AddControllersWithViews()
 .AddNewtonsoftJson(options =>
 options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services.AddSignalR();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// connecting client
builder.Services.AddCors(opt => {
    opt.AddDefaultPolicy(builder => {
        builder.AllowAnyOrigin();
        builder.AllowAnyMethod();
        builder.AllowAnyHeader();
    });
});


// --------Configure Method of startup class--------
var app = builder.Build();
app.UseHangfireServer();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
// middleware of connection with client-side
 
app.UseCors(a=> {
    a.WithOrigins(builder.Configuration.GetValue<string>("Client_URL"))
        .AllowAnyHeader()
        .AllowAnyMethod();
});

app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();
app.MapHub<NotificationHub>("/notificationhub");
app.Run();
