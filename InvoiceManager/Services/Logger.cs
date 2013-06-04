using System;
using System.IO;

namespace InvoiceManager.Services
{
	/// <summary>
	/// Service, providing logging methods. Log data is local and can be found in log.txt
	/// </summary>
	public static class Logger
	{
		public static void Log(string message)
		{
			File.AppendAllText("log.txt",
			                   DateTime.Now.ToString() + " " +
			                   SecurityManager.CurrentSessionInfo.CurrentUser.ID +
			                   ". " + message + Environment.NewLine);
		}
	}
}
