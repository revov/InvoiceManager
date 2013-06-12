/*
 * Created by Stoyan Revov
 * Date: 12.6.2013 г.
 * Time: 16:16 ч.
 */
using System;
using System.Collections.Generic;
using System.Linq;

using InvoiceManager.Entities;
using InvoiceManager.Repositories;

namespace InvoiceManager.Services
{
	/// <summary>
	/// Returns repositories based on an entity
	/// </summary>
	public static class RepositoryFactory<entityType> where entityType : IEntity
	{
		private static Type repositoryType;
		static RepositoryFactory()
		{
			Type[] repositoryTypes = System.Reflection.Assembly.GetExecutingAssembly().GetTypes()
					.Where(type => typeof(IRepository<entityType>).IsAssignableFrom(type)
				       && type.IsAbstract == false
				       && type.IsGenericTypeDefinition == false
				       && type.IsInterface == false).ToArray<Type>();
			if (repositoryTypes.Length == 1)
				repositoryType = repositoryTypes[0];
			if (repositoryTypes.Length == 0)
				throw new ArgumentException(String.Format("There is no corresponding repository defined for {0}.", typeof(entityType).Name));
			if (repositoryTypes.Length > 1)
				throw new ArgumentException(String.Format("There is more than one corresponding repository defined for {0}.", typeof(entityType).Name));
		}
		
		/// <summary>
		/// Returns an instance of the Repository that can handle the generic type of enity.
		/// </summary>
		public static IRepository<entityType> Initialize()
		{
			return (IRepository<entityType>)Activator.CreateInstance(repositoryType);
		}
	}
}
