namespace Balder.Core.Animation
{
	public class AnimationManager
	{
		public static readonly AnimationManager	Instance = new AnimationManager();

		private readonly AnimatableCollection _animatables;

		private AnimationManager()
		{
			_animatables = new AnimatableCollection();
		}

		public void Register(Animatable animatable)
		{
			lock (_animatables)
			{
				_animatables.Add(animatable);
			}
		}
	}
}
