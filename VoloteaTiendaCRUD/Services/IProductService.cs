using VoloteaTiendaCRUD.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VoloteaTiendaAPI.Services
{
    /// <summary>
    /// Interface defining the available operations for product management.
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Retrieves all products stored in the database.
        /// </summary>
        /// <returns>A list of products.</returns>
        Task<List<Product>> GetAllProductsAsync();

        /// <summary>
        /// Retrieves a specific product from the database based on its unique identifier.
        /// </summary>
        /// <param name="id">Unique identifier of the product.</param>
        /// <returns>The product corresponding to the specified identifier.</returns>
        Task<Product> GetProductByIdAsync(int id);

        /// <summary>
        /// Adds a new product to the database.
        /// </summary>
        /// <param name="product">Product to be added.</param>
        /// <returns>The added product.</returns>
        Task<Product> AddProductAsync(Product product);

        /// <summary>
        /// Updates the information of an existing product in the database.
        /// </summary>
        /// <param name="product">Product with the updated information.</param>
        /// <returns>The updated product.</returns>
        Task<Product> UpdateProductAsync(Product product);

        /// <summary>
        /// Deletes a product from the database based on its unique identifier.
        /// </summary>
        /// <param name="id">Unique identifier of the product to be deleted.</param>
        Task DeleteProductAsync(int id);
    }
}
