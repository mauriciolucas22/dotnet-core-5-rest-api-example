using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using dotnet_rest_api.Models;
using dotnet_rest_api.Data;
using dotnet_rest_api.HATEOAS;

namespace dotnet_rest_api.Controllers
{
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDBContext database;
        private HATEOAS.HATEOAS HATEOAS;

        public ProductController(ApplicationDBContext database)
        {
            this.database = database;
            HATEOAS = new HATEOAS.HATEOAS("http://localhost:5001/api/products");

            HATEOAS.AddAction("SHOW_PRODUCT", "GET");
            HATEOAS.AddAction("STORE_PRODUCT", "PATCH");
            HATEOAS.AddAction("DELETE_PRODUCT", "DELETE");


        }

        [Route("api/products")]
        [HttpGet]
        public IActionResult Index()
        {
            var products = database.Products.ToList();
            return Ok(products);
        }

        [Route("api/products/{id}")]
        [HttpGet]
        public IActionResult Show(int id)
        {
            try
            {
                var product = database.Products.First(item => item.Id == id);

                ProductContainer productHATEOAS = new ProductContainer(product, HATEOAS.GetActions(product.Id.ToString()));


                return Ok(productHATEOAS.GetProductContainer());
            }
            catch (Exception err)
            {
                return BadRequest(new
                {
                    msg = "Error"
                });
            }
        }

        [Route("api/products")]
        [HttpPost]
        public IActionResult Store([FromBody] Product data)
        {
            this.database.Add(data);
            database.SaveChanges();
            Response.StatusCode = 201;
            return new ObjectResult(data);
        }

        [Route("api/products/{id}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                var products = database.Products.First(item => item.Id == id);
                database.Products.Remove(products);
                database.SaveChanges();
                return Ok();
            }
            catch (Exception err)
            {
                return BadRequest(new
                {
                    msg = "Error"
                });
            }
        }

        [Route("api/products")]
        [HttpPatch]
        public IActionResult Patch([FromBody] Product product)
        {
            if (product.Name.Equals(""))
            {
                Response.StatusCode = 400;
                return new ObjectResult(new
                {
                    msg = "Invalid params"
                });
            }

            try
            {
                var _product = database.Products.First(item => item.Id == product.Id);

                _product.Name = product.Name;
                database.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                return new ObjectResult(new
                {
                    msg = "Erro Geral",
                });
            }

        }

        public class ProductContainer
        {
            private Product product;
            private Link[] links;

            public ProductContainer(Product product, Link[] links)
            {
                this.product = product;
                this.links = links;
            }

            public Object GetProductContainer()
            {
                return new
                {
                    product = this.product,
                    links = this.links
                };
            }
        }
    }

}