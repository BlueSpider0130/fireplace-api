﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FireplaceApi.Infrastructure.Entities
{
    [Index(nameof(AuthorEntityId), IsUnique = false)]
    [Index(nameof(AuthorEntityUsername), IsUnique = false)]
    [Index(nameof(CommunityEntityId), IsUnique = false)]
    [Index(nameof(CommunityEntityName), IsUnique = false)]
    public class PostEntity : BaseEntity
    {
        public ulong AuthorEntityId { get; set; }
        [Required]
        public string AuthorEntityUsername { get; set; }
        public ulong CommunityEntityId { get; set; }
        [Required]
        public string CommunityEntityName { get; set; }
        public int Vote { get; set; }
        [Required]
        public string Content { get; set; }
        public UserEntity AuthorEntity { get; set; }
        public CommunityEntity CommunityEntity { get; set; }
        public List<PostVoteEntity> PostVoteEntities { get; set; }
        public List<CommentEntity> CommentEntities { get; set; }

        private PostEntity() : base() { }

        public PostEntity(ulong id, ulong authorEntityId, string authorEntityUsername,
            ulong communityEntityId, string communityEntityName,
            string content, DateTime? creationDate = null,
            DateTime? modifiedDate = null, int vote = 0,
            UserEntity author = null, CommunityEntity communityEntity = null,
            List<PostVoteEntity> postVoteEntities = null,
            List<CommentEntity> commentEntities = null) : base(id, creationDate, modifiedDate)
        {
            AuthorEntityId = authorEntityId;
            AuthorEntityUsername = authorEntityUsername;
            CommunityEntityId = communityEntityId;
            CommunityEntityName = communityEntityName;
            Content = content ?? throw new ArgumentNullException(nameof(content));
            Vote = vote;
            AuthorEntity = author;
            CommunityEntity = communityEntity;
            PostVoteEntities = postVoteEntities;
            CommentEntities = commentEntities;
        }

        public PostEntity PureCopy() => new PostEntity(Id, AuthorEntityId,
            AuthorEntityUsername, CommunityEntityId, CommunityEntityName,
            Content, CreationDate, ModifiedDate, Vote);
    }

    public class PostEntityConfiguration : IEntityTypeConfiguration<PostEntity>
    {
        public void Configure(EntityTypeBuilder<PostEntity> modelBuilder)
        {
            // p => principal / d => dependent

            modelBuilder.DoBaseConfiguration();

            modelBuilder
                .HasOne(d => d.AuthorEntity)
                .WithMany(p => p.PostEntities)
                .HasForeignKey(d => new { d.AuthorEntityId, d.AuthorEntityUsername })
                .HasPrincipalKey(p => new { p.Id, p.Username })
                .IsRequired();

            modelBuilder
                .HasOne(d => d.CommunityEntity)
                .WithMany(p => p.PostEntities)
                .HasForeignKey(d => new { d.CommunityEntityId, d.CommunityEntityName })
                .HasPrincipalKey(p => new { p.Id, p.Name })
                .IsRequired();
        }
    }
}
