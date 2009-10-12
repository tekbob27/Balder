using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Coverflow
{
	public partial class Page : UserControl
	{
		public Page()
		{
			InitializeComponent();

			this.KeyDown += new KeyEventHandler(Page_KeyUp);

			for (int i = 0; i < 10; i++  )
			{
				this._flow.AddCover();
			}
				
		}

		void Page_KeyUp(object sender, KeyEventArgs e)
		{
			
			if (e.Key == Key.Left)
			{
				this._flow.MovePrevious();
			}
			if (e.Key == Key.Right)
			{
				this._flow.MoveNext();
			}
			

			if (e.Key == Key.D1)
			{
				this._flow.MoveTo(0);
			}
			if (e.Key == Key.D5)
			{
				this._flow.MoveTo(4);
			}
			if (e.Key == Key.D0)
			{
				this._flow.MoveTo(9);
			}
		}
	}
}
