﻿using FireplaceApi.Domain.Enums;
using FireplaceApi.Domain.Exceptions;
using FireplaceApi.Domain.Extensions;
using FireplaceApi.Domain.Interfaces;
using FireplaceApi.Domain.Models;
using FireplaceApi.Infrastructure.Converters;
using FireplaceApi.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace FireplaceApi.Infrastructure.Repositories;

public class PostVoteRepository : IPostVoteRepository
{
    private readonly ILogger<PostVoteRepository> _logger;
    private readonly ProjectDbContext _dbContext;
    private readonly DbSet<PostVoteEntity> _postVoteEntities;

    public PostVoteRepository(ILogger<PostVoteRepository> logger, ProjectDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
        _postVoteEntities = dbContext.PostVoteEntities;
    }

    public async Task<List<PostVote>> ListPostVotesAsync(List<ulong> Ids)
    {
        _logger.LogAppInformation(title: "DATABASE_INPUT", parameters: new { Ids });
        var sw = Stopwatch.StartNew();
        var postEntities = await _postVoteEntities
            .AsNoTracking()
            .Where(e => Ids.Contains(e.Id))
            .ToListAsync();

        var postEntityDictionary = new Dictionary<ulong, PostVoteEntity>();
        postEntities.ForEach(e => postEntityDictionary[e.Id] = e);
        postEntities = new List<PostVoteEntity>();
        Ids.ForEach(id => postEntities.Add(postEntityDictionary[id]));

        _logger.LogAppInformation(sw: sw, title: "DATABASE_OUTPUT",
            parameters: new { postEntities = postEntities.Select(e => e.Id) });
        return postEntities.Select(PostVoteConverter.ToModel).ToList();
    }

    public async Task<PostVote> GetPostVoteByIdAsync(ulong id,
        bool includeVoter = false, bool includePost = false)
    {
        _logger.LogAppInformation(title: "DATABASE_INPUT", parameters: new
        {
            id,
            includeVoter,
            includePost
        });
        var sw = Stopwatch.StartNew();
        var postEntity = await _postVoteEntities
            .AsNoTracking()
            .Where(e => e.Id == id)
            .Include(
                voterEntity: includeVoter,
                postEntity: includePost
            )
            .SingleOrDefaultAsync();

        _logger.LogAppInformation(sw: sw, title: "DATABASE_OUTPUT", parameters: new { postEntity });
        return postEntity.ToModel();
    }

    public async Task<PostVote> GetPostVoteAsync(ulong voterId,
        ulong postId, bool includeVoter = false, bool includePost = false)
    {
        _logger.LogAppInformation(title: "DATABASE_INPUT", parameters: new
        {
            voterId,
            postId,
            includeVoter,
            includePost
        });
        var sw = Stopwatch.StartNew();
        var postEntity = await _postVoteEntities
            .AsNoTracking()
            .Where(e => e.VoterEntityId == voterId
                && e.PostEntityId == postId)
            .Include(
                voterEntity: includeVoter,
                postEntity: includePost
            )
            .SingleOrDefaultAsync();

        _logger.LogAppInformation(sw: sw, title: "DATABASE_OUTPUT", parameters: new { postEntity });
        return postEntity.ToModel();
    }

    public async Task<PostVote> CreatePostVoteAsync(ulong id, ulong voterUserId,
        string voterUsername, ulong postId, bool isUp)
    {
        _logger.LogAppInformation(title: "DATABASE_INPUT", parameters: new
        {
            id,
            voterUserId,
            voterUsername,
            postId,
            isUp
        });
        var sw = Stopwatch.StartNew();
        var postEntity = new PostVoteEntity(id, voterUserId,
            voterUsername, postId, isUp);
        _postVoteEntities.Add(postEntity);
        await _dbContext.SaveChangesAsync();
        _dbContext.DetachAllEntries();

        _logger.LogAppInformation(sw: sw, title: "DATABASE_OUTPUT",
            parameters: new { postEntity });
        return postEntity.ToModel();
    }

    public async Task<PostVote> UpdatePostVoteAsync(PostVote postvote)
    {
        _logger.LogAppInformation(title: "DATABASE_INPUT", parameters: new { postvote });
        var sw = Stopwatch.StartNew();
        var postEntity = postvote.ToEntity();
        _postVoteEntities.Update(postEntity);
        try
        {
            await _dbContext.SaveChangesAsync();
            _dbContext.DetachAllEntries();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new InternalServerException("Can't update the post vote DbUpdateConcurrencyException!",
                parameters: postvote, systemException: ex);
        }

        _logger.LogAppInformation(sw: sw, title: "DATABASE_OUTPUT", parameters: new { postEntity });
        return postEntity.ToModel();
    }

    public async Task DeletePostVoteByIdAsync(ulong id)
    {
        _logger.LogAppInformation(title: "DATABASE_INPUT", parameters: new { id });
        var sw = Stopwatch.StartNew();
        var postEntity = await _postVoteEntities
            .Where(e => e.Id == id)
            .SingleOrDefaultAsync();

        _postVoteEntities.Remove(postEntity);
        await _dbContext.SaveChangesAsync();
        _dbContext.DetachAllEntries();

        _logger.LogAppInformation(sw: sw, title: "DATABASE_OUTPUT", parameters: new { postEntity });
    }

    public async Task<bool> DoesPostVoteIdExistAsync(ulong id)
    {
        _logger.LogAppInformation(title: "DATABASE_INPUT", parameters: new { id });
        var sw = Stopwatch.StartNew();
        var doesExist = await _postVoteEntities
            .AsNoTracking()
            .Where(e => e.Id == id)
            .AnyAsync();

        _logger.LogAppInformation(sw: sw, title: "DATABASE_OUTPUT", parameters: new { doesExist });
        return doesExist;
    }

    public async Task<bool> DoesPostVoteIdExistAsync(ulong voterId, ulong postId)
    {
        _logger.LogAppInformation(title: "DATABASE_INPUT", parameters: new { voterId, postId });
        var sw = Stopwatch.StartNew();
        var doesExist = await _postVoteEntities
            .AsNoTracking()
            .Where(e => e.VoterEntityId == voterId
                && e.PostEntityId == postId)
            .AnyAsync();

        _logger.LogAppInformation(sw: sw, title: "DATABASE_OUTPUT", parameters: new { doesExist });
        return doesExist;
    }
}

public static class PostVoteRepositoryExtensions
{
    public static IQueryable<PostVoteEntity> Include(
        [NotNull] this IQueryable<PostVoteEntity> q,
        bool voterEntity, bool postEntity)
    {
        if (voterEntity)
            q = q.Include(e => e.VoterEntity);

        if (postEntity)
            q = q.Include(e => e.PostEntity);

        return q;
    }

    public static IQueryable<PostVoteEntity> Search(
        [NotNull] this IQueryable<PostVoteEntity> q,
        ulong? voterId, ulong? postId, SortType? sort)
    {
        if (voterId.HasValue)
            q = q.Where(e => e.VoterEntityId == voterId.Value);

        if (postId.HasValue)
            q = q.Where(e => e.PostEntityId == postId.Value);

        if (sort.HasValue)
        {
            q = sort switch
            {
                SortType.TOP => q.OrderByDescending(e => e.IsUp),
                SortType.NEW => q.OrderByDescending(e => e.CreationDate),
                SortType.OLD => q.OrderBy(e => e.CreationDate),
                _ => q.OrderByDescending(e => e.CreationDate),
            };
        }
        else
            q = q.OrderByDescending(e => e.CreationDate);

        return q;
    }
}
