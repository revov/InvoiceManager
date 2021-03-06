﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;

using InvoiceManager.Entities;

namespace InvoiceManager.Repositories
{
	/// <summary>
	/// Partners CRUD.
	/// </summary>
	public class PartnerRepository : IRepository<Partner>
	{
		static private OleDbConnection conn;
		static PartnerRepository()
		{
			conn = new OleDbConnection(ConfigurationManager.ConnectionStrings["InvoiceDB"].ConnectionString);
		}
		
		/// <summary>
        /// Attempts to add a partner to the Acess database.
        /// </summary>
        public void Create(Partner partner)
        {
            const string statement = @"
                            insert into PARTNERS (
                                ID,
                                VAT_NUMBER,
                                PARTNER_NAME,
                                ADDRESS,
								POST_CODE,
								ADDITIONAL_INFO)
                            values (
                                @id,
                                @vat_number,
                                @partner_name,
                                @address,
                                @post_code,
								@additional_info)";

            OleDbCommand cmd = new OleDbCommand(statement, conn);

            cmd.Parameters.AddWithValue("@id", partner.ID);
            cmd.Parameters.AddWithValue("@vat_number", partner.VAT_NUMBER);
            cmd.Parameters.AddWithValue("@partner_name", partner.PARTNER_NAME);
            cmd.Parameters.AddWithValue("@address", partner.ADDRESS);
            cmd.Parameters.AddWithValue("@post_code", partner.POST_CODE);
            cmd.Parameters.AddWithValue("@additional_info", partner.ADDITIONAL_INFO);

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
        /// Deletes a partner from the database. If there are invoices with the partner, the procedure cannot be completed.
        /// </summary>
        public void Delete(object baseId)
        {
        	string id = (string)baseId;
        	
            const string statement = "delete from PARTNERS where ID=@id";
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
        /// Attempts to edit an existing partner.
        /// </summary>
        public void Update(Partner partner)
        {
            const string statement = @"update PARTNERS
                                    set VAT_NUMBER=@vat_number, PARTNER_NAME=@partner_name, ADDRESS=@address, POST_CODE=@post_code, ADDITIONAL_INFO=@additional_info
                                    where ID=@id";
            OleDbCommand cmd = new OleDbCommand(statement, conn);

            cmd.Parameters.AddWithValue("@vat_number", partner.VAT_NUMBER);
            cmd.Parameters.AddWithValue("@partner_name", partner.PARTNER_NAME);
            cmd.Parameters.AddWithValue("@address", partner.ADDRESS);
            cmd.Parameters.AddWithValue("@post_code", partner.POST_CODE);
            cmd.Parameters.AddWithValue("@additional_info", partner.ADDITIONAL_INFO);
            cmd.Parameters.AddWithValue("@id", partner.ID);

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
        /// Gets a partner by ID.
        /// </summary>
        public Partner Retrieve(object baseId)
        {
        	string id = (string)baseId;
        	
            const string statement = "select * from PARTNERS where ID=@id";
            OleDbCommand cmd = new OleDbCommand(statement, conn);
            cmd.Parameters.AddWithValue("@id", id);

            OleDbDataReader dataReader = null;

            try
            {
                conn.Open();
                dataReader = cmd.ExecuteReader();
                if (!dataReader.HasRows) return null;
                dataReader.Read();
                return new Partner
                                {
                					ID = dataReader["ID"].ToString(),
                                    VAT_NUMBER = dataReader["VAT_NUMBER"].ToString(),
                                    PARTNER_NAME = dataReader["PARTNER_NAME"].ToString(),
                                    ADDRESS = dataReader["ADDRESS"].ToString(),
                                    POST_CODE = Int32.Parse(dataReader["POST_CODE"].ToString()),
                                    ADDITIONAL_INFO = dataReader["ADDITIONAL_INFO"].ToString()
                                };
            }
            finally
            {
                if (dataReader != null) dataReader.Close();
                conn.Close();
            }
        }
        
        /// <summary>
        /// Gets all partners from the database.
        /// </summary>
        /// <returns>List of partners.</returns>
        public List<Partner> RetrieveAll()
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
                	List<Partner> partners = new List<Partner>();
                	while (dataReader.Read())
                	{
                		partners.Add(new Partner
                		             {
                		             	ID = dataReader["ID"].ToString(),
	                                    VAT_NUMBER = dataReader["VAT_NUMBER"].ToString(),
	                                    PARTNER_NAME = dataReader["PARTNER_NAME"].ToString(),
	                                    ADDRESS = dataReader["ADDRESS"].ToString(),
	                                    POST_CODE = Int32.Parse(dataReader["POST_CODE"].ToString()),
	                                    ADDITIONAL_INFO = dataReader["ADDITIONAL_INFO"].ToString()
                		             });
                	}
                	return partners;
                                
                }
                else return new List<Partner>();
               
            }
            finally
            {
                if (dataReader != null) dataReader.Close();
                conn.Close();
            }
        }
        
	}
}
