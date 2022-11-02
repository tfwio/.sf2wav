#region User/License
// oio * 7/17/2012 * 7:56 AM

// Permission is hereby granted, free of charge, to any person obtaining a
// copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included
// in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.
#endregion
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
namespace System.IO
{
	/// <summary>
	/// Structure Marshaling, String-casting, &#133;
	/// </summary>
	static public class IOHelper
	{
		static public T ReadChunk<T>(long position, Stream fs) where T : struct
		{
			T Structure = new T();
			fs.Seek(position, SeekOrigin.Begin);
			int size = Marshal.SizeOf(Structure/*cks.ckFmt*/);
			byte[] red = new byte[size];
			fs.Read(red, Convert.ToInt32(position), size);
			Structure = IOHelper.xread<T>(Structure, red);
			red = null;
			return Structure;
		}

		static public T ReadChunk<T>(int position, Stream fs) where T : struct
		{
			T Structure = new T();
			fs.Seek(position, SeekOrigin.Begin);
			int size = Marshal.SizeOf(Structure/*cks.ckFmt*/);
			byte[] red = new byte[size];
			fs.Read(red, Convert.ToInt32(position), size);
			Structure = IOHelper.xread<T>(Structure, red);
			red = null;
			return Structure;
		}

		static public T ReadChunk<T>(long position, BinaryReader bread) where T : struct
		{
			T Structure = new T();
			bread.BaseStream.Seek(position, SeekOrigin.Begin);
			byte[] red = bread.ReadBytes(Marshal.SizeOf(Structure));
			/*cks.ckFmt*/Structure = IOHelper.xread<T>(Structure, red);
			red = null;
			return Structure;
		}

		static public T[] ReadChunk<T>(long position, int numberOfChunks, Stream s) where T : struct
		{
			T[] Structure = new T[numberOfChunks];
			T test = new T();
			int StructureSize = Marshal.SizeOf(test);
			// the number of bytes we're going to read is "numberOfChunks * StructureSize"
			s.Seek(Convert.ToInt32(position), SeekOrigin.Begin);
			int size = Marshal.SizeOf(Structure);
			byte[] bytes = new byte[size];
			s.Read(bytes, Convert.ToInt32(position), size);
			Structure = IOHelper.xread<T[]>(Structure, bytes);
			bytes = null;
			return Structure;
		}

		static public T xread<T>(object o, byte[] data)
		{
			return (T)mread(o, data);
		}

		static public object mread(object reffer, byte[] data)
		{
			IntPtr hrez = Marshal.AllocHGlobal(Marshal.SizeOf(reffer));
			Marshal.Copy(data, 0, hrez, Marshal.SizeOf(reffer));
			reffer = Marshal.PtrToStructure(hrez, reffer.GetType());
			Marshal.FreeHGlobal(hrez);
			return reffer;
		}

		#region STRING CONVERSION UTILITY
		// this is a helper function that finds the first (char)0 in the string and
		// returns a string up to that point.
		public static string GetZerodStr(char[] stx)
		{
			int boo = GetString(stx).IndexOf((char)0, 0);
			string tst = GetString(stx);
			if (boo >= 0) tst = tst.Substring(0, boo).Trim();
			return tst;
		}

		/**
		 * There has got to be zero padding or something going on here.
		 **//// <summary>
		/// Converts char[] to Default String object
		/// </summary>
		/// <param name="inchr">[in] char[]</param>
		/// <returns>Default (encoding) string</returns>
		static public string GetString(char[] inchr)
		{
			var enc = System.Text.Encoding.ASCII;
			var c0 = enc.GetBytes(inchr);
			var c1 = enc.GetString(c0);
			return c1;
			//string m = ""; for (int i=0; i< inchr.Length; i++) m += inchr[i]; return m;
		}

		/// <summary>
		/// Converts byte[] to Default String object
		/// </summary>
		/// <param name="inchr">[in] byte[]</param>
		/// <returns>Default (encoding) string</returns>
		static public string GetString(byte[] inchr)
		{
			string m = "";
			for (int i = 0; i < inchr.Length; i++)
				m += (char)inchr[i];
			return m;
		}
	#endregion
	}
}


