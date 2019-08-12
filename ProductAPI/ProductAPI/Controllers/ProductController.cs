using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Entities.Validators;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductAPI.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private ILoggerManager _logManager;
        private IRepositoryWrapper _repositoryWrapper;

        public ProductController(ILoggerManager logManager, IRepositoryWrapper repositoryWrapper)
        {
            _logManager = logManager;
            _repositoryWrapper = repositoryWrapper;
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            try
            {
                var products = _repositoryWrapper.Product.GetAllProducts();
                _logManager.LogInfo("Request for Product List completed.");
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logManager.LogError($"Request for Product List Failed : {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("{productCode}", Name = "ProductByCode")]
        public IActionResult GetProductByCode(string productCode)
        {
            try
            {
                var product = _repositoryWrapper.Product.GetProductByCode(productCode);

                if (product == null)
                {
                    _logManager.LogError($"Product {productCode} not found.");
                    return NotFound();
                }
                else
                {
                    _logManager.LogInfo($"Returned Product : {productCode}");
                    return Ok(product);
                }
            }
            catch (Exception ex)
            {
                _logManager.LogError($"Error in GetProductByCode action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody]Product product)
        {
            try
            {
                if (product == null)
                {
                    _logManager.LogError("Product is null");
                    return BadRequest("Product object is null");
                }
                else if (!ModelState.IsValid)
                {
                    _logManager.LogError("Invalid product");
                    return BadRequest("Invalid product");
                }

                _repositoryWrapper.Product.CreateProduct(product);

                return CreatedAtRoute("ProductByCode", new { productCode = product.ProductCode }, product);
            }
            catch(Exception ex)
            {
                _logManager.LogError($"Error creating new product : {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut("{productCode}")]
        public IActionResult UpdateProduct(string productCode, [FromBody]Product updatedProduct)
        {
            try
            {
                if (updatedProduct.IsObjectNull())
                {
                    _logManager.LogError("Product is null.");
                    return BadRequest("Product is null");
                }
                if (!ModelState.IsValid)
                {
                    _logManager.LogError("Invalid product");
                    return BadRequest("Invalid product");
                }

                var currentProduct = _repositoryWrapper.Product.GetProductByCode(productCode);
                if (currentProduct.IsObjectNull())
                {
                    _logManager.LogError($"Product with code: {productCode}, was not found.");
                    return NotFound();
                }

                _repositoryWrapper.Product.UpdateProduct(currentProduct, updatedProduct);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logManager.LogError($"Error when trying to update product : {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

        }

        [HttpDelete("{productCode}")]
        public IActionResult DeleteProduct(string productCode)
        {
            try
            {
                var product = _repositoryWrapper.Product.GetProductByCode(productCode);
                if (product.IsObjectNull())
                {
                    _logManager.LogError($"Product with code: {productCode}, was not found.");
                    return NotFound();
                }

                _repositoryWrapper.Product.DeleteProduct(product);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logManager.LogError($"Error when Deleting product : {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
