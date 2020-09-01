using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webApi4.Models;

namespace webApi4.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private static List<Product> products = new List<Product>()
        {
            new Product
            {
                Id=1006368,
                Name="Austin and barbeque AABQ wifi Food thermometer",
                Description="Thermometer wifi for optimal innerTemperature",
                Price=399
            },
            new Product
            {
                Id=1009334,
                Name="Andesson electric tandare ECL 1.1",
                Description="Electric stormsaker tandare  helt utan gas and bransle",
                Price=89
            },
            new Product
            {
                Id=1002266,
                Name="Weber nostick spray",
                Description="BBQ som motverkar att ravaror fastnar pa gallret",
                Price=99
            }


        };

        //Get all product
        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            return products;
        }

        //Get specific product
        [HttpGet("{id}")]
        public ActionResult<Product> Get(int id)
        {
            var product = products.Find(p => p.Id == id);
            if(product==null)
            {
                return NotFound();
            }

            return product;
                
                
        }
        
        //Add new product
        [HttpPost]
        public ActionResult Post([FromBody]Product product)
        {
            if(products.Exists(p=>p.Id==product.Id))
            {
                return Conflict();
            }

            products.Add(product);
            return CreatedAtAction(nameof(Get), new { id = product.Id }, products);
        }

        //Delete the product
        [HttpDelete("{id}")]
        public ActionResult<IEnumerable<Product>>Delete(int id)
        {
            var product = products.Where(p => p.Id == id);
            if(product==null)
            {
                return NotFound();
            }
            products = products.Except(product).ToList();
            return products;
        }

        //Update product
        [HttpPut("{id}")]
        public ActionResult<IEnumerable<Product>> Put(int id, [FromBody] Product product)
        {
            if(id != product.Id)
            {
                return BadRequest();
            }
            var existingProduct = products.Where(p => p.Id == id);
            products = products.Except(existingProduct).ToList();

            products.Add(product);
            return products;
        }
    }
}