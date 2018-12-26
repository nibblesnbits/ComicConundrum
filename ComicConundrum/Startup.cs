using ComicConundrum.Configuration;
using ComicConundrum.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Net.Http;

namespace ComicConundrum {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.Configure<MarvelApiOptions>(Configuration.GetSection(nameof(MarvelApiOptions)));
            services.Configure<CosmosDbOptions>(Configuration.GetSection(nameof(CosmosDbOptions)));

            services.AddSpaStaticFiles(configuration => {
                configuration.RootPath = "ClientApp/build";
            });

            services.AddHttpClient("marvelApi", (sp, c) => {
                var options = sp.GetRequiredService<IOptions<MarvelApiOptions>>();
                c.BaseAddress = options.Value.BaseUri;
            });

            services.AddTransient(sp => {
                var options = sp.GetRequiredService<IOptions<MarvelApiOptions>>();
                return new MarvelComicsApi(
                    sp.GetRequiredService<IHttpClientFactory>(),
                    options.Value.PublicKey,
                    options.Value.PrivateKey);
            });

            services.AddTransient(s => {
                var config = s.GetService<IOptions<CosmosDbOptions>>().Value;
                return new DocumentClient(config.Uri, config.Key);
            });

            services.AddTransient<ComicDbService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            } else {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa => {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment()) {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
