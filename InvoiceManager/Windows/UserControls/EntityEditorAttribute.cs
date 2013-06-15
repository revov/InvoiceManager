/*
 * Created by Stoyan Revov
 * Date: 15.6.2013 г.
 * Time: 16:14 ч.
 */
using System;

namespace InvoiceManager.Windows.UserControls
{
	/// <summary>
	/// Represents the type of entity thes form can edit.
	/// </summary>
	public class EntityEditorAttribute : Attribute
	{
		public Type EditingType { get; private set; }
        public EntityEditorAttribute(Type editingType)
        {
            this.EditingType = editingType;
        }
	}
}
