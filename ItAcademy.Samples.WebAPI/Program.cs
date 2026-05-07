
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SampleSolution.Data.Db;
using SampleSolution.ServiceDefaults;
using System.Reflection;
using System.Text.Json.Serialization;
using FluentValidation;
using ItAcademy.Samples.WebAPI.Infrastructure;
using ItAcademy.Samples.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ItAcademy.Samples.WebAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.AddServiceDefaults();

        builder.Services.AddScoped<FluentValidatorActionFilter>();

        builder.Services.AddDbContext<SampleDbContext>(opt =>
            opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

        builder.RegisterArticleServices();
        builder.RegisterSourceServices();
        builder.RegisterUserServices();
        builder.ConfigureLogger();
        builder.RegisterCqs();

        builder.Services.AddValidatorsFromAssemblyContaining<UpdateArticleModel>();

        // Add services to the container.

        builder.Services.AddControllers(options =>
        {
            options.Filters.Add<FluentValidatorActionFilter>();
        })
        .AddJsonOptions(opt =>
        {
            opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        builder.Services.Configure<ApiBehaviorOptions>(ApiBehaviorOptionsConfigurations.Configure);
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddProblemDetails();

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(opt =>
        {
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            opt.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });

        var app = builder.Build();

        app.MapDefaultEndpoints();

        app.UseSwagger(opt =>
        {
            
        });
        app.UseSwaggerUI(opt =>
        {
            opt.DocumentTitle = "Good Article Aggregator API";
            opt.HeadContent = "Good Article Aggregator API";
            opt.SwaggerEndpoint("/swagger/v1/swagger.json", "Good Article Aggregator API V1");
        });

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseExceptionHandler();
        
        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}