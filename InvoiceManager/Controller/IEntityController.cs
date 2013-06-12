/*
 * Created by Stoyan Revov
 * Date: 11.6.2013 г.
 * Time: 23:38 ч.
 */
using System;
using InvoiceManager.Entities;
using InvoiceManager.Repositories;

namespace InvoiceManager.Controller
{
	/// <summary>
	/// Defines members for an entity controller used to create an abstraction layer over repositories/entities.
	/// </summary>
	public interface IEntityController
	{
		/// <summary>
		/// EntityRepository for direct access to the model
		/// </summary>
		IRepository<IEntity> Repository { get; set; }
		/// <summary>
		/// Call appropriate AddEntity Control (form)
		/// </summary>
		void Create();
		/// <summary>
		/// Call appropriate EditEntity Control (form)
		/// </summary>
		void Update(IEntity entity);
		/// <summary>
		/// Call confirmation form
		/// </summary>
		void Delete(IEntity entity);
		
	}
}
