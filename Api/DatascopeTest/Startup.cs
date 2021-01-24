using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using AutoMapper;
using DatascopeTest.Data;
using DatascopeTest.Data.Repositories;
using DatascopeTest.Extensions;
using DatascopeTest.Mappings;
using DatascopeTest.Models;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DatascopeTest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile).Assembly);
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(Configuration["DbConnectionString"]));

            services.AddScoped<IGenericRepository<Game>, GenericRepository<Game>>();
            services.AddScoped<IGamesRepository, GamesRepository>();

            services.Scan(s => s.FromAssemblyOf<Startup>()
                .AddClasses(c => c.AssignableTo(typeof(IValidator<>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());

            services.AddCors(opt =>
            {
                opt.AddPolicy("ClientApp", builder =>
                {
                    builder.WithOrigins(Configuration["ClientUrl"])
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithExposedHeaders("X-Pagination");
                });
            });

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AppDbContext context)
        {
            app.UseCors("ClientApp");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.ConfigureExceptionHandler();
            }

            app.UseValidationExceptionHandler();
            app.UseNotFoundExceptionHandler();

            context.Database.Migrate();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
