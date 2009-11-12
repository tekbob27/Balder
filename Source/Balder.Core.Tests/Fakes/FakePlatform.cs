using System;
using Balder.Core.Assets;
using Balder.Core.Display;
using Balder.Core.Execution;
using Balder.Core.Input;

namespace Balder.Core.Tests.Fakes
{
	public class FakePlatform : IPlatform
	{
		public event PlatformStateChange BeforeStateChange = (p, s) => { };
		public event PlatformStateChange StateChanged = (p, s) => { };


		public FakePlatform()
		{
			DisplayDevice = new FakeDisplayDevice();
		}

		public string PlatformName
		{
			get { return "Fake"; }
		}

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
		public void RegisterAssetLoaders(IAssetLoaderService assetLoaderService)
		{
			
		}

		public void ChangeState(PlatformState state)
		{
			BeforeStateChange(this, state);
			CurrentState = state;
			StateChanged(this, state);
		}
	}
}