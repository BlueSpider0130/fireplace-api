﻿// <auto-generated />
using System;
using GamingCommunityApi.Core.ValueObjects;
using GamingCommunityApi.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace GamingCommunityApi.Infrastructure.Migrations
{
    [DbContext(typeof(GamingCommunityApiContext))]
    [Migration("20200510081953_AddGoogleUserEntity")]
    partial class AddGoogleUserEntity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("GamingCommunityApi.Infrastructure.Entities.ErrorEntity", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ClientMessage")
                        .HasColumnType("text");

                    b.Property<int>("Code")
                        .HasColumnType("integer");

                    b.Property<int>("HttpStatusCode")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(400);

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

            modelBuilder.Entity("GamingCommunityApi.Infrastructure.Entities.FileEntity", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
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

            modelBuilder.Entity("GamingCommunityApi.Infrastructure.Entities.GlobalEntity", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<GlobalValues>("Values")
                        .HasColumnName("Values")
                        .HasColumnType("jsonb");

                    b.HasKey("Id");

                    b.ToTable("GlobalEntities");
                });

            modelBuilder.Entity("GamingCommunityApi.Infrastructure.Entities.UserInformationEntities.AccessTokenEntity", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("UserEntityId")
                        .HasColumnType("bigint");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserEntityId");

                    b.HasIndex("Value")
                        .IsUnique();

                    b.ToTable("AccessTokenEntities");
                });

            modelBuilder.Entity("GamingCommunityApi.Infrastructure.Entities.UserInformationEntities.EmailEntity", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("ActivationCode")
                        .HasColumnType("bigint");

                    b.Property<string>("ActivationStatus")
                        .HasColumnType("text");

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<long>("UserEntityId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("Address")
                        .IsUnique();

                    b.HasIndex("UserEntityId")
                        .IsUnique();

                    b.ToTable("EmailEntities");
                });

            modelBuilder.Entity("GamingCommunityApi.Infrastructure.Entities.UserInformationEntities.GoogleUserEntity", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

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

                    b.Property<long>("UserEntityId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("GmailAddress")
                        .IsUnique();

                    b.HasIndex("UserEntityId")
                        .IsUnique();

                    b.ToTable("GoogleUserEntities");
                });

            modelBuilder.Entity("GamingCommunityApi.Infrastructure.Entities.UserInformationEntities.SessionEntity", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("IpAddress")
                        .HasColumnType("text");

                    b.Property<string>("State")
                        .HasColumnType("text");

                    b.Property<long>("UserEntityId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserEntityId");

                    b.ToTable("SessionEntities");
                });

            modelBuilder.Entity("GamingCommunityApi.Infrastructure.Entities.UserInformationEntities.UserEntity", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("State")
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("UserEntities");
                });

            modelBuilder.Entity("GamingCommunityApi.Infrastructure.Entities.UserInformationEntities.AccessTokenEntity", b =>
                {
                    b.HasOne("GamingCommunityApi.Infrastructure.Entities.UserInformationEntities.UserEntity", "UserEntity")
                        .WithMany("AccessTokenEntities")
                        .HasForeignKey("UserEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GamingCommunityApi.Infrastructure.Entities.UserInformationEntities.EmailEntity", b =>
                {
                    b.HasOne("GamingCommunityApi.Infrastructure.Entities.UserInformationEntities.UserEntity", "UserEntity")
                        .WithOne("EmailEntity")
                        .HasForeignKey("GamingCommunityApi.Infrastructure.Entities.UserInformationEntities.EmailEntity", "UserEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GamingCommunityApi.Infrastructure.Entities.UserInformationEntities.GoogleUserEntity", b =>
                {
                    b.HasOne("GamingCommunityApi.Infrastructure.Entities.UserInformationEntities.UserEntity", "UserEntity")
                        .WithOne("GoogleUserEntity")
                        .HasForeignKey("GamingCommunityApi.Infrastructure.Entities.UserInformationEntities.GoogleUserEntity", "UserEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GamingCommunityApi.Infrastructure.Entities.UserInformationEntities.SessionEntity", b =>
                {
                    b.HasOne("GamingCommunityApi.Infrastructure.Entities.UserInformationEntities.UserEntity", "UserEntity")
                        .WithMany("SessionEntities")
                        .HasForeignKey("UserEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
