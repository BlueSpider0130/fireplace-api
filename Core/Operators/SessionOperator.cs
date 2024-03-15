﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using GamingCommunityApi.Core.Enums;
using GamingCommunityApi.Core.Models.UserInformations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GamingCommunityApi.Core.Interfaces.IRepositories;

namespace GamingCommunityApi.Core.Operators
{
    public class SessionOperator
    {
        private readonly ILogger<SessionOperator> _logger;
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;
        private readonly ISessionRepository _sessionRepository;

        public SessionOperator(ILogger<SessionOperator> logger, IConfiguration configuration,
            IServiceProvider serviceProvider, ISessionRepository sessionRepository)
        {
            _logger = logger;
            _configuration = configuration;
            _serviceProvider = serviceProvider;
            _sessionRepository = sessionRepository;
        }

        public async Task RevokeSessionByIdAsync(long id)
        {
            await PatchSessionByIdAsync(id, state: SessionState.CLOSED);
        }

        public async Task<List<Session>> ListSessionsAsync(User requesterUser)
        {
            var session = await _sessionRepository.ListSessionsAsync(requesterUser.Id, SessionState.OPENED, false);
            return session;
        }

        public async Task<Session> GetSessionByIdAsync(long id, bool includeUser = false)
        {
            var session = await _sessionRepository.GetSessionByIdAsync(id, includeUser);
            if (session == null)
                return session;

            return session;
        }

        public async Task<Session> FindSessionAsync(long userId, IPAddress IpAddress,
            bool includeUser = false)
        {
            var session = await _sessionRepository.FindSessionAsync(userId, IpAddress, false, includeUser);
            if (session == null)
                return session;

            return session;
        }

        public async Task<Session> CreateSessionAsync(long userId, IPAddress ipAddress)
        {
            var session = await _sessionRepository.CreateSessionAsync(userId, ipAddress, SessionState.OPENED);
            return session;
        }

        public async Task<Session> CreateOrUpdateSessionAsync(long userId, IPAddress ipAddress)
        {
            var session = await FindSessionAsync(userId, ipAddress, true);
            if (session == null)
            {
                session = await CreateSessionAsync(userId, ipAddress);
            }
            else
            {
                if (session.State == SessionState.CLOSED)
                    await ApplySessionChangesAsync(session, state: SessionState.OPENED);
                else
                {
                    ;// TODO: Update last time of session
                }
            }
            return session;
        }

        public async Task<Session> PatchSessionByIdAsync(long id, long? userId = null, 
            IPAddress ipAddress = null, SessionState? state = null)
        {
            var session = await _sessionRepository.GetSessionByIdAsync(id, true);
            if (session == null)
                return session;

            session = await ApplySessionChangesAsync(session, userId, ipAddress, state);
            session = await GetSessionByIdAsync(id, true);
            return session;
        }

        public async Task DeleteSessionAsync(long id)
        {
            await _sessionRepository.DeleteSessionAsync(id);
        }

        public async Task<bool> DoesSessionIdExistAsync(long id)
        {
            var sessionIdExists = await _sessionRepository.DoesSessionIdExistAsync(id);
            return sessionIdExists;
        }

        public async Task<Session> ApplySessionChangesAsync(Session session, long? userId = null, 
            IPAddress ipAddress = null, SessionState? state = null)
        {
            if (userId != null)
            {
                session.UserId = userId.Value;
            }

            if (ipAddress != null)
            {
                session.IpAddress = ipAddress;
            }

            if (state != null)
            {
                session.State = state.Value;
            }

            session = await _sessionRepository.UpdateSessionAsync(session);
            return session;
        }
    }
}