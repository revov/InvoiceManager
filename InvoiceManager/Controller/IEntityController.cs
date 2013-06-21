/*
 * Created by Stoyan Revov
 * Date: 11.6.2013 г.
 * Time: 23:38 ч.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows;

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
		/// Represents mapping between column headers and property names.
		/// </summary>
		Dictionary<string, string> Mapping { get; }
		
		/// <summary>
		/// Returns a collection of items
		/// </summary>
		List<IEntity> GetDataSource();
		
		/// <summary>
		/// Represents the currently selected item in the list.
		/// </summary>
		IEntity SelectedItem { get; set; }
		
		/// <summary>
		/// Call appropriate AddEntity Control through FormFactory
		/// </summary>
		void CreateForm();
		/// <summary>
		/// Call appropriate EditEntity Control through FormFactory
		/// </summary>
		void UpdateForm();
		/// <summary>
		/// Call confirmation form
		/// </summary>
		void DeleteForm();
		
		/// <summary>
		/// Creates an entity in the database
		/// </summary>
		/// <returns>True if operation successful.</returns>
		bool Create(IEntity entity);
		/// <summary>
		/// Updates an entity in the database
		/// </summary>
		/// <returns>True if operation successful.</returns>
		bool Update(IEntity entity);
		/// <summary>
		/// Deletes an entity in the database
		/// </summary>
		/// <returns>True if operation successful.</returns>
		bool Delete(IEntity entity);
		
		event EventHandler Changed;
	}
}
