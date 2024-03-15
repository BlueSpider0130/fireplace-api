﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamingCommunityApi.Api.Extensions;
using System.Net;
using GamingCommunityApi.Api.Controllers.Parameters.SessionParameters;
using GamingCommunityApi.Api.Controllers.Parameters.UserParameters;
using GamingCommunityApi.Core.Models.UserInformations;

namespace GamingCommunityApi.Api.Converters
{
    public class SessionConverter
    {
        private readonly ILogger<SessionConverter> _logger;
        private readonly IServiceProvider _serviceProvider;

        public SessionConverter(ILogger<SessionConverter> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public SessionDto ConvertToDto(Session session)
        {
            if (session == null)
                return null;

            UserDto userDto = null;
            if (session.User != null)
                userDto = _serviceProvider.GetService<UserConverter>().ConvertToDto(session.User.PureCopy());

            var sessionDto = new SessionDto(session.Id, session.UserId, session.IpAddress.ToString(),
                userDto);

            return sessionDto;
        }
    }
}