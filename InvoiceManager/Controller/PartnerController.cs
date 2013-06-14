/*
 * Created by Stoyan Revov
 * Date: 14.6.2013 г.
 * Time: 22:01 ч.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;

using InvoiceManager.Entities;
using InvoiceManager.Repositories;
using InvoiceManager.Services;

namespace InvoiceManager.Controller
{
	/// <summary>
	/// PartnerController provides controls to a DataBrowserControl for CRUD with partners.
	/// </summary>
	public class PartnerController : IEntityController<Partner>
	{
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
		
		public Partner SelectedItem { get; set; }
		
		public List<Partner> GetDataSource()
		{
			return (List<Partner>)partnerRepository.RetrieveAll();
		}
		
		public void CreateForm()
		{
			throw new NotImplementedException();
		}
		
		public void UpdateForm(Partner entity)
		{
			throw new NotImplementedException();
		}
		
		public void DeleteForm(Partner entity)
		{
			throw new NotImplementedException();
		}
		
		public void Create(Partner entity)
		{
			partnerRepository.Create(entity);
		}
		
		public void Update(Partner entity)
		{
			partnerRepository.Update(entity);
		}
		
		public void Delete(Partner entity)
		{
			partnerRepository.Delete(entity.BaseID);
		}
	}
}
