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
		public IEntityController Controller { get; private set; }
		public List<IBaseEntity> Items;
		
		public DataBrowserControl(IEntityController controller)
		{
			if (controller == null)
				throw new ArgumentNullException("IEntityController cannot be null");
			this.Controller = controller;
			Items = Controller.Repository.RetrieveAll();
			//TODO:mapping with column names
			InitializeComponent();
		}
	}
}