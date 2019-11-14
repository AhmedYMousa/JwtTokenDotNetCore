using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using System.Collections.Generic;
using EcommApi.Models;
using System.Linq;

namespace EcommApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowMyOrigin")]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext context;
        public ProductsController(AppDbContext context)
        {
            this.context = context;
        }
        public IEnumerable<Product> GetProducts()
        {
            return context.Products.OrderBy(x => x.Id).ToList();
        }
        [HttpGet("{id:int}")]
        public Product GetProduct(int id)
        {
            return context.Products.Find(id);
        }
    }
}