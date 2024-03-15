﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using GamingCommunityApi.Api.Extensions;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using GamingCommunityApi.Core.Extensions;
using GamingCommunityApi.Core.Enums;
using GamingCommunityApi.Core.Models;
using GamingCommunityApi.Core.Operators;

namespace GamingCommunityApi.Api.Tools.Swagger.SchemaFilters
{
    public class TypeExampleProvider : ISchemaFilter
    {
        private readonly ILogger<TypeExampleProvider> _logger;
        private readonly IServiceProvider _serviceProvider; 
        //private Dictionary<string, IOpenApiAny> _example;

        public TypeExampleProvider(ILogger<TypeExampleProvider> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public static async Task<OpenApiObject> FillErrorExampleFromName(OpenApiObject errorExample, 
            IServiceProvider serviceProvider, ILogger logger)
        {
            //var errorService = _serviceProvider.GetService<ErrorService>();
            var name = errorExample["name"].To<OpenApiString>().Value.ToEnum<ErrorName>();
            Error error = null;
            using (var scope = serviceProvider.CreateScope())
            {
                try
                {
                    var errorOperator = scope.ServiceProvider.GetService<ErrorOperator>();
                    error = await errorOperator.GetErrorByNameAsync(name);
                }
                catch (Exception ex)
                {
                    logger.LogInformation($"Exception occurred when trying to get error {name} details for examples => {ex.Message}");
                }
                finally
                {
                    if (error == null)
                    {
                        logger.LogInformation($"Error {name} seems does not exists yet or an exception occurred.");
                        error = Error.InternalServerError;
                    }
                }
            }
            errorExample.Remove("name");
            errorExample["code"] = new OpenApiInteger(error.Code);
            errorExample["message"] = new OpenApiString(error.ClientMessage);
            //example["http_status_code"] = new OpenApiInteger(error.HttpStatusCode);
            return errorExample;
        }

        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            var example = context.Type.ExtractExample();

            if (context.Type.Name.Contains("Error") && example.ContainsKey("message") == false)
                example = FillErrorExampleFromName(example, _serviceProvider, _logger).GetAwaiter().GetResult();

            schema.Example = example;
            schema.Default = example;
            //var openApiObject = new OpenApiObject();
            //foreach (var property in context.Type.GetProperties())
            //{
            //    if (
            //        property.HasAttribute<JsonIgnoreAttribute>() == false
            //        && example.ContainsKey(property.Name)
            //        && property.DeclaringType.Name == context.Type.Name)
            //    {
            //        var jsonPropertyNameAttribute = property.GetCustomAttribute<JsonPropertyNameAttribute>(true);
            //        string jsonPropertyName;
            //        if (jsonPropertyNameAttribute == null)
            //            jsonPropertyName = SnakeCaseNamingPolicy.Instance.ConvertName(property.Name);
            //        else
            //            jsonPropertyName = jsonPropertyNameAttribute.Name;

            //        example[jsonPropertyName].
            //        openApiObject[jsonPropertyName] = example[property.Name];
            //    }
            //}

            //schema.Reference.
            //schema.Example = openApiObject;
            //schema.Default = openApiObject;
        }
    }
}