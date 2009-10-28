using System.Windows;
using System.Windows.Media.Animation;

namespace Balder.Silverlight.Extensions
{
	public static class StoryboardExtensions
	{
		public static void SetValueForKeyFrame(this Storyboard storyboard, string keyFrameName, double value)
		{
			foreach (var timeline in storyboard.Children)
			{
				if (timeline is DoubleAnimationUsingKeyFrames)
				{
					var animation = timeline as DoubleAnimationUsingKeyFrames;
					foreach (DoubleKeyFrame keyframe in animation.KeyFrames)
					{
						string name = keyframe.GetValue(FrameworkElement.NameProperty) as string;
						if (null != name && name == keyFrameName)
						{
							keyframe.Value = value;
							return;
						}
					}
				}
			}			
		}

	}
}