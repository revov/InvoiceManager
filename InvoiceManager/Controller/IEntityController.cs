/*
 * Created by Stoyan Revov
 * Date: 11.6.2013 г.
 * Time: 23:38 ч.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

using InvoiceManager.Entities;
using InvoiceManager.Repositories;

namespace InvoiceManager.Controller
{
	/// <summary>
	/// Defines members for an entity controller used to create an abstraction layer over repositories/entities.
	/// </summary>
	public interface IEntityController<T> where T : IEntity
	{
		/// <summary>
		/// Represents mapping between column headers and property names.
		/// </summary>
		Dictionary<string, string> Mapping { get; }
		
		/// <summary>
		/// Returns a collection of items
		/// </summary>
		List<T> GetDataSource();
		
		/// <summary>
		/// Represents the currently selected item in the list.
		/// </summary>
		T SelectedItem { get; set; }
		
		/// <summary>
		/// Call appropriate AddEntity Control through FormFactory
		/// </summary>
		void CreateForm();
		/// <summary>
		/// Call appropriate EditEntity Control through FormFactory
		/// </summary>
		void UpdateForm(T entity);
		/// <summary>
		/// Call confirmation form
		/// </summary>
		void DeleteForm(T entity);
		
		void Create(T entity);
		void Update(T entity);
		void Delete(T entity);
	}
}
