using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StackoverflowBot.Models.Bing
{
    [JsonObject]
    public class WebPageValue
    {
        public string id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Snippet { get; set; }
    }
}