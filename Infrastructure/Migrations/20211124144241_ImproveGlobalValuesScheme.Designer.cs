﻿// <auto-generated />
using System;
using System.Collections.Generic;
using FireplaceApi.Core.ValueObjects;
using FireplaceApi.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace FireplaceApi.Infrastructure.Migrations
{
    [DbContext(typeof(FireplaceApiContext))]
    [Migration("20211124144241_ImproveGlobalValuesScheme")]
    partial class ImproveGlobalValuesScheme
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:CollationDefinition:case_insensitive", "en-u-ks-primary,en-u-ks-primary,icu,False")
                .HasAnnotation("Npgsql:DefaultColumnCollation", "case_insensitive")
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("FireplaceApi.Infrastructure.Entities.AccessTokenEntity", b =>
                {
                    b.Property<decimal>("Id")
                        .HasColumnType("numeric(20,0)");

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("NOW()");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<decimal>("UserEntityId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserEntityId");

                    b.HasIndex("Value")
                        .IsUnique();

                    b.ToTable("AccessTokenEntities");
                });

            modelBuilder.Entity("FireplaceApi.Infrastructure.Entities.CommentEntity", b =>
                {
                    b.Property<decimal>("Id")
                        .HasColumnType("numeric(20,0)");

                    b.Property<decimal>("AuthorEntityId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<string>("AuthorEntityUsername")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("NOW()");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<List<decimal>>("ParentCommentEntityIds")
                        .HasColumnType("numeric[]");

                    b.Property<decimal>("PostEntityId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<int>("Vote")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AuthorEntityId");

                    b.HasIndex("AuthorEntityUsername");

                    b.HasIndex("PostEntityId");

                    b.HasIndex("AuthorEntityId", "AuthorEntityUsername");

                    b.ToTable("CommentEntities");
                });

            modelBuilder.Entity("FireplaceApi.Infrastructure.Entities.CommentQueryResultEntity", b =>
                {
                    b.Property<decimal>("Id")
                        .HasColumnType("numeric(20,0)");

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("NOW()");

                    b.Property<int>("LastEnd")
                        .HasColumnType("integer");

                    b.Property<int>("LastLimit")
                        .HasColumnType("integer");

                    b.Property<int>("LastPage")
                        .HasColumnType("integer");

                    b.Property<int>("LastStart")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Pointer")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<List<decimal>>("ReferenceEntityIds")
                        .HasColumnType("numeric[]");

                    b.HasKey("Id");

                    b.HasIndex("Pointer")
                        .IsUnique();

                    b.ToTable("CommentQueryResultEntities");
                });

            modelBuilder.Entity("FireplaceApi.Infrastructure.Entities.CommentVoteEntity", b =>
                {
                    b.Property<decimal>("Id")
                        .HasColumnType("numeric(20,0)");

                    b.Property<decimal>("CommentEntityId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("NOW()");

                    b.Property<bool>("IsUp")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<decimal>("VoterEntityId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<string>("VoterEntityUsername")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasAlternateKey("VoterEntityId", "CommentEntityId");

                    b.HasIndex("CommentEntityId");

                    b.HasIndex("VoterEntityId");

                    b.HasIndex("VoterEntityUsername");

                    b.HasIndex("VoterEntityId", "VoterEntityUsername");

                    b.ToTable("CommentVoteEntities");
                });

            modelBuilder.Entity("FireplaceApi.Infrastructure.Entities.CommunityEntity", b =>
                {
                    b.Property<decimal>("Id")
                        .HasColumnType("numeric(20,0)");

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("NOW()");

                    b.Property<decimal>("CreatorEntityId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<string>("CreatorEntityUsername")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CreatorEntityId")
                        .IsUnique();

                    b.HasIndex("CreatorEntityUsername")
                        .IsUnique();

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("CreatorEntityId", "CreatorEntityUsername");

                    b.ToTable("CommunityEntities");
                });

            modelBuilder.Entity("FireplaceApi.Infrastructure.Entities.CommunityMembershipEntity", b =>
                {
                    b.Property<decimal>("Id")
                        .HasColumnType("numeric(20,0)");

                    b.Property<decimal>("CommunityEntityId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<string>("CommunityEntityName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("NOW()");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<decimal>("UserEntityId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<string>("UserEntityUsername")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasAlternateKey("UserEntityId", "CommunityEntityId");

                    b.HasIndex("CommunityEntityId");

                    b.HasIndex("CommunityEntityName");

                    b.HasIndex("UserEntityId");

                    b.HasIndex("UserEntityUsername");

                    b.HasIndex("CommunityEntityId", "CommunityEntityName");

                    b.HasIndex("UserEntityId", "UserEntityUsername");

                    b.ToTable("CommunityMembershipEntities");
                });

            modelBuilder.Entity("FireplaceApi.Infrastructure.Entities.CommunityMembershipQueryResultEntity", b =>
                {
                    b.Property<decimal>("Id")
                        .HasColumnType("numeric(20,0)");

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("NOW()");

                    b.Property<int>("LastEnd")
                        .HasColumnType("integer");

                    b.Property<int>("LastLimit")
                        .HasColumnType("integer");

                    b.Property<int>("LastPage")
                        .HasColumnType("integer");

                    b.Property<int>("LastStart")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Pointer")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<List<decimal>>("ReferenceEntityIds")
                        .HasColumnType("numeric[]");

                    b.HasKey("Id");

                    b.HasIndex("Pointer")
                        .IsUnique();

                    b.ToTable("CommunityMembershipQueryResultEntities");
                });

            modelBuilder.Entity("FireplaceApi.Infrastructure.Entities.CommunityQueryResultEntity", b =>
                {
                    b.Property<decimal>("Id")
                        .HasColumnType("numeric(20,0)");

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("NOW()");

                    b.Property<int>("LastEnd")
                        .HasColumnType("integer");

                    b.Property<int>("LastLimit")
                        .HasColumnType("integer");

                    b.Property<int>("LastPage")
                        .HasColumnType("integer");

                    b.Property<int>("LastStart")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Pointer")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<List<decimal>>("ReferenceEntityIds")
                        .HasColumnType("numeric[]");

                    b.HasKey("Id");

                    b.HasIndex("Pointer")
                        .IsUnique();

                    b.ToTable("CommunityQueryResultEntities");
                });

            modelBuilder.Entity("FireplaceApi.Infrastructure.Entities.EmailEntity", b =>
                {
                    b.Property<decimal>("Id")
                        .HasColumnType("numeric(20,0)");

                    b.Property<int?>("ActivationCode")
                        .HasColumnType("integer");

                    b.Property<string>("ActivationStatus")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("NOW()");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<decimal>("UserEntityId")
                        .HasColumnType("numeric(20,0)");

                    b.HasKey("Id");

                    b.HasIndex("Address")
                        .IsUnique();

                    b.HasIndex("UserEntityId")
                        .IsUnique();

                    b.ToTable("EmailEntities");
                });

            modelBuilder.Entity("FireplaceApi.Infrastructure.Entities.ErrorEntity", b =>
                {
                    b.Property<decimal>("Id")
                        .HasColumnType("numeric(20,0)");

                    b.Property<string>("ClientMessage")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Code")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("NOW()");

                    b.Property<int>("HttpStatusCode")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(400);

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text")
                        .HasDefaultValue("INTERNAL_SERVER");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("ErrorEntities");
                });

            modelBuilder.Entity("FireplaceApi.Infrastructure.Entities.FileEntity", b =>
                {
                    b.Property<decimal>("Id")
                        .HasColumnType("numeric(20,0)");

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("NOW()");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("RealName")
                        .HasColumnType("text");

                    b.Property<string>("RelativePhysicalPath")
                        .HasColumnType("text");

                    b.Property<string>("RelativeUri")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("FileEntities");
                });

            modelBuilder.Entity("FireplaceApi.Infrastructure.Entities.GlobalEntity", b =>
                {
                    b.Property<decimal>("Id")
                        .HasColumnType("numeric(20,0)");

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("NOW()");

                    b.Property<string>("EnvironmentName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<GlobalValues>("Values")
                        .IsRequired()
                        .HasColumnType("jsonb")
                        .HasColumnName("Values");

                    b.HasKey("Id");

                    b.ToTable("GlobalEntities");
                });

            modelBuilder.Entity("FireplaceApi.Infrastructure.Entities.GoogleUserEntity", b =>
                {
                    b.Property<decimal>("Id")
                        .HasColumnType("numeric(20,0)");

                    b.Property<string>("AccessToken")
                        .HasColumnType("text");

                    b.Property<long>("AccessTokenExpiresInSeconds")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("AccessTokenIssuedTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("AuthUser")
                        .HasColumnType("text");

                    b.Property<string>("Code")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("NOW()");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("FullName")
                        .HasColumnType("text");

                    b.Property<string>("GmailAddress")
                        .HasColumnType("text");

                    b.Property<long>("GmailIssuedTimeInSeconds")
                        .HasColumnType("bigint");

                    b.Property<bool>("GmailVerified")
                        .HasColumnType("boolean");

                    b.Property<string>("IdToken")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("Locale")
                        .HasColumnType("text");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("PictureUrl")
                        .HasColumnType("text");

                    b.Property<string>("Prompt")
                        .HasColumnType("text");

                    b.Property<string>("RedirectToUserUrl")
                        .HasColumnType("text");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("text");

                    b.Property<string>("Scope")
                        .HasColumnType("text");

                    b.Property<string>("State")
                        .HasColumnType("text");

                    b.Property<string>("TokenType")
                        .HasColumnType("text");

                    b.Property<decimal>("UserEntityId")
                        .HasColumnType("numeric(20,0)");

                    b.HasKey("Id");

                    b.HasIndex("GmailAddress")
                        .IsUnique();

                    b.HasIndex("UserEntityId")
                        .IsUnique();

                    b.ToTable("GoogleUserEntities");
                });

            modelBuilder.Entity("FireplaceApi.Infrastructure.Entities.PostEntity", b =>
                {
                    b.Property<decimal>("Id")
                        .HasColumnType("numeric(20,0)");

                    b.Property<decimal>("AuthorEntityId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<string>("AuthorEntityUsername")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("CommunityEntityId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<string>("CommunityEntityName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("NOW()");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Vote")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AuthorEntityId");

                    b.HasIndex("AuthorEntityUsername");

                    b.HasIndex("CommunityEntityId");

                    b.HasIndex("CommunityEntityName");

                    b.HasIndex("AuthorEntityId", "AuthorEntityUsername");

                    b.HasIndex("CommunityEntityId", "CommunityEntityName");

                    b.ToTable("PostEntities");
                });

            modelBuilder.Entity("FireplaceApi.Infrastructure.Entities.PostQueryResultEntity", b =>
                {
                    b.Property<decimal>("Id")
                        .HasColumnType("numeric(20,0)");

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("NOW()");

                    b.Property<int>("LastEnd")
                        .HasColumnType("integer");

                    b.Property<int>("LastLimit")
                        .HasColumnType("integer");

                    b.Property<int>("LastPage")
                        .HasColumnType("integer");

                    b.Property<int>("LastStart")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Pointer")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<List<decimal>>("ReferenceEntityIds")
                        .HasColumnType("numeric[]");

                    b.HasKey("Id");

                    b.HasIndex("Pointer")
                        .IsUnique();

                    b.ToTable("PostQueryResultEntities");
                });

            modelBuilder.Entity("FireplaceApi.Infrastructure.Entities.PostVoteEntity", b =>
                {
                    b.Property<decimal>("Id")
                        .HasColumnType("numeric(20,0)");

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("NOW()");

                    b.Property<bool>("IsUp")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<decimal>("PostEntityId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<decimal>("VoterEntityId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<string>("VoterEntityUsername")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasAlternateKey("VoterEntityId", "PostEntityId");

                    b.HasIndex("PostEntityId");

                    b.HasIndex("VoterEntityId");

                    b.HasIndex("VoterEntityUsername");

                    b.HasIndex("VoterEntityId", "VoterEntityUsername");

                    b.ToTable("PostVoteEntities");
                });

            modelBuilder.Entity("FireplaceApi.Infrastructure.Entities.SessionEntity", b =>
                {
                    b.Property<decimal>("Id")
                        .HasColumnType("numeric(20,0)");

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("NOW()");

                    b.Property<string>("IpAddress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("UserEntityId")
                        .HasColumnType("numeric(20,0)");

                    b.HasKey("Id");

                    b.HasIndex("UserEntityId");

                    b.ToTable("SessionEntities");
                });

            modelBuilder.Entity("FireplaceApi.Infrastructure.Entities.UserEntity", b =>
                {
                    b.Property<decimal>("Id")
                        .HasColumnType("numeric(20,0)");

                    b.Property<string>("About")
                        .HasColumnType("text");

                    b.Property<string>("AvatarUrl")
                        .HasColumnType("text");

                    b.Property<string>("BannerUrl")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("NOW()");

                    b.Property<string>("DisplayName")
                        .HasColumnType("text");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("UserEntities");
                });

            modelBuilder.Entity("FireplaceApi.Infrastructure.Entities.AccessTokenEntity", b =>
                {
                    b.HasOne("FireplaceApi.Infrastructure.Entities.UserEntity", "UserEntity")
                        .WithMany("AccessTokenEntities")
                        .HasForeignKey("UserEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserEntity");
                });

            modelBuilder.Entity("FireplaceApi.Infrastructure.Entities.CommentEntity", b =>
                {
                    b.HasOne("FireplaceApi.Infrastructure.Entities.PostEntity", "PostEntity")
                        .WithMany("CommentEntities")
                        .HasForeignKey("PostEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FireplaceApi.Infrastructure.Entities.UserEntity", "AuthorEntity")
                        .WithMany("CommentEntities")
                        .HasForeignKey("AuthorEntityId", "AuthorEntityUsername")
                        .HasPrincipalKey("Id", "Username")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AuthorEntity");

                    b.Navigation("PostEntity");
                });

            modelBuilder.Entity("FireplaceApi.Infrastructure.Entities.CommentVoteEntity", b =>
                {
                    b.HasOne("FireplaceApi.Infrastructure.Entities.CommentEntity", "CommentEntity")
                        .WithMany("CommentVoteEntities")
                        .HasForeignKey("CommentEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FireplaceApi.Infrastructure.Entities.UserEntity", "VoterEntity")
                        .WithMany("CommentVoteEntities")
                        .HasForeignKey("VoterEntityId", "VoterEntityUsername")
                        .HasPrincipalKey("Id", "Username")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CommentEntity");

                    b.Navigation("VoterEntity");
                });

            modelBuilder.Entity("FireplaceApi.Infrastructure.Entities.CommunityEntity", b =>
                {
                    b.HasOne("FireplaceApi.Infrastructure.Entities.UserEntity", "CreatorEntity")
                        .WithMany("OwnCommunities")
                        .HasForeignKey("CreatorEntityId", "CreatorEntityUsername")
                        .HasPrincipalKey("Id", "Username")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatorEntity");
                });

            modelBuilder.Entity("FireplaceApi.Infrastructure.Entities.CommunityMembershipEntity", b =>
                {
                    b.HasOne("FireplaceApi.Infrastructure.Entities.CommunityEntity", "CommunityEntity")
                        .WithMany("CommunityMemberEntities")
                        .HasForeignKey("CommunityEntityId", "CommunityEntityName")
                        .HasPrincipalKey("Id", "Name")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FireplaceApi.Infrastructure.Entities.UserEntity", "UserEntity")
                        .WithMany("JoinedCommunities")
                        .HasForeignKey("UserEntityId", "UserEntityUsername")
                        .HasPrincipalKey("Id", "Username")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CommunityEntity");

                    b.Navigation("UserEntity");
                });

            modelBuilder.Entity("FireplaceApi.Infrastructure.Entities.EmailEntity", b =>
                {
                    b.HasOne("FireplaceApi.Infrastructure.Entities.UserEntity", "UserEntity")
                        .WithOne("EmailEntity")
                        .HasForeignKey("FireplaceApi.Infrastructure.Entities.EmailEntity", "UserEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserEntity");
                });

            modelBuilder.Entity("FireplaceApi.Infrastructure.Entities.GoogleUserEntity", b =>
                {
                    b.HasOne("FireplaceApi.Infrastructure.Entities.UserEntity", "UserEntity")
                        .WithOne("GoogleUserEntity")
                        .HasForeignKey("FireplaceApi.Infrastructure.Entities.GoogleUserEntity", "UserEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserEntity");
                });

            modelBuilder.Entity("FireplaceApi.Infrastructure.Entities.PostEntity", b =>
                {
                    b.HasOne("FireplaceApi.Infrastructure.Entities.UserEntity", "AuthorEntity")
                        .WithMany("PostEntities")
                        .HasForeignKey("AuthorEntityId", "AuthorEntityUsername")
                        .HasPrincipalKey("Id", "Username")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FireplaceApi.Infrastructure.Entities.CommunityEntity", "CommunityEntity")
                        .WithMany("PostEntities")
                        .HasForeignKey("CommunityEntityId", "CommunityEntityName")
                        .HasPrincipalKey("Id", "Name")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AuthorEntity");

                    b.Navigation("CommunityEntity");
                });

            modelBuilder.Entity("FireplaceApi.Infrastructure.Entities.PostVoteEntity", b =>
                {
                    b.HasOne("FireplaceApi.Infrastructure.Entities.PostEntity", "PostEntity")
                        .WithMany("PostVoteEntities")
                        .HasForeignKey("PostEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FireplaceApi.Infrastructure.Entities.UserEntity", "VoterEntity")
                        .WithMany("PostVoteEntities")
                        .HasForeignKey("VoterEntityId", "VoterEntityUsername")
                        .HasPrincipalKey("Id", "Username")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PostEntity");

                    b.Navigation("VoterEntity");
                });

            modelBuilder.Entity("FireplaceApi.Infrastructure.Entities.SessionEntity", b =>
                {
                    b.HasOne("FireplaceApi.Infrastructure.Entities.UserEntity", "UserEntity")
                        .WithMany("SessionEntities")
                        .HasForeignKey("UserEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserEntity");
                });

            modelBuilder.Entity("FireplaceApi.Infrastructure.Entities.CommentEntity", b =>
                {
                    b.Navigation("CommentVoteEntities");
                });

            modelBuilder.Entity("FireplaceApi.Infrastructure.Entities.CommunityEntity", b =>
                {
                    b.Navigation("CommunityMemberEntities");

                    b.Navigation("PostEntities");
                });

            modelBuilder.Entity("FireplaceApi.Infrastructure.Entities.PostEntity", b =>
                {
                    b.Navigation("CommentEntities");

                    b.Navigation("PostVoteEntities");
                });

            modelBuilder.Entity("FireplaceApi.Infrastructure.Entities.UserEntity", b =>
                {
                    b.Navigation("AccessTokenEntities");

                    b.Navigation("CommentEntities");

                    b.Navigation("CommentVoteEntities");

                    b.Navigation("EmailEntity");

                    b.Navigation("GoogleUserEntity");

                    b.Navigation("JoinedCommunities");

                    b.Navigation("OwnCommunities");

                    b.Navigation("PostEntities");

                    b.Navigation("PostVoteEntities");

                    b.Navigation("SessionEntities");
                });
#pragma warning restore 612, 618
        }
    }
}
