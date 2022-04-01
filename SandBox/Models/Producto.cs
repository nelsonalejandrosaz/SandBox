using System.ComponentModel.DataAnnotations;

namespace SandBox.Models;

public class Producto
{
    public int Id { get; set; }

    [Required]
    public string Nombre { get; set; }

    [Required]
    public decimal PrecioUnitario { get; set; }

    [Required]
    public int Cantidad { get; set; }

}