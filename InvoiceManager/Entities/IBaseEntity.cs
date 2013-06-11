/*
 * Created by Stoyan Revov
 * Date: 10.6.2013 г.
 * Time: 17:06 ч.
 */
using System;

namespace InvoiceManager.Entities
{
	/// <summary>
	/// All entities implement this interface.
	/// </summary>
	public interface IBaseEntity
	{
		object BaseID { get; }
	}
}
