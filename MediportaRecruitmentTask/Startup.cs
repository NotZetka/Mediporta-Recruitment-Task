﻿using FluentValidation;
using MediatR;
using Mediporta_Recruitment_Task.Clients.TagsClient;
using Mediporta_Recruitment_Task.Database;
using Mediporta_Recruitment_Task.Extentions;
using Mediporta_Recruitment_Task.Handlers.Tags.CountPercentageShare;
using Mediporta_Recruitment_Task.Handlers.Tags.ListTags;
using Mediporta_Recruitment_Task.Handlers.Tags.ReloadTags;
using Mediporta_Recruitment_Task.Middleware;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Mediporta_Recruitment_Task
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(option =>
            {
                option.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            services.AddHttpClient();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddDbContext<TagsContext>(options =>
            {
                var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
                var dbName = Environment.GetEnvironmentVariable("DB_NAME");
                var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");
                var connectionstring = $"Data Source={dbHost};Initial Catalog={dbName};User ID=sa;Password={dbPassword};TrustServerCertificate=true";
                options.UseSqlServer(connectionstring);
            });
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });
                var filePath = Path.Combine(AppContext.BaseDirectory, "Mediporta Recruitment Task.xml");
                c.IncludeXmlComments(filePath);
            });
            services.AddTransient<ITagsClient, TagsClient>();
            services.AddSingleton<ILogger<ErrorHandlingMiddleware>, Logger<ErrorHandlingMiddleware>>();
            services.AddTransient<IValidator<ListTagsQuery>, ListTagsQueryValidator>();
            services.AddTransient<IValidator<ReloadTagsQuery>, ReloadTagsQueryValidator>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API v1"));
            }

            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.ApplyMigrations();
            app.InitDatabase();
        }
    }
}