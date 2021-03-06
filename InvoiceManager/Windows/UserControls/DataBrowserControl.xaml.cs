﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using InvoiceManager.Commands;
using InvoiceManager.Controller;
using InvoiceManager.Entities;
using InvoiceManager.Repositories;
using InvoiceManager.Services;

namespace InvoiceManager.Windows.UserControls
{
	/// <summary>
	/// Use it to display data from DB tables.
	/// </summary>
	public partial class DataBrowserControl : UserControl, IDisposable
	{
		public static readonly DependencyProperty ItemsProperty =
			DependencyProperty.Register("Items", typeof(List<IEntity>), typeof(DataBrowserControl),
			                            new FrameworkPropertyMetadata());
		
		public List<IEntity> Items
		{
			get { return (List<IEntity>)GetValue(ItemsProperty); }
			set { SetValue(ItemsProperty, value); }
		}
		
		public IEntityController Controller { get; private set; }
		
		public DataBrowserControl(IEntityController controller)
		{
			if (controller == null)
				throw new ArgumentNullException("IEntityController<T> cannot be null");
			this.Controller = controller;
			
			//Fill the properties with "fresh" data
			Items = controller.GetDataSource();
			
			//Listen when the data changes so that we can update the properties holding the data
			controller.Changed += Controller_Changed;
			
			InitializeComponent();
			
			//Setting the background is required => when using DataBrowserControl as a popup
			//and it flows out of the window its background shouldn't be transparent
			dockPanel.Background = App.Current.MainWindow.Background;
			
			#region Mapping with column names
			foreach(KeyValuePair<string, string> item in controller.Mapping)
			{
				GridViewColumn column = new GridViewColumn
					{
						Header = item.Key,
						DisplayMemberBinding = new Binding(item.Value)
							{
								StringFormat = item.Value.Contains("PRICE") ? "C" : "",
								ConverterCulture = Thread.CurrentThread.CurrentCulture
							}
					};
				gridView.Columns.Add(column);
			}
			#endregion
			
			//Create binding to the current controller
			Binding controllerBinding = new Binding()
				{
					Source = this,
					Path = new PropertyPath("Controller"),
					Mode = BindingMode.OneWay
				};
			
			//Create binding to the selected item (reversed binding used for the sake of simplicity)
			Binding selecteditemBinding = new Binding()
				{
					Source = this.Controller,
					Path = new PropertyPath("SelectedItem"),
					Mode = BindingMode.OneWayToSource
				};
			EntitiesListView.SetBinding(ListView.SelectedItemProperty, selecteditemBinding);
			
			#region Context menu
			Style style = new Style(typeof (ListViewItem));
				
				ContextMenu contextMenu = new ContextMenu();
				
					MenuItem newMenuItem = new MenuItem()
						{
							Command = InvoiceManagerCommands.New,
							Icon = new Image()
								{
									Source = new BitmapImage(new Uri("/Resources/new.png", UriKind.Relative)),
									Width = 20,
									Height = 20
								}
						};
					newMenuItem.SetBinding(MenuItem.CommandParameterProperty, controllerBinding);
				contextMenu.Items.Add(newMenuItem);
				
					MenuItem editMenuItem = new MenuItem()
						{
							Command = InvoiceManagerCommands.Edit,
							Icon = new Image()
								{
									Source = new BitmapImage(new Uri("/Resources/Edit.png", UriKind.Relative)),
									Width = 20,
									Height = 20
								}
						};
					editMenuItem.SetBinding(MenuItem.CommandParameterProperty, controllerBinding);
				contextMenu.Items.Add(editMenuItem);
				
					MenuItem deleteMenuItem = new MenuItem()
						{
							Command = InvoiceManagerCommands.Delete,
							Icon = new Image()
								{
									Source = new BitmapImage(new Uri("/Resources/Delete.png", UriKind.Relative)),
									Width = 20,
									Height = 20
								}
						};
					deleteMenuItem.SetBinding(MenuItem.CommandParameterProperty, controllerBinding);
				contextMenu.Items.Add(deleteMenuItem);
				
			style.Setters.Add(new Setter(ListViewItem.ContextMenuProperty, contextMenu));
			Resources.Add(typeof (ListViewItem), style);
			#endregion
			
		}
		
		public void Dispose()
		{
			Controller.Changed -= Controller_Changed;
			ContentManager.RemoveFromParent(this);
		}
		
		#region Event handlers
		void RefreshButton_Click(object sender, RoutedEventArgs e)
		{
			this.Items = Controller.GetDataSource();
		}
		
		void OKButton_Click(object sender, RoutedEventArgs e)
		{
			if (Controller.SelectedItem != null)
				ContentManager.FillEntity(Controller.SelectedItem);
			this.Dispose();
		}
		
		void EntitiesListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			OKButton_Click(sender, (RoutedEventArgs)e);
		}
		
		void Controller_Changed(object sender, EventArgs e)
		{
			RefreshButton_Click(sender, new RoutedEventArgs());
		}
		#endregion
		
	}
}