using System;

namespace BlueLight.Main
{
    public class OrderSession
    {
        public long Id { get; set; }
        public string SessionId { get; set; } = Guid.NewGuid().ToString();
        public DateTimeOffset CreatedDaDateTime { get; set; } = DateTime.Now;
        public bool IsClosed { get; set; }
    }

}