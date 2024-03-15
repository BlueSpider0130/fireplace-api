﻿using FireplaceApi.Application.Extensions;
using FireplaceApi.Application.Interfaces;
using FireplaceApi.Application.Tools;
using FireplaceApi.Application.Validators;
using FireplaceApi.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Any;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace FireplaceApi.Application.Controllers
{
    public class VotePostInputRouteParameters : IValidator
    {
        [Required]
        [FromRoute(Name = "id")]
        public string EncodedId { get; set; }

        [BindNever]
        public ulong Id { get; set; }

        public void Validate(IServiceProvider serviceProvider)
        {
            var applicationValidator = serviceProvider.GetService<PostValidator>();
            var domainValidator = applicationValidator.DomainValidator;

            Id = applicationValidator.ValidateEncodedIdFormat(EncodedId, FieldName.POST_ID).Value;
        }
    }

    [SwaggerSchemaFilter(typeof(TypeExampleProvider))]
    public class VotePostInputBodyParameters : IValidator
    {
        [Required]
        public bool? IsUpvote { get; set; }

        public static IOpenApiAny Example { get; } = new OpenApiObject
        {
            [nameof(IsUpvote).ToSnakeCase()] = new OpenApiBoolean(true),
        };

        public void Validate(IServiceProvider serviceProvider)
        {
            var applicationValidator = serviceProvider.GetService<PostValidator>();
            var domainValidator = applicationValidator.DomainValidator;

            applicationValidator.ValidateFieldIsNotMissing(IsUpvote, FieldName.IS_UPVOTE);
        }
    }

    public class ToggleVoteForPostInputRouteParameters : IValidator
    {
        [Required]
        [FromRoute(Name = "id")]
        public string EncodedId { get; set; }

        [BindNever]
        public ulong Id { get; set; }

        public void Validate(IServiceProvider serviceProvider)
        {
            var applicationValidator = serviceProvider.GetService<PostValidator>();
            var domainValidator = applicationValidator.DomainValidator;

            Id = applicationValidator.ValidateEncodedIdFormat(EncodedId, FieldName.POST_ID).Value;
        }
    }

    public class DeleteVoteForPostInputRouteParameters : IValidator
    {
        [Required]
        [FromRoute(Name = "id")]
        public string EncodedId { get; set; }

        [BindNever]
        public ulong Id { get; set; }

        public void Validate(IServiceProvider serviceProvider)
        {
            var applicationValidator = serviceProvider.GetService<PostValidator>();
            var domainValidator = applicationValidator.DomainValidator;

            Id = applicationValidator.ValidateEncodedIdFormat(EncodedId, FieldName.POST_ID).Value;
        }
    }
}
