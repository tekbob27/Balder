using System;
using Balder.Core.Display;
using System.Collections.Generic;
using Balder.Silverlight.SoftwareRendering;

namespace Balder.Silverlight.Display
{
	public class DisplayDevice : IDisplayDevice
	{
		private List<Display> _displays;


		public DisplayDevice()
		{
			_displays = new List<Display>();

			RenderingManager.Instance.Render += Render;
			RenderingManager.Instance.Show += Show;
			RenderingManager.Instance.Clear += Clear;
			RenderingManager.Instance.Swapped += Swapped;
			RenderingManager.Instance.Updated += Updated;

		}

		private void Render()
		{
		}

		private void Clear()
		{
			CallMethodOnDisplays(d=>d.Clear());
		}

		private void Show()
		{
			CallMethodOnDisplays(d => d.Show());
		}

		private void Swapped()
		{
			CallMethodOnDisplays(d => d.Swap());
		}

		private void Updated()
		{
		}
		

		public IDisplay CreateDisplay()
		{
			var display = new Display();
			_displays.Add(display);
			return display;
		}

		private void CallMethodOnDisplays(Action<Display> displayAction)
		{
			foreach (var display in _displays)
			{
				displayAction(display);
			}
			
		}
	}
}
