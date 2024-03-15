﻿using FireplaceApi.Core.Enums;
using FireplaceApi.Core.Extensions;
using FireplaceApi.Core.Interfaces;
using FireplaceApi.Core.Models;
using FireplaceApi.Core.Tools;
using FireplaceApi.Core.ValueObjects;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FireplaceApi.Core.Operators
{
    public class CommentOperator
    {
        private readonly ILogger<CommentOperator> _logger;
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;
        private readonly ICommentRepository _commentRepository;
        private readonly ICommentVoteRepository _commentVoteRepository;
        private readonly PageOperator _pageOperator;
        private readonly UserOperator _userOperator;
        private readonly PostOperator _postOperator;

        public CommentOperator(ILogger<CommentOperator> logger,
            IConfiguration configuration, IServiceProvider serviceProvider,
            ICommentRepository commentRepository,
            ICommentVoteRepository commentVoteRepository,
            PageOperator pageOperator, UserOperator userOperator,
            PostOperator postOperator)
        {
            _logger = logger;
            _configuration = configuration;
            _serviceProvider = serviceProvider;
            _commentRepository = commentRepository;
            _commentVoteRepository = commentVoteRepository;
            _pageOperator = pageOperator;
            _userOperator = userOperator;
            _postOperator = postOperator;
        }

        public async Task<Page<Comment>> ListSelfCommentsAsync(User requesterUser,
            PaginationInputParameters paginationInputParameters, SortType? sort)
        {
            Page<Comment> resultPage = default;
            if (string.IsNullOrWhiteSpace(paginationInputParameters.Pointer))
            {
                var commentIds = await _commentRepository.ListSelfCommentIdsAsync(
                    requesterUser.Id, sort);
                resultPage = await _pageOperator.CreatePageWithoutPointerAsync(
                    ModelName.COMMENT, paginationInputParameters, commentIds,
                    _commentRepository.ListCommentsAsync, requesterUser);
            }
            else
            {
                resultPage = await _pageOperator.CreatePageWithPointerAsync(
                    ModelName.COMMENT, paginationInputParameters,
                    _commentRepository.ListCommentsAsync, requesterUser);
            }
            return resultPage;
        }

        public async Task<Page<Comment>> ListPostCommentsAsync(User requesterUser,
            PaginationInputParameters paginationInputParameters, ulong postId,
            SortType? sort)
        {
            Page<Comment> resultPage = default;
            if (string.IsNullOrWhiteSpace(paginationInputParameters.Pointer))
            {
                var commentIds = await _commentRepository.ListPostCommentIdsAsync(
                    postId, sort);
                resultPage = await _pageOperator.CreatePageWithoutPointerAsync(
                    ModelName.COMMENT, paginationInputParameters, commentIds,
                    _commentRepository.ListCommentsAsync, requesterUser);
            }
            else
            {
                resultPage = await _pageOperator.CreatePageWithPointerAsync(
                    ModelName.COMMENT, paginationInputParameters,
                    _commentRepository.ListCommentsAsync, requesterUser);
            }
            var childComments = await _commentRepository.ListChildCommentsAsync(
                    postId, resultPage.ItemIds, requesterUser);
            SetChilds(resultPage.Items, childComments);
            return resultPage;
        }

        private void SetChilds(List<Comment> parentComments,
            List<Comment> childComments)
        {
            var childrenPerParentId = childComments
                .GroupBy(cc => cc.ParentCommentIds.Last())
                .ToDictionary(g => g.Key, g => g.ToList());
            SetChilds(parentComments, childrenPerParentId);
        }

        private void SetChilds(List<Comment> parentComments,
            Dictionary<ulong, List<Comment>> childrenPerParentId)
        {
            if (parentComments == null || parentComments.Count == 0)
                return;
            foreach (var pc in parentComments)
            {
                pc.ChildComments = childrenPerParentId.GetValueOrDefault(pc.Id);
                SetChilds(pc.ChildComments, childrenPerParentId);
            }
        }

        public async Task<List<Comment>> ListChildCommentsAsync(User requesterUser,
            ulong parentCommentId)
        {
            var postId = (await GetCommentByIdAsync(parentCommentId, false,
                false, null)).PostId;
            var childComments = await _commentRepository.ListChildCommentsAsync(
                    postId, parentCommentId, requesterUser);
            var rootComments = childComments.Where(cc => cc.ParentCommentIds.Last() == parentCommentId).ToList();
            rootComments.ForEach(rc => childComments.Remove(rc));
            SetChilds(rootComments, childComments);
            return rootComments;
        }

        public async Task<Comment> GetCommentByIdAsync(ulong id,
            bool includeAuthor, bool includePost, User requesterUser)
        {
            var comment = await _commentRepository.GetCommentByIdAsync(
                id, includeAuthor, includePost, requesterUser);
            if (comment == null)
                return comment;

            return comment;
        }

        public async Task<Comment> ReplyToPostAsync(User requesterUser,
            ulong postId, string content)
        {
            var id = await IdGenerator.GenerateNewIdAsync(DoesCommentIdExistAsync);
            var comment = await _commentRepository
                .CreateCommentAsync(id, requesterUser.Id,
                    requesterUser.Username, postId, content,
                    new List<ulong>());
            return comment;
        }

        public async Task<Comment> ReplyToCommentAsync(User requesterUser,
            ulong commentId, string content)
        {
            var parentComment = await _commentRepository
                .GetCommentByIdAsync(commentId);
            var postId = parentComment.PostId;
            var parentCommentIds = parentComment.ParentCommentIds.CopyOrDefault();
            parentCommentIds.Add(parentComment.Id);
            var id = await IdGenerator.GenerateNewIdAsync(DoesCommentIdExistAsync);
            var comment = await _commentRepository
                .CreateCommentAsync(id, requesterUser.Id, requesterUser.Username,
                    postId, content, parentCommentIds);
            return comment;
        }

        public async Task<Comment> VoteCommentAsync(User requesterUser,
            ulong id, bool isUp)
        {
            var commentVoteId = await IdGenerator.GenerateNewIdAsync(
                DoesCommentVoteIdExistAsync);
            var commentVote = await _commentVoteRepository
                .CreateCommentVoteAsync(commentVoteId, requesterUser.Id,
                    requesterUser.Username, id, isUp);
            var voteChange = commentVote.IsUp ? +1 : -1;
            var comment = await PatchCommentByIdAsync(requesterUser,
                id, null, voteChange: voteChange);
            return comment;
        }

        public async Task<Comment> ToggleVoteForCommentAsync(User requesterUser,
            ulong id)
        {
            var commentVote = await _commentVoteRepository.GetCommentVoteAsync(
                requesterUser.Id, id, includeComment: true);
            commentVote.IsUp = !commentVote.IsUp;
            await _commentVoteRepository.UpdateCommentVoteAsync(commentVote);
            var voteChange = commentVote.IsUp ? +2 : -2;
            var comment = await ApplyCommentChangesAsync(commentVote.Comment,
                null, voteChange: voteChange);
            comment = await GetCommentByIdAsync(comment.Id,
                false, false, requesterUser);
            return comment;
        }

        public async Task<Comment> DeleteVoteForCommentAsync(User requesterUser,
            ulong id)
        {
            var commentVote = await _commentVoteRepository.GetCommentVoteAsync(
                requesterUser.Id, id, includeComment: true);
            var voteChange = commentVote.IsUp ? -1 : +1;
            await _commentVoteRepository.DeleteCommentVoteByIdAsync(
                commentVote.Id);
            var comment = await ApplyCommentChangesAsync(commentVote.Comment,
                null, voteChange: voteChange);
            return comment;
        }

        public async Task<Comment> PatchCommentByIdAsync(User requesterUser,
            ulong id, string content, int? voteChange)
        {
            var comment = await _commentRepository
                .GetCommentByIdAsync(id);
            comment = await ApplyCommentChangesAsync(comment, content,
                voteChange);
            comment = await GetCommentByIdAsync(comment.Id,
                false, false, requesterUser);
            return comment;
        }

        public async Task DeleteCommentByIdAsync(ulong id)
        {
            await _commentRepository.DeleteCommentByIdAsync(id);
        }

        public async Task<bool> DoesCommentIdExistAsync(ulong id)
        {
            var commentIdExists = await _commentRepository
                .DoesCommentIdExistAsync(id);
            return commentIdExists;
        }

        public async Task<bool> DoesCommentVoteIdExistAsync(ulong commentVoteId)
        {
            var commentIdExists = await _commentVoteRepository
                .DoesCommentVoteIdExistAsync(commentVoteId);
            return commentIdExists;
        }

        public async Task<Comment> ApplyCommentChangesAsync(
            Comment comment, string content, int? voteChange)
        {
            if (content != null)
            {
                comment.Content = content;
            }

            if (voteChange.HasValue)
            {
                comment.Vote += voteChange.Value;
            }

            comment = await _commentRepository
                .UpdateCommentAsync(comment);
            return comment;
        }
    }
}
