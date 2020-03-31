using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Services
{
    public static class Gravatar
    {
        private static HttpClient httpClient;

        static Gravatar()
        {
            httpClient = new HttpClient();
            httpClient.Timeout = TimeSpan.FromSeconds(3);
        }

        public static string GetHash(string emailAddress)
        {
            using (var md5 = MD5.Create())
            {
                var inputBytes = Encoding.Default.GetBytes(emailAddress.Trim().ToLowerInvariant());
                var hashBytes = md5.ComputeHash(inputBytes);

                var builder = new StringBuilder();
                foreach (var b in hashBytes)
                {
                    builder.Append(b.ToString("X2"));
                }
                return builder.ToString().ToLowerInvariant();
            }
        }

        public static async Task<GravatarProfile> GetUserProfile(string emailAddress)
        {
            var hash = GetHash(emailAddress);
            var profileUrl = $"https://www.gravatar.com/{hash}.json";
            GravatarProfileResponse response = null;

            try
            {
                var requestMessage = new HttpRequestMessage(HttpMethod.Get, profileUrl);
                requestMessage.Headers.UserAgent.Add(new ProductInfoHeaderValue("ToDoListApp", "1.0"));
                var responseMessage = await httpClient.SendAsync(requestMessage);
                response = JsonConvert.DeserializeObject<GravatarProfileResponse>(await responseMessage.Content.ReadAsStringAsync());
            }
            catch (TaskCanceledException) { }
            catch (OperationCanceledException) { }
            catch (JsonSerializationException) { }
            catch (Exception)
            {
                throw;
            }
                        
            if (response?.Entry?.Count > 0)
            {
                return response.Entry[0];
            }
            else
            {
                return null;
            }
        }

        public class GravatarProfileResponse
        {
            public List<GravatarProfile> Entry { get; set; }
        }

        public class GravatarProfile
        {
            public string DisplayName { get; set; }
            public string PreferredUsername { get; set; }
            public string Id { get; set; }
        }
    }
}