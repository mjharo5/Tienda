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

        [Required(ErrorMessage = "La descripción de la categoría es obligatoria.")]
        [StringLength(200, ErrorMessage = "La descripción no puede exceder los 200 caracteres.")]
        [Display(Name = "Descripción")]
        public string? Descripcion { get; set; }

        // Propiedad de navegación (Una categoría puede tener muchos productos)
        public ICollection<Producto>? Productos { get; set; }
    }
}