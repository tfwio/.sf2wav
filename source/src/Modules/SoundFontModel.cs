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
using System;
using System.Collections.Generic;
using System.IO;
using on.soundfont_2_41;
using SFGenConst = on.soundfont_2_41.SoundFont2.SFGenConst;
using SFSampleLink = on.soundfont_2_41.SoundFont2.SFSampleLink;
namespace on.snd_module
{
	public class SoundFontModel
	{
		public string Name { get; set; }
		public string FileName { get; set; }
		public SoundFont2 sf2 { get; set; }

    public List<SFBank> Bank {
      get { return bank; }
      set { bank = value; }
    } List<SFBank> bank = new List<SFBank>();

		public List<SampleListItem> Sample = new List<SampleListItem>();
		
		public SoundFontModel(string fileName)
		{
			sf2 = new SoundFont2(fileName);
			Name = IOHelper.GetZerodStr(sf2.nfo.inam.StrValue) ?? "[value was null]";
		}
	}
}


