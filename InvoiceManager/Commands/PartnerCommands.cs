using System;
using System.Windows.Input;

namespace InvoiceManager.Commands
{//TODO:Commands should only be New, Edit and Delete and should be generic
	public static class PartnerCommands
	{
		private static RoutedUICommand _newPartner;
		public static RoutedUICommand NewPartner
		{
			get { return _newPartner; }
		}
		
		private static RoutedUICommand _editPartner;
		public static RoutedUICommand EditPartner
		{
			get { return _editPartner; }
		}
		
		private static RoutedUICommand _deletePartner;
		public static RoutedUICommand DeletePartner
		{
			get { return _deletePartner; }
		}
		
		static PartnerCommands()
		{
			//New partner
			InputGestureCollection newInputs = new InputGestureCollection();
			newInputs.Add(new KeyGesture(Key.F2, ModifierKeys.None,"F2"));
			_newPartner = new RoutedUICommand("Нов контрагент", "NewPartner", typeof(PartnerCommands), newInputs);
			//Edit partner
			InputGestureCollection editInputs = new InputGestureCollection();
			editInputs.Add(new KeyGesture(Key.F3, ModifierKeys.None, "F3"));
			_editPartner = new RoutedUICommand("Редактирай контрагент", "EditPartner", typeof(PartnerCommands), editInputs);
			//Delete partner
			InputGestureCollection deleteInputs = new InputGestureCollection();
			deleteInputs.Add(new KeyGesture(Key.Delete, ModifierKeys.None, "Del"));
			_deletePartner = new RoutedUICommand("Редактирай контрагент", "EditPartner", typeof(PartnerCommands), deleteInputs);
		}
	}
}
