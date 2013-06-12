using System;

namespace InvoiceManager.Entities
{
	/// <summary>
	/// Describes the access level of the user.
	/// </summary>
	public class Role : IEntity
	{
		public object BaseID
		{
			get { return (object)ID; }
		}
		public long ID {get; set;}
		public bool WRITE_ACCESS {get; set;}
		public bool MODIFY_ACCESS {get; set;}
		public bool USER_ACCESS {get; set;}
	}
}
