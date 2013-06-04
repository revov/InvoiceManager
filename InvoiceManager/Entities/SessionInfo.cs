using System;
using InvoiceManager.Repositories;

namespace InvoiceManager.Entities
{
	/// <summary>
	/// Holds information about current user, 
	/// </summary>
	public class SessionInfo
	{
		private User _currentUser;
		private Role _currentRole;
		private Partner _currentCompany;
		
		/// <summary>
		/// Setting the CurrentUser automatically sets the CurrentRole and CurrentCompany
		/// </summary>
		public User CurrentUser
		{
			get { return _currentUser; }
        	set
        	{
        		_currentUser = value;
        		if (value!=null)
        		{
	        		_currentRole = RoleRepository.Retrieve(value.ROLE_ID);
	        		if (value.SELLER_ID!="")
	        			_currentCompany = PartnerRepository.Retrieve(value.SELLER_ID);
	        		else _currentCompany = null;
        		}
        		else
        		{
        			_currentRole = null;
        			_currentCompany = null;
        		}
			}
		}
		
		public Role CurrentRole
		{
			get {return _currentRole;}
		}
		
		public Partner CurrentCompany
		{
			get {return _currentCompany;}
		}
	}
}
