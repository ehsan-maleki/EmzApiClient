using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace EMZAM.DevTools.EmzApiClient
{
    public class HttpRequestBuilder
    {
        private HttpMethod method;
        private string requestUri = "";
        private HttpContent content;
        private string bearerToken = "";
        private string acceptHeader = "application/json";
        private TimeSpan timeout = new TimeSpan(0, 0, 15);
        private bool allowAutoRedirect;

        public HttpRequestBuilder AddMethod(HttpMethod method)
        {
            this.method = method;
            return this;
        }

        public HttpRequestBuilder AddRequestUri(string requestUri)
        {
            this.requestUri = requestUri;
            return this;
        }

        public HttpRequestBuilder AddContent(HttpContent content)
        {
            this.content = content;
            return this;
        }

        public HttpRequestBuilder AddBearerToken(string bearerToken)
        {
            this.bearerToken = bearerToken;
            return this;
        }

        public HttpRequestBuilder AddAcceptHeader(string acceptHeader)
        {
            this.acceptHeader = acceptHeader;
            return this;
        }

        public HttpRequestBuilder AddTimeout(TimeSpan timeout)
        {
            this.timeout = timeout;
            return this;
        }

        public HttpRequestBuilder AddAllowAutoRedirect(bool allowAutoRedirect)
        {
            this.allowAutoRedirect = allowAutoRedirect;
            return this;
        }

        public async Task<HttpResponseMessage> SendAsync()
        {
            // Check required arguments
            EnsureArguments();

            // Set up request
            var request = new HttpRequestMessage
            {
                Method = method,
                RequestUri = new Uri(requestUri)
            };

            if (content != null)
                request.Content = content;

            if (!string.IsNullOrEmpty(bearerToken))
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

            request.Headers.Accept.Clear();
            if (!string.IsNullOrEmpty(acceptHeader))
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(acceptHeader));

            request.Headers.Add("User-Agent",
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.84 Safari/537.36");

            // Setup client
            using (var httpClientHandler = new HttpClientHandler {AllowAutoRedirect = allowAutoRedirect})
            {
                //httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
                using (var client = new HttpClient(httpClientHandler) {Timeout = timeout})
                {
                    return await client.SendAsync(request);
                }
            }
        }

        #region " Private "

        private void EnsureArguments()
        {
            if (method == null)
                throw new ArgumentNullException("Method");

            if (string.IsNullOrEmpty(requestUri))
                throw new ArgumentNullException("Request Uri");
        }

        #endregion
    }
}