/*
 * Created by Stoyan Revov
 * Date: 14.6.2013 г.
 * Time: 22:01 ч.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;

using InvoiceManager.Entities;
using InvoiceManager.Repositories;
using InvoiceManager.Services;

namespace InvoiceManager.Controller
{
	/// <summary>
	/// Singleton. PartnerController provides controls to a DataBrowserControl for CRUD with partners.
	/// </summary>
	public sealed class PartnerController : IEntityController
	{
		#region singleton
		static readonly PartnerController _partnerController = new PartnerController();
		private PartnerController() {}
		public static PartnerController Instance { get {return _partnerController;} }
		#endregion
		
		readonly Dictionary<string, string> _mapping = new Dictionary<string, string>()
		{
			{"ЕИК/ЕГН", "ID"},
			{"ИН по ЗДДС", "VAT_NUMBER"},
			{"Име", "PARTNER_NAME"},
			{"Адрес", "ADDRESS"},
			{"Пощенски код", "POST_CODE"}
		};
		IRepository<Partner> partnerRepository = RepositoryFactory<Partner>.Initialize();
		
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
			return ((IEnumerable<IEntity>)partnerRepository.RetrieveAll()).ToList();
		}
		
		public void CreateForm()
		{
			ContentManager.ShowContent(FormFactory<Partner>.InitializeAddForm());
		}
		
		public void UpdateForm()
		{
			ContentManager.ShowContent(FormFactory<Partner>.InitializeEditForm((Partner)SelectedItem));
		}
		
		public void DeleteForm()
		{
			MessageBoxResult res = MessageBox.Show("Сигурни ли сте, че искате да изтриете контрагентът " + ((Partner)SelectedItem).PARTNER_NAME,
			               "Изтриване", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
			if (res == MessageBoxResult.Yes)
			{
				string message = string.Format("Изтрит контрагент {0} с ID: {1}", ((Partner)SelectedItem).PARTNER_NAME, ((Partner)SelectedItem).ID);
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
				partnerRepository.Create((Partner)entity);
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
				partnerRepository.Update((Partner)entity);
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
				partnerRepository.Delete(entity.BaseID);
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
	}
}
