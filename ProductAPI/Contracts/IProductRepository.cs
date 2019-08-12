using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProductByCode(string productCode);
        void CreateProduct(Product product);
        void UpdateProduct(Product oldProduct, Product UpdatedProduct);
        void DeleteProduct(Product product);
    }
}
