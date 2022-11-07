using System;

public class LogHttpRaw
{
    public int Id { get; set; }

    public string SessionId { get; set; }
    public bool IsRequest { get; set; }
    public string Content { get; set; }


    public DateTimeOffset CreatedDate { get; set; } = DateTime.Now;


    public string Path { get; set; }
    public int StatusCode { get; set; }
    public string Method { get; set; }
    public string UserName { get; set; }
}


