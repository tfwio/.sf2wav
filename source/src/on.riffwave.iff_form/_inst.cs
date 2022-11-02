/*
 * tfooo in #DEVLEOP
 * DateTime: 12/27/2006 : 9:04 AM
 */

using System;
using System.Runtime.InteropServices;

namespace on.riffwave.iff_form
{
	[ StructLayout( LayoutKind.Sequential, Pack=1, CharSet=CharSet.Ansi )]
	public struct _inst
	{	[MarshalAs(UnmanagedType.ByValTStr,SizeConst=4)]
		public	int       ckID; // 'inst'
		public	int				ckLength;
		
		public	sbyte			uNote;
		public	byte			fineTune; // -50 to +50
		public	byte			Gain;     // -128 to +127
		public	sbyte			noteLow;  // (0-127?)
		public	sbyte			noteHigh; // you know
		public	sbyte			velLow;   // 1 to 127
		public	sbyte			velHigh;  // 1 to 127
    public void Prepare(sbyte note, byte tune=0, byte gain=0, sbyte vlo=1, sbyte vhi=127)
    {
      Prepare(note,tune,gain,note,note,vlo,vhi);
    }
		public void Prepare(sbyte note, byte tune, byte gain, sbyte klo, sbyte khi, sbyte vlo=1, sbyte vhi=127)
		{
		  ckID = 0x69687374;
		  ckLength = 7; // +4=15;
		  uNote = note;
		  Gain = gain;
		  noteLow = klo;
		  noteHigh = khi;
		  velLow = vlo;
		  velHigh = vhi;
		}
	}
}
