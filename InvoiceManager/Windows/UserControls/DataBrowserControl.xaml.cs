using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
	public partial class DataBrowserControl : UserControl
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
			Items = controller.GetDataSource();
			
			controller.Changed += (sender, e) => Items = controller.GetDataSource();
			
			InitializeComponent();
			dockPanel.Background = App.Current.MainWindow.Background;
			
			#region Mapping with column names
			foreach(KeyValuePair<string, string> item in controller.Mapping)
			{
				GridViewColumn column = new GridViewColumn
					{
						Header = item.Key,
						DisplayMemberBinding = new Binding(item.Value)
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
		
		#region Event handlers
		void RefreshButton_Click(object sender, RoutedEventArgs e)
		{
			this.Items = Controller.GetDataSource();
		}
		
		void OKButton_Click(object sender, RoutedEventArgs e)
		{
			if (Controller.SelectedItem != null)
				ContentManager.FillEntity(Controller.SelectedItem);
			ContentManager.RemoveFromParent(this);
		}
		#endregion
		
	}
}