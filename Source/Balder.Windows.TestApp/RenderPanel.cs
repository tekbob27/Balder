using System;
using System.Windows.Forms;
using Balder.Core.Runtime;
using Balder.Windows.Services;

namespace Balder.Windows.TestApp
{
	public class RenderPanel : Panel
	{
		public RenderPanel()
		{
			Invalidate();

			SetStyle(ControlStyles.OptimizedDoubleBuffer,true);
			SetStyle(ControlStyles.DoubleBuffer,true);

			if( DesignMode )
			{
				TargetDevice.Initialize();
				
			}
			
			if (!DesignMode)
			{
				var display = EngineRuntime.Instance.TargetDevice.Display;
				display.Draw += display_Draw;
			}

		}

		void display_Draw(object sender, EventArgs e)
		{
			//Invalidate();
		}
		 
		protected override void OnPaint(PaintEventArgs e)
		{
			//var display = EngineRuntime.Instance.TargetDevice.Display as Display;

			//display.BitmapMutex.WaitOne();
			//e.Graphics.DrawImageUnscaled(display.Bitmap, 0, 0);
			//display.BitmapMutex.ReleaseMutex();
			
		}
	}
}
             