using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Npgsql;
using PalpinApi.Extensions;
using System;
using System.IO;
using System.Reflection;
using WorkCloudTest.Contexts;
using WorkCloudTest.Extensions;
using WorkCloudTest.IRepositories;
using WorkCloudTest.Repositories;

namespace WorkCloudTest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string postgreConnectionString = Configuration.GetConnectionString("Pgsql");
            services.AddEntityFrameworkNpgsql().AddDbContext<PgsqlContext>(options => options.UseNpgsql(postgreConnectionString));

            services.AddAutoMapper(typeof(Startup).GetTypeInfo().Assembly);

            services.AddSerilogServices();

            services.AddSingleton<NpgsqlConnection>(new NpgsqlConnection(postgreConnectionString));

            services.AddControllers();

            services.AddScoped<IRepository, Repository<PgsqlContext>>();
            
            services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "WorldCloudTest",
                        Description = "A simple example ASP.NET Core Web API To World Cloud",
                        Contact = new OpenApiContact
                        {
                            Name = "Jheysson Diaz",
                            Email = "jjdg21090@gmail.com",
                            Url = new Uri("https://www.linkedin.com/in/jheysson-diaz-9a28ba122/"),
                        }
                    });

                    // Set the comments path for the Swagger JSON and UI.
                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    c.IncludeXmlComments(xmlPath);
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSeed();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WorldCloudTest V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
