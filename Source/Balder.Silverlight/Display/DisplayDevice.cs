using System;
using Balder.Core.Display;
using System.Collections.Generic;
using Balder.Core.Execution;
using Balder.Silverlight.SoftwareRendering;

namespace Balder.Silverlight.Display
{
	public class DisplayDevice : IDisplayDevice
	{
		public event DisplayEvent Update = (d) => { };
		public event DisplayEvent Render = (d) => { };

		private readonly List<Display> _displays;
		private readonly IPlatform _platform;

		public DisplayDevice(IPlatform platform)
		{
			_platform = platform;
			_displays = new List<Display>();
			InitializeRendering();
		}

		private void InitializeRendering()
		{
			if (!_platform.IsInDesignMode)
			{
				RenderingManager.Instance.Render += RenderingManagerRender;
				RenderingManager.Instance.Show += RenderingManagerShow;
				RenderingManager.Instance.Clear += RenderingManagerClear;
				RenderingManager.Instance.Swapped += RenderingManagerSwapped;
				RenderingManager.Instance.Updated += RenderingManagerUpdated;
				RenderingManager.Instance.Start();
			}
		}


		private void RenderingManagerRender()
		{
			foreach (var display in _displays)
			{
				display.PrepareRender();
				Render(display);
			}
		}

		private void RenderingManagerClear()
		{
			CallMethodOnDisplays(d => d.Clear());
		}

		private void RenderingManagerShow()
		{
			CallMethodOnDisplays(d => d.Show());
		}

		private void RenderingManagerSwapped()
		{
			CallMethodOnDisplays(d => d.Swap());
		}

		private void RenderingManagerUpdated()
		{
			CallMethodOnDisplays(d => d.Update());
			foreach (var display in _displays)
			{
				Update(display);
			}
		}

		public IDisplay CreateDisplay()
		{
			var display = new Display(_platform);
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

		public void RenderAndShow(Display display)
		{
			display.PrepareRender();
			display.Swap();
			display.Clear();
			Render(display);
			display.Swap();
			display.Show();
			display.Update();
		}
	}
}
