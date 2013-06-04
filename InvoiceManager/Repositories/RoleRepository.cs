using System;
using System.Configuration;
using System.Data.OleDb;

using InvoiceManager.Entities;

namespace InvoiceManager.Repositories
{
	/// <summary>
	/// Description of RoleRepository.
	/// </summary>
	public class RoleRepository
	{
		public static Role Retrieve(long id)
		{
			const string statement = "select * from ROLES where ID=@id";
            OleDbConnection conn = new OleDbConnection(ConfigurationManager.ConnectionStrings["InvoiceDB"].ConnectionString);
			OleDbCommand cmd = new OleDbCommand(statement, conn);
            cmd.Parameters.AddWithValue("@id", id);

            OleDbDataReader dataReader = null;

            try
            {
                conn.Open();
                dataReader = cmd.ExecuteReader();
                dataReader.Read();
                return new Role
                                {
                					ID = Convert.ToInt64(dataReader["ID"]),
                					WRITE_ACCESS = Convert.ToBoolean(dataReader["WRITE_ACCESS"]),
                					MODIFY_ACCESS = Convert.ToBoolean(dataReader["MODIFY_ACCESS"]),
                                    USER_ACCESS = Convert.ToBoolean(dataReader["USER_ACCESS"])
                                };
            }
            finally
            {
                if (dataReader != null) dataReader.Close();
                conn.Close();
            }
		}
	}
}
