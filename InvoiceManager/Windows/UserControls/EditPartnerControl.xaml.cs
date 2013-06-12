using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Text;
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
	/// Wrapper for the AddPartnerControl
	/// </summary>
	public partial class EditPartnerControl : UserControl
	{
		public EditPartnerControl(Partner partner)
		{
			InitializeComponent();
			//Populate the form
			partnerForm.IdField.Text = partner.ID;
			partnerForm.IdField.IsEnabled = false;
			partnerForm.VatNField.Text = partner.VAT_NUMBER;
			partnerForm.NameField.Text = partner.PARTNER_NAME;
			partnerForm.AddressField.Text = partner.ADDRESS;
			partnerForm.PostCodeField.Text = partner.POST_CODE.ToString();
			partnerForm.AdditionalInfoField.Text = partner.ADDITIONAL_INFO;
		}
		
		protected void EditPartnerButton_Click(object sender, RoutedEventArgs e)
		{
			partnerForm.FiltrateForm();
			if (partnerForm.ValidateForm())
			{
				try
				{
					Partner partner = new Partner
					                         {
					                         	ID = partnerForm.IdField.Text,
					                         	VAT_NUMBER = partnerForm.VatNField.Text,
					                         	PARTNER_NAME = partnerForm.NameField.Text,
					                         	ADDRESS = partnerForm.AddressField.Text,
					                         	POST_CODE = Int32.Parse(partnerForm.PostCodeField.Text),
					                         	ADDITIONAL_INFO = partnerForm.AdditionalInfoField.Text
					                         };
					IRepository<Partner> partnerRepository = RepositoryFactory<Partner>.Initialize();
					partnerRepository.Update(partner);
					//If an exception is not thrown:
					Logger.Log("Редактиран контрагент " + partner.ID);
					ContentManager.PrintStatus("Редактирането успешно");
					ContentManager.RemoveFromParent(this);
				}
				catch (OleDbException ex)
				{
					MessageBox.Show("Възникна грешка при редактирането на контрагент:" +
					                Environment.NewLine + Environment.NewLine + ex.Message,
					                "Грешка.", MessageBoxButton.OK,MessageBoxImage.Error);
					partnerForm.IdField.Focus();
					ContentManager.PrintStatus(ex.Message, Brushes.Red);
				}
			}
			else partnerForm.IdField.Focus();
		}
		
		protected void CancelPartnerButton_Click(object sender, RoutedEventArgs e)
		{
			ContentManager.RemoveFromParent(this);
		}
		
	}
}