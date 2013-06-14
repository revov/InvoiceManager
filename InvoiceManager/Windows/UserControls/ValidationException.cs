/*
 * Created by Stoyan Revov
 * Date: 15.6.2013 г.
 * Time: 01:40 ч.
 */
using System;

namespace InvoiceManager.Windows.UserControls
{
	/// <summary>
	/// Thrown when a form fails validation when hydrating an object
	/// </summary>
	public class ValidationException : Exception
	{
		public ValidationException(string message): base(message)
		{
		}
	}
}
