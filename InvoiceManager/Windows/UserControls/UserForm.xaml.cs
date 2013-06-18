using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

using InvoiceManager.Controller;
using InvoiceManager.Entities;
using InvoiceManager.Services;

namespace InvoiceManager.Windows.UserControls
{
	/// <summary>
	/// Interaction logic for PartnerForm.xaml
	/// </summary>
	[EntityEditor(typeof(User))]
	public partial class UserForm : UserControl, IForm
	{
		bool isEditing = false;
		
		public UserForm()
		{
			ContentManager.EntityChosen += (sender, e) => {if (sender is Partner) SellerField.Text = ((Partner)sender).ID;};
			InitializeComponent();
			usersPopup.Child = new DataBrowserControl(PartnerController.Instance);
		}
		
		public UserForm(User user) : this()
		{
			isEditing = true;
			
			//populate the form
			IdField.Text = user.ID;
			IdField.IsEnabled = false;
			RoleField.SelectedIndex = Convert.ToInt32(user.ROLE_ID) - 1;
			SellerField.Text = user.SELLER_ID;
		}
		
		public void FiltrateForm()
		{
			IdField.Text = IdField.Text.Trim().ToLower();
			PasswordField.Password = PasswordField.Password.Trim().ToLower();
			RoleField.Text = RoleField.Text.Trim();
			SellerField.Text = SellerField.Text.Trim();
		}
		
		/// <summary>
		/// Validates the data in the input fields.
		/// </summary>
		/// <returns>True, if the form is valid.</returns>
		public bool ValidateForm()
		{
			bool returnValue = true;
			
			#region idValidation
			if (Regex.IsMatch(IdField.Text, "^[a-z0-9_]{4,}$"))
			{
				IdError.Visibility = Visibility.Collapsed;
			}
			else 
			{
				returnValue = false;
				if (IdField.Text.Length == 0)
				{
					IdError.Visibility = Visibility.Visible;
					IdError.Text = "Потребителското име е задължително за попълване.";
				}
				else
				{
					IdError.Visibility = Visibility.Visible;
					IdError.Text = "Въвели сте невалидно потребителско име.";
				}
			}
			#endregion
			
			#region PasswordValidation
			if (PasswordField.Password.Length > 0)
			{
				PasswordError.Visibility = Visibility.Collapsed;
			}
			else 
			{
				returnValue = false;
				PasswordError.Visibility = Visibility.Visible;
				PasswordError.Text = "Полето парола е задължително за попълване.";
			}
			#endregion
			
			#region Seller validation
			if ((RoleField.SelectedIndex == 1 && (SellerField.Text.Length == 9 || SellerField.Text.Length == 13)) ||
			    (SellerField.Text.Length == 0 && RoleField.SelectedIndex != 1))
			{
				SellerError.Visibility = Visibility.Collapsed;
			}
			else 
			{
				if (SellerField.Text.Length == 0)
				{
					returnValue = false;
					SellerError.Visibility = Visibility.Visible;
					SellerError.Text = "Полето Юридическо лице е задължително за попълване за счетоводители.";
				}
				else
				{
					returnValue = false;
					SellerError.Visibility = Visibility.Visible;
					SellerError.Text = "Въвели сте невалидно ЕИК.";
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
				User u = new User()
				{
	             	ID = IdField.Text,
	             	PASSWORD = PasswordField.Password,
	             	ROLE_ID = Convert.ToInt64(RoleField.SelectedIndex) + 1,
	             	SELLER_ID = SellerField.Text
				};
				if (isEditing) return UserController.Instance.Update(u);
				else return UserController.Instance.Create(u);
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
		
		protected void UserForm_Loaded(object sender, RoutedEventArgs e)
		{
			IdField.Focus();
		}
		
		void BrowseUsers_Click(object sender, RoutedEventArgs e)
		{
			usersPopup.IsOpen = !usersPopup.IsOpen;
		}
		
	}
}