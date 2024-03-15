﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using FireplaceApi.Core.ValueObjects;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using System;
using Microsoft.Extensions.Logging;

namespace FireplaceApi.Infrastructure.Entities
{
    public class FireplaceApiContext : DbContext
    {
        private readonly ILogger<FireplaceApiContext> _logger;

        public DbSet<CommentEntity> CommentEntities { get; set; }
        public DbSet<CommentVoteEntity> CommentVoteEntities { get; set; }

        public DbSet<CommunityEntity> CommunityEntities{ get; set; }
        public DbSet<CommunityMemberEntity> CommunityMemberEntities { get; set; }

        public DbSet<PostEntity> PostEntities { get; set; }
        public DbSet<PostVoteEntity> PostVoteEntities { get; set; }

        public DbSet<AccessTokenEntity> AccessTokenEntities { get; set; }
        public DbSet<EmailEntity> EmailEntities { get; set; }
        public DbSet<GoogleUserEntity> GoogleUserEntities { get; set; }
        public DbSet<SessionEntity> SessionEntities { get; set; }
        public DbSet<UserEntity> UserEntities { get; set; }

        public DbSet<ErrorEntity> ErrorEntities { get; set; }
        public DbSet<FileEntity> FileEntities { get; set; }
        public DbSet<GlobalEntity> GlobalEntities { get; set; }

        public FireplaceApiContext(ILogger<FireplaceApiContext> logger, DbContextOptions<FireplaceApiContext> options) 
            : base(options)
        {
            _logger = logger;
            //ChangeTracker.LazyLoadingEnabled = false;
            //ChangeTracker.AutoDetectChangesEnabled = false;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public FireplaceApiContext(string connectionString)
            : base(CreateOptionsFromConnectionString(connectionString))
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.EnableSensitiveDataLogging();
            //optionsBuilder.EnableDetailedErrors();
            //optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            //optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            //optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CommentEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CommentVoteEntityConfiguration());

            modelBuilder.ApplyConfiguration(new CommunityEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CommunityMemberEntityConfiguration());

            modelBuilder.ApplyConfiguration(new PostEntityConfiguration());
            modelBuilder.ApplyConfiguration(new PostVoteEntityConfiguration());

            modelBuilder.ApplyConfiguration(new AccessTokenEntityConfiguration());
            modelBuilder.ApplyConfiguration(new EmailEntityConfiguration());
            modelBuilder.ApplyConfiguration(new GoogleUserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new SessionEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());

            modelBuilder.ApplyConfiguration(new ErrorEntityConfiguration());
            modelBuilder.ApplyConfiguration(new FileEntityConfiguration());
            modelBuilder.ApplyConfiguration(new GlobalEntityConfiguration());
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            var EditedEntities = ChangeTracker.Entries()
                .Where(E => E.State == EntityState.Modified)
                .ToList();

            EditedEntities.ForEach(E =>
            {
                E.Property("ModifiedDate").CurrentValue = DateTime.UtcNow;
            });

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public void DetachAllEntries()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                Entry(entry.Entity).State = EntityState.Detached;
            }
        }

        public static DbContextOptions<FireplaceApiContext> CreateOptionsFromConnectionString(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<FireplaceApiContext>();
            optionsBuilder.UseNpgsql(connectionString);
            return optionsBuilder.Options;
        }
    }
}