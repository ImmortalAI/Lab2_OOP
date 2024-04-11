using Microsoft.VisualBasic;
using System;
using System.Net;
using System.Numerics;
using System.Text;
using System.Text.Json;

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

        static string makeSyncRequest(string uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var response = new HttpClient().Send(request);
            response.EnsureSuccessStatusCode();
            var stream = response.Content.ReadAsStream();
            StreamReader streamReader = new StreamReader(stream);
            return streamReader.ReadToEnd();
        }

        static void Main(string[] args)
        {
            /*List<Task<string>> strings =
            [
                makeRequest("http://example.com/").Result?.ReadAsStringAsync(),
                makeRequest("http://ya.ru/").Result?.ReadAsStringAsync(),
                makeRequest("https://google.com/").Result?.ReadAsStringAsync(),
            ];
            Task.WaitAll(strings.ToArray());

            foreach (var task in strings)
            {
                Console.WriteLine(task.Result);
            }*/

            var task = makeRequest("http://example.com/").Result?.ReadAsStringAsync();
            task.Wait();
            Console.WriteLine(task.Result);
            task = makeRequest("http://ya.ru/").Result?.ReadAsStringAsync();
            task.Wait();
            Console.WriteLine(task.Result);
            task = makeRequest("https://google.com/").Result?.ReadAsStringAsync();
            task.Wait();
            Console.WriteLine(task.Result);

            /*Console.WriteLine(makeSyncRequest("https://google.com/"));*/
        }
    }
}
