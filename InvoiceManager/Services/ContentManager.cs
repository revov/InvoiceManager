using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using InvoiceManager.Windows;

namespace InvoiceManager.Services
{
	/// <summary>
	/// Provides methods for content control.
	/// </summary>
	public static class ContentManager
	{
		/// <summary>
		/// Removes an userControl from its logical parent.
		/// </summary>
		public static void RemoveFromParent(UserControl userControl)
		{
			DependencyObject parent = LogicalTreeHelper.GetParent(userControl);
			if (parent is ContentControl)
				((ContentControl)parent).Content = null;
			else if (parent is Panel)
				((Panel)parent).Children.Remove(userControl);
			else if (parent is Decorator)
				((Decorator)parent).Child = null;
			else if (parent is ItemsControl)
			{
				((ItemsControl)parent).Items.Remove(userControl);
			}
			else throw new InvalidCastException("The parent of the userControl is not of a known type.");
		}
		
		/// <summary>
		/// Prints a message in the statusbar.
		/// </summary>
		public static void PrintStatus(String message)
		{
			IMainWindow win = (IMainWindow)App.Current.MainWindow;
			win.PrintStatus(message);
		}
		
		/// <summary>
		/// Prints a message in the statusbar.
		/// </summary>
		/// <param name="brush">The brush for the Foreground of the message. Defaults to Brushes.Black</param>
		public static void PrintStatus(String message, SolidColorBrush brush)
		{
			IMainWindow win = (IMainWindow)App.Current.MainWindow;
			win.PrintStatus(message, brush);
		}
			
	}
}
