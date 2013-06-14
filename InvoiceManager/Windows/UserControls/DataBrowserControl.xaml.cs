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
using InvoiceManager.Repositories;
using InvoiceManager.Controller;

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
		
		public IEntityController<IEntity> Controller { get; private set; }
		
		public DataBrowserControl(IEntityController<IEntity> controller)
		{
			if (controller == null)
				throw new ArgumentNullException("IEntityController<T> cannot be null");
			this.Controller = controller;
			Items = controller.GetDataSource();
			//TODO:mapping with column names
			InitializeComponent();
		}
	}
}