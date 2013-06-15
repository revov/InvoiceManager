using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

using InvoiceManager.Controller;
using InvoiceManager.Entities;

namespace InvoiceManager.Windows.UserControls
{
	/// <summary>
	/// Interaction logic for PartnerForm.xaml
	/// </summary>
	[EntityEditor(typeof(Partner))]
	public partial class PartnerForm : UserControl, IForm
	{
		bool isEditing = false;
		
		public PartnerForm()
		{
			InitializeComponent();
		}
		
		public PartnerForm(Partner partner)
		{
			isEditing = true;
			InitializeComponent();
			//populate the form
			IdField.Text = partner.ID;
			IdField.IsEnabled = false;
			VatNField.Text = partner.VAT_NUMBER;
			NameField.Text = partner.PARTNER_NAME;
			AddressField.Text = partner.ADDRESS;
			PostCodeField.Text = partner.POST_CODE.ToString();
			AdditionalInfoField.Text = partner.ADDITIONAL_INFO;
		}
		
		public void FiltrateForm()
		{
			IdField.Text = IdField.Text.Trim().ToUpper();
			VatNField.Text = VatNField.Text.Trim().ToUpper();
			NameField.Text = NameField.Text.Trim();
			AddressField.Text = AddressField.Text.Trim();
			PostCodeField.Text = PostCodeField.Text.Trim();
			AdditionalInfoField.Text = AdditionalInfoField.Text.Trim();
		}
		
		/// <summary>
		/// Validates the data in the input fields.
		/// </summary>
		/// <returns>True, if the form is valid.</returns>
		public bool ValidateForm()
		{
			bool returnValue = true;
			
			//TODO: True ЕИК and ЕГН validation
			#region idValidation
			if (IdField.Text.Length == 9 || IdField.Text.Length == 10 || IdField.Text.Length == 13)
			{
				IdError.Visibility = Visibility.Collapsed;
			}
			else 
			{
				if (IdField.Text.Length == 0)
				{
					returnValue = false;
					IdError.Visibility = Visibility.Visible;
					IdError.Text = "Полето ЕИК/ЕГН е задължително за попълване.";
				}
				else
				{
					returnValue = false;
					IdError.Visibility = Visibility.Visible;
					IdError.Text = "Въвели сте невалидно ЕИК/ЕГН.";
				}
			}
			#endregion
			
			#region VATNumberValidation
			if (VatNField.Text.Length == 11 || VatNField.Text.Length == 15 || VatNField.Text.Length == 0)
			{
				VatNError.Visibility = Visibility.Collapsed;
			}
			else 
			{
				returnValue = false;
				VatNError.Visibility = Visibility.Visible;
				VatNError.Text = "Въвели сте невалиден ИН по ЗДДС";
			}
			#endregion
			
			#region NameValidation
			if (NameField.Text.Length > 0)
			{
				NameError.Visibility = Visibility.Collapsed;
			}
			else
			{
				returnValue = false;
				NameError.Visibility = Visibility.Visible;
				NameError.Text = "Полето име е задължително за попълване.";
			}
			#endregion
			
			#region AddressValidation
			if (AddressField.Text.Length > 0)
			{
				AddressError.Visibility = Visibility.Collapsed;
			}
			else
			{
				returnValue = false;
				AddressError.Visibility = Visibility.Visible;
				AddressError.Text = "Полето адрес е задължително за попълване.";
			}
			#endregion
			
			#region PostCodeValidation
			if (Regex.IsMatch(PostCodeField.Text, "^[1-9]\\d{3}$"))
			{
				PostCodeError.Visibility = Visibility.Collapsed;
			}
			else 
			{
				if (PostCodeField.Text.Length == 0)
				{
					returnValue = false;
					PostCodeError.Visibility = Visibility.Visible;
					PostCodeError.Text = "Полето пощенски код е задължително за попълване.";
				}
				else
				{
					returnValue = false;
					PostCodeError.Visibility = Visibility.Visible;
					PostCodeError.Text = "Въвели сте невалиден пощенски код";
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
				Partner p = new Partner()
				{
	             	ID = IdField.Text,
	             	VAT_NUMBER = VatNField.Text,
	             	PARTNER_NAME = NameField.Text,
	             	ADDRESS = AddressField.Text,
	             	POST_CODE = Int32.Parse(PostCodeField.Text),
	             	ADDITIONAL_INFO = AdditionalInfoField.Text
				};
				if (isEditing) return PartnerController.Instance.Update(p);
				else return PartnerController.Instance.Create(p);
			}
			else
			{
				FocusFirstField();
				throw new ValidationException("Не сте попълнили валидно всички полета.");
			}
		}
		
		public void FocusFirstField()
		{
			IdField.Focus();
		}
		
		protected void PartnerForm_Loaded(object sender, RoutedEventArgs e)
		{
			IdField.Focus();
		}
		
	}
}