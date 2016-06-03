using Newtonsoft.Json;
using StackoverflowBot.Models;
using StackoverflowBot.Models.Bing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace StackoverflowBot.Services
{
    /// <summary>
    /// Service to query Stack Overflow through the Bing Search cognitive service API.
    /// </summary>
    public class BingStackOverflowService
    {
        public string ApiKey
        {
            get
            {
                return "428cbed90a2244568c7af63264120de0";
            }
        }

        private string CreateWebRequestUrl(string searchTerm)
        {
            var url = new StringBuilder();
            url.Append("https://bingapis.azure-api.net/api/v5/search");
            url.AppendFormat("?q=site:stackoverflow.com+{0}", HttpUtility.UrlEncode(searchTerm));
            url.Append("&mkt=en-us");
            return url.ToString();
        }

        public async Task<List<AnswerResult>> ExecuteSearch(string searchTerm)
        {
            var result = new List<AnswerResult>();

            HttpClientHandler handler = new HttpClientHandler();
          
            using (var client = new HttpClient())
            {
                var url = CreateWebRequestUrl(searchTerm);
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ApiKey);
                var httpResult = await client.GetAsync(url);
                var httpContent = await httpResult.Content.ReadAsStringAsync();
                var searchResult = JsonConvert.DeserializeObject<SearchResponse>(httpContent);

                if (searchResult.WebPages.Value.Length > 0)
                {
                    foreach(var page in searchResult.WebPages.Value)
                    {
                        result.Add(new AnswerResult() { Description = page.Name, Url = page.Url });
                    }
                }
            } 

            return result;
        }
    }
}