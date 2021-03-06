﻿using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Text;
using System.Text.RegularExpressions;
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
	/// Interaction logic for AddControl.xaml
	/// </summary>
	public partial class AddControl : UserControl, IDisposable
	{
		IForm entityForm;
		
		public AddControl(IForm form)
		{
			InitializeComponent();
			entityForm = form;
			UIElement uiForm = (UIElement)form;
			DockPanel.SetDock(uiForm, Dock.Top);
			AddControlDockPanel.Children.Add(uiForm);
		}
		
		public void Dispose()
		{
			((IDisposable)entityForm).Dispose();
			ContentManager.RemoveFromParent(this);
		}
		
		protected void AddButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (entityForm.Persist())
				{
					ContentManager.PrintStatus("Добавянето успешно!");
					this.Dispose();
				}
			}
			catch (ValidationException ex)
			{
				ContentManager.PrintStatus(ex.Message, Brushes.Red);
			}
		}
		
		protected void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			this.Dispose();
		}
	}
}