using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using dotnet_rest_api.Models;
using dotnet_rest_api.Data;

namespace dotnet_rest_api.Controllers
{
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDBContext database;

        public ProductController(ApplicationDBContext database)
        {
            this.database = database;
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
                var products = database.Products.First(item => item.Id == id);
                return Ok(products);
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
    }

}