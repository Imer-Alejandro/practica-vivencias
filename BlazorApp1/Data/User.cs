using System.Collections.Generic;

public class User
{
    public int Id { get; set; }
    public string? Username { get; set; } 
    public string? Email { get; set; } 
    public string? Password { get; set; } 

    // Relación 1:N con Vivencias
    public ICollection<Experience>? Experiences { get; set; } = new List<Experience>();
}
