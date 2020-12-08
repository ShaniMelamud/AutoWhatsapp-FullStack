using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Quartz.Impl;

namespace Tomedia
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
            services.AddCors(setup => setup.AddPolicy("EntireWorld", policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
            services.AddCors(setup => setup.AddPolicy("localHostDevelopment", policy => policy.WithOrigins("http://localhost:4200", "http://localhost:3000", "http://localhost:5000").AllowAnyMethod().AllowAnyHeader()));
            services.AddDbContext<AutoWhatsappContext>(Options => Options.UseSqlServer(Configuration.GetConnectionString("AutoWhatsapp")));
            services.AddTransient<ContactsLogic>();
            services.AddTransient<BusinessesLogic>();
            services.AddTransient<MailingListsLogic>();
            services.AddTransient<MessagesLogic>();

            JwtHelper jwtHelper = new JwtHelper(Configuration.GetValue<string>("JWT:Key"));
            services.AddSingleton(jwtHelper);

            services.AddControllers();
            //services.AddSpaStaticFiles(config => config.RootPath = "wwwroot");

            var scheduler =
            StdSchedulerFactory.GetDefaultScheduler().GetAwaiter().GetResult();
            services.AddSingleton(scheduler);
            services.AddHostedService<CustomQuartzHostedService>();


            services.AddAuthentication(options => jwtHelper.SetAuthenticationOptions(options)).AddJwtBearer(options => jwtHelper.SetBearerOptions(options));
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
            app.UseCors();
            app.UseAuthentication(); // אימות
            app.UseAuthorization();

            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
