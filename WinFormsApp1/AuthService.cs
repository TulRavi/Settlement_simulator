using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace SettlementGame.Domain
{
    public class AuthService 
    {
        public HttpClient _client { get; private set; }

        public AuthService(HttpClient client)
        {
            _client = client;
        }

        public async Task<string> LoginTask(string email, string password)
        {
            var request = new { email, password };
            string json = JsonSerializer.Serialize(request);

            // Create the HTTP request body.
            using StringContent content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json");

            // Send the Login request to the Web API.
            HttpResponseMessage response =
                await _client.PostAsync("api/world/Login", content);

            // The server returns an unsuccessful status code
            // when authentication fails.
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            // Read the JWT returned by the server.
            string token = await response.Content.ReadAsStringAsync();

            return token;
        }
    }
}
