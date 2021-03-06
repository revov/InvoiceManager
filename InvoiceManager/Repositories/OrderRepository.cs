﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;

using InvoiceManager.Entities;

namespace InvoiceManager.Repositories
{
	/// <summary>
	/// Orders CRUD.
	/// </summary>
	public class OrderRepository : IRepository<Order>
	{
		static private OleDbConnection conn;
		static OrderRepository()
		{
			conn = new OleDbConnection(ConfigurationManager.ConnectionStrings["InvoiceDB"].ConnectionString);
		}
		
		/// <summary>
        /// Attempts to add an order to the Acess database.
        /// </summary>
        public void Create(Order order)
        {
            const string statement = @"
                            insert into ORDERS (
                                INVOICE_ID,
                                PRODUCT_ID,
                                QUANTITY,
                                VAT,
								DISCOUNT)
                            values (
                                @invoice_id,
                                @product_id,
                                @quantity,
                                @vat,
                                @discount)";

            OleDbCommand cmd = new OleDbCommand(statement, conn);

            cmd.Parameters.AddWithValue("@invoice_id", order.INVOICE_ID);
            cmd.Parameters.AddWithValue("@product_id", order.PRODUCT_ID);
            cmd.Parameters.AddWithValue("@quantity", order.QUANTITY);
            cmd.Parameters.AddWithValue("@vat", order.VAT);
            cmd.Parameters.AddWithValue("@discount", order.DISCOUNT);

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
        /// USE WITH CAUTION!
        /// Deletes an order from the database. If there are invoices or products with the order, the procedure cannot be completed.
        /// </summary>
        public void Delete(object baseId)
        {
        	string invoice_id = (string)(((object[])baseId)[0]);
        	ulong product_id = (ulong)(((object[])baseId)[1]);
        	
            const string statement = "delete from ORDERS where INVOICE_ID=@invoice_id and PRODUCT_ID=@product_id";
            OleDbCommand cmd = new OleDbCommand(statement, conn);

            cmd.Parameters.AddWithValue("@invoice_id", invoice_id);
            cmd.Parameters.AddWithValue("@product_id", product_id);

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
        /// Attempts to edit an existing order.
        /// </summary>
        public void Update(Order order)
        {
            const string statement = @"update ORDERS
                                    set QUANTITY=@quantity, VAT=@vat, DISCOUNT=@discount
                                    where INVOICE_ID=@invoice_id and PRODUCT_ID=@product_id";
            OleDbCommand cmd = new OleDbCommand(statement, conn);

            cmd.Parameters.AddWithValue("@invoice_id", order.INVOICE_ID);
            cmd.Parameters.AddWithValue("@product_id", order.PRODUCT_ID);
            cmd.Parameters.AddWithValue("@quantity", order.QUANTITY);
            cmd.Parameters.AddWithValue("@vat", order.VAT);
            cmd.Parameters.AddWithValue("@discount", order.DISCOUNT);

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
        /// Gets an order by ID.
        /// </summary>
        public Order Retrieve(object baseId)
        {
        	string invoice_id = (string)(((object[])baseId)[0]);
        	ulong product_id = (ulong)(((object[])baseId)[1]);
        	
            const string statement = "select * from ORDERS where INVOICE_ID=@invoice_id and PRODUCT_ID=@product_id";
            OleDbCommand cmd = new OleDbCommand(statement, conn);
            cmd.Parameters.AddWithValue("@invoice_id", invoice_id);
            cmd.Parameters.AddWithValue("@product_id", product_id);

            OleDbDataReader dataReader = null;

            try
            {
                conn.Open();
                dataReader = cmd.ExecuteReader();
                if (!dataReader.HasRows) return null;
                dataReader.Read();
                return new Order
                                {
                					INVOICE_ID = dataReader["INVOICE_ID"].ToString(),
                					PRODUCT_ID = ulong.Parse(dataReader["PRODUCT_ID"].ToString()),
                					QUANTITY = double.Parse(dataReader["QUANTITY"].ToString()),
                					VAT = double.Parse(dataReader["VAT"].ToString()),
                                    DISCOUNT = double.Parse(dataReader["DISCOUNT"].ToString())
                                };
            }
            finally
            {
                if (dataReader != null) dataReader.Close();
                conn.Close();
            }
        }
        
        /// <summary>
        /// Gets all orders from the database.
        /// </summary>
        /// <returns>List of orders.</returns>
        public List<Order> RetrieveAll()
        {
        	const string statement = "select * from ORDERS";
            OleDbCommand cmd = new OleDbCommand(statement, conn);

            OleDbDataReader dataReader = null;

            try
            {
                conn.Open();
                dataReader = cmd.ExecuteReader();
                if (dataReader.HasRows)
                {
                	List<Order> orders = new List<Order>();
                	while (dataReader.Read())
                	{
                		orders.Add(new Order
                		             {
                		             	INVOICE_ID = dataReader["INVOICE_ID"].ToString(),
	                					PRODUCT_ID = ulong.Parse(dataReader["PRODUCT_ID"].ToString()),
	                					QUANTITY = double.Parse(dataReader["QUANTITY"].ToString()),
	                					VAT = double.Parse(dataReader["VAT"].ToString()),
	                                    DISCOUNT = double.Parse(dataReader["DISCOUNT"].ToString())
                		             });
                	}
                	return orders;
                                
                }
                else return new List<Order>();
               
            }
            finally
            {
                if (dataReader != null) dataReader.Close();
                conn.Close();
            }
        }
        
	}
}
