using Contracts;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class ProductRepository : Repositorybase<Product>, IProductRepository
    {
        public ProductRepository(RepositoryContext dBContext) 
            : base(dBContext)
        {
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return FindAll().OrderBy(p => p.ProductName);
        }

        public Product GetProductByCode(string productCode)
        {
            return FindByCondition(p => p.ProductCode == productCode).FirstOrDefault();
        }

        public IEnumerable<Product> GetDuplicates()
        {
            return FindAll().GroupBy(p => p.ProductName).Where(p => p.Count() > 1).SelectMany(p => p);
        }

        public void CreateProduct(Product product)
        {
            Create(product);
            Save();
        }

        public void UpdateProduct(Product oldProduct, Product updatedProduct)
        {
            oldProduct.ProductCode = updatedProduct.ProductCode;
            oldProduct.ProductName = updatedProduct.ProductName;
            oldProduct.ProductUrl = updatedProduct.ProductUrl;

            Update(oldProduct);
            Save();
        }

        public void DeleteProduct(Product product)
        {
            Delete(product);
            Save();
        }
    }
}
