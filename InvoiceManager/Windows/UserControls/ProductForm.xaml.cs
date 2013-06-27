/*
 * Created by Stoyan Revov
 * Date: 27.6.2013 г.
 * Time: 15:51 ч.
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

using InvoiceManager.Controller;
using InvoiceManager.Entities;
using InvoiceManager.Services;

namespace InvoiceManager.Windows.UserControls
{
	/// <summary>
	/// Interaction logic for ProductForm.xaml
	/// </summary>
	[EntityEditor(typeof(Product))]
	public partial class ProductForm : UserControl, IForm, IDisposable
	{
		bool isEditing = false;
		
		public ProductForm()
		{
			InitializeComponent();
		}
		
		public ProductForm(Product product)
		{
			isEditing = true;
			InitializeComponent();
			//populate the form
			IdField.Text = product.ID.ToString();
			NameField.Text = product.PRODUCT_NAME;
			MUField.Text = product.MEASUREMENT_UNIT;
			PriceField.Text = product.PRICE.ToString();
		}
		
		public void Dispose()
		{
			ContentManager.RemoveFromParent(this);
		}
		
		public void FiltrateForm()
		{
			NameField.Text = NameField.Text.Trim();
			MUField.Text = MUField.Text.Trim();
			PriceField.Text = PriceField.Text.Trim().Replace('.', ',');
		}
		
		/// <summary>
		/// Validates the data in the input fields.
		/// </summary>
		/// <returns>True, if the form is valid.</returns>
		public bool ValidateForm()
		{
			bool returnValue = true;
			
			#region NameValidation
			if (NameField.Text.Length > 0)
			{
				NameError.Visibility = Visibility.Collapsed;
			}
			else
			{
				returnValue = false;
				NameError.Visibility = Visibility.Visible;
				NameError.Text = "Полето Артикул е задължително за попълване.";
			}
			#endregion
			
			#region MeasurementUnitValidation
			if (MUField.Text.Length > 0)
			{
				MUError.Visibility = Visibility.Collapsed;
			}
			else
			{
				returnValue = false;
				MUError.Visibility = Visibility.Visible;
				MUError.Text = "Мерната единица е задължителна за попълване.";
			}
			#endregion
			
			#region PriceValidation
			decimal price;
			if (decimal.TryParse(PriceField.Text, out price))
			{
				PriceError.Visibility = Visibility.Collapsed;
			}
			else 
			{
				if (PriceField.Text.Length == 0)
				{
					returnValue = false;
					PriceError.Visibility = Visibility.Visible;
					PriceError.Text = "Полето цена е задължително за попълване.";
				}
				else
				{
					returnValue = false;
					PriceError.Visibility = Visibility.Visible;
					PriceError.Text = "Въвели сте невалидна цена.";
				}
			}
			#endregion
			
			return returnValue;
		}
		
		public bool Persist()
		{
			FiltrateForm();
			if (ValidateForm())
			{
				Product p = new Product()
				{
					ID = isEditing ? ulong.Parse(IdField.Text) : 0,
	             	PRODUCT_NAME = NameField.Text,
	             	MEASUREMENT_UNIT = MUField.Text,
	             	PRICE = decimal.Parse(PriceField.Text)
				};
				if (isEditing) return ProductController.Instance.Update(p);
				else return ProductController.Instance.Create(p);
			}
			else
			{
				FocusFirstField();
				throw new ValidationException("Не сте попълнили валидно всички полета.");
			}
		}
		
		public void FocusFirstField()
		{
			NameField.Focus();
		}
		
		protected void PartnerForm_Loaded(object sender, RoutedEventArgs e)
		{
			NameField.Focus();
		}
		
	}
}