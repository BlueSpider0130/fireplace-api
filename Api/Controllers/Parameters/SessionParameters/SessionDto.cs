﻿using Microsoft.OpenApi.Any;
using FireplaceApi.Api.Extensions;
using FireplaceApi.Api.Interfaces;
using FireplaceApi.Api.Tools;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using FireplaceApi.Core.Extensions;

namespace FireplaceApi.Api.Controllers
{
    [SwaggerSchemaFilter(typeof(TypeExampleProvider))]
    public class SessionDto
    {
        [Required]
        public long? Id { get; set; }
        [Required]
        public long UserId { get; set; }
        [Required]
        public string IpAddress { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        public UserDto User { get; set; }

        public static OpenApiObject PureExample11 { get; } = new OpenApiObject
        {
            [nameof(Id).ToSnakeCase()] = new OpenApiInteger(10001),
            [nameof(UserId).ToSnakeCase()] = null,
            [nameof(IpAddress).ToSnakeCase()] = new OpenApiString("111.111.111.111"),
            [nameof(CreationDate).ToSnakeCase()] = new OpenApiDateTime(Utils.GetYesterdayDate()),
            [nameof(User).ToSnakeCase()] = new OpenApiNull(),
        };
        public static OpenApiObject PureExample12 { get; } = new OpenApiObject
        {
            [nameof(Id).ToSnakeCase()] = new OpenApiInteger(10002),
            [nameof(UserId).ToSnakeCase()] = null,
            [nameof(IpAddress).ToSnakeCase()] = new OpenApiString("111.111.111.112"),
            [nameof(CreationDate).ToSnakeCase()] = new OpenApiDateTime(Utils.GetYesterdayDate()),
            [nameof(User).ToSnakeCase()] = new OpenApiNull(),
        };
        public static OpenApiObject PureExample21 { get; } = new OpenApiObject
        {
            [nameof(Id).ToSnakeCase()] = new OpenApiInteger(20001),
            [nameof(UserId).ToSnakeCase()] = null,
            [nameof(IpAddress).ToSnakeCase()] = new OpenApiString("222.222.222.222"),
            [nameof(CreationDate).ToSnakeCase()] = new OpenApiDateTime(Utils.GetYesterdayDate()),
            [nameof(User).ToSnakeCase()] = new OpenApiNull(),
        };
        public static OpenApiObject PureExample22 { get; } = new OpenApiObject
        {
            [nameof(Id).ToSnakeCase()] = new OpenApiInteger(20002),
            [nameof(UserId).ToSnakeCase()] = null,
            [nameof(IpAddress).ToSnakeCase()] = new OpenApiString("222.222.222.223"),
            [nameof(CreationDate).ToSnakeCase()] = new OpenApiDateTime(Utils.GetYesterdayDate()),
            [nameof(User).ToSnakeCase()] = new OpenApiNull(),
        };
        public static OpenApiArray PureListExample1 { get; } = new OpenApiArray
        {
            PureExample11, PureExample12
        };
        public static OpenApiArray PureListExample2 { get; } = new OpenApiArray
        {
            PureExample21, PureExample22
        };

        public static OpenApiObject Example11 { get; } = new OpenApiObject
        {
            [nameof(Id).ToSnakeCase()] = PureExample11[nameof(Id).ToSnakeCase()],
            [nameof(UserId).ToSnakeCase()] = PureExample11[nameof(UserId).ToSnakeCase()],
            [nameof(IpAddress).ToSnakeCase()] = PureExample11[nameof(IpAddress).ToSnakeCase()],
            [nameof(CreationDate).ToSnakeCase()] = new OpenApiDateTime(Utils.GetYesterdayDate()),
            [nameof(User).ToSnakeCase()] = UserDto.PureExample1,
        };
        public static OpenApiObject Example12 { get; } = new OpenApiObject
        {
            [nameof(Id).ToSnakeCase()] = PureExample12[nameof(Id).ToSnakeCase()],
            [nameof(UserId).ToSnakeCase()] = PureExample12[nameof(UserId).ToSnakeCase()],
            [nameof(IpAddress).ToSnakeCase()] = PureExample12[nameof(IpAddress).ToSnakeCase()],
            [nameof(CreationDate).ToSnakeCase()] = new OpenApiDateTime(Utils.GetYesterdayDate()),
            [nameof(User).ToSnakeCase()] = UserDto.PureExample1,
        };
        public static OpenApiObject Example21 { get; } = new OpenApiObject
        {
            [nameof(Id).ToSnakeCase()] = PureExample21[nameof(Id).ToSnakeCase()],
            [nameof(UserId).ToSnakeCase()] = PureExample21[nameof(UserId).ToSnakeCase()],
            [nameof(IpAddress).ToSnakeCase()] = PureExample21[nameof(IpAddress).ToSnakeCase()],
            [nameof(CreationDate).ToSnakeCase()] = new OpenApiDateTime(Utils.GetYesterdayDate()),
            [nameof(User).ToSnakeCase()] = UserDto.PureExample2,
        };
        public static OpenApiObject Example22 { get; } = new OpenApiObject
        {
            [nameof(Id).ToSnakeCase()] = PureExample22[nameof(Id).ToSnakeCase()],
            [nameof(UserId).ToSnakeCase()] = PureExample22[nameof(UserId).ToSnakeCase()],
            [nameof(IpAddress).ToSnakeCase()] = PureExample22[nameof(IpAddress).ToSnakeCase()],
            [nameof(CreationDate).ToSnakeCase()] = new OpenApiDateTime(Utils.GetYesterdayDate()),
            [nameof(User).ToSnakeCase()] = UserDto.PureExample2,
        };
        public static OpenApiArray ListExample1 { get; } = new OpenApiArray
        {
            Example11, Example12
        };
        public static OpenApiArray ListExample2 { get; } = new OpenApiArray
        {
            Example21, Example22
        };

        public static OpenApiObject Example { get; } = Example11;
        public static Dictionary<string, IOpenApiAny> ActionExamples { get; } = new Dictionary<string, IOpenApiAny>
        {
            [nameof(SessionController.GetSessionByIdAsync)] = Example11,
            [nameof(SessionController.ListSessionsAsync)] = PureListExample1,
        };

        static SessionDto()
        {
            PureExample11[nameof(UserId).ToSnakeCase()] = UserDto.PureExample1[nameof(UserDto.Id).ToSnakeCase()];
            PureExample12[nameof(UserId).ToSnakeCase()] = UserDto.PureExample1[nameof(UserDto.Id).ToSnakeCase()];
            PureExample21[nameof(UserId).ToSnakeCase()] = UserDto.PureExample2[nameof(UserDto.Id).ToSnakeCase()];
            PureExample22[nameof(UserId).ToSnakeCase()] = UserDto.PureExample2[nameof(UserDto.Id).ToSnakeCase()];
        }

        public SessionDto(long? id, long userId, string ipAddress,  
            DateTime creationDate, UserDto user)
        {
            Id = id;
            UserId = userId;
            IpAddress = ipAddress;
            CreationDate = creationDate;
            User = user;
        }
    }
}
