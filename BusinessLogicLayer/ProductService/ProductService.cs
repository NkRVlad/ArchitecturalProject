using BusinessLogicLayer.Models;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IApplicationDbContext _dbContext;

        public ProductService(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Product> GetAll()
        {
            var product = _dbContext.Products.ToList();
            var productListResult = new List<Product>();

            foreach (var tempProduct in product)
            {
                productListResult.Add(new Product
                {
                    Name = tempProduct.Name,
                    Count = tempProduct.Count,
                    Price = tempProduct.Price

                });
            }

            return productListResult;
        }
    }
}
