using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StackoverflowBot.Models.StackOverflow
{
    [JsonObject]
    public class SearchResult
    {
        public Item[] Items { get; set; }
        public string has_more { get; set; }
        public int quota_max { get; set; }
        public int quota_remaining { get; set; }
    }

}