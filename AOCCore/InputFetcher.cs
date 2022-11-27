using System;
using System.Collections.Generic;
using System.Linq;
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
                return;
            }
            else
            {
                Console.WriteLine(folder + " does not  exists");
                Directory.CreateDirectory(folder);
            }
            string cookie = "session=53616c7465645f5f6a7421e82bcec60d223e79afe7deea2d7cb51fdb2c2fa7c284ab377007c14de0a333c5165a987b915a72b781e4092995f0ad6a3997ced285";
            //Download input file from advent of code with cookie session 
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, string.Format(@"https://adventofcode.com/{0}/day/{1}/input", year, day));
            request.Headers.Add("Cookie", cookie);
            var result = await client.SendAsync(request);
            Console.WriteLine(result.StatusCode);
            var nogEenKeerResult = await result.Content.ReadAsStringAsync();
            Console.WriteLine(nogEenKeerResult);
            File.WriteAllText(folder + "input", nogEenKeerResult);
            File.WriteAllText(folder + "test.txt", nogEenKeerResult);
        }
    }
}
