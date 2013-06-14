/*
 * Created by Stoyan Revov
 * Date: 14.6.2013 г.
 * Time: 23:40 ч.
 */
using System;
using System.Collections.Generic;
using System.Linq;

using InvoiceManager.Controller;
using InvoiceManager.Entities;

namespace InvoiceManager.Services
{
	/// <summary>
	/// Description of EntityControllerFactory.
	/// </summary>
	public static class EntityControllerFactory<entityType> where entityType : IEntity
	{
		private static Type entityControllerType;
		static EntityControllerFactory()
		{
			//TODO:URGENT!!!! Complete EntityControllerFactory
		}
		
		/// <summary>
		/// Returns an instance of the Form that can handle the generic type of enity.
		/// </summary>
		public static IEntityController<entityType> Initialize()
		{
			//return (IEntityController<entityType>)Activator.CreateInstance(repositoryType);
			return null;
		}
	}
}
