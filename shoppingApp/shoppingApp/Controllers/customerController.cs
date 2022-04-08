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



    public class customerController : Controller
    {
        customerModel model_c = new customerModel();

        #region HttpGet : See all customer List
        [HttpGet]
        [Route("See all customer List")]
        public IActionResult customerModel()
        {
         
            return Ok(model_c.getCustomerList());
        }
        #endregion

        #region HttpGet : Search customer by customer id
        [HttpGet]
        [Route("Search customer by their customer userId")]
        public IActionResult customerModel(string userID)
        {
            try
            {
                return Ok(model_c.getCustomerDetail(userID));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region HttpPost : Add new customer
        [HttpPost]
        [Route("Add New Customer")]
        public IActionResult addCustomer(string userID, string Name, string Email, int Age)
        {

         
            

            try
            {

                if (userID == null)
                {
                    return BadRequest("You must put userID");
                }
                if (Name == null)
                {
                    return BadRequest("You must put name");
                }
                if (Email == null)
                {
                    return BadRequest("You must put valid Email");
                }
                if (Age < 21)
                {
                    return BadRequest("Sorry, you should be over 21 to join");
                }
                return Created("", model_c.addCustomer(userID, Name, Email, Age));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region HttpDelete : Delete customer
        [HttpDelete]
        [Route("Delete customer by customer ID")]
        public IActionResult deleteCustomer(string userID)
        {
            try
            {
                return Accepted(model_c.deleteCustomer(userID));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
        #endregion

        #region HttpPut update customer info
        [HttpPut]
        [Route("Update customer information ")]
        public IActionResult updateCustomer(string userID, string new_name, string new_Email)
        {
            try
            {
                return Accepted(model_c.updateCustomer(userID, new_name, new_Email));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion


    }
}
