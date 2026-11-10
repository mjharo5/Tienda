using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tienda.Models
{
    public class Producto
    {
        public int Id { get; set; }

        [Display(Name = "Nombre del producto")]
        [Required(ErrorMessage = "El nombre del producto es obligatorio.")]
        [StringLength(100, MinimumLength = 3,
            ErrorMessage = "El nombre debe tener entre {2} y {1} caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        [Display(Name = "Precio")]
        [Required(ErrorMessage = "El precio es obligatorio.")]
        [Range(0.01, 100000, ErrorMessage = "El precio debe estar entre {1} y {2}.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Precio { get; set; }

        [Display(Name = "Stock disponible")]
        [Required(ErrorMessage = "El stock es obligatorio.")]
        [Range(0, 10000, ErrorMessage = "El stock debe estar entre {1} y {2} unidades.")]
        public int Stock { get; set; }
    }
}