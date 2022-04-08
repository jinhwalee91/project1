using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;



namespace shoppingApp.Models
{
    public class ordersModel
    {


        #region Properties 
        public int orderId { get; set; }
        public int cNo { get; set; }       
        public int productId { get; set; }           
        public int orderQty { get; set; }     
        public string orderDate { get; set; }
        #endregion

        #region SQL Conncetion
        SqlConnection con = new SqlConnection("server=DESKTOP-TDF3AT3\\SQLEXPRESS; database = project1; integrated security = true");

        #endregion

        #region View all order List 
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
                        cNo = Convert.ToInt32(readAllOrders[1]), 
                        productId = Convert.ToInt32(readAllOrders[2]),
                        orderQty = Convert.ToInt32(readAllOrders[3]),
                        orderDate = Convert.ToString(readAllOrders[4]),


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
        #endregion

        #region get order record by putting customer Number
        public List<ordersModel> getOrderByCustomer(int cNo)
        {
            List<ordersModel> oModel = new List<ordersModel>();

            SqlCommand cmd_getOrderByCustomer = new SqlCommand("select * from orders where @cNo=cNo", con);

              SqlDataReader read_order = null;
            //SqlDataReader read_order;

            cmd_getOrderByCustomer.Parameters.AddWithValue("@cNo", cNo);
          
            try
            {
                con.Open();
                read_order = cmd_getOrderByCustomer.ExecuteReader();

                while (read_order.Read() == true)
                {
                   
                        var om = new ordersModel();
                        om.orderId = Convert.ToInt32(read_order[0]);
                        om.cNo = Convert.ToInt32(read_order[1]);
                        om.productId = Convert.ToInt32(read_order[2]);
                        om.orderQty = Convert.ToInt32(read_order[3]);
                        om.orderDate = Convert.ToString(read_order[4]);

                        oModel.Add(om);
                    

                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
            finally
            {
                //    read_order.Close();
                con.Close();
            }
            return oModel;

        }

        #endregion 

        #region place order 
        public string placeOrder(int cNo, int productId , int orderQty)
        {
             
            SqlCommand cmd_newOrder = new SqlCommand("exec procedure_newOrder @cNo, @productId, @orderQty ", con);

            cmd_newOrder.Parameters.AddWithValue("@cNo", cNo);
            cmd_newOrder.Parameters.AddWithValue("@productId", productId);
            cmd_newOrder.Parameters.AddWithValue("@orderQty", orderQty);

            try
            {
                con.Open();
                cmd_newOrder.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return "New order has been added successfully ! Thank you for your order";
        }

        #endregion

        #region Search order history by date range
        public List<ordersModel> checkOrderHistoy_by_Date(string startDate, string endDate)
        {
              List<ordersModel>  orderHistoryList = new List<ordersModel>();


            SqlCommand cmd_checkOrders = new SqlCommand("select * from orders where orderDate between @startDate and @endDate ", con);

            SqlDataReader read_orders_history; // = null;

            cmd_checkOrders.Parameters.AddWithValue("@startDate", startDate);
            cmd_checkOrders.Parameters.AddWithValue("@endDate", endDate);

            try
            {
                con.Open();
                read_orders_history = cmd_checkOrders.ExecuteReader();

                while (read_orders_history.Read())
                {
                    orderHistoryList.Add(new ordersModel()
                    {
                        orderId = Convert.ToInt32(read_orders_history[0]),
                        cNo = Convert.ToInt32(read_orders_history[1]),
                        productId = Convert.ToInt32(read_orders_history[2]),
                        orderQty = Convert.ToInt32(read_orders_history[3]),
                        orderDate = Convert.ToString(read_orders_history[4]),
                    });

        
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                //  read_orders_history.Close();
                con.Close();
            }
            return orderHistoryList;
        }
        #endregion

        #region check total bill by order
        public int checkInvoice(int orderId)
        {
           
            int totalAmount  ;
            
            SqlCommand cmd_checkInvoice = new SqlCommand("select orderQty * pPrice from product join orders on product.pId = orders.pId  where @orderId = orderId ", con);

            SqlDataReader read_invoice = null ;

            cmd_checkInvoice.Parameters.AddWithValue("@orderId", orderId);
           

            try
            {
                con.Open();
                read_invoice = cmd_checkInvoice.ExecuteReader();
                if (read_invoice.Read()) 
                {
 
                    totalAmount =Convert.ToInt32(read_invoice[0]);
                   
                }

                else
                {
                    throw new Exception("The invoice doesn't exist");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                read_invoice.Close();
                con.Close();
            }
            return totalAmount;
        }
        #endregion

        #region check total bill by customer
        // 5. Check Total bill amount by putting customer No  
        public int checkInvoice_by_cNo(int cNo)
        {

            int totalAmount_ofCustomer;

            SqlCommand cmd_checkInvoice2 = new SqlCommand("select isNull(sum(orderQty * pPrice),0) from product join orders on product.pId = orders.Pid where @cNo = cNo ", con);

            SqlDataReader read_invoice2  = null;

            cmd_checkInvoice2.Parameters.AddWithValue("@cNo", cNo);


            try
            {
                con.Open();
                read_invoice2 = cmd_checkInvoice2.ExecuteReader();
                if (read_invoice2.Read())
                {

                 totalAmount_ofCustomer = Convert.ToInt32(read_invoice2[0]);
        


                }
                else
                {
                    throw new Exception("The invoice doesn't exist");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                read_invoice2.Close();
                con.Close();
            }
            return totalAmount_ofCustomer;
            
        }
        #endregion

        #region Cancel exisint order (Refund / Return) 
        public string deleteOrder(int orderId)
        {
            SqlCommand cmd_deleteOrder = new SqlCommand("delete from orders where @orderId = orderId", con);

            cmd_deleteOrder.Parameters.AddWithValue("@orderId", orderId);

            SqlDataReader read_delete ;   

            try
            {
                con.Open();
                cmd_deleteOrder.ExecuteNonQuery();
           
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
               
                con.Close();
            }
            return "The selected order has been deleted";
        }

        #endregion

    }




}


