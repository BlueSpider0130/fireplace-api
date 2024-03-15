﻿using GamingCommunityApi.Core.Tools;
using GamingCommunityApi.Core.Tools.NewtonsoftSerializer;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GamingCommunityApi.Core.Extensions
{
    public static class StringExtensions
    {
        private static readonly JsonSerializerSettings _jsonSerializerSettings;

        static StringExtensions()
        {
            _jsonSerializerSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ContractResolver = CoreContractResolver.Instance,
                //NullValueHandling = NullValueHandling.Ignore,
            };
        }

        public static T FromJson<T>(this string json)
        {
            if (json == null)
                return default;
            return JsonConvert.DeserializeObject<T>(json, _jsonSerializerSettings);
        }

        public static bool IsJson(this string strInput)
        {
            strInput = strInput.Trim();
            if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || //For object
                (strInput.StartsWith("[") && strInput.EndsWith("]"))) //For array
            {
                try
                {
                    var obj = System.Text.Json.JsonDocument.Parse(strInput);
                    return true;
                }
                catch (Exception) //some other exception
                {
                    //Console.WriteLine(ex.ToString());
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static string FirstCharToUpper(this string str) =>
            str switch
            {
                null => throw new ArgumentNullException(nameof(str)),
                "" => throw new ArgumentException($"{nameof(str)} cannot be empty", nameof(str)),
                _ => str.First().ToString().ToUpper() + str.Substring(1)
            };

        public static string Shuffle(this string str)
        {
            char[] array = str.ToCharArray();
            Random rng = new Random();
            int n = array.Length;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                var value = array[k];
                array[k] = array[n];
                array[n] = value;
            }
            return new string(array);
        }

        public static T ToEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static IPAddress ToIPAddress(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;
            return IPAddress.Parse(value);
        }

        public static bool IsMobileNumber(this string value)
        {
            bool isMobileNumber;
            var match = Regexes.MobileNumber.Match(value);
            isMobileNumber = match.Success;
            return isMobileNumber;
        }

        public static bool IsEmailAddress(this string value)
        {
            bool isEmailAddress;
            try
            {
                MailAddress address = new MailAddress(value);
                isEmailAddress = (address.Address == value);
            }
            catch (FormatException)
            {
                isEmailAddress = false;
            }
            return isEmailAddress;
        }

        public static bool IsUsername(this string value)
        {
            bool isUsername;
            var match = Regexes.Username.Match(value);
            isUsername = match.Success;
            return isUsername;
        }

        public static string RemoveLineBreaks(this string str)
        {
            string result;
            result = str.Replace("\r\n", "").Replace("\n", "").Replace("\r", "");
            return Regex.Replace(result, @"\s+", " ");
        }

    }
}
