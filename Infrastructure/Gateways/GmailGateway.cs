﻿using FireplaceApi.Core.Extensions;
using FireplaceApi.Core.Interfaces;
using FireplaceApi.Core.ValueObjects;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using static Google.Apis.Gmail.v1.GmailService;

namespace FireplaceApi.Infrastructure.Gateways
{
    public class GmailGateway : IEmailGateway
    {
        private readonly ILogger<GmailGateway> _logger;
        private GmailService _gmailService;
        private const string _projectName = "fireplace-api";
        private const string _credentialDirectory = "Secrets";

        public GmailGateway(ILogger<GmailGateway> logger)
        {
            _logger = logger;
            InitializeGmailService();
        }

        public void InitializeGmailService()
        {
            var clientSecrets = new ClientSecrets
            {
                ClientId = Configs.Instance.Google.ClientId,
                ClientSecret = Configs.Instance.Google.ClientSecret,
            };

            var scopes = new string[] { ScopeConstants.GmailSend };
            var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                        clientSecrets,
                        scopes,
                        _projectName,
                        CancellationToken.None,
                        new FileDataStore(_credentialDirectory, true)).Result;

            _gmailService = new GmailService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = _projectName,
            });
        }

        public async Task SendEmailMessageAsync(string toEmailAddress,
            string subject, string body)
        {
            var sw = Stopwatch.StartNew();
            string message = $"To: {toEmailAddress}\r\nSubject: {subject}\r\nContent-Type: text/html;charset=utf-8\r\n\r\n{body}";
            var newMsg = new Message
            {
                Raw = message.ToBase64UrlEncode(),
            };

            Message response = await _gmailService.Users.Messages.Send(newMsg, "me").ExecuteAsync();

            if (response != null)
            {
                string serverLog = $"Email has been sent to {toEmailAddress}! body: {body[..10]}...";
                _logger.LogAppInformation(serverLog, sw);
            }
            else
            {
                string serverLog = $"Can't send email from to {toEmailAddress}! body: {body[..10]}...";
                _logger.LogAppError(serverLog, sw);
            }
        }
    }
}