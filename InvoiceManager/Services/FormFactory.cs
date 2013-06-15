/*
 * Created by Stoyan Revov
 * Date: 14.6.2013 г.
 * Time: 23:40 ч.
 */
using System;
using System.Linq;
using System.Windows.Controls;
using InvoiceManager.Controller;
using InvoiceManager.Entities;
using InvoiceManager.Windows.UserControls;

namespace InvoiceManager.Services
{	
	/// <summary>
	/// Description of EntityControllerFactory.
	/// </summary>
	public static class FormFactory<entityType> where entityType : IEntity
	{
		private static Type formType;
		static FormFactory()
		{
			Type[] formTypes = System.Reflection.Assembly.GetExecutingAssembly().GetTypes()
					.Where(type => typeof(IForm).IsAssignableFrom(type)).ToArray<Type>();
			foreach(Type t in formTypes)
			{
				foreach(Attribute attrib in t.GetCustomAttributes(false))
	            {
					if (attrib is EntityEditorAttribute && ((EntityEditorAttribute)attrib).EditingType == typeof(entityType))
	                {
	                    formType = t;
	                    break;
	                }
	            }
				if (formType != null)
					break;
			}
		}
		
		/// <summary>
		/// Initializes an appropriate Add Form.
		/// </summary>
		/// <returns>A user control that can hydrate an IEntity object.</returns>
		public static UserControl InitializeAddForm()
		{
			return new AddControl((IForm)Activator.CreateInstance(formType));
		}
		
		/// <summary>
		/// Initializes an appropriate Edit Form.
		/// </summary>
		/// <returns>A user control that can hydrate an IEntity object.</returns>
		public static UserControl InitializeEditForm(entityType entity)
		{
			if (entity == null)
				throw new ArgumentNullException("Entity cannot be null.");
			return new EditControl((IForm)Activator.CreateInstance(formType, entity));
		}
		
	}
}
