using System;

namespace InvoiceManager.Entities
{
	/// <summary>
	/// Represents Quantity, VAT and discount for single Product in a single Invoice.
	/// </summary>
	public class Order
	{
		public string INVOICE_ID {get; set;}
		public long PRODUCT_ID {get; set;}
		public double QUANTITY {get; set;}
		/// <summary>
		/// Value Added Tax measured in percents.
		/// </summary>
		public double VAT {get; set;}
		/// <summary>
		/// Discount measured in percents.
		/// </summary>
		public double DISCOUNT {get; set;}
	}
	
	/*
	 * Will be used for compatibility issues with the IBaseRepository.
	public struct OrderKey
	{
		string INVOICE_ID;
		long PRODUCT_ID;
	}
	*/
}
