using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Net;

using shoppingApp.Models;



namespace shoppingApp.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class productController : Controller
    {
        productModel model = new productModel();



        #region HttpGet Get all produt List 
        [HttpGet]
        [Route("See all product List")]
        public IActionResult productModel()
        {
            return Ok(model.getProductList());
        }
        #endregion

        #region HttpGet : Search product by product number
        [HttpGet]
        [Route("Search Product by product No")]
        public IActionResult getProductDetail(int product_No)
        {
            try
            {
                return Ok(model.getProductDetail(product_No));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region HttpGet : Search product by category 
        [HttpGet]
        [Route("Search product by category")]
        public IActionResult getProductByCategory(string product_Category) 
        {
            try
            {
           
                return Ok(model.getProductByCategory(product_Category));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region HttpPost : Add new product
        [HttpPost]
        [Route("Add New Product")]
        public async Task<ActionResult<productModel>> addProduct(string Product_Name, string Category, int Price, int Quantity)
        {
            try
            {

                if (Product_Name == null)
                {
                    return BadRequest("You must put prodcut name");
                }
                if (Category == null)
                {
                    return BadRequest("You must put prodcut category");
                }
                if (Price < 0)
                {
                    return BadRequest("Price should be more than 0");
                }
                if (Quantity < 1)
                {
                    return BadRequest("Quantity should be more than 1");
                }

                return Created("", model.addProduct(Product_Name, Category, Price, Quantity));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
          }
        #endregion

        #region HttpDelete Delete product by product Number
        [HttpDelete]
        [Route("Delete Prodcut by product Number")]
        public IActionResult deleteProduct(int product_No)
        {
            try
            {
                if (product_No < 0)
                {
                    return BadRequest("The product number has to be over than 0");
                    
                }
             
                    return Accepted(model.deleteProduct(product_No));
                
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
        #endregion

        #region HttpPut Update product
        [HttpPut]
        [Route("Update Product")]
        public IActionResult updateProduct(int produt_Number, string new_Name, string new_Category, int New_Price, int new_Qantity)
        {
            try
            {
                return Accepted(model.updateProduct(produt_Number, new_Name, new_Category, New_Price, new_Qantity));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region HttpPut Add/reduce more product Qty
        [HttpPut]
        [Route("Add more Product")]
        public IActionResult addmoreProduct(int product_Number, int Qty_To_Add)
        {
            try
            {
                return Accepted(model.addmoreProduct(product_Number, Qty_To_Add));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

    }
}
