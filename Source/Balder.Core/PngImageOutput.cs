using System;
using System.IO;
using Balder.Core.SharpPNG;

namespace Balder.Core
{
	public class PngImageOutput : IPngImageOutput
	{
		public byte[] Bytes { get; private set; }

		private MemoryStream _stream;


		public void start(Png png)
		{
			Bytes = new byte[png.ihdr.width*png.ihdr.height*4];
			
			_stream = new MemoryStream();
		}

		public void writeLine(byte[] data, int offset)
		{
			_stream.Write(data,offset,data.Length-offset);
		}

		public void finish()
		{
			Bytes = _stream.GetBuffer();
		}
	}
}
