using System;
using System.IO;
using System.Windows.Media;
using Balder.Core.SoftwareRendering;
using System.Windows.Media.Imaging;

namespace Balder.Silverlight.SoftwareRendering
{
	public class RawPngBufferStream : Stream, IFrameBuffer
	{
		public struct BlockInfo
		{
			public byte[] LengthBytes;
			public byte[] ComplimentLengthBytes;
			
		}

		#region Offsets
		public enum PreBufferOffsets
		{
			Start = 0,
			FileHeaderOffset = 0,
			IHDROffset = 8,			// Header = 4 bytes type, 4 bytes length, 13 bytes data, 4 bytes CRC

			GAMAOffset = 33,		// Gamma = 4 bytes type, 4 bytes length, 4 bytes data, 4 bytes crc

			DataLengthOffset = 49,	// 4 bytes
			IDATOffset = 53,		// 4 bytes

			HeadersOffset = 57,		// Headers = 2 bytes
			DataOffset = 59,		// actual data
			End = 59
		}

		public enum PostBufferOffsets
		{
			Start = 0,
			Adler = 0,
			IENDOffset = 4,
			EOF = 20
		}
		#endregion

		#region PNG Consts and statics
		private const int _ADLER32_BASE = 65521;
		private const int _ADLER32_BITWIZE_AND = _ADLER32_BASE - 1;
		private const int _MAXBLOCK = 0xFFFF;
		private static byte[] _HEADER = { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A }; // 8 bytes
		private static byte[] _IHDR = { (byte)'I', (byte)'H', (byte)'D', (byte)'R' };		// 4 bytes
		private static byte[] _GAMA = { (byte)'g', (byte)'A', (byte)'M', (byte)'A' };		// 4 bytes
		private static byte[] _IDAT = { (byte)'I', (byte)'D', (byte)'A', (byte)'T' };		// 4 bytes
		private static byte[] _IEND = { (byte)'I', (byte)'E', (byte)'N', (byte)'D' };		// 4 bytes
		private static byte[] _ARGB = { 0, 0, 0, 0, 0, 0, 0, 0, 8, 6, 0, 0, 0 };			// 13 bytes
		private static uint[] _crcTable = new uint[256];
		#endregion

		#region Private members

		private uint _rowLength;
		private uint _postBufferOffset;
		private uint _rowsPerBlock;
		private uint _blockSize;
		private uint _blockCount;
		private uint _totalBufferSize;

		private uint _length;

		private byte[] _preBufferBytes;
		private byte[] _postBufferBytes;
		private byte[] _buffer;

		private BlockInfo[] _bufferBlockInfos;
		#endregion

		#region Constructor(s)
		static RawPngBufferStream()
		{
			MakeCRCTable();
		}

		public RawPngBufferStream()
		{
			
		}

		public RawPngBufferStream(int width, int height)
		{
			Initialize(width,height);
		}
		#endregion

		#region Private static PNG helpers
		private static void MakeCRCTable()
		{
			uint c;

			for (int n = 0; n < 256; n++)
			{
				c = (uint)n;
				for (int k = 0; k < 8; k++)
				{
					if ((c & (0x00000001)) > 0)
					{
						c = 0xEDB88320 ^ (c >> 1);
					}
					else
					{
						c = c >> 1;
					}
				}
				_crcTable[n] = c;
			}
		}

		private static uint UpdateCRC(uint crc, byte[] buf, int len)
		{
			uint c = crc;

			for (int n = 0; n < len; n++)
			{
				c = _crcTable[(c ^ buf[n]) & 0xFF] ^ (c >> 8);
			}

			return c;
		}


		/* Return the CRC of the bytes buf[0..len-1]. */
		private static uint GetCRC(byte[] buf)
		{
			return UpdateCRC(0xFFFFFFFF, buf, buf.Length) ^ 0xFFFFFFFF;
		}

		private static uint ComputeAdler32(byte[] buf)
		{
			uint s1 = 1;
			uint s2 = 0;

			for (var idx = 0; idx < buf.Length; idx++)
			{
				s1 = (s1 + (uint)buf[idx]) & _ADLER32_BITWIZE_AND;
				s2 = (s2 + s1) & _ADLER32_BITWIZE_AND;
			}

			return (s2 << 16) + s1;
		}

		#endregion

		#region Private Helpers

		private void SetupPreBuffer()
		{
			SetBuffer((int)PreBufferOffsets.FileHeaderOffset, _HEADER, _preBufferBytes, false);

			var widthBytes = BitConverter.GetBytes(Width);
			var heightBytes = BitConverter.GetBytes(Height);

			_ARGB[0] = widthBytes[3]; _ARGB[1] = widthBytes[2]; _ARGB[2] = widthBytes[1]; _ARGB[3] = widthBytes[0];
			_ARGB[4] = heightBytes[3]; _ARGB[5] = heightBytes[2]; _ARGB[6] = heightBytes[1]; _ARGB[7] = heightBytes[0];

			SetChunk((int)PreBufferOffsets.IHDROffset, _IHDR, _ARGB, _preBufferBytes);

			var gammaBytes = BitConverter.GetBytes(1 * 100000);
			var gammaReversedBytes = new byte[4]
			                         	{
											gammaBytes[3],
											gammaBytes[2],
											gammaBytes[1],
											gammaBytes[0]
			                         	};
			SetChunk((int)PreBufferOffsets.GAMAOffset, _GAMA, gammaReversedBytes, _preBufferBytes);

			SetBuffer((int)PreBufferOffsets.IDATOffset, _IDAT, _preBufferBytes, false);

			_preBufferBytes[(int)PreBufferOffsets.HeadersOffset] = 0x78;
			_preBufferBytes[(int)PreBufferOffsets.HeadersOffset + 1] = 0xDA;
		}

		private void SetupPostBuffer()
		{
			SetChunk((int)PostBufferOffsets.IENDOffset, _IEND, new byte[0], _postBufferBytes);
		}


		private void CalculateBlocks()
		{
			_totalBufferSize = _rowLength * (uint)Height;
			_rowsPerBlock = _MAXBLOCK / _rowLength;
			_blockSize = _rowsPerBlock * _rowLength;

			if ((_totalBufferSize % _blockSize) == 0)
			{
				_blockCount = _totalBufferSize / _blockSize;
			}
			else
			{
				_blockCount = (_totalBufferSize / _blockSize) + 1;
			}
		}

		private void CalculatePostBufferOffset()
		{
			var remainder = _totalBufferSize;
			ushort length = 0;
			_postBufferOffset = (int)PreBufferOffsets.DataOffset;

			for (uint blocks = 0; blocks < _blockCount; blocks++)
			{
				length = (ushort)((remainder < _blockSize) ? remainder : _blockSize);

				// Blocktype
				_postBufferOffset++;

				// Length
				_postBufferOffset += 2;

				// Length compliment
				_postBufferOffset += 2;

				_postBufferOffset += length;

				remainder -= _blockSize;
			}
		}


		private static void SetChunk(int offset, byte[] type, byte[] data, byte[] destination)
		{
			var size = type.Length;
			var buffer = new byte[type.Length + data.Length];

			// Initialize buffer
			for (var index = 0; index < type.Length; index++)
			{
				buffer[index] = type[index];
			}

			for (var index = 0; index < data.Length; index++)
			{
				buffer[index + size] = data[index];
			}

			// Write length
			var lengthBytes = BitConverter.GetBytes(data.Length);
			SetBuffer(offset, lengthBytes, destination, true);


			// Write type and data
			SetBuffer(offset + lengthBytes.Length, buffer, destination, false);

			// Compute and write the CRC
			var crcBytes = BitConverter.GetBytes(GetCRC(buffer));
			SetBuffer(offset + lengthBytes.Length + buffer.Length, crcBytes, destination, true);
		}

		private static void SetBuffer(int offset, byte[] data, byte[] destination, bool reversed)
		{
			var length = data.Length;

			if (reversed)
			{
				for (var index = 0; index < length; index++)
				{
					destination[offset + index] = data[length - 1 - index];
				}
			}
			else
			{
				for (var index = 0; index < length; index++)
				{
					destination[offset + index] = data[index];
				}
			}

		}

		private void CopyPreBuffer(byte[] destination)
		{
			for (var byteIndex = 0; byteIndex < _preBufferBytes.Length; byteIndex++)
			{
				destination[byteIndex + (int)PreBufferOffsets.Start] = _preBufferBytes[byteIndex];
			}

		}

		private void CopyBufferLength(byte[] destination)
		{
			var length = _postBufferOffset - (int)PreBufferOffsets.End + 6;
			var lengthBytes = BitConverter.GetBytes(length);
			//BitConverter.GetBytes(_totalBufferSize+6);
			SetBuffer((int)PreBufferOffsets.DataLengthOffset, lengthBytes, destination, true);
		}


		private void SetupBlockInfos()
		{
			_bufferBlockInfos = new BlockInfo[_blockCount];
			ushort length = 0;
			var remainder = _totalBufferSize;
			for (var blockIndex = 0; blockIndex < _blockCount; blockIndex++)
			{
				length = (ushort) ((remainder < _blockSize) ? remainder : _blockSize);
				var lengthData = BitConverter.GetBytes(length);
				var complimentLengthData = BitConverter.GetBytes((ushort)~length);

				_bufferBlockInfos[blockIndex].LengthBytes = lengthData;
				_bufferBlockInfos[blockIndex].ComplimentLengthBytes = complimentLengthData;

				remainder -= _blockSize;
			}
		}

		private void CopyBufferAsBlocks(byte[] destination)
		{
			var remainder = _totalBufferSize;
			ushort length = 0;
			var bufferIndex = (int)PreBufferOffsets.DataOffset;
			var sourceIndex = 0;

			for (var blockIndex = 0; blockIndex < _blockCount; blockIndex++)
			{
				length = (ushort)((remainder < _blockSize) ? remainder : _blockSize);

				if (length == remainder)
				{
					destination[bufferIndex] = 0x01;
				}
				else
				{
					destination[bufferIndex] = 0x00;
				}
				bufferIndex++;

				var lengthData = _bufferBlockInfos[blockIndex].LengthBytes;
				SetBuffer(bufferIndex, lengthData, destination, false);
				bufferIndex += 2;

				var complimentLengthData = _bufferBlockInfos[blockIndex].ComplimentLengthBytes;
				SetBuffer(bufferIndex, complimentLengthData, destination, false);
				bufferIndex += 2;

				// Write blocks
				for (var byteIndex = 0; byteIndex < length; byteIndex++)
				{
					destination[bufferIndex + byteIndex] = _buffer[sourceIndex + byteIndex];
				}
				bufferIndex += length;
				sourceIndex += length;

				// Next block
				remainder -= _blockSize;
			}
		}

		private void CopyPostBuffer(byte[] destination)
		{
			for (var byteIndex = 0; byteIndex < _postBufferBytes.Length; byteIndex++)
			{
				destination[byteIndex + _postBufferOffset + (int)PostBufferOffsets.Start] = _postBufferBytes[byteIndex];
			}
		}

		private void CopyAdler(byte[] destination)
		{
			var adler = ComputeAdler32(_buffer);
			var adlerBytes = BitConverter.GetBytes(adler);
			SetBuffer((int)_postBufferOffset + (int)PostBufferOffsets.Adler, adlerBytes, destination, true);
		}

		#endregion

		#region Public Properties
		public int Width { get; private set; }
		public int Height { get; private set; }
		#endregion

		#region IFrameBuffer Implementation

		public void Initialize(int width, int height)
		{
			Width = width;
			Height = height;

			_preBufferBytes = new byte[(int)PreBufferOffsets.DataOffset + 4];
			_rowLength = (uint)width * 4 + 1;
			Stride = (int)_rowLength;
			_buffer = new byte[_rowLength * height];
			_postBufferBytes = new byte[(int)PostBufferOffsets.EOF];

			CalculateBlocks();
			CalculatePostBufferOffset();
			SetupPreBuffer();
			SetupPostBuffer();
			SetupBlockInfos();

			_length = _postBufferOffset + (int)PostBufferOffsets.EOF;

			Clear(Color.FromArgb(0xff, 0, 0, 0));
		}

		public int Stride { get; private set; }
		public int RedPosition { get { return 0; } }
		public int GreenPosition { get { return 1; } }
		public int BluePosition { get { return 2; } }
		public int AlphaPosition { get { return 3; } }

		public void Clear(Color color)
		{
			var offset = 1;
			for (var y = 0; y < Height; y++)
			{
				for (var x = 0; x < Width; x++)
				{
                   _buffer[offset + RedPosition]    = color.R;
                   _buffer[offset + GreenPosition]  = color.G;
                   _buffer[offset + BluePosition]   = color.B;
                   _buffer[offset + AlphaPosition]  = color.A;

					offset += 4;
				}
				offset++;
			}
		}

		public void SetPixel(int x, int y, Color color)
		{
			
		}

		public Color GetPixel(int x, int y)
		{
			return Color.FromArgb(0, 0, 0, 0);
		}

        public void Begin()
        {
        }

        public void End()
        {
        }

        public BitmapImage BitmapSource { get { return null; } }

		public byte this[int offset]
		{
			get { return _buffer[offset+1]; }
			set { _buffer[offset+1] = value; }
		}

		#endregion

		#region Stream implementation
		public override void Flush()
		{

		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			var before = DateTime.Now;
			CopyPreBuffer(buffer);
			CopyBufferLength(buffer);
			
			CopyBufferAsBlocks(buffer);
			
			CopyAdler(buffer);
			CopyPostBuffer(buffer);
			var after = DateTime.Now;
			var delta = after.Subtract(before);
			return count;
		}


		public override bool CanRead
		{
			get { return true; }
		}

		public override bool CanSeek
		{
			get { return false; }
		}

		public override bool CanWrite
		{
			get { return false; }
		}

		public override long Length { get { return _length; } }

		public override long Position { get; set; }

		#region Unsupported
		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotImplementedException();
		}
		#endregion

		#endregion
	}
}
