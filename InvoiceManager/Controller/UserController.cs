/*
 * Created by Stoyan Revov
 * Date: 15.6.2013 г.
 * Time: 16:04 ч.
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
	/// Singleton. UserController provides controls to a DataBrowserControl for CRUD with users. 
	/// </summary>
	public sealed class UserController : IEntityController
	{
		#region singleton
		static readonly UserController _userController = new UserController();
		private UserController() {}
		public static UserController Instance { get {return _userController;} }
		#endregion
		
		readonly Dictionary<string, string> _mapping = new Dictionary<string, string>()
		{
			{"Потребител", "ID"},
			{"Привилегии", "ROLE_ID"},
			{"Фирма", "SELLER_ID"}
		};
		IRepository<User> userRepository = RepositoryFactory<User>.Initialize();
		
		public Dictionary<string, string> Mapping
		{
			get
			{
				return _mapping;
			}
		}
		
		public IEntity SelectedItem { get; set; }
		
		public List<IEntity> GetDataSource()
		{
			return ((IEnumerable<IEntity>)userRepository.RetrieveAll()).ToList();
		}
		
		public void CreateForm()
		{
			ContentManager.ShowContent(FormFactory<User>.InitializeAddForm());
		}
		
		public void UpdateForm()
		{
			ContentManager.ShowContent(FormFactory<User>.InitializeEditForm((User)SelectedItem));
		}
		
		public void DeleteForm()
		{
			MessageBoxResult res = MessageBox.Show("Сигурни ли сте, че искате да изтриете потребителят " + ((User)SelectedItem).ID,
			               "Изтриване", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
			if (res == MessageBoxResult.Yes)
			{
				string message = string.Format("Изтрит потребител {0}", ((User)SelectedItem).ID);
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
				userRepository.Create((User)entity);
				Changed.Invoke(this, new EventArgs());
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
				userRepository.Update((User)entity);
				Changed.Invoke(this, new EventArgs());
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
				userRepository.Delete(entity.BaseID);
				Changed.Invoke(this, new EventArgs());
				return true;
			}
			catch (Exception ex)
			{
				ContentManager.PrintStatus(ex.Message, Brushes.Red);
				return false;
			}
		}
		
		public event EventHandler Changed;
	}
}
