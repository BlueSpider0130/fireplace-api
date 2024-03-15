﻿using System;
using System.Text.Json.Serialization;

namespace FireplaceApi.Core.ValueObjects
{
    public class TokenResponse
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }

        [JsonPropertyName("expires_in")]
        public long? ExpiresInSeconds { get; set; }

        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonPropertyName("scope")]
        public string Scope { get; set; }

        [JsonPropertyName("id_token")]
        public string IdToken { get; set; }

        [JsonPropertyName("Issued")]
        [JsonPropertyOrder(1)]
        public DateTime Issued
        {
            get
            {
                return IssuedUtc.ToLocalTime();
            }
            set
            {
                IssuedUtc = value.ToUniversalTime();
            }
        }

        [JsonPropertyName("IssuedUtc")]
        [JsonPropertyOrder(2)]
        public DateTime IssuedUtc { get; set; }
    }
}