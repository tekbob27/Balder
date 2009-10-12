using System;
using System.Diagnostics;
using System.Windows.Forms;
using Balder.Windows.Services;

namespace Balder.Windows.TestApp
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			TargetDevice.Initialize<MyGame>();

			Application.Run(new Form1());

			Process.GetCurrentProcess().Kill();
		}
	}
}
