﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.OpenApi.Any;
using GamingCommunityApi.Api.Controllers.Parameters.EmailParameters;
using GamingCommunityApi.Api.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace GamingCommunityApi.Api.Controllers.Parameters.SessionParameters
{
    public class ControllerListSessionsOutputHeaderParameters : IControllerOutputHeaderParameters
    {
        public HeaderDictionary GetHeaderDictionary()
        {
            return default;
        }
    }
}