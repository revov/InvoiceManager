using System;

namespace InvoiceManager.Entities
{
	/// <summary>
	/// Represents a single User.
	/// </summary>
	public class User : IBaseEntity
	{
		public object BaseID
		{
			get { return (object)ID; }
		}
		public string ID {get; set;}
		public string PASSWORD {get; set;}
		public long ROLE_ID {get; set;}
		public string SELLER_ID {get; set;}
	}
}
