#region User/License
// 
// Copyright (c) Thomas F Wroble 2003-2016
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
#endregion
using on.soundfont_2_41;
using SFSampleLink = on.soundfont_2_41.SoundFont2.SFSampleLink;
namespace on.snd_module
{
    static class MStringHelpers
	{
		static public string GetSampleRate(this SoundFont2.SHDR shdr)
		{
			return shdr.SampleRate.ToString("##,###,##0");
		}

		static public string GetSampleLength(this SoundFont2.SHDR shdr)
		{
			return (shdr.LenB - shdr.LenA).ToString("##,###,##0");
		}

		static public string GetSampleLink(this SoundFont2.SHDR shdr)
		{
			switch ((SFSampleLink)shdr.Type) {
				case SFSampleLink.monoSample:
					return "mono";
				case SFSampleLink.leftSample:
					return "left";
				case SFSampleLink.rightSample:
					return "right";
				default:
					return string.Format("{0} {1}", ((SFSampleLink)shdr.Type).ToString(), (shdr.Link));
			}
		}
	}
}


