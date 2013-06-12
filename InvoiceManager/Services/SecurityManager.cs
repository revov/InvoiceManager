using System;
using System.Security.Cryptography;
using System.Text;

using InvoiceManager.Entities;
using InvoiceManager.Repositories;

namespace InvoiceManager.Services
{
	/// <summary>
	/// Static class for storing session data.
	/// </summary>
	public static class SecurityManager
	{
		private static SessionInfo _currentSessionInfo;
		public static SessionInfo CurrentSessionInfo { get{return _currentSessionInfo;} }
		
		public static string CalculateSHA1(string password)
		{
			ASCIIEncoding encoder = new ASCIIEncoding();
		    byte[] buffer = encoder.GetBytes(password);
		    SHA1CryptoServiceProvider cryptoTransformSHA1 = new SHA1CryptoServiceProvider();
		    string hash = BitConverter.ToString(cryptoTransformSHA1.ComputeHash(buffer)).Replace("-", "").ToLower();
		
		    return hash;
		}
		
		/// <summary>
		/// Checks the user's username and password in the database; If a user is found the Currentuser and CurrentRole are set.
		/// </summary>
		public static bool AuthenticateUser(string username, string password)
		{
			try
			{
				_currentSessionInfo = new SessionInfo();
				IRepository<User> userRepository = RepositoryFactory<User>.Initialize();
				CurrentSessionInfo.CurrentUser = userRepository.Retrieve(username);
				if (CurrentSessionInfo.CurrentUser.PASSWORD == CalculateSHA1(password))
				{
					Logger.Log("Вход в системата.");
					return true;
				}
				else 
				{
					_currentSessionInfo = null;
					return false;
				}
			}
			catch (InvalidOperationException)
			{
				return false;
			}
		}
		
		/// <summary>
		/// Nullifies the CurrentSessionInfo and logs it.
		/// </summary>
		public static void DeAuthenticateUser()
		{
			Logger.Log("Изход от системата.");
			_currentSessionInfo = null;
		}
	}
}
