using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MediatR;
using OPM.Infrastructure;
using OPM.Infrastructure.Repositories.Interfaces;
using OPM.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using OPM.Queries.API.Controllers;

namespace OPM.Queries.API
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
            services.AddDbContextPool<ProfileContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ProfileDBConnection")));
            //services.AddSingleton<DbContextOptions<ProfileContext>, DbContextOptions<ProfileContext>>();   
            services.AddScoped<IProfileRepository, ProfileRepository>();           
            services.AddMediatR(typeof(Startup));
            services.AddControllers();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "HTTP Web API Demo",
                    Version = "v1",
                    Description = "Swagger in Web API"
                });
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger().UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "OPM Query API");
            });
        }
    }
}
