using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using WorkPlanning.API.Data.Context;
using WorkPlanning.API.Middlewares;
using WorkPlanning.Domain.Interfaces;
using WorkPlanning.Data.Context;
using WorkPlanning.Data.Repositories;
using WorkPlanning.Domain.Services;

namespace WorkPlanning.API
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
            services.AddScoped<IWorkerService, WorkerService>();
            services.AddScoped<IShiftService, ShiftService>();
            services.AddScoped<IWorkerRepository, WorkerRepository>();
            services.Configure<WorkPlanningDBSettings>(Configuration.GetSection(nameof(WorkPlanningDBSettings)));
            services.AddSingleton<IWorkPlanningDBSettings>(x => x.GetRequiredService<IOptions<WorkPlanningDBSettings>>().Value);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WorkPlanning.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WorkPlanning.API v1"));
            }

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

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
