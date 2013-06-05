using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

using InvoiceManager.Services;

namespace InvoiceManager.Windows.UserControls
{
	/// <summary>
	/// User Layout Panel
	/// </summary>
	public partial class ModulePanel : UserControl
	{
		public ModulePanel()
		{
			InitializeComponent();
		}
		
		public void Add(Control control)
		{
			if (ModuleGrid.Children.Count == 1 && ModuleWrapPanel.Children.Count == 0)
				ModuleGrid.Children.Add(control);
			else if(ModuleGrid.Children.Count == 2)
			{
				Control gridControl = (Control)ModuleGrid.Children[1];
				ContentManager.RemoveFromParent(gridControl);
				ModuleWrapPanel.Children.Add(gridControl);
				ModuleWrapPanel.Children.Add(control);
			}
			else
				ModuleWrapPanel.Children.Add(control);
		}
		
		void Module_LayoutUpdated(object sender, EventArgs e)
		{
			if (ModuleGrid.Children.Count == 1 && ModuleWrapPanel.Children.Count == 1)
			{
				Control control = (Control)ModuleWrapPanel.Children[0];
				ContentManager.RemoveFromParent(control);
				ModuleGrid.Children.Add(control);
			}
		}
	}
}