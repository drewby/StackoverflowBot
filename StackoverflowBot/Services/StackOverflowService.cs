using Newtonsoft.Json;
using StackoverflowBot.Models;
using StackoverflowBot.Models.StackOverflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace StackoverflowBot.Services
{
    public class StackOverflowService
    {
        public async Task<SearchResult> ExecuteSearch(string searchTerm)
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };

            var searchString = HttpUtility.UrlEncode(searchTerm);

            var client = new HttpClient(handler);
            var httpResult = await client.GetAsync("https://api.stackexchange.com/2.2/search?order=desc&sort=activity&intitle=" + searchString + "&site=stackoverflow");
            var httpContent = await httpResult.Content.ReadAsStringAsync();
            var searchResult = JsonConvert.DeserializeObject<SearchResult>(httpContent);

            return searchResult;
        }
    }
}