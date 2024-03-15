﻿using FireplaceApi.Core.ValueObjects;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FireplaceApi.Api.Tools
{
    /// <summary>
    /// Configures the Swagger generation options.
    /// </summary>
    /// <remarks>This allows API versioning to define a Swagger document per API version after the
    /// <see cref="IApiVersionDescriptionProvider"/> service has been resolved from the service container.</remarks>
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        readonly IApiVersionDescriptionProvider _provider;
        readonly IWebHostEnvironment _env;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigureSwaggerOptions"/> class.
        /// </summary>
        /// <param name="provider">The <see cref="IApiVersionDescriptionProvider">provider</see> used to generate Swagger documents.</param>
        /// <param name="env"></param>
        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider, IWebHostEnvironment env)
        {
            _provider = provider;
            _env = env;
        }

        /// <inheritdoc />
        public void Configure(SwaggerGenOptions options)
        {
            // add a swagger document for each discovered API version
            // note: you might choose to skip or document deprecated API versions differently
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description, _env));
            }
        }

        static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description, IWebHostEnvironment env)
        {
            var description_html = "<div>";

            description_html += @"
                <div>
                    <p><strong><i>Welcome</i></strong>! Fireplace is a discussion application with communities, posts, and comments, just like Reddit.</p>
                    <p>This project, Fireplace API, provides an API for Fireplace, and it aims to be a real-world example of web API concepts with the ASP.NET Core framework. </p>
                    <br/>
                    <strong><i>Note:</i></strong> 
                    <p>
                        After you have logged in at this page, your access token will automatically be saved at cookies, and you are not needed to use headers for authentication. 
                        Check the <i>User</i> section to proceed...
                    </p>
                    <br />
                    Check the <a target=""_blank"" href=""https://github.com/soroushkavousi/fireplace-api""><b><i>Fireplace API GitHub Repository</i></b></a>
                    <br />
                </div> <br /> <br />";

            description_html += $@"
                <div>
                    <div>Sample urls:</div>
                    <p style=""margin-left: 40px;""><a href=""{Configs.Instance.Api.BaseUrlPath}/communities?name=developers"">{Configs.Instance.Api.BaseUrlPath}/communities?name=developers</a></p>
                </div>";

            description_html += $@"
                <a id=""google-btn"" target=""_blank"" href=""{Configs.Instance.Api.BaseUrlPath}/v0.1/users/open-google-log-in-page"">
                    <div id=""google-icon-wrapper"">
                        <img id=""google-icon"" src=""https://upload.wikimedia.org/wikipedia/commons/5/53/Google_%22G%22_Logo.svg""/>
                    </div>
                    <p id=""btn-text""><b>Log in with Google</b></p>
                </a> 
                ";

            description_html += "</div> <br />";

            var info = new OpenApiInfo()
            {
                Title = "Fireplace Api",
                Version = description.ApiVersion.ToString(),
                Description = description_html,
                Contact = new OpenApiContact
                {
                    Name = "Soroush Kavousi",
                    Email = "soroushkavousi.me@gmail.com",
                },
                //License = new OpenApiLicense
                //{
                //    Name = "Use under LICX",
                //    Url = new Uri("https://example.com/license")
                //},
            };

            if (description.IsDeprecated)
            {
                info.Description += " This API version has been deprecated.";
            }

            return info;
        }
    }
}
