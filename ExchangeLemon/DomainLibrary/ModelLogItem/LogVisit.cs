using System;

namespace BlueLight.Main
{
    public class LogVisit
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public DateTimeOffset CreatedDate { get; set; } = DateTime.Now;

    }
}