using System.ComponentModel.DataAnnotations;

namespace SandBox.Models;

public class Policy
{
    public int Id { get; set; }

    [Required]
    public string Nombre { get; set; }

    public ICollection<AppRole> Roles { get; set; }

}