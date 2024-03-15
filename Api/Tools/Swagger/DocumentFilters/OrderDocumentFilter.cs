﻿//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Logging;
//using Microsoft.OpenApi.Models;
//using Swashbuckle.AspNetCore.SwaggerGen;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace FireplaceApi.Api.Tools
//{
//    //Usage : "options.DocumentFilter<CustomModelDocumentFilter<Error>>();"
//    public class OrderDocumentFilter<T> : IDocumentFilter where T : class
//    {
//        private readonly ILogger<CustomModelDocumentFilter<T>> _logger;
//        private readonly string _swaggerDocHost;
//        private readonly IHttpContextAccessor _httpContextAccessor;

//        public OrderDocumentFilter(ILogger<CustomModelDocumentFilter<T>> logger, IHttpContextAccessor httpContextAccessor)
//        {
//            _logger = logger;
//            var scheme = httpContextAccessor?.HttpContext?.Request?.Scheme;
//            var host = httpContextAccessor?.HttpContext?.Request?.Host;
//            var pathBase = httpContextAccessor?.HttpContext?.Request?.PathBase;
//            _swaggerDocHost = $"{scheme}://{host}{pathBase}";
//            _httpContextAccessor = httpContextAccessor;
//            logger.LogInformation($"_swaggerDocHost : {_swaggerDocHost}");
//        }

//        public void Apply(OpenApiDocument openapiDoc, DocumentFilterContext context)
//        {
//            openapiDoc.Paths.OrderBy(e => e.Value.Operations[0].)
//            openapiDoc.Servers = new List<OpenApiServer>() { new OpenApiServer() { Url = _swaggerDocHost } };
//            context.SchemaGenerator.GenerateSchema(typeof(T), context.SchemaRepository);
//        }

//        public void Apply(SwaggerDocument swaggerDoc, SchemaRegistry schemaRegistry, System.Web.Http.Description.IApiExplorer apiExplorer)
//        {
//            //make operations alphabetic
//            var paths = swaggerDoc.paths.OrderBy(e => e.Key).ToList();
//            swaggerDoc.paths = paths.ToDictionary(e => e.Key, e => e.Value);


//        }
//    }
//}