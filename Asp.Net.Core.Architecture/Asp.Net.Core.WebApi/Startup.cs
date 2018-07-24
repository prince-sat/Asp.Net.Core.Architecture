using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Asp.Net.Core.BusinessLayer.Interfaces;
using Asp.Net.Core.BusinessLayer.Services;
using Asp.Net.Core.DataAccessLayer.Implementation;
using Asp.Net.Core.DataAccessLayer.Implementation.Scaffolding;
using Asp.Net.Core.DataAccessLayer.Interface;
using Asp.Net.Core.Helpers.Extensions;
using Asp.Net.Core.Transverse.Logger;
using Asp.Net.Core.Transverse.Logger.Interface;
using Asp.Net.Core.WebApi.Constantes;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;

namespace Asp.Net.Core.WebApi
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }
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



        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = Configuration.GetConnectionString("PhotoGalleryConnection");

            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);

            // Add framework services.
            services.AddMvc();

            //Swagger
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "Asp.net Core WebApi",
                        Version = "v1"
                    });
                //options.IncludeXmlComments(Path.ChangeExtension(Assembly.GetEntryAssembly().Location, "xml"));
                options.DescribeAllEnumsAsStrings();
            });

            // Accès au contexte de données
            services.AddMemoryCache();

            services.AddSingleton<IConfiguration>(Configuration);

            // Accès au contexte de données
            services.AddDbContext<PhotoGalleryContext>(options => options.UseSqlServer(connectionString));
            services.AddTransient<SqlConnection>(e => new SqlConnection(connectionString));

            // Accès aux données
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Logger
            services.AddSingleton(typeof(IGenericLogger), typeof(SerilogLogger<object>));
            services.AddSingleton(typeof(IGenericLogger<>), typeof(SerilogLogger<>));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<ICacheManager, CacheManager>();



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
            app.UseMvc();
            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SupportedSubmitMethods(HttpMethods.Get.ToLower(),
                    HttpMethods.Post.ToLower(),
                    HttpMethods.Patch.ToLower(),
                    HttpMethods.Put.ToLower(),
                    HttpMethods.Delete.ToLower(),
                    HttpMethods.Head.ToLower() //Pour le ping
                    );
                //Script permettant d'injecter le token xsrf dans l'entête HTTP à chaque requête POST, PUT, DELETE et PATCH envoyée
               // options.InjectOnCompleteJavaScript($"{SwaggerUI.SwaggerUIRootPath}/swaggerui-xsrf-injector.js");
                //Configuration du endpoint de swagger ui
                options.SwaggerEndpoint($"{SwaggerUI.SwaggerRootPath}/v1/swagger.json", $"Asp.net Core WebApi V1");
            });


           // InitializeDatabase(app.ApplicationServices);
          
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
