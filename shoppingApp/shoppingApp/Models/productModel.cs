using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;


namespace shoppingApp.Models
{
    public class productModel
    {

        #region properties
        public int product_No { get; set; }   
        public string product_Name { get; set; }
        public string product_Category { get; set; }
        public int product_Price { get; set; }
        public int product_Qty { get; set; }
        #endregion

        #region SQL connection
        SqlConnection con = new SqlConnection("server=DESKTOP-TDF3AT3\\SQLEXPRESS; database = project1; integrated security = true");
        #endregion

        #region Get product List
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
                        product_No = Convert.ToInt32(readAllProduct[0]),
                        product_Name = Convert.ToString(readAllProduct[1]),
                        product_Category = Convert.ToString(readAllProduct[2]),
                        product_Price = Convert.ToInt32(readAllProduct[3]),
                        product_Qty = Convert.ToInt32(readAllProduct[4]),
                  
                       

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
        #endregion

        #region Get product detail by putting prodcut number
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
                    pModel.product_No = Convert.ToInt32(read_product[0]);
                    pModel.product_Name = Convert.ToString(read_product[1]);
                    pModel.product_Category = Convert.ToString(read_product[2]);
                    pModel.product_Price = Convert.ToInt32(read_product[3]);
                    pModel.product_Qty = Convert.ToInt32(read_product[4]);
                  
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
        #endregion
  
        #region Get product by Category search
        public List<productModel> getProductByCategory(string product_Category)
        {
            var pModel_category = new List<productModel>();

            SqlCommand cmd_getProductByCategory = new SqlCommand("select * from product where pCategory= @pCategory", con);

            //  SqlDataReader read_order = null;
            SqlDataReader read_product;

            cmd_getProductByCategory.Parameters.AddWithValue("@pCategory", product_Category);

            try
            {
                con.Open();
                read_product = cmd_getProductByCategory.ExecuteReader();

                while (read_product.Read())
                {
                  
                        var pm = new productModel();
                        pm.product_No = Convert.ToInt32(read_product[0]);
                        pm.product_Name = Convert.ToString(read_product[1]);
                        pm.product_Category = Convert.ToString(read_product[2]);
                        pm.product_Price = Convert.ToInt32(read_product[3]);
                        pm.product_Qty = Convert.ToInt32(read_product[4]);

                        pModel_category.Add(pm);
                    
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
            return pModel_category;

        }
        #endregion

        #region Add new product
        public string addProduct(string pName, string pCategory, int pPrice, int pQty)
        {
            SqlCommand cmd_addProduct = new SqlCommand("Insert into product values( @pName, @pCategory, @pPrice, @pQty)", con);

            
            cmd_addProduct.Parameters.AddWithValue("@pName", pName);
            cmd_addProduct.Parameters.AddWithValue("@pCategory", pCategory);
            cmd_addProduct.Parameters.AddWithValue("@pPrice", pPrice);
            cmd_addProduct.Parameters.AddWithValue("@pQty", pQty);



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
        #endregion
 
        #region Delete product
        public string deleteProduct(int pId)
        {
            SqlCommand cmd_deleteProduct = new SqlCommand("delete from product where @pId = pId", con);

            cmd_deleteProduct.Parameters.AddWithValue("@pId", pId);

            // SqlDataReader reader = null;
            productModel pModel = new productModel();

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
        #endregion

        #region Update Product
        public string updateProduct(int pId, string pName, string pCategory, int pPrice, int pQty)
        {
            SqlCommand cmd_update = new SqlCommand("Update product set pName=@pName, pCategory=@pCategory, pPrice=@pPrice, pQty=@pQty where pId=@pId", con);

            cmd_update.Parameters.AddWithValue("@pId", pId);
            cmd_update.Parameters.AddWithValue("@pName", pName);
            cmd_update.Parameters.AddWithValue("@pCategory", pCategory);
            cmd_update.Parameters.AddWithValue("@pPrice", pPrice);
            cmd_update.Parameters.AddWithValue("@pQty", pQty);
            

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
        #endregion

        #region Add more product
        public string addmoreProduct(int pId, int pQty)
        {
            SqlCommand cmd_addmoreProduct = new SqlCommand("exec procedure_updateProduct @productId, @productQty", con);

            cmd_addmoreProduct.Parameters.AddWithValue("@productId", pId);
            cmd_addmoreProduct.Parameters.AddWithValue("@productQty", pQty);



            try
            {
                con.Open();
                cmd_addmoreProduct.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return "Product QTY has been added successfully";

        }
        #endregion
    }
}
