using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace shoppingApp.Models
{
    public class customerModel
    {

        #region Properties here
        public int cNo { get; set; }

        public string cId { get; set; }

        public string cName { get; set; }

        public string cEmail { get; set; }

        public int cAge { get; set; }
      //   public DateTime cdob { get; set; }
        #endregion

        #region SQL connection
        SqlConnection con = new SqlConnection("server=DESKTOP-TDF3AT3\\SQLEXPRESS; database = project1; integrated security = true");
        #endregion

        #region Customer List
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
                        cNo = Convert.ToInt32(readAllCustomer[0]),
                        cId = Convert.ToString(readAllCustomer[1]),
                        cName = Convert.ToString(readAllCustomer[2]),
                        cEmail = Convert.ToString(readAllCustomer[3]),
                        //cdob = Convert.ToDateTime(readAllCustomer[4]),
                        cAge = Convert.ToInt32(readAllCustomer[4]),
                       

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
        #endregion
 
        #region Get customer detail by putting customer id 
        public customerModel getCustomerDetail(string cId)
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
                    cModel.cNo = Convert.ToInt32(read_customer[0]);
                    cModel.cId = Convert.ToString(read_customer[1]);
                    cModel.cName = Convert.ToString(read_customer[2]);
                    cModel.cEmail= Convert.ToString(read_customer[3]);
                    // cModel.cdob = Convert.ToDateTime(read_customer[4]);
                    cModel.cAge = Convert.ToInt32(read_customer[4]);
                    
                    
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
        #endregion

        #region Add new customer
        public string addCustomer(string userID, string Name, string Email, int Age)
        {
            SqlCommand cmd_addCustomer = new SqlCommand("Insert into customer values(@cId, @cName, @cEmail,@cDob)", con);

            cmd_addCustomer.Parameters.AddWithValue("@cId", userID);
            cmd_addCustomer.Parameters.AddWithValue("@cName", Name);
            cmd_addCustomer.Parameters.AddWithValue("@cEmail", Email);
            cmd_addCustomer.Parameters.AddWithValue("@cDob", Age);
          


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
        #endregion

        #region Delete Customer
        public string deleteCustomer(string cId)
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
        #endregion

        #region Update customer
        public string updateCustomer(string cId, string cName, string cEmail)
        {
            SqlCommand cmd_updateCustomer = new SqlCommand("Update customer set cName=@cName, cEmail = @cEmail where cId=@cId", con);

            cmd_updateCustomer.Parameters.AddWithValue("@cId", cId);
            cmd_updateCustomer.Parameters.AddWithValue("@cName", cName);
            cmd_updateCustomer.Parameters.AddWithValue("@cEmail", cEmail);


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
        #endregion
      
    }
}
