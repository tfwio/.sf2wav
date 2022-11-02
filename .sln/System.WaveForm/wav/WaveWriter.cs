/* tfwxo * 1/18/2016 * 9:56 PM */
using System;
using System.IO;
namespace System.WaveFormat
{
  /// <summary>
  /// This class is used generally to write a simple
  /// Microsoft RIFF Wave file.
  /// </summary>
  class WaveWriter
  {
    IFFCHUNK riff { get; set; } = new IFFCHUNK {
      Name = RiffType.RIFF,
      Tag  = RiffType.WAVE
    };
    WAVE_FMT fmt_ { get; set; } = new WAVE_FMT();
    LISTINFO list { get; set; } = new LISTINFO();
    
    const  uint   data = RiffType.data;
    uint          dataLength;
    
    public WaveWriter(string comment, long nSamples, int Fs=44100, ushort nch=1, ushort bps=16)
    {
      fmt_.Config(Fs,nch,bps);
      list.Comment.Value = comment;
      dataLength = Convert.ToUInt32(nSamples*2);
      uint listLen = list.GetLength();
      string strLength = string.Format("{0:X4}",nSamples*2);
      // calculate total size to assign to (IFFCHUNK) riff section
      // ignore first
      riff.Length = 4 + // 'WAVE' (4) +
        (4+4+16) + // 'fmt ' (4) + 0x10000000 (4) + format (16)
        (8+dataLength) + //
        (8+listLen);
    }
    
    public void WriteHeader(BinaryWriter writer)
    {
      // we need to assign the total length of the RIFF-FILE to the riff header (IFFCHUNK)
      // see the constructor method...
      riff.Write(writer);
      fmt_.Write(writer);
      writer.Write(data);
      writer.Write(dataLength);
    }
    public void WriteTerminal(BinaryWriter writer)
    {
      // we need to assign the total length of the RIFF-FILE to the riff header (IFFCHUNK)
      // see the constructor method...
      list.Write(writer);
    }
  }
  
}
