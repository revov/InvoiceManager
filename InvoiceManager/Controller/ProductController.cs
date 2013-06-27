/*
 * Created by Stoyan Revov
 * Date: 27.6.2013 г.
 * Time: 14:15 ч.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

using InvoiceManager.Entities;
using InvoiceManager.Repositories;
using InvoiceManager.Services;

namespace InvoiceManager.Controller
{
	/// <summary>
	/// Singleton. ProductController provides controls to a DataBrowserControl for CRUD with products.
	/// </summary>
	public sealed class ProductController : IEntityController
	{
		#region singleton
		static readonly ProductController _productController = new ProductController();
		private ProductController() {}
		public static ProductController Instance { get {return _productController;} }
		#endregion
		
		IRepository<Product> productRepository = RepositoryFactory<Product>.Initialize();
		
		readonly Dictionary<string, string> _mapping = new Dictionary<string, string>()
		{
			{"Артикул №", "ID"},
			{"Наименование", "PRODUCT_NAME"},
			{"Мерна единица", "MEASUREMENT_UNIT"},
			{"Цена в лв.", "PRICE"}
		};
		
		#region interface implementation
		public Dictionary<string, string> Mapping
		{
			get { return _mapping; }
		}
		
		public IEntity SelectedItem { get; set; }
		
		public List<IEntity> GetDataSource()
		{
			return ((IEnumerable<IEntity>)productRepository.RetrieveAll()).ToList();
		}
		
		public void CreateForm()
		{
			ContentManager.ShowContent(FormFactory<Product>.InitializeAddForm());
		}
		
		public void UpdateForm()
		{
			ContentManager.ShowContent(FormFactory<Product>.InitializeEditForm((Product)SelectedItem));
		}
		
		public void DeleteForm()
		{
			MessageBoxResult res = MessageBox.Show("Сигурни ли сте, че искате да изтриете продукта " + ((Product)SelectedItem).PRODUCT_NAME,
			               "Изтриване", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
			if (res == MessageBoxResult.Yes)
			{
				string message = string.Format("Изтрит продукт {0} с ID: {1}", ((Product)SelectedItem).PRODUCT_NAME, ((Product)SelectedItem).ID);
				if (Delete(SelectedItem))
				{
					ContentManager.PrintStatus(message);
					Logger.Log(message);
				}
			}
		}
		
		public bool Create(IEntity entity)
		{
			try
			{
				productRepository.Create((Product)entity);
				Logger.Log(string.Format("Добавен продукт {0} с ID: {1}", ((Product)entity).PRODUCT_NAME, ((Product)entity).ID));
				OnChanged();
				return true;
			}
			catch (Exception ex)
			{
				ContentManager.PrintStatus(ex.Message, Brushes.Red);
				return false;
			}
		}
		
		public bool Update(IEntity entity)
		{
			try
			{
				productRepository.Update((Product)entity);
				Logger.Log(string.Format("Променен продукт {0} с ID: {1}", ((Product)entity).PRODUCT_NAME, ((Product)entity).ID));
				OnChanged();
				return true;
			}
			catch (Exception ex)
			{
				ContentManager.PrintStatus(ex.Message, Brushes.Red);
				return false;
			}
		}
		
		public bool Delete(IEntity entity)
		{
			try
			{
				productRepository.Delete(entity.BaseID);
				Logger.Log(string.Format("Изтрит продукт {0} с ID: {1}", ((Product)entity).PRODUCT_NAME, ((Product)entity).ID));
				OnChanged();
				return true;
			}
			catch (Exception ex)
			{
				ContentManager.PrintStatus(ex.Message, Brushes.Red);
				return false;
			}
		}
		
		void OnChanged()
		{
			if (Changed != null)
				Changed.Invoke(this, new EventArgs());
		}
		
		public event EventHandler Changed;
		
		#endregion
		
	}
}
