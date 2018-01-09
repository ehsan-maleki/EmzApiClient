using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace EMZAM.DevTools.EmzApiClient
{
    public class HttpRequestBuilder
    {
        private HttpMethod _method;
        private string _requestUri = "";
        private HttpContent _content;
        private string _bearerToken = "";
        private string _acceptHeader = "application/json";
        private TimeSpan _timeout;
        private bool _allowAutoRedirect;

        public HttpRequestBuilder(TimeSpan timeout)
        {
            _timeout = timeout;
        }

        public HttpRequestBuilder AddMethod(HttpMethod method)
        {
            this._method = method;
            return this;
        }

        public HttpRequestBuilder AddRequestUri(string requestUri)
        {
            this._requestUri = requestUri;
            return this;
        }

        public HttpRequestBuilder AddContent(HttpContent content)
        {
            this._content = content;
            return this;
        }

        public HttpRequestBuilder AddBearerToken(string bearerToken)
        {
            this._bearerToken = bearerToken;
            return this;
        }

        public HttpRequestBuilder AddAcceptHeader(string acceptHeader)
        {
            this._acceptHeader = acceptHeader;
            return this;
        }

        public HttpRequestBuilder AddTimeout(TimeSpan timeout)
        {
            this._timeout = timeout;
            return this;
        }

        public HttpRequestBuilder AddAllowAutoRedirect(bool allowAutoRedirect)
        {
            this._allowAutoRedirect = allowAutoRedirect;
            return this;
        }

        public async Task<HttpResponseMessage> SendAsync()
        {
            // Check required arguments
            EnsureArguments();

            // Set up request
            var request = new HttpRequestMessage
            {
                Method = _method,
                RequestUri = new Uri(_requestUri)
            };

            if (_content != null)
                request.Content = _content;

            if (!string.IsNullOrEmpty(_bearerToken))
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _bearerToken);

            request.Headers.Accept.Clear();
            if (!string.IsNullOrEmpty(_acceptHeader))
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(_acceptHeader));

            request.Headers.Add("User-Agent",
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.84 Safari/537.36");

            // Setup client
            using (var httpClientHandler = new HttpClientHandler {AllowAutoRedirect = _allowAutoRedirect})
                using (var client = new HttpClient(httpClientHandler) {Timeout = _timeout})
                    return await client.SendAsync(request);
        }

        #region " Private "

        private void EnsureArguments()
        {
            if (_method == null)
                throw new ArgumentNullException("Method");

            if (string.IsNullOrEmpty(_requestUri))
                throw new ArgumentNullException("Request Uri");
        }

        #endregion
    }
}