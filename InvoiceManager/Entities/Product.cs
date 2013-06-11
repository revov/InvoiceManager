using System;

namespace InvoiceManager.Entities
{
	/// <summary>
	/// Represents a single Product or Service.
	/// </summary>
	public class Product : IBaseEntity
	{
		public object BaseID
		{
			get { return (object)ID; }
		}
		public long ID {get; set;}
		public string PRODUCT_NAME {get; set;}
		public string MEASUREMENT_UNIT {get; set;}
		public decimal PRICE {get; set;}
	}
}
