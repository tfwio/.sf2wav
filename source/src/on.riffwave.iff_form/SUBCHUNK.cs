// 
// Copyright tfw 12/27/2006 : 9:04 AM
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
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace on.riffwave.iff_form
{
	[ StructLayout( LayoutKind.Sequential, Pack=1, CharSet=CharSet.Ansi )]
	public struct SUBCHUNK
	{
		static readonly SUBCHUNK empty = new SUBCHUNK(){ ckID=0, ckLength=-1};
		public static SUBCHUNK Empty { get { return empty; } }
		
		[MarshalAs(UnmanagedType.ByValTStr,SizeConst=4)]
		public	uint			ckID;		//	usually RIFF unless it's a sub-tag
		public	int				ckLength;	//	minus eight
		public SUBCHUNK(BinaryReader bx)
		{
			this.ckID = bx.ReadUInt32e();
			try
			{
				this.ckLength = bx.ReadInt32();
			}
			catch (System.IO.EndOfStreamException e)
			{
				System.Windows.Forms.MessageBox.Show(e.Message, ckID.ToString4());
        //	this.ckID = null;
        this.ckLength = 0;
			}
		}
	}
}
