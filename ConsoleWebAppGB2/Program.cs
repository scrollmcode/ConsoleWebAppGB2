using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;

namespace ConsoleWebAppGB2
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        private static readonly string URL = "https://jsonplaceholder.typicode.com/posts/";
        private static readonly string fileName = @".\result.txt";

        static async Task Main(string[] args)
        {
            Console.WriteLine("Начало");

            FileInfo fileData = new FileInfo(fileName);

            using (StreamWriter sw = fileData.CreateText())
            {
                for (int i = 4; i <= 13; i++)
                {
                    var response = await client.GetAsync(URL + i);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        DataModel jsonContent =
                            JsonSerializer.Deserialize<DataModel>(content);
                        sw.WriteLine(jsonContent.userId);
                        sw.WriteLine(jsonContent.id);
                        sw.WriteLine(jsonContent.title);
                        sw.WriteLine(jsonContent.body);
                        sw.WriteLine();
                    }
                }
            }

            Console.WriteLine("Завершено");
            Console.ReadLine();
        }
    }

    class DataModel
    {
        public int userId { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public string body { get; set; }
    }
}
