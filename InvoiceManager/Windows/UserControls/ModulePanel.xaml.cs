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
		
		/// <summary>
		/// Adds a control to the panel
		/// </summary>
		/// <param name="control">Must implement IDisposable.</param>
		public void Add(Control control)
		{
			if ( !(control is IDisposable) )
				throw new ArgumentException("You cannot add controls to tha ModulePanel which do not implement the IDisposable interface.");
			
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
		
		public void Clear()
		{
			for (int i = ModuleWrapPanel.Children.Count - 1; i >= 0; i--)
			{
				((IDisposable)ModuleWrapPanel.Children[i]).Dispose();
			}
			
			for (int i = ModuleGrid.Children.Count - 1; i > 0; i--)
			{
				((IDisposable)ModuleGrid.Children[i]).Dispose();
			}
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