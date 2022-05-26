using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using SikumkumServerBL.Models;
using Microsoft.EntityFrameworkCore;


namespace SikumkumServer
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

            //Add Controllers and set the Json Serializer to handle loop referencing
            services.AddControllers().AddJsonOptions(o => o.JsonSerializerOptions
                        .ReferenceHandler = ReferenceHandler.Preserve);
            //The following two commands set the Session state to work!
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(180);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            //The following set the connection string to the DB and DB context!
            #region Add DB Context Support
            string connectionString = this.Configuration.GetConnectionString("DBSikumkum");

            services.AddDbContext<DBSikumkumContext>(options => options
                                                                .UseSqlServer(connectionString));
            //.UseLazyLoadingProxies());

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles(); //Added to have the wwwroot folder and server to accept calls to static files
            app.UseRouting();
            app.UseSession(); //Added to tell the server to use sessions!

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}