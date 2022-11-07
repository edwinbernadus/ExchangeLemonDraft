using System;

public class LogReceiveAddressDetail
{
    public int Id { get; set; }
    public virtual LogReceiveAddress LogReceiveAddress { get; set; }
    public string Address { get; set; }
    public DateTimeOffset CreatedDate { get; set; } = DateTime.Now;
}


