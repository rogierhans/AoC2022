using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AOCCore
{
    public static class InputFetcher
    {
        public static async Task GetFile(string year, string day)
        {
            string folder = string.Format(@"C:\Users\Rogier\Desktop\InputFiles\{0}_{1}\", year, day);
            //check if folder exists
            if (Directory.Exists(folder))
            {
                Console.WriteLine(folder + "already exists");
                var lines = File.ReadAllLines(folder + "input.txt");
                lines.ToList().Take(10).ToList().ForEach(x => x.P());
                Console.WriteLine("########################");
                return;
            }
            else
            {
                Console.WriteLine(folder + " does not  exists");
                Directory.CreateDirectory(folder);

                string cookie = File.ReadAllText(@"C:\Users\Rogier\Desktop\Cookie.txt");
                string site = string.Format(@"https://adventofcode.com/{0}/day/{1}/input", year, int.Parse(day).ToString());
                //Download input file from advent of code with cookie session 
                var client = new HttpClient();

                var request = new HttpRequestMessage(HttpMethod.Get, site);
                request.Headers.Add("Cookie", cookie);
                var productValue = new ProductInfoHeaderValue("r"+"ogie"+"rhan"+"s@gm"+"ail.com");
                var commentValue = new ProductInfoHeaderValue(@"https://github.com/rogierhans/AoC2022/blob/Master/AOCCore/InputFetcher.cs");

                request.Headers.UserAgent.Add(productValue);
                request.Headers.UserAgent.Add(commentValue);
                var result = await client.SendAsync(request);
                Console.WriteLine("code: " + result.StatusCode);
                var nogEenKeerResult = await result.Content.ReadAsStringAsync();
                Console.WriteLine(nogEenKeerResult[100]);
                File.WriteAllText(folder + "input.txt", nogEenKeerResult);
                File.WriteAllText(folder + "test.txt", nogEenKeerResult);
            }
        }
    }
}
