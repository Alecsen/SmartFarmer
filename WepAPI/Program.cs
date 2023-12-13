using System.Reflection;
using System.Text;
using Application;
using Application.DAOInterface;
using Application.EventHandlers;
using Application.Logic;
using Application.LogicInterface;
using Domain.Auth;
using Domain.Models;
using EfcDataAccess;
using EfcDataAccess.DAOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;


public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

// Add services to the containerasdadss.

        builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddScoped<IUserDao, UserEfcDao>();
        builder.Services.AddScoped<IFieldDao, FieldEfcDao>();
        builder.Services.AddScoped<IUserLogic, UserLogic>();
        builder.Services.AddScoped<IFieldLogic, FieldLogic>();
        builder.Services.AddScoped<WeatherStationDataGenerator>();
        builder.Services.AddScoped<IWeatherStationDao, WeatherEfcStationDao>();
        builder.Services.AddScoped<IIrrigationMachineDao, IrrigationMachineEfcDao>();
        builder.Services.AddScoped<IIrrigationMachineLogic, IrrigationMachineLogic>();
        builder.Services.AddDbContext<SmartFarmerAppContext>();
        builder.Services.AddHostedService<WeatherStationDataGenerator>();
        builder.Services.AddScoped<FieldLogic>();
        builder.Services.AddMediatR(c=> c.RegisterServicesFromAssemblyContaining<MediatREnetryRegrisation>());
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = builder.Configuration["Jwt:Audience"],
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
            };
        });
        
        AuthorizationPolicies.AddPolicies(builder.Services);

        var app = builder.Build();
        

        app.UseCors(x => x
            .AllowAnyMethod()
            .AllowAnyHeader()
            .SetIsOriginAllowed(origin => true) // allow any origin
            .AllowCredentials());

// Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }



        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}