using System;
using System.Windows.Media;

namespace InvoiceManager.Windows
{
	public interface IMainWindow
	{
		void PrintStatus(string message);
		void PrintStatus(string message, SolidColorBrush brush);
	}
}
