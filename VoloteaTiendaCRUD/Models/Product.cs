using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VoloteaTiendaCRUD.Models
{
    /// <summary>
    /// Represents a product entity.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Gets or sets the unique identifier of the product.
        /// </summary>

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the size of the product.
        /// </summary>

        [Required(ErrorMessage = "La talla del producto es obligatoria.")]
        [EnumDataType(typeof(Size), ErrorMessage = "La talla proporcionada no es válida.")]
        public Size Size { get; set; }

        /// <summary>
        /// Gets or sets the color of the product.
        /// </summary>

        [Required(ErrorMessage = "El color del producto es obligatorio.")]
        [EnumDataType(typeof(Color), ErrorMessage = "El color proporcionado no es válido.")]
        public Color Color { get; set; }

        /// <summary>
        /// Gets or sets the price of the product.
        /// </summary>

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "El precio debe ser igual o mayor a 0.")]
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the description of the product.
        /// </summary>

        [MaxLength(1000, ErrorMessage = "La descripción no debe superar los 1000 caracteres.")]
        public string? Description { get; set; }
    }

    /// <summary>
    /// Enumeration representing the available sizes for a product.
    /// </summary>
    public enum Size
    {
        XS = 0,
        S = 1,
        M = 2,
        L = 3,
        XL = 4,
        XXL = 5
    }

    /// <summary>
    /// Enumeration representing the available colors for a product.
    /// </summary>
    public enum Color
    {
        Red = 0,
        Green = 1,
        Blue = 2,
        Black = 3,
        White = 4,
        Yellow = 5,
        Pink = 6,
        Orange = 7,
        Purple = 8,
        Grey = 9
    }
}
