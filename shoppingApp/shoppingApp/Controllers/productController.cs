using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using shoppingApp.Models;



namespace shoppingApp.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class productController : Controller
    {
        productModel model = new productModel();

        [HttpGet]
        [Route("See all product List")]
        public IActionResult productModel()
        {
            return Ok(model.getProductList());
        }


        [HttpGet]
        [Route("Search Product by product ID")]
        public IActionResult productModel(int pId)
        {
            try
            {
                return Ok(model.getProductDetail(pId));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        [Route("Add New Product")]
        public IActionResult addProduct(productModel newProduct)
        {
            try
            {
                return Created("", model.addProduct(newProduct));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete]
        [Route("Delete Prodcut by product ID")]
        public IActionResult deleteProduct(int pId)
        {
            try
            {
                return Accepted(model.deleteProduct(pId));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }


        [HttpPut]
        [Route("Update Product")]
        public IActionResult updateProduct(productModel updates)
        {
            try
            {
                return Accepted(model.updateProduct(updates));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}
