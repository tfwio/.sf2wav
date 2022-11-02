/*
 * tfooo in #DEVLEOP
 * DateTime: 12/27/2006 : 9:04 AM
 */

using System;
using System.IO;
using System.Runtime.InteropServices;

namespace on.riffwave.iff_form
{
  [ StructLayout( LayoutKind.Sequential, Pack=1, CharSet=CharSet.Ansi )]
  public struct CHUNK
  {
    [MarshalAs(UnmanagedType.ByValTStr,SizeConst=4)]
    public  uint   ckID;      //  usually RIFF unless it's a sub-tag
    public  uint   ckLength;  //  minus eight
    public uint ckTag;    //  the tagname
    
    public CHUNK(BinaryReader bx)
    {
      this.ckID = bx.ReadUInt32e();
      this.ckLength = bx.ReadUInt32e();
      this.ckTag = bx.ReadUInt32e();
    }
  }
}
