using System.Windows.Controls;
using Balder.Core;
using Balder.Core.Content;

namespace Balder.Silverlight.Controls
{
	public class RenderedNode : Control
	{
		public RenderableNode Node { get; protected set; }

		public virtual void Initialize(IContentManager contentManager, Scene scene)
		{
			
		}
	}
}
