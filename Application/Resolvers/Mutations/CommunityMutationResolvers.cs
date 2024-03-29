﻿using FireplaceApi.Application.Converters;
using FireplaceApi.Application.Dtos;
using FireplaceApi.Application.Interfaces;
using FireplaceApi.Application.Tool;
using FireplaceApi.Application.Validators;
using FireplaceApi.Domain.Enums;
using FireplaceApi.Domain.Models;
using FireplaceApi.Domain.Services;
using HotChocolate;
using HotChocolate.Types;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace FireplaceApi.Application.Resolvers;

[ExtendObjectType(typeof(GraphQLMutation))]
public class CommunityMutationResolvers
{
    public async Task<CommunityDto> CreateCommunitiesAsync(
        [Service(ServiceKind.Resolver)] CommunityService communityService,
        [Service] IServiceProvider serviceProvider,
        [User] User requestingUser,
        [GraphQLNonNullType] CreateCommunityInput input)
    {
        input.Validate(serviceProvider);
        var community = await communityService.CreateCommunityAsync(requestingUser, input.Name);
        var communityDto = community.ToDto();
        return communityDto;
    }
}

public class CreateCommunityInput : IValidator
{
    public string Name { get; set; }

    public void Validate(IServiceProvider serviceProvider)
    {
        var applicationValidator = serviceProvider.GetService<CommunityValidator>();
        var domainValidator = applicationValidator.DomainValidator;

        applicationValidator.ValidateFieldIsNotMissing(Name, FieldName.COMMUNITY_NAME);
        domainValidator.ValidateCommunityNameFormat(Name);
    }
}
