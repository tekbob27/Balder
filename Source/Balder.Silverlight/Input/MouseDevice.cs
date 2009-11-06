using System.Windows;
using System.Windows.Input;
using Balder.Core.Input;

namespace Balder.Silverlight.Input
{
	public class MouseDevice : IMouseDevice
	{
		private UIElement _element;
		private int _xPosition;
		private int _yPosition;
		private bool _isLeftButtonPressed;

		public void Initialize(UIElement element)
		{
			_element = element;
			element.MouseLeftButtonDown += MouseLeftButtonDown;
			element.MouseLeftButtonUp += MouseLeftButtonUp;
			element.MouseMove += MouseMove;
		}

		private void MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			_isLeftButtonPressed = false;
		}

		private void MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			_isLeftButtonPressed = true;
		}

		private void MouseMove(object sender, MouseEventArgs e)
		{
			var position = e.GetPosition(_element);
			_xPosition = (int) position.X;
			_yPosition = (int) position.Y;
		}

		public bool IsButtonPressed(MouseButton button)
		{
			return _isLeftButtonPressed;
		}

		public int GetXPosition()
		{
			return _xPosition;
		}

		public int GetYPosition()
		{
			return _yPosition;
		}
	}
}
