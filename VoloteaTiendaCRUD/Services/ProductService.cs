using VoloteaTiendaCRUD.Models;
using VoloteaTiendaAPI.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace VoloteaTiendaAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Product> AddProductAsync(Product product)
        {

            // Convertir el precio a un formato numérico válido
            try
            {
                var price = Convert.ToDecimal(product.Price, new CultureInfo("en-US"));
                product.Price = price; 
               
            }
            catch (FormatException)
            {
                throw new ValidationException("El formato del precio es inválido.");
            }

            // Validar si el precio es negativo
            if (product.Price < 0)
            {
                throw new ValidationException("El precio no puede ser negativo.");
            }

            //Validamos que se usen colores o tallas válidos
            product.Id = 0;
            if (!Enum.IsDefined(typeof(Size), product.Size) || !Enum.IsDefined(typeof(Color), product.Color))
            {
                throw new ValidationException("Talla o color no válido.");
            }

            await _repository.AddAsync(product);
            var dbProduct = await _repository.GetByIdAsync(product.Id);

            if (dbProduct == null)
            {
                return null;
            }

            return product;
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            // Validar si el precio es negativo
            if (product.Price < 0)
            {
                throw new ValidationException("El precio no puede ser negativo.");
            }

            //Validamos que se usen colores o tallas válidos
            var dbProduct = await _repository.GetByIdAsync(product.Id);

            if (!Enum.IsDefined(typeof(Size), product.Size) || !Enum.IsDefined(typeof(Color), product.Color))
            {
                throw new ValidationException("Talla o color no válido.");
            }

            if (dbProduct == null)
            {
                return null;
            }

            // Si el producto existe, actualiza las propiedades y guarda los cambios.
            dbProduct.Description = product.Description;
            dbProduct.Price = product.Price;
            dbProduct.Size = product.Size;
            dbProduct.Color = product.Color;

            await _repository.UpdateAsync(dbProduct);
            return dbProduct; // Devuelve el producto actualizado.
        }


        public async Task DeleteProductAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product != null)
            {
                await _repository.DeleteAsync(id);
            }
            else
            {
                throw new NotFoundException("El producto no existe.");
            }
        }
    }
}
