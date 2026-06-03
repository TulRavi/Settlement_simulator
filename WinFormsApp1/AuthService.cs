using System;
using System.Collections.Generic;
using System.Text;

namespace SettlementGame.Domain
{
    public class AuthService 
    {
        public HttpClient _client { get; private set; }

        public AuthService(HttpClient client)
        {
            _client = client;
        }

        public async Task<string> loginTask(string email, string password)
        {
            // вся логика HTTP здесь
            string json = $"{{\"email\":\"{email}\",\"password\":\"{password}\"}}";
            // формируем JSON вручную (альтернатива — JsonSerializer)
            StringContent content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json");
            // тело POST запроса

            
            HttpResponseMessage response = await _client.PostAsync("api/world/login", content);
            // отправляем POST api/world/login

            if (!response.IsSuccessStatusCode)
            {
                return null;
                //
                //todo: переписать на мэссэджбокс с ошибкой и повторить ввод
            }

            string token = await response.Content.ReadAsStringAsync();
            // читаем токен из ответа

            return token;
        }
    }
}
