/*
 * tfooo in #DEVLEOP
 * DateTime: 12/27/2006 : 9:04 AM
 */

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace on.riffwave.iff_form
{
	[ StructLayout( LayoutKind.Sequential, Pack=1, CharSet=CharSet.Ansi )]
	public struct ChunkCollection
	{
		public	CHUNK					ckMain;
		public	Dictionary<long,SUBCHUNK>		SubChunks;
		public	WaveFormat		ckFmt;
		public	ChunkFact			ckFact;
		public  System.WaveFormat._smp ckSmpl;
		public	_inst					ckInst;
		[MarshalAs(UnmanagedType.ByValArray)]
		public System.WaveFormat._smpLoop[]		ckSmpLoop;
	}
}
