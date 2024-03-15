﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FireplaceApi.Api.Controllers
{
    public class ControllerPostFileInputFormParameters
    {
        [Required, FromForm(Name = "file")]
        public IFormFile FormFile { get; set; }
    }
}
