using Xunit;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using VoloteaTiendaAPI.Services;
using VoloteaTiendaAPI.Data;
using VoloteaTiendaCRUD.Models;
using Microsoft.EntityFrameworkCore;
using VoloteaTiendaAPI.Repositories;
using System.ComponentModel.DataAnnotations;
using VoloteaTiendaAPI;

public class ProductServiceTests
{
    [Fact]
    public async Task GetAllProductsAsync_ReturnsAllProducts()
    {
        // Arrange
        var mockRepository = new Mock<IProductRepository>();
        var expectedProducts = new List<Product>
        {
            new Product { Id = 1, Price = 100, Size = Size.M, Color = Color.Red, Description = "Red Shirt" },
            new Product { Id = 2, Price = 200, Size = Size.L, Color = Color.Blue, Description = "Blue Jeans" }
        };

        mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(expectedProducts);
        var productService = new ProductService(mockRepository.Object);

        // Act
        var result = await productService.GetAllProductsAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal(expectedProducts, result); 
    }

    [Fact]
    public async Task GetProductByIdAsync_ReturnsProduct_WhenProductExists()
    {
        // Arrange
        int productId = 1; 
        var mockRepository = new Mock<IProductRepository>();
        var expectedProduct = new Product { Id = productId, Price = 100, Size = Size.M, Color = Color.Red, Description = "Red Shirt" };

        mockRepository.Setup(repo => repo.GetByIdAsync(productId)).ReturnsAsync(expectedProduct);
        var productService = new ProductService(mockRepository.Object);

        // Act
        var result = await productService.GetProductByIdAsync(productId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedProduct, result); 
    }

    [Fact]
    public async Task GetProductByIdAsync_ReturnsNull_WhenProductDoesNotExist()
    {
        // Arrange
        int productId = 1; 
        var mockRepository = new Mock<IProductRepository>();

        mockRepository.Setup(repo => repo.GetByIdAsync(productId)).ReturnsAsync((Product)null);
        var productService = new ProductService(mockRepository.Object);

        // Act
        var result = await productService.GetProductByIdAsync(productId);

        // Assert
        Assert.Null(result); 
    }

    [Fact]
    public async Task DeleteProductAsync_DeletesProduct_WhenProductExists()
    {
        // Arrange
        int productId = 1; 
        var mockRepository = new Mock<IProductRepository>();
        var productToDelete = new Product { Id = productId, Price = 100, Size = Size.M, Color = Color.Red, Description = "Red Shirt" };

        mockRepository.Setup(repo => repo.GetByIdAsync(productId)).ReturnsAsync(productToDelete);
        mockRepository.Setup(repo => repo.DeleteAsync(productId)).Verifiable("Product was not deleted");
        var productService = new ProductService(mockRepository.Object);

        // Act
        await productService.DeleteProductAsync(productId);

        // Assert
        mockRepository.Verify(repo => repo.DeleteAsync(productId), Times.Once()); 
    }

    [Fact]
    public async Task DeleteProductAsync_ThrowsNotFoundException_WhenProductDoesNotExist()
    {
        // Arrange
        int productId = 1;
        var mockRepository = new Mock<IProductRepository>();

        // Simula que no hay ningún producto con el ID especificado
        mockRepository.Setup(repo => repo.GetByIdAsync(productId)).ReturnsAsync((Product)null);

        var productService = new ProductService(mockRepository.Object);

        // Act y Assert
        var exception = await Assert.ThrowsAsync<NotFoundException>(() => productService.DeleteProductAsync(productId));

        // Assert
        Assert.Equal("El producto no existe.", exception.Message);
    }

    [Fact]
    public async Task UpdateProductAsync_UpdatesProduct_WhenProductExists()
    {
        // Arrange
        var productId = 1;
        var existingProduct = new Product { Id = productId, Description = "Old Description", Price = 100, Size = Size.M, Color = Color.Red };
        var updatedProduct = new Product { Id = productId, Description = "New Description", Price = 150, Size = Size.L, Color = Color.Blue };

        var mockRepository = new Mock<IProductRepository>();
        mockRepository.Setup(repo => repo.GetByIdAsync(productId)).ReturnsAsync(existingProduct);
        mockRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Product>())).Verifiable("The product was not updated");

        var productService = new ProductService(mockRepository.Object);

        // Act
        var result = await productService.UpdateProductAsync(updatedProduct);

        // Assert
        Assert.NotNull(result);
        mockRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Product>()), Times.Once());
        Assert.Equal("New Description", result.Description);
        Assert.Equal(150, result.Price);
    }

    [Fact]
    public async Task UpdateProductAsync_ReturnsNull_WhenProductDoesNotExist()
    {
        // Arrange
        var productId = 1;
        var updatedProduct = new Product { Id = productId, Description = "New Description", Price = 150, Size = Size.L, Color = Color.Blue };

        var mockRepository = new Mock<IProductRepository>();
        mockRepository.Setup(repo => repo.GetByIdAsync(productId)).ReturnsAsync((Product)null);  

        var productService = new ProductService(mockRepository.Object);

        // Act
        var result = await productService.UpdateProductAsync(updatedProduct);

        // Assert
        Assert.Null(result);  
    }

    [Fact]
    public async Task UpdateProductAsync_Fails_WhenPriceIsNegative()
    {
        // Arrange
        var product = new Product { Id = 1, Description = "Test Product", Price = -10, Size = Size.M, Color = Color.Red };
        var mockRepository = new Mock<IProductRepository>();
        mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(product);

        var productService = new ProductService(mockRepository.Object);

        // Act
        var exception = await Assert.ThrowsAsync<ValidationException>(() => productService.UpdateProductAsync(product));

        // Assert
        Assert.Contains("El precio no puede ser negativo", exception.Message);
    }

    [Fact]
    public async Task UpdateProductAsync_Fails_WhenColorOrSizeIsInvalid()
    {
        // Arrange
        var product = new Product { Id = 1, Description = "Test Product", Price = 100, Size = (Size)999, Color = Color.Red }; 
        var mockRepository = new Mock<IProductRepository>();
        mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(product);

        var productService = new ProductService(mockRepository.Object);

        // Act
        var exception = await Assert.ThrowsAsync<ValidationException>(() => productService.UpdateProductAsync(product));

        // Assert
        Assert.Contains("Talla o color no válido", exception.Message);
    }

    [Fact]
    public async Task AddProductAsync_Fails_WhenPriceIsNegative()
    {
        // Arrange
        var product = new Product { Description = "Test Product", Price = -10, Size = Size.M, Color = Color.Red };
        var mockRepository = new Mock<IProductRepository>();

        var productService = new ProductService(mockRepository.Object);

        // Act y Assert
        var exception = await Assert.ThrowsAsync<ValidationException>(() => productService.AddProductAsync(product));

        // Assert
        Assert.Contains("El precio no puede ser negativo", exception.Message);
    }

    [Fact]
    public async Task AddProductAsync_Fails_WhenInvalidSizeOrColor()
    {
        // Arrange
        var product = new Product { Description = "Test Product", Price = 50, Size = (Size)100, Color = (Color)100 };
        var mockRepository = new Mock<IProductRepository>();

        var productService = new ProductService(mockRepository.Object);

        // Act y Assert
        var exception = await Assert.ThrowsAsync<ValidationException>(() => productService.AddProductAsync(product));

        // Assert
        Assert.Contains("Talla o color no válido", exception.Message);
    }
}
