using System;

namespace InvoiceManager.Entities
{
	/// <summary>
	/// An invoice contains information about its orders and parties.
	/// </summary>
	public class Invoice : IEntity
	{
		public object BaseID
		{
			get { return (object)ID; }
		}
		public string ID {get; set;}
		public string SELLER_ID {get; set;}
		public string CUSTOMER_ID {get; set;}
		
		public DateTime INVOICE_DATE {get; set;}
		public DateTime FISCAL_EVENT_DATE {get; set;}
		/// <summary>
		/// True if the invoice has no pending payemnt
		/// </summary>
		public bool PAYMENT_COMPLETED {get; set;}
	}
}
