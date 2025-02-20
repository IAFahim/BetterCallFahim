using System.ComponentModel.DataAnnotations;

namespace Web.App.Entities;

public class Car
{
    [Key]
    public Guid Id { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
}