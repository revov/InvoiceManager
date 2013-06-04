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

namespace InvoiceManager.Windows.UserControls
{
	/// <summary>
	/// Interaction logic for PartnersListControl.xaml
	/// </summary>
	public partial class PartnersListControl : UserControl
	{
		private List<Partner> _partners = PartnerRepository.RetrieveAll();
		public List<Partner> Partners
		{
			get { return _partners; }
		}
		public PartnersListControl()
		{
			InitializeComponent();
		}
		
		void This_Loaded(object sender, RoutedEventArgs e)
		{
			//TODO:Add commands!
			#region Context menu and privilidges
			Style style = new Style(typeof (ListViewItem));
				ContextMenu contextMenu = new ContextMenu();
					MenuItem newMenuItem = new MenuItem();
					newMenuItem.IsEnabled = SecurityManager.CurrentSessionInfo.CurrentRole.WRITE_ACCESS;
					newMenuItem.Header = "Нов";
					newMenuItem.Icon = new Image()
					{
						Source = new BitmapImage(new Uri("/Resources/new.png", UriKind.Relative)),
						Width = 20,
						Height = 20
					};
				contextMenu.Items.Add(newMenuItem);
					MenuItem editMenuItem = new MenuItem();
					editMenuItem.IsEnabled = SecurityManager.CurrentSessionInfo.CurrentRole.MODIFY_ACCESS;
					editMenuItem.Header = "Редактирай";
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
	}
}