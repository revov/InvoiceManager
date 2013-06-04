/*
 * Created by SharpDevelop.
 * User: reuf
 * Date: 2.6.2013 г.
 * Time: 15:09 ч.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

using InvoiceManager.Entities;
using InvoiceManager.Repositories;
using InvoiceManager.Services;

namespace InvoiceManager.Windows.UserControls
{
	/// <summary>
	/// Interaction logic for AddPartnerControl.xaml
	/// </summary>
	public partial class AddPartnerControl : UserControl
	{
		public AddPartnerControl()
		{
			InitializeComponent();
		}
		
		protected void FiltrateForm()
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
		protected bool ValidateForm()
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
		
		protected void AddPartnerButton_Click(object sender, RoutedEventArgs e)
		{
			FiltrateForm();
			if (ValidateForm())
			{
				try
				{
					Partner partner = new Partner
					                         {
					                         	ID = IdField.Text,
					                         	VAT_NUMBER = VatNField.Text,
					                         	PARTNER_NAME = NameField.Text,
					                         	ADDRESS = AddressField.Text,
					                         	POST_CODE = Int32.Parse(PostCodeField.Text),
					                         	ADDITIONAL_INFO = AdditionalInfoField.Text
					                         };
					PartnerRepository.Create(partner);
					//If an exception is not thrown:
					Logger.Log("Добавен контрагент " + partner.ID);
					ContentManager.PrintStatus("Добавянето успешно");
					ContentManager.RemoveFromParent(this);
				}
				catch (OleDbException ex)
				{
					MessageBox.Show("Възникна грешка при добавянето на контрагент:" +
					                Environment.NewLine + Environment.NewLine + ex.Message,
					                "Грешка.", MessageBoxButton.OK,MessageBoxImage.Error);
					IdField.Focus();
					ContentManager.PrintStatus(ex.Message, Brushes.Red);
				}
			}
			else IdField.Focus();
		}
		
		protected void CancelPartnerButton_Click(object sender, RoutedEventArgs e)
		{
			ContentManager.RemoveFromParent(this);
		}
		
		protected void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
			IdField.Focus();
		}
	}
}