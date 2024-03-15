﻿using FireplaceApi.Api.Tools;
using FireplaceApi.Core.Extensions;
using Microsoft.OpenApi.Any;
using Swashbuckle.AspNetCore.Annotations;

namespace FireplaceApi.Api.Controllers
{
    [SwaggerSchemaFilter(typeof(TypeExampleProvider))]
    public class ControllerCreateCommunityMembershipInputBodyParameters
    {
        public long? CommunityId { get; set; }
        public string CommunityName { get; set; }

        public static IOpenApiAny Example { get; } = new OpenApiObject
        {
            [nameof(CommunityName).ToSnakeCase()] = CommunityDto.PureExample1[nameof(CommunityDto.Name).ToSnakeCase()],
        };
    }
}
