using System.Text;
using Api.Domain.Services;
using Api.Infrastructure.Database;
using Api.Utils.Interfaces;
using Api.Utils.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;

namespace Program
{
    public class Program 
    {
        public static void Main (string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddTransient<TokenService>();
            builder.Services.AddScoped<AdministratorsInterface, AdministratorsServices>();
            builder.Services.AddScoped<DoctorsInterface, DoctorsServices>();
            builder.Services.AddScoped<PatientsInterface, PatientsServices>();
            builder.Services.AddScoped<AppointmentsInterface, AppointmentsServices>();
            builder.Services.AddScoped<DoctorsDaysAvailableInterface, DoctorsDaysAvailableServices>();

            builder.Services.AddCors(options => 
            {
                options.AddPolicy("Cors", options => 
                {
                    options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                });
            });

            var key = Configuration.KEY;
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            builder.Services.AddDbContext<DatabaseContext>(options => 
            {
                options.UseMySql(builder.Configuration.GetConnectionString("Mysql"), 
                ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("Mysql"))
                );
            });

            var app = builder.Build();
            app.UseCors("Cors");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}


