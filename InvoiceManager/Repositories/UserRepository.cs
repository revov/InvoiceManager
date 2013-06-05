using System;
using System.Data.OleDb;
using System.Collections.Generic;
using System.Configuration;

using InvoiceManager.Entities;

namespace InvoiceManager.Repositories
{
	/// <summary>
	/// Users CRUD.
	/// </summary>
	public class UserRepository
	{
		static private OleDbConnection conn;
		static UserRepository()
		{
			conn = new OleDbConnection(ConfigurationManager.ConnectionStrings["InvoiceDB"].ConnectionString);
		}
		
		/// <summary>
        /// Attempts to add an user to the Acess database.
        /// </summary>
        public static void Create(User user)
        {
            const string statement = @"
                            insert into USERS (
                                ID,
                                PASSWORD,
                                ROLE_ID,
                                SELLER_ID)
                            values (
                                @id,
                                @password,
                                @role_id,
                                @seller_id)";

            OleDbCommand cmd = new OleDbCommand(statement, conn);

            cmd.Parameters.AddWithValue("@id", user.ID);
            cmd.Parameters.AddWithValue("@password", user.PASSWORD);
            cmd.Parameters.AddWithValue("@role_id", user.ROLE_ID);
            cmd.Parameters.AddWithValue("@seller_id", user.SELLER_ID);

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
        /// Attempts to delete an user from the database.
        /// </summary>
        public static void Delete(string id)
        {
            const string statement = "delete from USERS where ID=@id";
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
        /// Attempts to edit an existing user.
        /// </summary>
        public static void Update(User user)
        {
            const string statement = @"update USERS
                                    set PASSWORD=@password, ROLE_ID=@role_id, SELLER_ID=@seller_id
                                    where ID=@id";
            OleDbCommand cmd = new OleDbCommand(statement, conn);

            cmd.Parameters.AddWithValue("@password", user.PASSWORD);
            cmd.Parameters.AddWithValue("@role_id", user.ROLE_ID);
            cmd.Parameters.AddWithValue("@seller_id", user.SELLER_ID);
            cmd.Parameters.AddWithValue("@id", user.ID);

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
        /// Gets an user by ID.
        /// </summary>
        public static User Retrieve(string id)
        {
            const string statement = "select * from USERS where ID=@id";
            OleDbCommand cmd = new OleDbCommand(statement, conn);
            cmd.Parameters.AddWithValue("@id", id);

            OleDbDataReader dataReader = null;

            try
            {
                conn.Open();
                dataReader = cmd.ExecuteReader();
                dataReader.Read();
                return new User
                                {
                					ID = dataReader["ID"].ToString(),
                                    PASSWORD = dataReader["PASSWORD"].ToString(),
                                    ROLE_ID = Convert.ToInt64(dataReader["ROLE_ID"]),
                                    SELLER_ID = dataReader["SELLER_ID"].ToString()
                                };
            }
            finally
            {
                if (dataReader != null) dataReader.Close();
                conn.Close();
            }
        }
        
        /// <summary>
        /// Gets all users from the database.
        /// </summary>
        /// <returns>List of users.</returns>
        public static List<User> RetrieveAll()
        {
        	const string statement = "select * from USERS";
            OleDbCommand cmd = new OleDbCommand(statement, conn);

            OleDbDataReader dataReader = null;

            try
            {
                conn.Open();
                dataReader = cmd.ExecuteReader();
                if (dataReader.HasRows)
                {
                	List<User> users = new List<User>();
                	while (dataReader.Read())
                	{
                		users.Add(new User
                		             {
                		             	ID = dataReader["ID"].ToString(),
	                                    PASSWORD = dataReader["PASSWORD"].ToString(),
	                                    ROLE_ID = Convert.ToInt64(dataReader["ROLE_ID"]),
	                                    SELLER_ID = dataReader["SELLER_ID"].ToString()
                		             });
                	}
                	return users;
                                
                }
                else return new List<User>();
               
            }
            finally
            {
                if (dataReader != null) dataReader.Close();
                conn.Close();
            }
        }
	}
}
