using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StackoverflowBot.Models.Bing
{
    [JsonObject]
    public class WebPages
    {
        public WebPageValue[] Value { get; set; }
    }
}