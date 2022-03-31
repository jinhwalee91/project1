using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace shoppingApp.Models
{
    public class ordersModel
    {

        customerModel customerModel_i = new customerModel();
        // customerModel class properties = customerModel_i.____ 
        productModel productModel_i = new productModel();
        // productModel class properties = productModel_i.____ 
        public int cId2 = customerModel.cId;
      
        // put order properties here
        public int orderId { get; set; }
        public string orderDate { get; set; }



        // sql connection here
        SqlConnection con = new SqlConnection("server=DESKTOP-TDF3AT3\\SQLEXPRESS; database = project1; integrated security = true");


        // methods here

        // 1. View all order list
        public List<ordersModel> getOrdersList()
        {
            SqlCommand cmd_getOrdersList = new SqlCommand("select * from orders", con);

            List<ordersModel> oList = new List<ordersModel>();

            SqlDataReader readAllOrders = null;

            try
            {
                con.Open();
                readAllOrders = cmd_getOrdersList.ExecuteReader();

                while (readAllOrders.Read())
                {
                    oList.Add(new ordersModel()
                    {
                        orderId = Convert.ToInt32(readAllOrders[0]),
                        cId2 = Convert.ToInt32(readAllOrders[1]), 
                        productModel_i.pId = Convert.ToString(readAllOrders[2]),
                        orderDate = Convert.ToString(readAllOrders[3]),

                    });
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                readAllOrders.Close();
                con.Close();
            }
            return oList;
        }



    }
}
