using System;

namespace BlueLight.Main
{
    public class Dashboard
    {
        public int Id { get; set; }
        public string TypeEvent { get; set; }
        public int Counter { get; set; }
        //public DateTime LastUpdate { get; set; } = DateTime.Now;
        public DateTimeOffset LastUpdate { get; set; } = DateTime.Now;
    }
}