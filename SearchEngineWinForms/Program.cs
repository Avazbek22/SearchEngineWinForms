using System;
using System.Windows.Forms;

namespace SearchEngineWinForms
{
	internal static class Program
	{
		[STAThread]
		static void Main()
		{
			ApplicationConfiguration.Initialize(); // .NET 8 шаблон
			Application.Run(new MainForm());
		}
	}
}
