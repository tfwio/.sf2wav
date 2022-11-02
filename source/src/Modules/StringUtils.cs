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
using System.Drawing;
using System.IO;
using on.soundfont_2_41;
using SFGenConst = on.soundfont_2_41.SoundFont2.SFGenConst;
using SFSampleLink = on.soundfont_2_41.SoundFont2.SFSampleLink;
namespace on.snd_module
{
	static class StringUtils
	{

		static public string ToStringSL(this ushort shortType) {
	    return string.Format("{0}:{1}", ((SFSampleLink)shortType), shortType);
	  }
		static public string ToStringK(this int input) { return input.ToString("##,###,###,##0"); }
		
		static public string ToStringK(this byte input) { return input.ToString("##,###,###,##0"); }

		static public string ToString_SHDR_Length(this SoundFont2.SHDR input)
		{
			return (input.LenB - input.LenA).ToStringK();
		}
		static public string[] ToString_SHDR(this SoundFont2 sf2, int sid)
		{
		  var shdr = sf2.hyde.shdr[sid];
		  
		  var rate = shdr.SampleRate.ToStringK();
		  var type = shdr.Type.ToStringSL();
		  var note = sf2.nn[shdr.Pitch];
		  var cent = shdr.PitchC.ToStringK();
		  
			return new string[] { rate, type, note, cent };
		}

		// Generate a SampleListItem from SampleID
		static public SampleListItem ToItem(this SoundFont2 sf2, int sid)
		{
		  var n = IOHelper.GetZerodStr(sf2.hyde.shdr[sid].iName);
			return new SampleListItem() {
				Name = n,
					ImageKey = "agrad",
					Columns = sf2.ToString_SHDR(sid)
				};
		}

		static string GetInstString(this SoundFont2 sf2, int igen_id)
		{
			var igen = sf2.hyde.igen[igen_id];
			var tgen = (SFGenConst)igen.gen;

			switch (tgen)
			{
				case SFGenConst.sampleID:          return string.Format("{0}:“{1}”", sf2str.GenString(sf2, igen_id), IOHelper.GetZerodStr(sf2.hyde.shdr[igen.Hi].iName));
				case SFGenConst.sampleModes:       return string.Format("{0}", sf2str.IGEN_Range2String(sf2, igen_id));
				case SFGenConst.keyRange:          return string.Format("{1} ({2})", igen.Generator, sf2str.GetNumRange(igen), sf2str.GetKeyRange(sf2, igen_id));
				case SFGenConst.overridingRootKey: return string.Format("{0}:{1}", igen.Generator, igen.Hi, sf2str.GetRootKey(sf2, igen));
				case SFGenConst.exclusiveClass:
				case SFGenConst.delayModEnv:
				case SFGenConst.attackModEnv:
				case SFGenConst.decayModEnv:
				case SFGenConst.holdModEnv:
				case SFGenConst.sustainModEnv:
				case SFGenConst.releaseModEnv:
				case SFGenConst.pan:
          return $"{igen.SHORT}i";
				case SFGenConst.delayVibLFO:
				case SFGenConst.delayModLFO:
				// timecents
				case SFGenConst.freqModLFO:
				// freq:cents
				case SFGenConst.freqVibLFO:
				// freq:cents
				case SFGenConst.modLfoToFilterFc:
				// degree:cents
				case SFGenConst.modLfoToPitch:
				// degree:cents
				case SFGenConst.modLfoToVolume:
          return $"amt: 0x{igen.WORD:X4}, {igen.SHORT}i, {igen.WORD}u";
				default:
          return $"amt: 0x{igen.WORD:X4}, {igen.SHORT}i, {igen.WORD}u, A: {igen.Hi}, B: {igen.Lo}";
      }
		}

		static readonly string[] ColourKeyStr = { "agreen", "bgreen", "ared", "acyan", "agrad" };

		static string GetInstImageKey(this SoundFont2 sf2, int indexIGEN)
		{
			switch (sf2.hyde.igen[indexIGEN].Generator)
			{
				case SFGenConst.sampleID:
					return "agreen";
				case SFGenConst.sampleModes:
					return "bgreen";
				case SFGenConst.keyRange:
				case SFGenConst.overridingRootKey:
				case SFGenConst.exclusiveClass:
				case SFGenConst.pan:
					return "ared";
				case SFGenConst.delayModEnv:
				case SFGenConst.attackModEnv:
				case SFGenConst.decayModEnv:
				case SFGenConst.holdModEnv:
				case SFGenConst.delayModLFO:
				case SFGenConst.freqModLFO:
				case SFGenConst.sustainModEnv:
				case SFGenConst.releaseModEnv:
				case SFGenConst.modLfoToFilterFc:
				case SFGenConst.modLfoToPitch:
				case SFGenConst.modLfoToVolume:
					return "acyan";
				default:
					return "agrad";
			}
		}
	}
}


