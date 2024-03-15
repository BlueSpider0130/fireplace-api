﻿using FireplaceApi.Application.IntegrationTests.Extensions;
using FireplaceApi.Domain.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace FireplaceApi.Application.IntegrationTests.Models
{
    public class TestUser : User
    {
        public HttpClient HttpClient { get; set; }

        public TestUser(User user, HttpClient httpClient) : base(user.Id, user.Username, user.State, user.CreationDate,
                user.DisplayName, user.About, user.AvatarUrl, user.BannerUrl, user.ModifiedDate,
                user.Password, user.ResetPasswordCode, user.Email, user.GoogleUser, user.AccessTokens, user.Sessions)
        {
            HttpClient = httpClient;
        }

        public async Task<HttpResponseMessage> SendRequestAsync(HttpRequestMessage request)
        {
            var response = await HttpClient.SendAsync(request);
            HttpClient.AddOrUpdateCsrfToken(response);
            return response;
        }
    }
}