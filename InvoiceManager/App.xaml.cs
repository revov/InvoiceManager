using System;
using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Xml;

using InvoiceManager.Services;
//TODO:!!! Fix memory leaks: userControls are not destructed by the Garbage collector due to databindings
namespace InvoiceManager
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		#region AutoSelect TextBoxes and PasswordBoxes on GotFocus
		void Application_Startup(object sender, StartupEventArgs e)
		{
		        //works for tab into textbox
		        EventManager.RegisterClassHandler(typeof(TextBox),
		            TextBox.GotFocusEvent,
		            new RoutedEventHandler(TextBox_GotFocus));
		        //works for tab into passwordbox
		        EventManager.RegisterClassHandler(typeof(PasswordBox),
		            PasswordBox.GotFocusEvent,
		            new RoutedEventHandler(PasswordBox_GotFocus));
		}
		
		private void TextBox_GotFocus(object sender, RoutedEventArgs e)
	    {
	        (sender as TextBox).SelectAll();
	    }
	    
	    private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
	    {
	        (sender as PasswordBox).SelectAll();
	    }
	    
		void Application_Exit(object sender, ExitEventArgs e)
		{
			if (SecurityManager.CurrentSessionInfo != null)
				SecurityManager.DeAuthenticateUser();
		}
	    #endregion
	}
}