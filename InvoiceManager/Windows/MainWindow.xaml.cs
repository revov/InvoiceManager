using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

using InvoiceManager.Entities;
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
			InitializeComponent();
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
		
		#region Event handlers
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
		
		void NewPartner_Click(object sender, RoutedEventArgs e)
		{
			AddPartnerControl addPartnerControl = new AddPartnerControl();
			addPartnerControl.SetValue(Grid.RowProperty, 1);
			MainWindowGrid.Children.Add(addPartnerControl);
		}
		
		void StatusBarMessage_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (StatusBarMessage.Text!="")
				MessageBox.Show(StatusBarMessage.Text, "Съобщение в статусбара", MessageBoxButton.OK);
		}
		
		void Window_Loaded(object sender, RoutedEventArgs e)
		{
			App.Current.MainWindow = this;
			ContentManager.PrintStatus("Добре дошли в мениджъра на фактури.");
		}
		#endregion
	}
}