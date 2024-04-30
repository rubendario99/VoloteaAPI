using Microsoft.AspNetCore.Mvc;
using VoloteaTiendaCRUD.Models;
using VoloteaTiendaAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using VoloteaTiendaAPI;

namespace ProductAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Retrieves all products.
        /// </summary>
        /// <returns>
        /// A list of products.
        /// </returns>

        [HttpGet("GetAllProducts")]
        public async Task<ActionResult<List<Product>>> GetAllProducts()
        {
            try
            {
                var products = await _productService.GetAllProductsAsync();
                return Ok(products);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while retrieving products. Please try again later.");
            }
        }

        // <summary>
        /// Retrieves a product by its unique identifier.
        /// </summary>
        /// <param name="id">The identifier of the product to retrieve.</param>
        /// <returns>
        /// The product with the specified identifier.
        /// </returns>
        /// <response code="200">Returns the requested product.</response>
        /// <response code="404">If the product with the specified identifier is not found.</response>
        /// <response code="500">If an error occurs while retrieving the product.</response>

        [HttpGet("GetProductById/{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while retrieving products. Please try again later.");
            }
        }

        /// <summary>
        /// Adds a new product.
        /// </summary>
        /// <param name="product">The product to add.</param>
        /// <returns>
        /// The newly added product.
        /// </returns>
        /// <response code="201">Returns the newly added product.</response>
        /// <response code="400">If the provided product data is invalid.</response>
        /// <response code="500">If an error occurs while adding the product.</response>

        [HttpPost("AddProduct")]
        public async Task<ActionResult<Product>> AddProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _productService.AddProductAsync(product);
                return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while retrieving products. Please try again later.");
            }
        }

        /// <summary>
        /// Deletes a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        /// <returns>
        /// No content.
        /// </returns>
        /// <response code="204">No content is returned upon successful deletion.</response>
        /// <response code="404">If the product with the specified ID does not exist.</response>
        /// <response code="500">If an error occurs while deleting the product.</response>

        [HttpDelete("DeleteProduct/{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            try
            {
                await _productService.DeleteProductAsync(id);
                return NoContent();
            }
            catch (NotFoundException e)
            {
                return StatusCode(500, e.Message); //Podríamos usar un código 404 Not Found también
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while retrieving products. Please try again later.");
            }
        }

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="product">The updated product object.</param>
        /// <returns>
        /// The updated product.
        /// </returns>
        /// <response code="200">Returns the updated product upon successful update.</response>
        /// <response code="400">If the request is invalid.</response>
        /// <response code="404">If the product to update does not exist.</response>
        /// <response code="500">If an error occurs while updating the product.</response>

        [HttpPut("UpdateProduct")]
        public async Task<ActionResult<Product>> UpdateProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedProduct = await _productService.UpdateProductAsync(product);

                if (updatedProduct == null)
                {
                    return NotFound("Product not found...");
                }

                return Ok(updatedProduct);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while retrieving products. Please try again later.");
            }
        }
    }
}
