using Newtonsoft.Json;
using System;

namespace StackoverflowBot.Models.StackOverflow
{
    [JsonObject]
    public class Item
    {
        public string[] Tags { get; set; }
        public User Owner{ get; set; }
        public bool is_answered { get; set; }
        public int view_count { get; set; }
        public int answer_count { get; set; }
        public int score { get; set; }
        public string last_activity_date { get; set; }
        public string creation_date { get; set; }
        public int question_id { get; set; }
        public string link { get; set; }
        public string title { get; set; }
    }
}