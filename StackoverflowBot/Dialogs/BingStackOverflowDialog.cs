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
using StackoverflowBot.Services;

namespace StackoverflowBot.Dialogs
{
    [Serializable]
    public class BingStackOverflowDialog : IDialog<object>
    {
        
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }

        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<Message> argument)
        {
            var message = await argument;
            await context.PostAsync("I am looking for an answer to: " + message.Text);

            var service = new BingStackOverflowService();
            var searchResult = await service.ExecuteSearch(message.Text);

            if (searchResult != null && searchResult.Count>0)
            {
                await context.PostAsync("I found " + searchResult.Count + " answers. Here is the first: ");
                await context.PostAsync(searchResult[0].Description);
                await context.PostAsync("More info: " + searchResult[0].Url);
            }
            else
            {
                await context.PostAsync("Sorry, but I could not find an answer. Can you ask in a different way?");
            }

            context.Wait(MessageReceivedAsync);
        }


    }
}