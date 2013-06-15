/*
 * Created by Stoyan Revov
 * Date: 14.6.2013 г.
 * Time: 23:50 ч.
 */
using System;
using System.Windows;
using System.Windows.Controls;

using InvoiceManager.Entities;

namespace InvoiceManager.Windows.UserControls
{
	/// <summary>
	/// Represents a form that can edit a single type of IEntity. Defines methods for validation and filtration. 
	/// </summary>
	public interface IForm
	{
		void FiltrateForm();
		bool ValidateForm();
		bool Persist();
		void FocusFirstField();
	}
}
