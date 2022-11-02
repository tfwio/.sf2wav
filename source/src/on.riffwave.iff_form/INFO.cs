/*
 * tfooo in #DEVLEOP
 * DateTime: 12/27/2006 : 9:04 AM
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace on.riffwave.iff_form
{
  //https://sharkysoft.com/jwave/docs/javadocs/lava/riff/wave/doc-files/riffwave-frameset.htm
  //http://soundfile.sapp.org/doc/WaveFormat/
  /// <summary>
  /// "labl", "note", or "ltxt"---inam, isng
  /// </summary>
	[StructLayout(LayoutKind.Sequential, Pack=1, CharSet=CharSet.Ansi) ]
	public struct INFO
	{	[MarshalAs(UnmanagedType.ByValTStr,SizeConst=4)]
		public	uint    infoHead; // INFO?
		public	int			Length;
		public System.WaveFormat.iver ifil;
		public	ZSTR		inam;
		public	ZSTR		isng;
		public Dictionary<long,INFOsub> nfosub;
		
		public INFO(int ckSize, BinaryReader bir, FileStream fis)
		{
			ifil = new System.WaveFormat.iver();
			inam = new ZSTR();
			isng = new ZSTR();
			
			long origin = fis.Seek(8,SeekOrigin.Current);
			infoHead = bir.ReadUInt32e();
			Length = bir.ReadInt32e();
			long pos = fis.Position;
			nfosub = new Dictionary<long,INFOsub>();
			while (fis.Position < pos+ckSize-4)
			{
				long hand = fis.Position;
				INFOsub inx = new INFOsub(bir,fis,this);
				fis.Seek(hand+inx.Length+8,SeekOrigin.Begin);
			}
		}
	}
  [StructLayout(LayoutKind.Sequential) ]
  public struct INFOsub
  {  [MarshalAs(UnmanagedType.ByValTStr,SizeConst=4)]
    public  uint   Type;
    public  int    Length;
    public  INFOsub(BinaryReader bx, FileStream fx, INFO nfo)
    {
      long orig = fx.Position;
      Type = bx.ReadUInt32e();
      Length = bx.ReadInt32e();
      nfo.nfosub.Add(orig, this);
    }
    
  }
}
