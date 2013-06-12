using System;
using System.Windows.Input;

namespace InvoiceManager.Commands
{
	public static class InvoiceManagerCommands
	{
		private static RoutedUICommand _new;
		/// <summary>
		/// Loads a form for creating new entity. Entity type must be passed as CommandParameter.
		/// </summary>
		public static RoutedUICommand New
		{
			get { return _new; }
		}
		
		private static RoutedUICommand _edit;
		/// <summary>
		/// Loads a form for editin an entity. Entity must be passed as CommandParameter.
		/// </summary>
		public static RoutedUICommand Edit
		{
			get { return _edit; }
		}
		
		private static RoutedUICommand _delete;
		/// <summary>
		/// Loads a form for confirming entity delete. Entity must be passed as CommandParameter.
		/// </summary>
		public static RoutedUICommand Delete
		{
			get { return _delete; }
		}
		
		static InvoiceManagerCommands()
		{
			//New partner
			InputGestureCollection newInputs = new InputGestureCollection();
			newInputs.Add(new KeyGesture(Key.F2, ModifierKeys.None,"F2"));
			_new = new RoutedUICommand("Нов", "New", typeof(InvoiceManagerCommands), newInputs);
			//Edit partner
			InputGestureCollection editInputs = new InputGestureCollection();
			editInputs.Add(new KeyGesture(Key.F3, ModifierKeys.None, "F3"));
			_edit = new RoutedUICommand("Редактирай", "Delete", typeof(InvoiceManagerCommands), editInputs);
			//Delete partner
			InputGestureCollection deleteInputs = new InputGestureCollection();
			deleteInputs.Add(new KeyGesture(Key.Delete, ModifierKeys.None, "Del"));
			_delete = new RoutedUICommand("Изтрий", "Delete", typeof(InvoiceManagerCommands), deleteInputs);
		}
	}
}
