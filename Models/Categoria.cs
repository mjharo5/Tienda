using System.ComponentModel.DataAnnotations;

namespace Tienda.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la categoría es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres.")]
        [Display(Name = "Nombre de la Categoría")]
        public string Nombre { get; set; } = string.Empty;

        // Propiedad de navegación (Una categoría puede tener muchos productos)
        public ICollection<Producto>? Productos { get; set; }
    }
}