using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using shoppingApp.Models;

using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Logging;

namespace shoppingApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ordersController : Controller
    {

    ordersModel model_o = new ordersModel();


        #region HttpGet : See all order list 
        [HttpGet]
            [Route("See all order List")]
            public IActionResult ordersModel()
            {
                return Ok(model_o.getOrdersList());
            }
        #endregion
        
        #region HttpGet : Search order by customer number
        [HttpGet]
            [Route("Search order by customer No")]
            public IActionResult ordersModel(int customer_No)
             {
            try
            {
                return Ok(model_o.getOrderByCustomer(customer_No));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
            }
        #endregion

        #region HttpPost : Add New order
        [HttpPost]
        [Route("Add New Order")]
        public IActionResult placeOrder(int Customer_No, int product_Id, int order_Qty)
        {

            try
            {

                    if (Customer_No == null)
                {
                    return BadRequest("You must put customer number");
                }
                    if (product_Id == null)
                {
                    return BadRequest("You must put product ID");
                }
                    if (order_Qty < 0){
                    return BadRequest("You must put qty more than 0");
                    }
                    
                    else
                
                    return Ok(model_o.placeOrder(Customer_No, product_Id, order_Qty));
          
                 }
                catch // (System.Exception ex)
               {
                    return BadRequest("Not available for some reason.. please try again");
                }

        }
        #endregion

        #region Search order history by date
        [HttpGet]
        [Route("See all order List by date")]
        public IActionResult checkOrderHistoy_by_Date(string startDate, string endDate)
        {
            try {
                return Ok(model_o.checkOrderHistoy_by_Date(startDate, endDate));
            }
            catch // (System.Exception ex)
            {
                return BadRequest("There is some problem here.., please check your date or try again");
            }
        }
        #endregion

        #region HttpGet : Search invoice by order Id
        [HttpGet]
        [Route("Search Invoice by order Id")]
        public IActionResult checkInvoice(int orderId)
        {
            try
            {
                return Ok(model_o.checkInvoice(orderId));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region HttpGet : Search invice by customer Number
        [HttpGet]
        [Route("Search Invoice by customer No")]
        public IActionResult checkInvoice_by_cNo(int cNo)
        {
            try
            {
                return Ok(model_o.checkInvoice_by_cNo(cNo));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region HttpDelete : Delete order by order Id (Refund / Cancel)
        [HttpDelete]
        [Route("Delete order by order Id (number)")]
        public IActionResult deleteOrder(int orderId)
        {
            try
            {
                return Accepted(model_o.deleteOrder(orderId));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
        #endregion

    }
}
