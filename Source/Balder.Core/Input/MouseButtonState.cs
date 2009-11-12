namespace Balder.Core.Input
{
	public class MouseButtonState
	{
		private bool _isEdge;
		public bool IsEdge
		{
			get { return _isEdge; }
			set
			{
				_isEdge = value;
			}
		}

		public bool IsPreviousEdge { get; set; }
		public bool IsDown { get; set; }
	}
}
