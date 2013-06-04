using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

using InvoiceManager.Services;

namespace InvoiceManager
{
	/// <summary>
	/// Interaction logic for LoginWindow.xaml
	/// </summary>
	public partial class LoginWindow : Window
	{
		public LoginWindow()
		{
			InitializeComponent();
		}
		
		private void FiltrateForm()
		{
			UsernameField.Text = UsernameField.Text.Trim().ToLower();
			PasswordField.Password = PasswordField.Password.Trim().ToLower();
		}
		
		void LoginBtn_Click(object sender, RoutedEventArgs e)
		{
			FiltrateForm();
			try
			{
				if (SecurityManager.AuthenticateUser(UsernameField.Text, PasswordField.Password))
				{
					new Windows.MainWindow(SecurityManager.CurrentSessionInfo).Show();
					this.Close();
				}
				else
				{
					ErrorTextBlock.Text = "Въвели сте грешно потребителско име или парола!";
					UsernameField.Focus();
				}
			}
			catch (Exception ex)
			{
				ErrorTextBlock.Text = "Възникна грешка във връзката с базата данни:"
					+ Environment.NewLine + ex.Message;
				UsernameField.Focus();
				UsernameField.SelectAll();
			}
		}
		
		void CancelBtn_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown();
		}
		
		void Window_Loaded(object sender, RoutedEventArgs e)
		{
			UsernameField.Focus();
		}
	}
}