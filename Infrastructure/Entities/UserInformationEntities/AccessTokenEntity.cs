﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.ComponentModel.DataAnnotations;

namespace FireplaceApi.Infrastructure.Entities
{
    [Index(nameof(Value), IsUnique = true)]
    public class AccessTokenEntity : BaseEntity
    {
        public long UserEntityId { get; set; }
        public string Value { get; set; }
        [Key]
        public long? Id { get; set; }
        public UserEntity UserEntity { get; set; }

        private AccessTokenEntity() : base() { }

        public AccessTokenEntity(long userEntityId, string value,
            DateTime? creationDate = null, DateTime? modifiedDate = null,
            long? id = null, UserEntity userEntity = null) : base(creationDate, modifiedDate)
        {
            UserEntityId = userEntityId;
            Value = value ?? throw new ArgumentNullException(nameof(value));
            Id = id;
            UserEntity = userEntity;
        }

        public AccessTokenEntity PureCopy() => new AccessTokenEntity(UserEntityId,
            Value, CreationDate, ModifiedDate, Id);

        public void RemoveLoopReferencing()
        {
            UserEntity = UserEntity?.PureCopy();
        }
    }

    public class AccessTokenEntityConfiguration : IEntityTypeConfiguration<AccessTokenEntity>
    {
        public void Configure(EntityTypeBuilder<AccessTokenEntity> modelBuilder)
        {
            // p => principal / d => dependent

            modelBuilder.DoBaseConfiguration();

            modelBuilder
                .HasOne(d => d.UserEntity)
                .WithMany(p => p.AccessTokenEntities)
                .HasForeignKey(d => d.UserEntityId)
                .HasPrincipalKey(p => p.Id)
                .IsRequired();
        }
    }
}
