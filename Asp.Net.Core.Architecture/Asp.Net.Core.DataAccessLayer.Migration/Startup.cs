using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asp.Net.Core.DataAccessLayer.Interface;
using Asp.Net.Core.DataAccessLayer.Migration.Scaffolding;
using Asp.Net.Core.Helpers.Extensions;
using Asp.Net.Core.Transverse.Logger;
using Asp.Net.Core.Transverse.Logger.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Asp.Net.Core.DataAccessLayer.Migration
{
    public class Startup
    {
        private IConfigurationRoot _configuration { get; }
        private static string _applicationPath = string.Empty;
        private static string _contentRootPath = string.Empty;
        public Startup(IHostingEnvironment env)
        {
            _applicationPath = env.WebRootPath;
            _contentRootPath = env.ContentRootPath;

            var builder = new ConfigurationBuilder()
                .SetBasePath(_contentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            // Add framework services.
            services.AddApplicationInsightsTelemetry(_configuration);

            string sqlConnectionString = Configuration["ConnectionStrings:DefaultConnection"];
            bool useInMemoryProvider = bool.Parse(Configuration["Data:PhotoGalleryConnection:InMemoryProvider"]);

            services.AddDbContext<PhotoGalleryContext>(options =>
            {
                switch (useInMemoryProvider)
                {
                    case true:
                        options.UseInMemoryDatabase();
                        break;
                    default:
                        options.UseSqlServer(sqlConnectionString);
                        break;
                }
            });

            // Accès aux données
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Logger
            services.AddSingleton(typeof(IGenericLogger), typeof(SerilogLogger<object>));
            services.AddSingleton(typeof(IGenericLogger<>), typeof(SerilogLogger<>));

            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            InitializeDatabase(app.ApplicationServices);
            // DbInitializer.Initialize(app.ApplicationServices, _applicationPath);
        }

        #region Private methods

        /// <summary>
        /// Fonction d'initialisation de la base de données
        /// </summary>
        private void InitializeDatabase(IServiceProvider serviceProvider)
        {
            IGenericLogger logger = serviceProvider.GetRequiredService<IGenericLogger>();
            try
            {
                IHostingEnvironment hostingEnvironment = serviceProvider.GetRequiredService<IHostingEnvironment>();

                //On initialize la base de données uniquement lorsque l'on est en environnement 
                //de développement ou d'intégration 
                if (hostingEnvironment.IsDevelopment()
                    || hostingEnvironment.IsIntegration())
                {
                    IUnitOfWork unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();

                    ScaffoldingDbInitializer dbInitializer = new ScaffoldingDbInitializer();
                    //Création du contexte du dbInitializer
                    DbInitializerContext dbInitializerContext = new DbInitializerContext(unitOfWork, logger);
                    dbInitializer.Initialize(dbInitializerContext, _applicationPath);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "An error occurred while seeding the database.");
                throw;
            }
        }


        #endregion

    }
}
