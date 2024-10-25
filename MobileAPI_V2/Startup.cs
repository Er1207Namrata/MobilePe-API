using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MobileAPI_V2.DataLayer;
using MobileAPI_V2.Filter;
using MobileAPI_V2.Model;
using MobileAPI_V2.Model.Fineque;
using MobileAPI_V2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MobileAPI_V2
{
    public class Startup
    {
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _env;
        private ConnectionString connectionString;

        public MallConnectionString MobilePeMallConnection { get; private set; }

        public Startup(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            Configuration = configuration;
            _env = env;


        }
        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            // If using IIS:
            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            services.AddScoped<ApiLog>();
            services.AddScoped<CheckUser>();


            services.AddHttpClient();
            services.AddScoped<EmailTemplateService>();
            //services.AddScoped<ApiManager>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddControllers();
            services.AddSwaggerGen();
            services.AddHttpContextAccessor();
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
            //services.AddCors(options =>
            //{
            //    options.AddPolicy(name: MyAllowSpecificOrigins,
            //                      builder =>
            //                      {
            //                          builder.WithOrigins("https://apiv2.mobilepe.co.in",
            //                                              "https://api.mobilepe.co.in", 
            //                                              "https://topay.live", 
            //                                              "https://web.mobilepe.co.in", 
            //                                              "https://mall.mobilepe.co.in",
            //                                              "https://uatapiv2.mobilepe.co.in"
            //                                              );
            //                      });
            //});
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MobileAPI_V2", Version = "v1" });
            });


            if (_env.IsDevelopment())
            {
                connectionString = new ConnectionString(Configuration.GetConnectionString("DefaultConnectionDev"));
                MobilePeMallConnection = new MallConnectionString(Configuration.GetConnectionString("MobilePeMallConnectionDev"));
            }
            else
            {
                connectionString = new ConnectionString(Configuration.GetConnectionString("DefaultConnection"));
                MobilePeMallConnection = new MallConnectionString(Configuration.GetConnectionString("MobilePeMallConnection"));
            }


            services.AddSingleton(connectionString);
            services.AddSingleton(MobilePeMallConnection);
            services.AddTransient<IDataRepositoryEcomm, DataRepositoryEcomm>();
            services.AddTransient<IDataRepository, DataRepository>();
            services.AddScoped<ApiManager>();
            services.AddScoped<FinqueService>();
            services.AddTransient<DBHelperService>();
            services.AddScoped<AffiliateService>();
            services.AddScoped<Tenant>();
            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperConfiguration());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddMvc();
            services.AddApiVersioning(x =>
            {
                x.DefaultApiVersion = new ApiVersion(1, 1);
                x.AssumeDefaultVersionWhenUnspecified = true;
                x.ReportApiVersions = true;
            });
            //services.AddControllers().AddNewtonsoftJson();


            //   services.AddHostedService<RechargeSchedular>();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MobileAPI_V2 v1"));


            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("X-Frame-Options", "sameorigin");
                context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");
                context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
                context.Response.Headers.Remove("X-Powered-By");
                context.Response.Headers.Remove("Server");

                await next();
            });



            //if (env.IsProduction())
            //{
            //    app.UseDeveloperExceptionPage();
            //    app.UseSwagger();
            //    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MobileAPI_V2 v1"));
            //}
            app.UseHttpsRedirection();


            app.UseRouting();
            app.UseCors("MyPolicy");
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseStaticFiles();
        }

    }
}
