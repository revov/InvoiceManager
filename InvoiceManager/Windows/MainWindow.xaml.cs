using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

using InvoiceManager.Commands;
using InvoiceManager.Entities;
using InvoiceManager.Repositories;
using InvoiceManager.Services;
using InvoiceManager.Windows.UserControls;

namespace InvoiceManager.Windows
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window, IMainWindow
	{
		public MainWindow(SessionInfo sessionInfo)
		{
			this.DataContext = sessionInfo;
			
			#region Command bindings
			//New Partner command binding
			CommandBinding newPartnerBinding = new CommandBinding(PartnerCommands.NewPartner);
			newPartnerBinding.Executed += NewPartnerCommand_Executed;
			newPartnerBinding.CanExecute += NewPartnerCommand_CanExecute;
			CommandBindings.Add(newPartnerBinding);
			//Edit Partner command binding
			CommandBinding editPartnerBinding = new CommandBinding(PartnerCommands.EditPartner);
			editPartnerBinding.Executed += EditPartnerCommand_Executed;
			editPartnerBinding.CanExecute += EditPartnerCommand_CanExecute;
			CommandBindings.Add(editPartnerBinding);
			//Delete Partner command binding
			CommandBinding deletePartnerBinding = new CommandBinding(PartnerCommands.DeletePartner);
			deletePartnerBinding.Executed += DeletePartnerCommand_Executed;
			deletePartnerBinding.CanExecute += DeletePartnerCommand_CanExecute;
			CommandBindings.Add(deletePartnerBinding);
			#endregion
			
			InitializeComponent();
			App.Current.MainWindow = this;
		}
		
		public void PrintStatus(string message)
		{
			StatusBarMessage.Text = message;
		}
		
		public void PrintStatus(string message, SolidColorBrush brush)
		{
			StatusBarMessage.Foreground = brush;
			StatusBarMessage.Text = message;
		}
		
		public void ShowContent(Control control)
		{
			MainAreaModulePanel.Add(control);
			
		}
		
		#region Event handlers
		
		#region New partner command
		void NewPartnerCommand_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			AddPartnerControl addPartnerControl = new AddPartnerControl();
			ContentManager.ShowContent(addPartnerControl);
		}
		
		void NewPartnerCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = SecurityManager.CurrentSessionInfo.CurrentRole.WRITE_ACCESS;
		}
		#endregion
		
		#region Edit partner command
		void EditPartnerCommand_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			if (e.Parameter == null)
			{
				ContentManager.PrintStatus("TODO: Не сте избрали контрагент за редактиране.", Brushes.Red);
				return;
			}
			EditPartnerControl editPartnerControl = new EditPartnerControl((Partner)e.Parameter);
			ContentManager.ShowContent(editPartnerControl);
		}
		
		void EditPartnerCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = SecurityManager.CurrentSessionInfo.CurrentRole.MODIFY_ACCESS;
		}
		#endregion
		
		#region Delete partner command
		void DeletePartnerCommand_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			if (e.Parameter == null)
			{
				ContentManager.PrintStatus("TODO: Не сте избрали контрагент за изтриване.", Brushes.Red);
				return;
			}
			Partner partner = (Partner)e.Parameter;
			IBaseRepository<Partner> partnerRepository = new PartnerRepository();
			partnerRepository.Delete(partner.ID);
			Logger.Log("Изтрит контрагент " + partner.ID);
			ContentManager.PrintStatus("Контрагентът беше изтрит успешно.");
		}
		
		void DeletePartnerCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = SecurityManager.CurrentSessionInfo.CurrentRole.MODIFY_ACCESS;
		}
		#endregion
		
		void MenuExit_Click(object sender, RoutedEventArgs e)
		{
			App.Current.Shutdown();
		}
		
		void MenuSignOut_Click(object sender, RoutedEventArgs e)
		{
			SecurityManager.DeAuthenticateUser();
			Window loginWindow = new LoginWindow();
			loginWindow.Show();
			foreach (Window window in App.Current.Windows)
			{
				if (loginWindow != window)
					window.Close();
			}
		}
		
		void BrowsePartners_Click(object sender, RoutedEventArgs e)
		{
			PartnersListControl partnerListControl = new PartnersListControl();
			ContentManager.ShowContent(partnerListControl);
		}
		
		void StatusBarMessage_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (StatusBarMessage.Text!="")
				MessageBox.Show(StatusBarMessage.Text, "Съобщение в статусбара", MessageBoxButton.OK);
		}
		#endregion
	}
}