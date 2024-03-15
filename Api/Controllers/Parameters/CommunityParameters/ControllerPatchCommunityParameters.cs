﻿using FireplaceApi.Api.Tools;
using FireplaceApi.Core.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FireplaceApi.Api.Controllers
{
    public class ControllerPatchCommunityByIdInputRouteParameters
    {
        [Required]
        [FromRoute(Name = "id")]
        public long Id { get; set; }
    }

    public class ControllerPatchCommunityByNameInputRouteParameters
    {
        [Required]
        [FromRoute(Name = "name")]
        public string Name { get; set; }
    }

    [SwaggerSchemaFilter(typeof(TypeExampleProvider))]
    public class ControllerPatchCommunityInputBodyParameters
    {
        [JsonPropertyName("name")]
        public string NewName { get; set; }

        public static IOpenApiAny Example { get; } = new OpenApiObject
        {
            [nameof(NewName).ToSnakeCase()] = new OpenApiString("new-name"),
        };
    }
}
