using System;

namespace InvoiceManager.Entities
{
	/// <summary>
	/// Represents a customer or supplier account.
	/// </summary>
	public class Partner
	{
		public string ID {get; set;}
		public string VAT_NUMBER {get; set;}
		public string PARTNER_NAME {get; set;}
		public string ADDRESS {get; set;}
		public int POST_CODE {get; set;}
		public string ADDITIONAL_INFO {get; set;}
	}
}
