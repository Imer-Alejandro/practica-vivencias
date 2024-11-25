using System;

public class Experience
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string? ImagePath { get; set; }

    // Foreign Key
    public int UserId { get; set; }
    public User? User { get; set; }
}
