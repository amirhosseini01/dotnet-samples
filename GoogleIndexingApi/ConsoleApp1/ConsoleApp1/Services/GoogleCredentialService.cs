namespace ConsoleApp1.Services
{
    using Google.Apis.Auth.OAuth2;
    using Microsoft.Extensions.Hosting.Internal;
    using RestSharp;
    using System.IO;
    using System.Net;
    using System.Security.Cryptography.X509Certificates;
    using System.Threading.Tasks;

    public class GoogleCredentialService
    {
        private string _jsonPrivateKey;
        public GoogleCredentialService(string jsonPrivateKey)
        {
            _jsonPrivateKey = jsonPrivateKey;
        }
        public async Task<string> GetAccessTokenWithJsonPrivateKey()
        {
            var privateKeyStream = File.OpenRead(_jsonPrivateKey);

            var serviceAccountCredential = ServiceAccountCredential.FromServiceAccountData(privateKeyStream);

            var googleCredetial = GoogleCredential.FromServiceAccountCredential(serviceAccountCredential).CreateScoped(new[] { "https://www.googleapis.com/auth/indexing" });

            var result = await googleCredetial.UnderlyingCredential.GetAccessTokenForRequestAsync("https://www.googleapis.com/auth/indexing");

            return result;
        }

        public async Task<HttpStatusCode> GetNotificationStatus()
        {
            var client = new RestClient("https://indexing.googleapis.com/v3/urlNotifications/metadata");
            var request = new RestRequest();
            var accessToken = GetAccessTokenWithJsonPrivateKey();
            request.AddHeader("Authorization", $"Bearer {accessToken}");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("scope", "https://www.googleapis.com/auth/indexing");
            var response = await client.GetAsync(request);

            return response.StatusCode;
        }
        public GoogleCredential GetGoogleCredential()
        {
            GoogleCredential credential;

            using (var stream = new FileStream(_jsonPrivateKey, FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream).CreateScoped(new[] { "https://www.googleapis.com/auth/indexing" });
            }

            return credential;
        }
    }
}
