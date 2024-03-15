using FireplaceApi.Api.Attributes;
using FireplaceApi.Api.Controllers;
using FireplaceApi.Api.Extensions;
using FireplaceApi.Api.Middlewares;
using FireplaceApi.Api.Tools;
using FireplaceApi.Infrastructure.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.IO;
using System.Reflection;
using System.Text.Json.Serialization;

var program = new FireplaceApi.Api.Program();
program.Start(args);


namespace FireplaceApi.Api
{
    public class Program
    {
        private Logger _logger;

        public void Start(string[] args)
        {
            ProjectInitializer.Start();
            _logger = ProjectInitializer.Logger;
            try
            {
                var builder = CreateBuilder(args);
                var app = CreateApp(builder);
                app.Run();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Stopped program because of exception!");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                _logger.Trace("Flushing logger...");
                LogManager.Shutdown();
            }
        }

        private WebApplicationBuilder CreateBuilder(string[] args)
        {
            _logger.Trace("Starting api...");
            var builder = WebApplication.CreateBuilder(args);
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            builder.WebHost.UseNLog();
            ConfigureBuilderServices(builder);
            return builder;
        }

        private void ConfigureBuilderServices(WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddSingleton<IObjectModelValidator, NullObjectModelValidator>();
            var infrastructureAssemblyName = $"{nameof(FireplaceApi)}.{nameof(FireplaceApi.Infrastructure)}";
            builder.Services.AddDbContext<FireplaceApiContext>(
                optionsBuilder => optionsBuilder.UseNpgsql(
                    builder.Configuration.GetConnectionString(Constants.MainDatabaseKey),
                    optionsBuilder => optionsBuilder.MigrationsAssembly(infrastructureAssemblyName))
            );
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            builder.Services.AddInfrastructurConverters();
            builder.Services.AddRepositories();
            builder.Services.AddGateways();
            builder.Services.AddTools();
            builder.Services.AddOperators();
            builder.Services.AddValidators();
            builder.Services.AddServices();
            builder.Services.AddApiConverters();

            builder.Services.AddRouting(options => options.LowercaseUrls = true);
            builder.Services.AddControllers(options =>
            {
                options.Filters.Add(typeof(RequestingUserInjectorAttribute));
                options.Filters.Add(typeof(InputHeaderParametersInjectorAttribute));
                options.Filters.Add(typeof(InputCookieParametersInjectorAttribute));
            }).AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = SnakeCaseNamingPolicy.Instance;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            builder.Services.AddRouting(options =>
            {
                options.ConstraintMap.Add(UlongRouteConstraint.Name, typeof(UlongRouteConstraint));
            });

            builder.Services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
            });

            builder.Services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            builder.Services.AddSwaggerGen(options =>
            {
                options.EnableAnnotations(false, true);
                options.OperationFilter<SwaggerDefaultValues>();
                options.OperationFilter<ActionResponseExampleProvider>();
                options.DocumentFilter<CustomModelDocumentFilter<ApiExceptionErrorDto>>();

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);

                options.AddSecurityDefinition("Bearer",
                    new OpenApiSecurityScheme
                    {
                        Description = "Authorization header using the Bearer scheme.",
                        Type = SecuritySchemeType.Http,
                        Scheme = "bearer",
                        In = ParameterLocation.Header,
                    });

                options.DocumentFilter<OrderDocumentFilter>();



            });

            builder.Services.AddHsts(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(3);
                //options.ExcludedHosts.Add("example.com");
            });

            //builder.Services.AddHttpsRedirection(options =>
            //{
            //    options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
            //    options.HttpsPort = 5021;
            //});

            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
                options.SuppressMapClientErrors = true;
            });

        }

        private WebApplication CreateApp(WebApplicationBuilder builder)
        {
            var app = builder.Build();

            app.UseRequestDurationMiddleware();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });


            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseRewriter(new RewriteOptions()
                // Saving for time that api wants redirecting.
                //.AddRedirect(@"api/(?!v\d)(.*)", "api/v0.2/$1", (int)HttpStatusCode.Redirect)); 
                .AddRewrite(@"^(?!v\d\.)(?!docs)(?!swagger)(.*)", "v0.1/$1", false));
            app.UseRouting();
            //app.UseAuthorization();

            app.UseStaticFiles();

            app.UseSwagger(options =>
            {
                options.PreSerializeFilters.Add((swaggerDoc, httpRequest) =>
                {
                    // hide all SwaggerDocument PathItems with added Security information for OAuth2 without accepted roles (for example, "AcceptedRole")
                    var x = swaggerDoc;
                    var y = httpRequest;
                });
                options.RouteTemplate = "docs/{documentName}/swagger.json";
            });
            app.UseSwaggerUI(options =>
            {
                options.DocumentTitle = "Fireplace Api Docs";
                options.EnableDeepLinking();
                //options.EnableFilter();
                options.RoutePrefix = "docs";
                options.SwaggerEndpoint($"/docs/v0.1/swagger.json", "V0.1");
                //Todo
                // build a swagger endpoint for each discovered API version
                //foreach (var description in app. provider.ApiVersionDescriptions.OrderByDescending(x => x.ApiVersion).ToList())
                //{
                //    options.SwaggerEndpoint($"/docs/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                //}
                options.DocExpansion(DocExpansion.List);
                options.DisplayRequestDuration();
                options.InjectStylesheet("https://fonts.googleapis.com/css?family=Roboto");
                options.InjectStylesheet("/swagger-ui/custom-swagger-ui.css");
                options.InjectJavascript("https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js");
                options.InjectJavascript("https://apis.google.com/js/platform.js");
                options.InjectJavascript("/swagger-ui/custom-swagger-ui.js");
            });
            app.UseRequestResponseLoggingMiddleware();
            app.UseExceptionMiddleware();
            app.UseHeaderParametersMiddleware();
            app.UseCookieParametersMiddleware();
            app.UseFirewallMiddleware();
            app.MapControllers();
            return app;
        }
    }
}
