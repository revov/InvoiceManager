using System;
using InvoiceManager.Repositories;

namespace InvoiceManager.Entities
{
	/// <summary>
	/// Represents a single User.
	/// </summary>
	public class User : IEntity
	{
		public object BaseID
		{
			get { return (object)ID; }
		}
		public string ID {get; set;}
		public string PASSWORD {get; set;}
		public long ROLE_ID {get; set;}
		public string Role
		{
			get
			{
				switch (ROLE_ID)
				{
					case 1 : return "Администратор";
					case 2 : return "Счетоводител";
					case 3 : return "Одитор";
					default: return "";
				}
			}
		}
		public string SELLER_ID {get; set;}
		public string Seller
		{
			get
			{
				//TODO:PERFORMANCE issues. Make use of DataAdapter and local DataSet
				PartnerRepository repo = new PartnerRepository();
				Partner seller = repo.Retrieve(SELLER_ID);
				return (seller==null) ? "" : seller.PARTNER_NAME;
			}
		}
	}
}
