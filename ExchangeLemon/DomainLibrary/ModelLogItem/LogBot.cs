using System;

namespace BlueLight.Main
{



    public class LogBot
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string ClassType { get; set; }
    }
}