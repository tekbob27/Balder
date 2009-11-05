using System;
using Balder.Core.Runtime;

namespace Balder.Core.Input
{
	public class MouseManager : IMouseManager
	{
		private readonly IMouseDevice _mouseDevice;

		public MouseManager(IMouseDevice mouseDevice)
		{
			_mouseDevice = mouseDevice;
		}

		public void HandleButtonSignals(Mouse mouse)
		{
			var buttons = EnumHelper.GetValues<MouseButton>();
			foreach( var button in buttons )
			{
				var mouseButtonState = GetMouseButtonStateObjectFromMouse(mouse, button);
				if (null != mouseButtonState)
				{
					mouseButtonState.IsDown = _mouseDevice.IsButtonPressed(button);
					mouseButtonState.IsEdge = mouseButtonState.IsDown & !mouseButtonState.IsPreviousEdge;
				}
			}
		}


		private static MouseButtonState GetMouseButtonStateObjectFromMouse(Mouse mouse, MouseButton button)
		{
			switch( button )
			{
				case MouseButton.Left:
					{
						return mouse.LeftButton;
					}
				
				case MouseButton.Right:
					{
						return mouse.RightButton;
					}
			}
			return null;
		}

		public void HandlePosition(Mouse mouse)
		{
			mouse.XPosition = _mouseDevice.GetXPosition();
			mouse.YPosition = _mouseDevice.GetYPosition();
		}
	}
}
