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

        [HttpGet]
        [Route("See all customer List")]
        public IActionResult customerModel()
        {
            return Ok(model_c.getCustomerList());
        }


        [HttpGet]
        [Route("Search customer by their ID")]
        public IActionResult customerModel(int cId)
        {
            try
            {
                return Ok(model_c.getCustomerDetail(cId));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        [Route("Add New Customer")]
        public IActionResult addCustomer(customerModel newCustomer)
        {
            try
            {
                return Created("", model_c.addCustomer(newCustomer));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete]
        [Route("Delete customer by customer ID")]
        public IActionResult deleteCustomer(int cId)
        {
            try
            {
                return Accepted(model_c.deleteCustomer(cId));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }


        [HttpPut]
        [Route("Update Customer")]
        public IActionResult updateCustomer(customerModel update)
        {
            try
            {
                return Accepted(model_c.updateCustomer(update));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}
