using System;
using Balder.Core.Animation;

namespace Balder.Core.Implementation
{
	public class FrameAnimatable : Animatable
	{
		public AnimationStatus Status
		{
			get { throw new NotImplementedException(); }
		}

		public AnimationDirection Direction
		{
			get { throw new NotImplementedException(); }
			set { throw new NotImplementedException(); }
		}

		public AnimationBehaviour Behaviour
		{
			get { throw new NotImplementedException(); }
			set { throw new NotImplementedException(); }
		}

		public event AnimationEventHandler AnimationLooped;
		public event AnimationEventHandler AnimationFinished;

		protected override void HandleAnimation(TimeSpan deltaTime)
		{
			throw new NotImplementedException();
		}

		public void Play()
		{
			throw new NotImplementedException();
		}

		public void Play(AnimationBehaviour behaviour)
		{
			throw new NotImplementedException();
		}

		public void Play(AnimationBehaviour behaviour, AnimationDirection direction)
		{
			throw new NotImplementedException();
		}

		public void Play(AnimationBehaviour behaviour, AnimationDirection direction, int loopCount)
		{
			throw new NotImplementedException();
		}

		public void Stop()
		{
			throw new NotImplementedException();
		}

		public void Pause()
		{
			throw new NotImplementedException();
		}
	}
}
