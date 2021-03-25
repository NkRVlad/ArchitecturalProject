using BusinessLogicLayer.Models;
using BusinessLogicLayer.ProductService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task_1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PublicDataController : ControllerBase
    {

        private IProductService _productService;
        public PublicDataController(IProductService productService)
        {
            _productService = productService;
        }


        [HttpGet]
        [Route("get-product")]
        public IEnumerable<Product> GetUsers()
        {
            return _productService.GetAll();
        }

    }
}
