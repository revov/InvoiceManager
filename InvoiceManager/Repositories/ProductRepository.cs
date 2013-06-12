using System;
using System.Data.OleDb;
using System.Collections.Generic;
using System.Configuration;

using InvoiceManager.Entities;

namespace InvoiceManager.Repositories
{
	/// <summary>
	/// Parties CRUD.
	/// </summary>
	public class ProductRepository : IRepository<Product>
	{
		static private OleDbConnection conn;
		static ProductRepository()
		{
			conn = new OleDbConnection(ConfigurationManager.ConnectionStrings["InvoiceDB"].ConnectionString);
		}
		
		/// <summary>
        /// Attempts to add a product to the Acess database.
        /// </summary>
        public void Create(Product product)
        {
            const string statement = @"
                            insert into PRODUCTS (
                                ID,
                                PRODUCT_NAME,
                                MEASUREMENT_UNIT,
                                PRICE)
                            values (
                                @id,
                                @product_name,
                                @measurement_unit,
                                @price)";

            OleDbCommand cmd = new OleDbCommand(statement, conn);

            cmd.Parameters.AddWithValue("@id", product.ID);
            cmd.Parameters.AddWithValue("@product_name", product.PRODUCT_NAME);
            cmd.Parameters.AddWithValue("@measurement_unit", product.MEASUREMENT_UNIT);
            cmd.Parameters.AddWithValue("@price", product.PRICE);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Deletes a product from the database. If there are orders with the product, the procedure cannot be completed.
        /// </summary>
        public void Delete(object baseId)
        {
        	ulong id = (ulong)baseId;
        	
            const string statement = "delete from PRODUCTS where ID=@id";
            OleDbCommand cmd = new OleDbCommand(statement, conn);

            cmd.Parameters.AddWithValue("@id", id);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Attempts to edit an existing product.
        /// </summary>
        public void Update(Product product)
        {
            const string statement = @"update PRODUCTS
                                    set PRODUCT_NAME=@product_name, MEASUREMENT_UNIT=@measurement_unit, PRICE=@price
                                    where ID=@id";
            OleDbCommand cmd = new OleDbCommand(statement, conn);

            cmd.Parameters.AddWithValue("@product_name", product.PRODUCT_NAME);
            cmd.Parameters.AddWithValue("@measurement_unit", product.MEASUREMENT_UNIT);
            cmd.Parameters.AddWithValue("@price", product.PRICE);
            cmd.Parameters.AddWithValue("@id", product.ID);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Gets a product by ID.
        /// </summary>
        public Product Retrieve(object baseId)
        {
        	ulong id = (ulong)baseId;
        	
            const string statement = "select * from PRODUCTS where ID=@id";
            OleDbCommand cmd = new OleDbCommand(statement, conn);
            cmd.Parameters.AddWithValue("@id", id);

            OleDbDataReader dataReader = null;

            try
            {
                conn.Open();
                dataReader = cmd.ExecuteReader();
                dataReader.Read();
                return new Product
                                {
                					ID = ulong.Parse(dataReader["PRICE"].ToString()),
                                    PRODUCT_NAME = dataReader["PRODUCT_NAME"].ToString(),
                                    MEASUREMENT_UNIT = dataReader["MEASUREMENT_UNIT"].ToString(),
                                    PRICE = Convert.ToInt32(dataReader["PRICE"])
                                };
            }
            finally
            {
                if (dataReader != null) dataReader.Close();
                conn.Close();
            }
        }
        
        /// <summary>
        /// Gets all products from the database.
        /// </summary>
        /// <returns>List of partners.</returns>
        public List<Product> RetrieveAll()
        {
        	const string statement = "select * from PRODUCTS";
            OleDbCommand cmd = new OleDbCommand(statement, conn);

            OleDbDataReader dataReader = null;

            try
            {
                conn.Open();
                dataReader = cmd.ExecuteReader();
                if (dataReader.HasRows)
                {
                	List<Product> products = new List<Product>();
                	while (dataReader.Read())
                	{
                		products.Add(new Product
                		             {
                		             	ID = ulong.Parse(dataReader["PRICE"].ToString()),
	                                    PRODUCT_NAME = dataReader["PRODUCT_NAME"].ToString(),
	                                    MEASUREMENT_UNIT = dataReader["MEASUREMENT_UNIT"].ToString(),
	                                    PRICE = Convert.ToInt32(dataReader["PRICE"])
                		             });
                	}
                	return products;
                                
                }
                else return new List<Product>();
               
            }
            finally
            {
                if (dataReader != null) dataReader.Close();
                conn.Close();
            }
        }
	}
}
