﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using GamingCommunityApi.Controllers;
using GamingCommunityApi.Extensions;
using GamingCommunityApi.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GamingCommunityApi.Entities
{
    public class ErrorEntity
    {
        
        public string Name { get; set; }
        public int Code { get; set; }
        public string ClientMessage { get; set; }
        public int HttpStatusCode { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }

        private ErrorEntity() { }

        public ErrorEntity(string name, int code, 
            string clientMessage, int httpStatusCode, int? id = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(clientMessage));
            Code = code;
            ClientMessage = clientMessage ?? throw new ArgumentNullException(nameof(clientMessage));
            HttpStatusCode = httpStatusCode;
            Id = id;
        }

        public ErrorEntity PureCopy() => new ErrorEntity(Name, Code, ClientMessage,
            HttpStatusCode, Id);

        public void RemoveLoopReferencing()
        {

        }
    }

    public class ErrorEntityConfiguration : IEntityTypeConfiguration<ErrorEntity>
    {
        public void Configure(EntityTypeBuilder<ErrorEntity> modelBuilder)
        {
            // p => principal / d => dependent / e => entity

            modelBuilder
               .Property(e => e.Name)
               .HasDefaultValue(ErrorName.INTERNAL_SERVER.ToString())
               .IsRequired();

            modelBuilder
                .HasIndex(e => e.Name)
                .IsUnique();

            modelBuilder
                .HasIndex(e => e.Code)
                .IsUnique();

            modelBuilder
                .Property(e => e.HttpStatusCode)
                .HasDefaultValue(400);
        }
    }
}
