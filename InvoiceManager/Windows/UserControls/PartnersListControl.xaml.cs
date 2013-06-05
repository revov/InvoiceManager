/*
 * Created by SharpDevelop.
 * User: reuf
 * Date: 4.6.2013 г.
 * Time: 20:31 ч.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
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
using System.Windows.Media.Imaging;

using InvoiceManager.Entities;
using InvoiceManager.Repositories;
using InvoiceManager.Services;
using InvoiceManager.Commands;

namespace InvoiceManager.Windows.UserControls
{
	/// <summary>
	/// Interaction logic for PartnersListControl.xaml
	/// </summary>
	public partial class PartnersListControl : UserControl
	{
		public static readonly DependencyProperty PartnersProperty =
			DependencyProperty.Register("Partners", typeof(List<Partner>), typeof(PartnersListControl),
			                            new FrameworkPropertyMetadata());
		
		public List<Partner> Partners {
			get { return (List<Partner>)GetValue(PartnersProperty); }
			set { SetValue(PartnersProperty, value); }
		}
		public PartnersListControl()
		{
			//Initialize Partners
			Partners = PartnerRepository.RetrieveAll();
			
			InitializeComponent();
			
			#region Context menu
			Style style = new Style(typeof (ListViewItem));
				
				ContextMenu contextMenu = new ContextMenu();
				
					MenuItem newMenuItem = new MenuItem();
					newMenuItem.Header = "Нов";
					newMenuItem.Command = PartnerCommands.NewPartner;
					newMenuItem.Icon = new Image()
					{
						Source = new BitmapImage(new Uri("/Resources/new.png", UriKind.Relative)),
						Width = 20,
						Height = 20
					};
				contextMenu.Items.Add(newMenuItem);
				
					MenuItem editMenuItem = new MenuItem();
					editMenuItem.Header = "Редактирай";
					editMenuItem.Command = PartnerCommands.EditPartner;
					
					//Bind the command property to the selected item
					
					Binding editBinding = new Binding();
					editBinding.Source = PartnersListView;
					editBinding.Path = new PropertyPath("SelectedItem");
					editBinding.Mode = BindingMode.OneWay;
					editMenuItem.SetBinding(MenuItem.CommandParameterProperty, editBinding);
					
					editMenuItem.Icon = new Image()
					{
						Source = new BitmapImage(new Uri("/Resources/Edit.png", UriKind.Relative)),
						Width = 20,
						Height = 20
					};
				contextMenu.Items.Add(editMenuItem);
				
					MenuItem deleteMenuItem = new MenuItem();
					deleteMenuItem.IsEnabled = SecurityManager.CurrentSessionInfo.CurrentRole.MODIFY_ACCESS;
					deleteMenuItem.Header = "Изтрий";
					deleteMenuItem.Command = PartnerCommands.DeletePartner;
					
					//Bind the command property to the selected item
					Binding deleteBinding = new Binding();
					deleteBinding.Source = PartnersListView;
					deleteBinding.Path = new PropertyPath("SelectedItem");
					deleteBinding.Mode = BindingMode.OneWay;
					deleteMenuItem.SetBinding(MenuItem.CommandParameterProperty, deleteBinding);
					
					deleteMenuItem.Icon = new Image()
					{
						Source = new BitmapImage(new Uri("/Resources/Delete.png", UriKind.Relative)),
						Width = 20,
						Height = 20
					};
				contextMenu.Items.Add(deleteMenuItem);
				
			style.Setters.Add(new Setter(ListViewItem.ContextMenuProperty, contextMenu));
			Resources.Add(typeof (ListViewItem), style);
			#endregion
		}
		
		#region EventHandlers
		void CloseButton_Click(object sender, RoutedEventArgs e)
		{
			ContentManager.RemoveFromParent(this);
		}
		
		void RefreshButton_Click(object sender, RoutedEventArgs e)
		{
			Partners = PartnerRepository.RetrieveAll();
		}
		#endregion
	}
}