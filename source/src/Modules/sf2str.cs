#region User/License
// oio * 2005-11-12 * 04:19 PM
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
using on.soundfont_2_41;
using SFGenConst = on.soundfont_2_41.SoundFont2.SFGenConst;
using SFSampleLink = on.soundfont_2_41.SoundFont2.SFSampleLink;
namespace on.snd_module
{
	class sf2str
	{
	  static public string IGEN_Range2String(SoundFont2 sf, int genIndex) { return string.Format("{0:00#} - {1:00#}", sf.hyde.igen[genIndex].Hi, sf.hyde.igen[genIndex].Lo); }

		static public string[] IGEN_Range2StringA(SoundFont2 sf, int genIndex)
		{
			return new string[] {
				IGEN_Range2String(sf, genIndex)
			};
		}

		static public string GenString(SoundFont2 sf, int genIndex)
		{
			return ((SFGenConst)sf.hyde.igen[genIndex].gen).ToString();
		}

		/// <summary>
		/// Centibels takes a value from 0 to 6000
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		static public short ConvertCentibels(short value)
		{
			return 0;
		}

		static public string GetRootKey(SoundFont2 mod, SoundFont2.IGEN gen)
		{
			return mod.nn[gen.Hi];
		}

		static public string GetKeyRange(SoundFont2 mod, int gen)
		{
			int hi = (int)mod.hyde.igen[gen].Hi, lo = (int)mod.hyde.igen[gen].Lo;
			return string.Format("{0} to {1}", mod.nn[hi], mod.nn[lo]);
		}

		static public string GetNumRange(SoundFont2.IGEN gen)
		{
			int hi = (int)gen.Hi, lo = (int)gen.Lo;
			return string.Format("{0} to {1}", hi, lo);
		}
	}
}



