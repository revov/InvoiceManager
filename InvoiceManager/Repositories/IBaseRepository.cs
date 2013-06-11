/*
 * Created by Stoyan Revov
 * Date: 10.6.2013 г.
 * Time: 23:01 ч.
 */
using System;
using System.Collections.Generic;
using InvoiceManager.Entities;

namespace InvoiceManager.Repositories
{
	/// <summary>
	/// All repositories implement this interface.
	/// </summary>
	public interface IBaseRepository<T> where T : IBaseEntity
	{
		//TODO:!!!!! All repositories should implement IBaseRepository and other classes should instantiate their own repository objects
		void Create(T entity);
		T Retrieve(object baseID);
		List<T> RetrieveAll();
		void Update(T entity);
		void Delete(object baseID);
	}
}
