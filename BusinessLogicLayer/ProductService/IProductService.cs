using BusinessLogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.ProductService
{
    public interface IProductService
    {
        List<Product> GetAll();
    }
}
