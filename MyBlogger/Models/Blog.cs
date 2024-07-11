namespace MyBlogger.Models;

public class Blog
{
    public Guid BlogId { get; set; }
    public required string UserId { get; set; }
    public DateTime PostedAt { get; set; }
    public string Header { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public string? ImageFileName { get; set; } = null;
}
