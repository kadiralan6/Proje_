using Proje_.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Proje_.Dal
{
    public class ProductDal
    {
        string cString = ConfigurationManager.ConnectionStrings["ADONet"].ToString();

        //Listeleme
        public List<Product_> GetAll()
        {
            List<Product_> productlist = new List<Product_>();
            using (SqlConnection connection = new SqlConnection(cString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_GetAllProducts";
                SqlDataAdapter sqlDa = new SqlDataAdapter(command);
                DataTable dtProducts = new DataTable();
                connection.Open();
                sqlDa.Fill(dtProducts);
                connection.Close();
                foreach (DataRow item in dtProducts.Rows)
                {
                    productlist.Add(new Product_
                    {
                        pID = Convert.ToInt32(item["Product_ID"]),
                        pName = item["Product_Name"].ToString(),
                        PType = item["Product_Type"].ToString()
                    });
                }
            }

            return productlist;
        }

        //ekleme
        public bool InsertProduct(Product_ product)
        {
            int id;
            using (SqlConnection connection = new SqlConnection(cString))
            {
                SqlCommand command = new SqlCommand("sp_InsertProduct1", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ProductName", product.pName);
                command.Parameters.AddWithValue("@ProductType", product.PType);
                connection.Open();
                id = command.ExecuteNonQuery();
                connection.Close();
                if (id > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        //id ye göre getirme
        public Product_ GetById(int productid)
        {
            Product_ product = new Product_();
            using (SqlConnection connection = new SqlConnection(cString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_ProductUpdate";
                command.Parameters.AddWithValue("@productid", productid);
                SqlDataAdapter sqlDa = new SqlDataAdapter(command);
                DataTable dtProducts = new DataTable();
                connection.Open();
                sqlDa.Fill(dtProducts);
                connection.Close();
                foreach (DataRow item in dtProducts.Rows)
                {
                    product.pID = Convert.ToInt32(item["Product_ID"]);
                    product.pName = item["Product_Name"].ToString();
                    product.PType = item["Product_Type"].ToString();
                }
            }

            return product;
        }
        //güncelleme
        public bool UpdateProduct(Product_ product)
        {
            int id;
            using (SqlConnection connection = new SqlConnection(cString))
            {
                SqlCommand command = new SqlCommand("sp_UpdateGetProduct", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@productid", product.pID);
                command.Parameters.AddWithValue("@ProductName", product.pName);
                command.Parameters.AddWithValue("@ProductType", product.PType);
                connection.Open();
                id = command.ExecuteNonQuery();
                connection.Close();
                if (id > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public List<Product_> SearchProduct(string name)
        {
            List<Product_> productlist = new List<Product_>();
            using (SqlConnection connection = new SqlConnection(cString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_SearchProduct";
                command.Parameters.AddWithValue("@productName", name);
                SqlDataAdapter sqlDa = new SqlDataAdapter(command);
                DataTable dtProducts = new DataTable();
                connection.Open();
                sqlDa.Fill(dtProducts);
                connection.Close();
                foreach (DataRow item in dtProducts.Rows)
                {
                    productlist.Add(new Product_
                    {
                        pID = Convert.ToInt32(item["Product_ID"]),
                        pName = item["Product_Name"].ToString(),
                        PType = item["Product_Type"].ToString()
                    });
                }
            }

            return productlist;
        }
        public int Admin_Login(String admin_name, string password)
        {
            int res = 0;
            using (SqlConnection connection = new SqlConnection(cString))
            {
                SqlCommand command = new SqlCommand("Sp_Login", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@password", password);
                command.Parameters.AddWithValue("@UserName", admin_name);
                SqlParameter oblogin = new SqlParameter();
                oblogin.ParameterName = "@Isvalid";
                oblogin.SqlDbType = SqlDbType.Bit;
                oblogin.Direction = ParameterDirection.Output;
                command.Parameters.Add(oblogin);
                connection.Open();
                command.ExecuteNonQuery();
                res = Convert.ToInt32(oblogin.Value);
                return res;
            }
        }

    }
}