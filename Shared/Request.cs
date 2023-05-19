namespace Shared
{
    public static class Request
    {
        private static HttpClientHandler ByPassCertificate()
        {
            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) =>
            {
                return true;
            };

            return handler;
        }

        public static async Task<string> Get(string uri)
        {
            // Will only be used with if DEBUG normally
            HttpClientHandler handler = ByPassCertificate();

            HttpClient client = new HttpClient(handler);

            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");

            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsStringAsync();
            else
                throw new HttpRequestException($"{response.StatusCode} - {response.Content}");
        }

        public static async Task<string> Post(string uri, string json)
        {
            // Will only be used with if DEBUG normally
            HttpClientHandler handler = ByPassCertificate();

            HttpClient client = new HttpClient(handler);

            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");

            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await client.PostAsync(uri, httpContent);

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsStringAsync();
            else
                throw new HttpRequestException($"{response.StatusCode} - {response.Content}");
        }
    }
}
