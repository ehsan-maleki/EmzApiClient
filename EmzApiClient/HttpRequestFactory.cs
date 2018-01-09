﻿using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace EMZAM.DevTools.EmzApiClient
{
    public static class HttpRequestFactory
    {
        public static readonly TimeSpan RequestTimeout = new TimeSpan(0, 5, 0);

        public static async Task<HttpResponseMessage> Get(string requestUri)
            => await Get(requestUri, "");

        public static async Task<HttpResponseMessage> Get(string requestUri, string bearerToken)
        {
            var builder = new HttpRequestBuilder(RequestTimeout)
                .AddMethod(HttpMethod.Get)
                .AddRequestUri(requestUri)
                .AddBearerToken(bearerToken);

            return await builder.SendAsync();
        }

        public static async Task<HttpResponseMessage> Post(string requestUri, object value)
            => await Post(requestUri, value, "");

        public static async Task<HttpResponseMessage> Post(
            string requestUri, object value, string bearerToken)
        {
            var builder = new HttpRequestBuilder(RequestTimeout)
                .AddMethod(HttpMethod.Post)
                .AddRequestUri(requestUri)
                .AddContent(new JsonContent(value))
                .AddBearerToken(bearerToken);

            return await builder.SendAsync();
        }

        public static async Task<HttpResponseMessage> Put(string requestUri, object value)
            => await Put(requestUri, value, "");

        public static async Task<HttpResponseMessage> Put(
            string requestUri, object value, string bearerToken)
        {
            var builder = new HttpRequestBuilder(RequestTimeout)
                .AddMethod(HttpMethod.Put)
                .AddRequestUri(requestUri)
                .AddContent(new JsonContent(value))
                .AddBearerToken(bearerToken);

            return await builder.SendAsync();
        }

        public static async Task<HttpResponseMessage> Patch(string requestUri, object value)
            => await Patch(requestUri, value, "");

        public static async Task<HttpResponseMessage> Patch(
            string requestUri, object value, string bearerToken)
        {
            var builder = new HttpRequestBuilder(RequestTimeout)
                .AddMethod(new HttpMethod("PATCH"))
                .AddRequestUri(requestUri)
                .AddContent(new PatchContent(value))
                .AddBearerToken(bearerToken);

            return await builder.SendAsync();
        }

        public static async Task<HttpResponseMessage> Delete(string requestUri)
            => await Delete(requestUri, "");

        public static async Task<HttpResponseMessage> Delete(
            string requestUri, string bearerToken)
        {
            var builder = new HttpRequestBuilder(RequestTimeout)
                .AddMethod(HttpMethod.Delete)
                .AddRequestUri(requestUri)
                .AddBearerToken(bearerToken);

            return await builder.SendAsync();
        }

        public static async Task<HttpResponseMessage> PostFile(string requestUri,
            string filePath, string apiParamName)
            => await PostFile(requestUri, filePath, apiParamName, "");

        public static async Task<HttpResponseMessage> PostFile(string requestUri,
            string filePath, string apiParamName, string bearerToken)
        {
            var builder = new HttpRequestBuilder(RequestTimeout)
                .AddMethod(HttpMethod.Post)
                .AddRequestUri(requestUri)
                .AddContent(new FileContent(filePath, apiParamName))
                .AddBearerToken(bearerToken);

            return await builder.SendAsync();
        }
    }
}