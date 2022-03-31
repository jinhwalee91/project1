using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace shoppingApp.Models
{
    public class productModel
    {

        // put product properties here
        public int pId { get; set; }
        public string pName { get; set; }
        public string pCategory { get; set; }
        public int pPrice { get; set; }
        public int pQty { get; set; }
        public bool pInStock { get; set; }


        // sql connection here
        SqlConnection con = new SqlConnection("server=DESKTOP-TDF3AT3\\SQLEXPRESS; database = project1; integrated security = true");
        
        
        // methods here

        // 1. View all product list
        public List<productModel> getProductList()
        {
            SqlCommand cmd_getProductList = new SqlCommand("select * from product", con); 

            List<productModel> pList = new List<productModel>();
            
            SqlDataReader readAllProduct = null; 

            try
            {
                con.Open();
                readAllProduct = cmd_getProductList.ExecuteReader();

                while (readAllProduct.Read())
                {
                    pList.Add(new productModel()
                    {
                        pId = Convert.ToInt32(readAllProduct[0]),
                        pName = Convert.ToString(readAllProduct[1]),
                        pCategory = Convert.ToString(readAllProduct[2]),
                        pPrice = Convert.ToInt32(readAllProduct[3]),
                        pQty = Convert.ToInt32(readAllProduct[4]),
                        pInStock = Convert.ToBoolean(readAllProduct[5])
                       

                    });
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                readAllProduct.Close();
                con.Close();
            }
            return pList;
        }

        // 2. Get product detail by putting product id 
        public productModel getProductDetail (int pId)
        {
            SqlCommand cmd_getProductDetail = new SqlCommand("select * from product where @pId=pId", con);

            cmd_getProductDetail.Parameters.AddWithValue("@pId", pId);

            SqlDataReader read_product = null;

            productModel pModel = new productModel();

            try
            {
                con.Open();
                read_product = cmd_getProductDetail.ExecuteReader();
                if (read_product.Read())
                {
                    pModel.pId = Convert.ToInt32(read_product[0]);
                    pModel.pName = Convert.ToString(read_product[1]);
                    pCategory = Convert.ToString(read_product[2]);
                    pPrice = Convert.ToInt32(read_product[3]);
                    pQty = Convert.ToInt32(read_product[4]);
                    pInStock = Convert.ToBoolean(read_product[5]);
                }
                else
                {
                    throw new Exception("That product doesn't exist");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                read_product.Close();
                con.Close();
            }
            return pModel;

        }



        // 3. Add new product 


        public string addProduct(productModel newProduct)
        {
            SqlCommand cmd_addProduct = new SqlCommand("Insert into product values(@pId, @pName, @pCategory, @pPrice, @pQty, @pInStock)", con);

            cmd_addProduct.Parameters.AddWithValue("@empNo", newProduct.pId);
            cmd_addProduct.Parameters.AddWithValue("@empName", newProduct.pName);
            cmd_addProduct.Parameters.AddWithValue("@empDesignation", newProduct.pCategory);
            cmd_addProduct.Parameters.AddWithValue("@empSalary", newProduct.pQty);
            cmd_addProduct.Parameters.AddWithValue("@empIsPermenant", newProduct.pInStock);


            try
            {
                con.Open();
                cmd_addProduct.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return "Product has been added successfully";
        }

        // 4. delete product 


        public string deleteProduct(int pId)
        {
            SqlCommand cmd_deleteProduct = new SqlCommand("delete from product where @pId = pId", con);

            cmd_deleteProduct.Parameters.AddWithValue("@pId", pId);

            try
            {
                con.Open();
                cmd_deleteProduct.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return "The selected product has been deleted";
        }

        // 5. Update the product Method

        public string updateProduct(productModel update)
        {
            SqlCommand cmd_update = new SqlCommand("Update product set pName=@pName, pCategory=@pCategory, pPrice=@pPrice, pQty=@pQty, pInstock = @pInstock where pId=@pId", con);

            cmd_update.Parameters.AddWithValue("@pName", update.pName);
            cmd_update.Parameters.AddWithValue("@pCategory", update.pCategory);
            cmd_update.Parameters.AddWithValue("@pPrice", update.pPrice);
            cmd_update.Parameters.AddWithValue("@pQty", update.pQty);
            cmd_update.Parameters.AddWithValue("@pInstock", update.pInStock);
            cmd_update.Parameters.AddWithValue("@pId", update.pId);

            try
            {
                con.Open();
                cmd_update.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
            finally
            {
                con.Close();
            }
            return "The product has been updated successfully";
        }



    }
}
