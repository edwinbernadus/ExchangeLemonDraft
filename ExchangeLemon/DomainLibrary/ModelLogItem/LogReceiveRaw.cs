using System;

public class LogReceiveRaw
{
    public int Id { get; set; }
    public Guid SessionId { get; set; }
    public string EventType { get; set; }
    public string Content { get; set; }
    public DateTimeOffset CreatedDate { get; set; } = DateTime.Now;
}


