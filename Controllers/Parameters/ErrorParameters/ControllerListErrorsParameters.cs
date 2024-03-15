﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.OpenApi.Any;
using GamingCommunityApi.Enums;
using GamingCommunityApi.Interfaces;
using GamingCommunityApi.Models;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GamingCommunityApi.Controllers.Parameters.ErrorParameters
{
    [BindNever]
    public class ControllerListErrorsInputQueryParameters
    {

    }

    public class ControllerListErrorsOutputHeaderParameters : IControllerOutputHeaderParameters
    {
        public HeaderDictionary GetHeaderDictionary()
        {
            return default;
        }
    }
}
