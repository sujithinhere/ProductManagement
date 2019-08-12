using Contracts;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext _dBContext;
        private IProductRepository _product;

        public IProductRepository Product
        {
            get
            {
                if(_product == null)
                {
                    _product = new ProductRepository(_dBContext);
                }
                return _product;
            }
        }

        public RepositoryWrapper(RepositoryContext dBContext)
        {
            _dBContext = dBContext;
        }
    }
}
