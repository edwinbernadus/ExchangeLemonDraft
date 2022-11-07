using System;

namespace BlueLight.Main
{
    public class AccountHistory
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public double RunningBalance { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public long Version { get; set; }
    }
}