using Api.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
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
            builder.Services.AddDbContext<DatabaseContext>(options => 
            {
                options.UseMySql(builder.Configuration.GetConnectionString("Mysql"), 
                ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("Mysql"))
                );
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}


