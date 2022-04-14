namespace GoogleIndexingAPIMVC.Services
{
    using Google.Apis.Auth.OAuth2;
    using RestSharp;
    using System.IO;
    using System.Net;
    using System.Security.Cryptography.X509Certificates;
    using System.Threading.Tasks;

    public class GoogleCredentialService
    {
        string _filename = "indexing-api-338810-0abaac5b7a23.json";
        public async Task<string> GetAccessTokenWithJsonPrivateKey()
        {
            var privateKeyStream = File.OpenRead(System.IO.Directory.GetCurrentDirectory() + @"\PrivateKey\"+ _filename);

            var serviceAccountCredential = ServiceAccountCredential.FromServiceAccountData(privateKeyStream);

            var googleCredetial = GoogleCredential.FromServiceAccountCredential(serviceAccountCredential).CreateScoped(new[] { "https://www.googleapis.com/auth/indexing" });

            var result = await googleCredetial.UnderlyingCredential.GetAccessTokenForRequestAsync("https://www.googleapis.com/auth/indexing");

            return result;
        }

        public async Task<string> GetAccessTokenWithP12PrivateKey()
        {
            var serviceAccountEmail = "your@account.iam.gserviceaccount.com";

            var certificate = new X509Certificate2(@"C:\.p12",
                "notasecret", X509KeyStorageFlags.Exportable);

            var serviceAccountCredential = new ServiceAccountCredential(
                new ServiceAccountCredential.Initializer(serviceAccountEmail)
                    .FromCertificate(certificate));

            var googleCredetial = GoogleCredential.FromServiceAccountCredential(serviceAccountCredential).CreateScoped(new[] { "https://www.googleapis.com/auth/indexing" });

            var result = await googleCredetial.UnderlyingCredential.GetAccessTokenForRequestAsync("https://www.googleapis.com/auth/indexing");

            return result;
        }

        //public async Task<HttpStatusCode> GetNotificationStatus()
        //{
        //    var client = new RestClient("https://indexing.googleapis.com/v3/urlNotifications/metadata");
        //    var request = new RestRequest(Method.GET);
        //    var accessToken = GetAccessTokenWithJsonPrivateKey();
        //    request.AddHeader("Authorization", $"Bearer {accessToken}");
        //    request.AddHeader("Content-Type", "application/json");
        //    request.AddParameter("scope", "https://www.googleapis.com/auth/indexing");
        //    IRestResponse response = client.Execute(request);

        //    return await Task.FromResult(response.StatusCode);
        //}

        public GoogleCredential GetGoogleCredential()
        {
            var path = System.IO.Directory.GetCurrentDirectory() + @"\PrivateKey\"+ _filename;

            GoogleCredential credential;

            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream).CreateScoped(new[] { "https://www.googleapis.com/auth/indexing" });
            }

            return credential;
        }
    }
}
