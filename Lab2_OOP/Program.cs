using System;
using System.Net;
using System.Numerics;

namespace Lab2_OOP
{
    public class Program
    {
        static async Task<HttpContent?> makeRequest(string uri)
        {
            var httpClient = new HttpClient();
            // Выполняем HTTP-запрос и получаем ответ
            HttpResponseMessage response = await httpClient.GetAsync(uri);

            // Проверяем успешность ответа
            if (response.IsSuccessStatusCode)
            {
                // Получаем содержимое ответа в виде строки
                await response.Content.ReadAsStringAsync();
                return response.Content;
            }
            else
            {
                Console.WriteLine($"Request failed with status code: {response.StatusCode}");
                return null;
            }
        }

        static void Main(string[] args)
        {
            List<Task<string>> strings =
            [
                makeRequest("http://example.com/").Result?.ReadAsStringAsync(),
                makeRequest("http://ya.ru/").Result?.ReadAsStringAsync(),
                makeRequest("https://google.com/").Result?.ReadAsStringAsync(),
            ];
            Task.WaitAll(strings.ToArray());

            foreach (var task in strings)
            {
                Console.WriteLine(task.Result);
            }
        }
    }
}
