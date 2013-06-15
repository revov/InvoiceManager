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
using InvoiceManager.Controller;
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
			//New command binding
			CommandBinding newCommandBinding = new CommandBinding(InvoiceManagerCommands.New);
			newCommandBinding.Executed += NewCommand_Executed;
			newCommandBinding.CanExecute += NewCommand_CanExecute;
			CommandBindings.Add(newCommandBinding);
			//Edit command binding
			CommandBinding editCommandBinding = new CommandBinding(InvoiceManagerCommands.Edit);
			editCommandBinding.Executed += EditCommand_Executed;
			editCommandBinding.CanExecute += EditCommand_CanExecute;
			CommandBindings.Add(editCommandBinding);
			//Delete command binding
			CommandBinding deleteCommandBinding = new CommandBinding(InvoiceManagerCommands.Delete);
			deleteCommandBinding.Executed += DeleteCommand_Executed;
			deleteCommandBinding.CanExecute += DeleteCommand_CanExecute;
			CommandBindings.Add(deleteCommandBinding);
			#endregion
			
			InitializeComponent();
			App.Current.MainWindow = this;
		}
		
		public void PrintStatus(string message)
		{
			StatusBarMessage.Foreground = Brushes.Black;
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
		
		#region New command
		void NewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			if (e.Parameter == null)
			{
				ContentManager.PrintStatus("TODO: Не сте избрали какво искате да добавите.", Brushes.Red);
				return;
			}
			IEntityController controller = (IEntityController)e.Parameter;
			controller.CreateForm();
		}
		
		void NewCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = SecurityManager.CurrentSessionInfo.CurrentRole.WRITE_ACCESS;
		}
		#endregion
		
		#region Edit command
		void EditCommand_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			if (e.Parameter == null)
			{
				ContentManager.PrintStatus("TODO: Не сте избрали елемент за редактиране.", Brushes.Red);
				return;
			}
			IEntityController controller = (IEntityController)e.Parameter;
			controller.UpdateForm();
		}
		
		void EditCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = SecurityManager.CurrentSessionInfo.CurrentRole.MODIFY_ACCESS;
		}
		#endregion
		
		#region Delete command
		void DeleteCommand_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			if (e.Parameter == null)
			{
				ContentManager.PrintStatus("TODO: Не сте избрали елемент за изтриване.", Brushes.Red);
				return;
			}
			IEntityController controller = (IEntityController)e.Parameter;
			controller.DeleteForm();
		}
		
		void DeleteCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
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
			ContentManager.ShowContent(new DataBrowserControl(PartnerController.Instance));
		}
		
		void StatusBarMessage_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (StatusBarMessage.Text!="")
				MessageBox.Show(StatusBarMessage.Text, "Съобщение в статусбара", MessageBoxButton.OK);
		}
		
		void BrowseUsers_Click(object sender, RoutedEventArgs e)
		{
			ContentManager.ShowContent(new DataBrowserControl(UserController.Instance));
		}
		#endregion
	}
}