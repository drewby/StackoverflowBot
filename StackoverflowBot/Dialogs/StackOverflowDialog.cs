using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.IO;
using StackoverflowBot.Models;
using System.Web;

namespace StackoverflowBot.Dialogs
{
    [Serializable]
    public class StackOverflowDialog : IDialog<object>
    {
        
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }

        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<Message> argument)
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };

            var message = await argument;
            await context.PostAsync("I am looking for an answer to: " + message.Text);

            var searchString = HttpUtility.UrlEncode(message.Text);

            var client = new HttpClient(handler);
            var httpResult = await client.GetAsync("https://api.stackexchange.com/2.2/search?order=desc&sort=activity&intitle=" + searchString + "&site=stackoverflow");
            var httpContent = await httpResult.Content.ReadAsStringAsync();
            var searchResult = JsonConvert.DeserializeObject<SearchResult>(httpContent);

            if (searchResult.Items != null && searchResult.Items.Length>0)
            {
                await context.PostAsync("I found " + searchResult.Items.Length + " answers. Here is the first: ");
                await context.PostAsync(searchResult.Items[0].title);
                await context.PostAsync("More info: " + searchResult.Items[0].link);
            }
            else
            {
                await context.PostAsync("Sorry, but I could not find an answer. Can you ask in a different way?");
            }

            context.Wait(MessageReceivedAsync);
        }


    }
}