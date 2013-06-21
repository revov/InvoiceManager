using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;

using InvoiceManager.Entities;

namespace InvoiceManager.Repositories
{
	/// <summary>
	/// Invoices CRUD.
	/// </summary>
	public class InvoiceRepository : IRepository<Invoice>
	{
		static private OleDbConnection conn;
		static InvoiceRepository()
		{
			conn = new OleDbConnection(ConfigurationManager.ConnectionStrings["InvoiceDB"].ConnectionString);
		}
		
		/// <summary>
        /// Attempts to add a partner to the Acess database.
        /// </summary>
        public void Create(Invoice invoice)
        {
            const string statement = @"
                            insert into INVOICES (
                                ID,
                                SELLER_ID,
                                CUSTOMER_ID,
                                INVOICE_DATE,
								FISCAL_EVENT_DATE,
								PAYMENT_COMPLETED)
                            values (
                                @id,
                                @seller_id,
                                @customer_id,
                                @invoice_date,
                                @fiscal_event_date,
								@payment_completed)";

            OleDbCommand cmd = new OleDbCommand(statement, conn);

            cmd.Parameters.AddWithValue("@id", invoice.ID);
            cmd.Parameters.AddWithValue("@seller_id", invoice.SELLER_ID);
            cmd.Parameters.AddWithValue("@customer_id", invoice.CUSTOMER_ID);
            cmd.Parameters.AddWithValue("@invoice_date", invoice.INVOICE_DATE);
            cmd.Parameters.AddWithValue("@fiscal_event_date", invoice.FISCAL_EVENT_DATE);
            cmd.Parameters.AddWithValue("@payment_completed", invoice.PAYMENT_COMPLETED);

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
        /// Deletes an invoice from the database. If there are orders in this invoice, the procedure cannot be completed.
        /// </summary>
        public void Delete(object baseId)
        {
        	string id = (string)baseId;
        	
            const string statement = "delete from INVOICES where ID=@id";
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
        /// Attempts to edit an existing invoice.
        /// </summary>
        public void Update(Invoice invoice)
        {
            const string statement = @"update INVOICES
                                    set SELLER_ID=@seller_id, CUSTOMER_ID=@customer_id, INVOICE_DATE=@invoice_date, FISCAL_EVENT_DATE=@fiscal_event_date, PAYMENT_COMPLETED=@payment_completed
                                    where ID=@id";
            OleDbCommand cmd = new OleDbCommand(statement, conn);

            cmd.Parameters.AddWithValue("@seller_id", invoice.SELLER_ID);
            cmd.Parameters.AddWithValue("@customer_id", invoice.CUSTOMER_ID);
            cmd.Parameters.AddWithValue("@invoice_date", invoice.INVOICE_DATE);
            cmd.Parameters.AddWithValue("@fiscal_event_date", invoice.FISCAL_EVENT_DATE);
            cmd.Parameters.AddWithValue("@payment_completed", invoice.PAYMENT_COMPLETED);
            cmd.Parameters.AddWithValue("@id", invoice.ID);

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
        /// Gets an invoice by ID.
        /// </summary>
        public Invoice Retrieve(object baseId)
        {
        	string id = (string)baseId;
        	
            const string statement = "select * from INVOICES where ID=@id";
            OleDbCommand cmd = new OleDbCommand(statement, conn);
            cmd.Parameters.AddWithValue("@id", id);

            OleDbDataReader dataReader = null;

            try
            {
                conn.Open();
                dataReader = cmd.ExecuteReader();
                if (!dataReader.HasRows) return null;
                dataReader.Read();
                return new Invoice
                                {
                					ID = dataReader["ID"].ToString(),
                                    SELLER_ID = dataReader["CUSTOMER_ID"].ToString(),
                                    CUSTOMER_ID = dataReader["PARTNER_NAME"].ToString(),
                                    INVOICE_DATE = Convert.ToDateTime(dataReader["INVOICE_DATE"]),
                                    FISCAL_EVENT_DATE = Convert.ToDateTime(dataReader["FISCAL_EVENT_DATE"]),
                                    PAYMENT_COMPLETED = Convert.ToBoolean(dataReader["PAYMENT_COMPLETED"])
                                };
            }
            finally
            {
                if (dataReader != null) dataReader.Close();
                conn.Close();
            }
        }
        
        /// <summary>
        /// Gets all invoices from the database.
        /// </summary>
        /// <returns>List of invoices.</returns>
        public List<Invoice> RetrieveAll()
        {
        	const string statement = "select * from PARTNERS";
            OleDbCommand cmd = new OleDbCommand(statement, conn);

            OleDbDataReader dataReader = null;

            try
            {
                conn.Open();
                dataReader = cmd.ExecuteReader();
                if (dataReader.HasRows)
                {
                	List<Invoice> invoices = new List<Invoice>();
                	while (dataReader.Read())
                	{
                		invoices.Add(new Invoice
                		             {
                		             	ID = dataReader["ID"].ToString(),
	                                    SELLER_ID = dataReader["CUSTOMER_ID"].ToString(),
	                                    CUSTOMER_ID = dataReader["PARTNER_NAME"].ToString(),
	                                    INVOICE_DATE = Convert.ToDateTime(dataReader["INVOICE_DATE"]),
	                                    FISCAL_EVENT_DATE = Convert.ToDateTime(dataReader["FISCAL_EVENT_DATE"]),
	                                    PAYMENT_COMPLETED = Convert.ToBoolean(dataReader["PAYMENT_COMPLETED"])
                		             });
                	}
                	return invoices;
                                
                }
                else return new List<Invoice>();
               
            }
            finally
            {
                if (dataReader != null) dataReader.Close();
                conn.Close();
            }
        }
        
	}
}
