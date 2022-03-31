using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace shoppingApp.Models
{
    public class customerModel
    {

        // put customer properties here
        public int cId { get; set; }
        public string cName { get; set; }
        public string cEmail { get; set; }



        // sql connection here
        SqlConnection con = new SqlConnection("server=DESKTOP-TDF3AT3\\SQLEXPRESS; database = project1; integrated security = true");


        // methods here

        // 1. View all customer list
        public List<customerModel> getCustomerList()
        {
            SqlCommand cmd_getCustomerList = new SqlCommand("select * from customer", con);

            List<customerModel> cList = new List<customerModel>();

            SqlDataReader readAllCustomer = null;

            try
            {
                con.Open();
                readAllCustomer = cmd_getCustomerList.ExecuteReader();

                while (readAllCustomer.Read())
                {
                    cList.Add(new customerModel()
                    {
                        cId = Convert.ToInt32(readAllCustomer[0]),
                        cName = Convert.ToString(readAllCustomer[1]),
                        cEmail = Convert.ToString(readAllCustomer[2]),
                       

                    });
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                readAllCustomer.Close();
                con.Close();
            }
            return cList;
        }

        // 2. Get customer detail by putting customer id 
        public customerModel getCustomerDetail(int cId)
        {
            SqlCommand cmd_getCustomerDetail = new SqlCommand("select * from customer where cId=@cId", con);

            cmd_getCustomerDetail.Parameters.AddWithValue("@cId", cId);

            SqlDataReader read_customer = null;

            customerModel cModel = new customerModel();

            try
            {
                con.Open();
                read_customer = cmd_getCustomerDetail.ExecuteReader();
                if (read_customer.Read())
                {
                    
                    cModel.cId = Convert.ToInt32(read_customer[0]);
                    cModel.cName = Convert.ToString(read_customer[1]);
                    cModel.cEmail= Convert.ToString(read_customer[2]);
                    
                    
                }
                else
                {
                    throw new Exception("The customer doesn't exist");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                read_customer.Close();
                con.Close();
            }
            return cModel;

        }



        // 3. Add new customer 


        public string addCustomer(customerModel newcustomer)
        {
            SqlCommand cmd_addCustomer = new SqlCommand("Insert into customer values(@cId, @cName, @cEmail)", con);

            cmd_addCustomer.Parameters.AddWithValue("@cId", newcustomer.cId);
            cmd_addCustomer.Parameters.AddWithValue("@cName", newcustomer.cName);
            cmd_addCustomer.Parameters.AddWithValue("@cEmail", newcustomer.cEmail);
          


            try
            {
                con.Open();
                cmd_addCustomer.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return "The customer has been added successfully";
        }

        // 4. delete customer 


        public string deleteCustomer(int cId)
        {
            SqlCommand cmd_deleteCustomer = new SqlCommand("delete from customer where @cId = cId", con);

            cmd_deleteCustomer.Parameters.AddWithValue("@cId", cId);

            try
            {
                con.Open();
                cmd_deleteCustomer.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return "The selected customer has been deleted";
        }

        // 5. Update the customer Method

        public string updateCustomer(customerModel update)
        {
            SqlCommand cmd_updateCustomer = new SqlCommand("Update customer set cName=@cName, cEmail = @cEmail where cId=@cId", con);

            cmd_updateCustomer.Parameters.AddWithValue("@cName", update.cName);
            cmd_updateCustomer.Parameters.AddWithValue("@cEmail", update.cEmail);
            cmd_updateCustomer.Parameters.AddWithValue("@cId", update.cId);

            try
            {
                con.Open();
                cmd_updateCustomer.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
            finally
            {
                con.Close();
            }
            return "The customer has been updated successfully";
        }




    }
}
