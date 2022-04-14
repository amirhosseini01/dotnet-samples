namespace ConsoleApp1.Services
{
    using System.Threading.Tasks;
    using System.Web;
    using Google.Apis.Auth.OAuth2;
    using Google.Apis.Indexing.v3;
    using Google.Apis.Indexing.v3.Data;
    using Google.Apis.Services;

    public class GoogleSingleIndexingService
    {
        private GoogleCredentialService _googleCredentialService;
        private GoogleCredential _googleCredential;

        public GoogleSingleIndexingService(string jsonPrivateKey)
        {
            _googleCredentialService = new GoogleCredentialService(jsonPrivateKey);
            _googleCredential = _googleCredentialService.GetGoogleCredential();
        }

        public async Task<PublishUrlNotificationResponse> AddOrUpdateGoogleIndex(string jobUrl)
        {
            return await AddUpdateIndex(jobUrl, "URL_UPDATED");
        }

        public async Task<PublishUrlNotificationResponse> RemoveGoogleIndex(string jobUrl)
        {
            return await AddUpdateIndex(jobUrl, "URL_DELETED");
        }

        public async Task<UrlNotificationMetadata> GetGoogleIndexStatus(string jobUrl)
        {
            return await GetIndexStatus(jobUrl);
        }

        private Task<PublishUrlNotificationResponse> AddUpdateIndex(string jobUrl, string action)
        {
            var credential = _googleCredential.UnderlyingCredential;

            var googleIndexingApiClientService = new IndexingService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential
            });

            var requestBody = new UrlNotification
            {
                Url = jobUrl,
                Type = action
            };

            var publishRequest = new UrlNotificationsResource.PublishRequest(googleIndexingApiClientService, requestBody);

            return publishRequest.ExecuteAsync();
        }

        private Task<UrlNotificationMetadata> GetIndexStatus(string jobUrl)
        {
            var credential = _googleCredential.UnderlyingCredential;

            var googleIndexingApiClientService = new IndexingService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential
            });

            var metaDataRequest = new GetMetadataRequest(googleIndexingApiClientService, jobUrl);

            return metaDataRequest.ExecuteAsync();
        }
    }
}
