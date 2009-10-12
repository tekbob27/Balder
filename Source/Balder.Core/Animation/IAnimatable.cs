using System;

namespace Balder.Core.Animation
{
	public enum AnimationStatus
	{
		Stopped=1,
		Playing,
		Paused
	}

	public enum AnimationDirection
	{
		Forward=1,
		Backwards,
	}

	public enum AnimationBehaviour
	{
		Once=1,
		Loop,
		PingPoing
	}

	public delegate void AnimationEventHandler(object sender, AnimationEventArgs e);

	public class AnimationEventArgs
	{
		private readonly Animatable _object;

		internal AnimationEventArgs(Animatable theObject)
		{
			_object = theObject;
		}

		public Animatable Object
		{
			get { return _object; }
		}
	}


	public abstract class Animatable
	{
		public AnimationStatus Status { get; private set; }
		public AnimationDirection Direction { get; private set; }
		public AnimationBehaviour Behaviour { get; private set; }

		public event AnimationEventHandler AnimationLooped;
		public event AnimationEventHandler AnimationFinished;

		protected abstract void HandleAnimation(TimeSpan deltaTime);

		public void Play()
		{
			
		}


		public void Play(AnimationBehaviour behaviour)
		{
			
		}

		public void Play(AnimationBehaviour behaviour, AnimationDirection direction)
		{
			
		}

		public void Play(AnimationBehaviour behaviour, AnimationDirection direction, int loopCount)
		{
			
		}

		public void Stop()
		{
			
		}

		public void Pause()
		{
			
		}
	}
}