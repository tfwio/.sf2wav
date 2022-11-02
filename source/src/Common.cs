// 
// Copyright tfw 7/17/2012 * 7:56 AM
// 
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
using System;
using System.Windows.Forms;

namespace sf2wav
{
  static class FnFilterExt
  {
    readonly static char[] bad = { '<', '>', '!', ':', '/', '\\', '|', '?', '*', };

    static public bool Good(this char c)
    {
      foreach (char n in bad) return false;
      return char.IsLetterOrDigit(c) || char.IsWhiteSpace(c) || char.IsPunctuation(c);
    }

    static public string Filter(this string input)
    {
      string result = "";
      foreach (char c in input)
      {
        result += c.Good() ? c : '_';
      }
      return result;
    }
  }

}
namespace System
{
 /*
  * around half of this (file) is quite un-usable.
  */
  static class ckext
  {
    static System.Text.Encoding Check(this System.Text.Encoding enc)
    {
      if (enc == null) return System.Text.Encoding.Default;
      return enc;
    }
    static public string ToString4(this uint input, System.Text.Encoding enc = null)
    {
      return enc.Check().GetString(BitConverter.GetBytes(input));
    }
    static byte[] ToBytes(this string input, System.Text.Encoding enc = null)
    {
      return enc.Check().GetBytes(input);
    }
    static string CkidString(this string input)
    {
      var result = input;
      if (input.Length > 4) result = input.Substring(0, 4);
      if (input.Length < 4) result = input.PadRight(4, ' ');
      return result;
    }
    static public uint ToUInt32(this string input, System.Text.Encoding enc = null)
    {
      var bytes = input.CkidString().ToBytes(enc);
      return BitConverter.ToUInt32(bytes, 0);
    }
  }
	
  static class C2S
  {
    /// <summary>
    /// `char[]` to `string`.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    static public string getChars(this char[] input)
    {
      var result = "";
      foreach (char c in input)
        result += c;
      return result;
    }
  }
	static class Strings
	{
		internal const string Thousands = "##,###,###,##0";
		internal const string WaveInfo =
			"Rate: {0:##,###,###,##0} | " +
			"Block: {1:##,###,###,##0} | " +
			"Tag: {2:##,###,###,##0} | " +
			"CH: {3:##,###,###,##0}";
	}
	
	/// <summary>
	/// ListView Helper.
	/// </summary>
	static class Common
	{
		internal static void lvcols(ref ListView lv, string[] columns)
		{
			lv.Columns.Clear();
			foreach (string str in columns) { lv.Columns.Add(str);  }
		}
		internal static void lvsize(ref ListView lv, ColumnHeaderAutoResizeStyle style)
		{
			try {
				foreach (ColumnHeader ch in lv.Columns) ch.AutoResize(style);
			}catch{}
		}
		internal static void lvsize(ListView[] lv, ColumnHeaderAutoResizeStyle style)
		{
			for (int i=0;i<lv.Length;i++) lvsize(ref lv[i],style);
		}
		internal static void lVisi(ListView lv, bool flag)
		{
			lv.Visible = flag;
		}
		internal static void SetVisibility(ListView[] lvz, bool flag)
		{
			foreach (ListView lv in lvz) lv.Visible = flag;
		}
		
	}
}
