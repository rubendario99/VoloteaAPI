using Microsoft.EntityFrameworkCore;
using VoloteaTiendaCRUD.Models;
using VoloteaTiendaAPI.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VoloteaTiendaAPI.Repositories
{
    /// <summary>
    /// Concrete implementation of IRepository for managing products in the database.
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;

        /// <summary>
        /// Constructor for the ProductRepository class.
        /// </summary>
        /// <param name="context">DataContext used to access the database.</param>
        public ProductRepository(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all products stored in the database.
        /// </summary>
        /// <returns>A list of products.</returns>
        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        /// <summary>
        /// Retrieves a specific product from the database based on its unique identifier.
        /// </summary>
        /// <param name="id">Unique identifier of the product.</param>
        /// <returns>The product corresponding to the specified identifier.</returns>
        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        /// <summary>
        /// Adds a new product to the database.
        /// </summary>
        /// <param name="product">Product to be added.</param>
        public async Task AddAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates the information of an existing product in the database.
        /// </summary>
        /// <param name="product">Product with the updated information.</param>
        public async Task UpdateAsync(Product product)
        {
            var existingProduct = await _context.Products.FindAsync(product.Id);
            if (existingProduct != null)
            {
                _context.Entry(existingProduct).CurrentValues.SetValues(product);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Deletes a product from the database based on its unique identifier.
        /// </summary>
        /// <param name="id">Unique identifier of the product to be deleted.</param>
        public async Task DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}
