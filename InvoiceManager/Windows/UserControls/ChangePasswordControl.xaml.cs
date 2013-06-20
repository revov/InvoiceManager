/*
 * Created by Stoyan Revov
 * Date: 20.6.2013 г.
 * Time: 18:25 ч.
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
	/// Interaction logic for ChangePasswordControl.xaml
	/// </summary>
	public partial class ChangePasswordControl : UserControl, IDisposable
	{
		public IEntityController Controller { get; private set; }
		
		public ChangePasswordControl(IEntityController controller)
		{
			this.Controller = controller;
			InitializeComponent();
		}
		
		public void Dispose()
		{
			ContentManager.RemoveFromParent(this);
		}
		
		void OKButton_Click(object sender, RoutedEventArgs e)
		{
			User currentUser = SecurityManager.CurrentSessionInfo.CurrentUser;
			if (SecurityManager.CalculateSHA1(OldPass.Password) == currentUser.PASSWORD)
			{
				if (NewPass.Password == NewPassRepeated.Password)
				{
					currentUser.PASSWORD = NewPass.Password;
					if (Controller.Update(currentUser))
					{
						ContentManager.PrintStatus("Паролата бе променена успешно.");
						this.Dispose();
					}
					else
					{
						ErrorField.Visibility = Visibility.Collapsed;
						OldPass.Focus();
					}
				}
				else
				{
					ErrorField.Visibility = Visibility.Visible;
					ErrorField.Text = "Новата ви парола не съвпада.";
					NewPass.Focus();
				}
			}
			else
			{
				ErrorField.Visibility = Visibility.Visible;
				ErrorField.Text = "Не сте въвели правилно старата си парола";
				OldPass.Focus();
			}
		}
		
		void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			this.Dispose();
		}
		
	}
}