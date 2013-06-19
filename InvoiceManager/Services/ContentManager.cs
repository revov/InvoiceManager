using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

using InvoiceManager.Entities;
using InvoiceManager.Windows;
using InvoiceManager.Windows.UserControls;

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
		public static void RemoveFromParent(Control control)
		{
			DependencyObject parent = LogicalTreeHelper.GetParent(control);
			if (parent is ContentControl)
				((ContentControl)parent).Content = null;
			else if (parent is Panel)
				((Panel)parent).Children.Remove(control);
			else if (parent is Decorator)
				((Decorator)parent).Child = null;
			else if (parent is ItemsControl)
				((ItemsControl)parent).Items.Remove(control);
			else if (parent is Popup)
				((Popup)parent).IsOpen = false;
			else throw new InvalidCastException("The parent of the userControl is not of a known type.");
		}
		
		/// <summary>
		/// Attempts to print a message in the main window's statusbar.
		/// </summary>
		public static void PrintStatus(String message)
		{
			if (App.Current.MainWindow != null)
			{
				IMainWindow win = (IMainWindow)App.Current.MainWindow;
				win.PrintStatus(message);
			}
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
			
		/// <summary>
		/// Makes the Main Window show the control.
		/// </summary>
		public static void ShowContent(Control control)
		{
			IMainWindow win = (IMainWindow)App.Current.MainWindow;
			win.ShowContent(control);
		}
		
		/// <summary>
		/// Raises the EntityChosen event
		/// </summary>
		public static void FillEntity(IEntity entity)
		{
			if (EntityChosen != null)
				EntityChosen.Invoke(entity, new EventArgs());
		}
		
		public static event EventHandler EntityChosen;
	}
}
