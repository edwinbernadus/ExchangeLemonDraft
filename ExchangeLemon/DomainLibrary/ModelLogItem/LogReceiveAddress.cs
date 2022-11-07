using System;
using System.Collections.Generic;

public class LogReceiveAddress
{
    public int Id { get; set; }
    public Guid SessionId { get; set; }
    public string EventType { get; set; }
    public virtual ICollection<LogReceiveAddressDetail> Details { get; set; }
    public DateTimeOffset CreatedDate { get; set; } = DateTime.Now;
}


