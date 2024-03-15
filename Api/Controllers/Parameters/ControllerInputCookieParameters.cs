﻿using Microsoft.AspNetCore.Http;
using GamingCommunityApi.Api.Extensions;
using GamingCommunityApi.Api.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GamingCommunityApi.Api.Controllers.Parameters
{
    public class ControllerInputCookieParameters
    {
        public string AccessTokenValue { get; set; }

        public ControllerInputCookieParameters(HttpContext httpContext)
        {
            AccessTokenValue = ExtractAccessTokenValue(httpContext);
        }

        private string ExtractAccessTokenValue(HttpContext httpContext)
        {
            string accessTokenValue = null;
            return accessTokenValue;
        }
    }
}