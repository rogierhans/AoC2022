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
                var lines = File.ReadAllLines(folder + "input.txt");
                lines.ToList().Take(10).ToList().ForEach(x => x.P());
                Console.WriteLine("########################");
                return;
            }
            else
            {
                Console.WriteLine(folder + " does not  exists");
                Directory.CreateDirectory(folder);

                string cookie = "session=53616c7465645f5f38e4710264fb0b68580b6722b42d44b4e96d5b80e782ad1e66217a013b2e454714918eb2e081863a3c0c053547b3e620a8681dc025529f9f";
                string site = string.Format(@"https://adventofcode.com/{0}/day/{1}/input", year, int.Parse(day).ToString());
                //Download input file from advent of code with cookie session 
                var client = new HttpClient();

                var request = new HttpRequestMessage(HttpMethod.Get, site);
                request.Headers.Add("Cookie", cookie);
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
