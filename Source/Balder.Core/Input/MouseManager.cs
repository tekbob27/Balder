using Balder.Core.Utils;

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

					/*
					psInput->vEdge = (~psInput->vPrev) & (psInput->vCurrent);
					psInput->vRepeat = psInput->vEdge;
					 * */

					mouseButtonState.IsPreviousEdge = mouseButtonState.IsDown;
					mouseButtonState.IsDown = _mouseDevice.IsButtonPressed(button);
					mouseButtonState.IsEdge = false;
					mouseButtonState.IsEdge = (mouseButtonState.IsEdge^!mouseButtonState.IsPreviousEdge) & (mouseButtonState.IsDown);
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
