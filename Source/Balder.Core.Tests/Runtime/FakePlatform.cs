using System;
using Balder.Core.Display;
using Balder.Core.Execution;
using Balder.Core.Input;

namespace Balder.Core.Tests.Runtime
{
	public class FakePlatform : IPlatform
	{
		public event PlatformStateChange BeforeStateChange = (p, s) => { };
		public event PlatformStateChange StateChanged = (p, s) => { };
		public event EventHandler Render = (s, e) => { };
		public event EventHandler Update = (s, e) => { };

		public IDisplayDevice DisplayDevice { get; set; }
		public IMouseDevice MouseDevice { get; set; }
		public Type FileLoaderType
		{
			get { throw new NotImplementedException(); }
		}

		public Type GeometryContextType
		{
			get { throw new NotImplementedException(); }
		}

		public Type SpriteContextType
		{
			get { throw new NotImplementedException(); }
		}

		public Type ImageContextType
		{
			get { throw new NotImplementedException(); }
		}

		public PlatformState CurrentState { get; set; }

		public void ChangeState(PlatformState state)
		{
			BeforeStateChange(this, state);
			CurrentState = state;
			StateChanged(this, state);
		}
	}
}
