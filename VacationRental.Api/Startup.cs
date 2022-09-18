using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using VacationRental.Api.Models;
using VacationRental.Infrastructure.Entities;
using VacationRental.Infrastructure.Repositories.Implementations;
using VacationRental.Infrastructure.Repositories.Interfaces;
using VacationRental.Logic.Implementations;
using VacationRental.Logic.Interfaces;

namespace VacationRental.Api
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
            services.AddMvc();

            services.AddSwaggerGen(opts => opts.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Vacation rental information", Version = "v1" }));
            services.AddDatabaseServices();
            services.AddLogicServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseSwagger();
            app.UseSwaggerUI(opts => opts.SwaggerEndpoint("/swagger/v1/swagger.json", "VacationRental v1"));

            app.UseRouting();

            app.UseEndpoints(conf => conf.MapControllers());
        }
    }


    public static class StartupConfig
    {
        public static IServiceCollection AddDatabaseServices(this IServiceCollection services)
        {
            services.AddSingleton<IDictionary<int, RentalEntity>>(new Dictionary<int, RentalEntity>());
            services.AddSingleton<IDictionary<int, BookingEntity>>(new Dictionary<int, BookingEntity>());
            services.AddSingleton<IRentalDatabaseRepository, RentalDatabaseRepository>();
            services.AddSingleton<IBookingDatabaseRepository, BookingDatabaseRepository>();
            return services;
        }
        public static IServiceCollection AddLogicServices(this IServiceCollection services)
        {
            services.AddSingleton<IRentalLogic, RentalLogic>();
            services.AddSingleton<IBookingLogic, BookingLogic>();
            services.AddSingleton<ICalendarLogic, CalendarLogic>();
            return services;
        }
    }
}
