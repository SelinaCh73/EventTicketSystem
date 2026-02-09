
using CarService.BL;
using CarService.DL;
using CarService.Host.Healthchecks;
using CarService.Host.Validators;
using FluentValidation;

//using FluentValidation;
using Mapster;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace CarService.Host
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .Enrich.FromLogContext()
            .WriteTo.Console(theme: AnsiConsoleTheme.Code)
            .CreateLogger();

            // Add services to the container.
            builder.Services
                .AddDataLayer(builder.Configuration)
                .AddBusinessLayer();

            builder.Services.AddMapster();

            builder.Services
                .AddValidatorsFromAssemblyContaining<AddCarRequestValidator>();

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Car Service 2", Version = "v1" });
            });

            builder.Host.UseSerilog();

            builder.Services
                .AddHealthChecks()
                .AddCheck<MongoDbHealthCheck>("mongoDbCheck");

            var app = builder.Build();

            app.MapHealthChecks("/healthz");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseSwaggerUI(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Car Service MyVersion", Version = "v1" });
            });

            app.UseSwagger();

            //app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
