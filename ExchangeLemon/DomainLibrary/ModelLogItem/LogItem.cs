using Newtonsoft.Json;
using System;

namespace BlueLight.Main
{



    public class LogItem
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string AddtionalContent { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string ModuleName { get; set; }
        public string ClassName { get; set; }
        public string CallerName { get; set; }
        public string SessionId { get; set; }
        public long Duration { get; set; }

        public DateTime? StartBatchDate { get; set; }
    }
}