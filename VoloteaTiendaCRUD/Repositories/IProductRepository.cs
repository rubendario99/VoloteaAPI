using System.Collections.Generic;
using System.Threading.Tasks;
using VoloteaTiendaCRUD.Models;

namespace VoloteaTiendaAPI.Repositories
{
    /// <summary>
    /// Interface for accessing and managing product data.
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// Retrieves all products from the repository.
        /// </summary>
        /// <returns>A list of all products.</returns>
        Task<List<Product>> GetAllAsync();

        /// <summary>
        /// Retrieves a product by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the product.</param>
        /// <returns>The product with the specified identifier, or null if not found.</returns>
        Task<Product> GetByIdAsync(int id);

        /// <summary>
        /// Adds a new product to the repository.
        /// </summary>
        /// <param name="product">The product to add.</param>
        Task AddAsync(Product product);

        /// <summary>
        /// Updates an existing product in the repository.
        /// </summary>
        /// <param name="product">The updated product.</param>
        Task UpdateAsync(Product product);

        /// <summary>
        /// Deletes a product from the repository by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the product to delete.</param>
        Task DeleteAsync(int id);
    }
}
