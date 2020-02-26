using System;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sample_NetCore.Infrastructure.OutputWrapper;
using Sample_NetCore.Infrastructure.OutputWrapper.Filters;

namespace Sample_NetCore
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
            services.AddControllers(options =>
            {
                options.Filters.Add(new ValidateExceptionFilter());
                options.Filters.Add(new EvertrustExceptionFilter());
                options.Filters.Add(new EvertrustActionResultFilter());

                options.OutputFormatters.RemoveType<XmlDataContractSerializerOutputFormatter>();
            })
                    .AddFluentValidation
                    (
                        config => config.RegisterValidatorsFromAssemblyContaining<Startup>()
                    );
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

            app.Use(async (context, next) =>
            {
                EvertrustAsyncContext.CorrelationId = Guid.NewGuid();
                EvertrustAsyncContext.Domain = "Sample";
                EvertrustAsyncContext.Version = "1.0.0";

                await next.Invoke();
            });

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}