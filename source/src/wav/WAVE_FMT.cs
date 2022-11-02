/* tfwxo * 1/18/2016 * 9:56 PM */
using System.IO;
namespace System.WaveFormat
{
  class WAVE_FMT
  {
    readonly internal SUBCHUNK CKID = new SUBCHUNK{ Name=RiffType.fmt, Length=16 };
    /// <summary>
    /// 4 ('fmt ') + 16 (inner-data) = 20
    /// </summary>
    internal const int ChunkLength = 20;
    // 
    public ushort wFormatTag  = (ushort)WaveFormatEncoding.Pcm;
    public ushort nChannels;
    public uint   nSamplesPerSec;
    public uint   nAvgBytesPerSec;
    public ushort nBlockAlign;
    public ushort wBitsPerSample;
    
    internal void Config(int Fs=44100, ushort nch=1, ushort bps=16)
    {
      nChannels       = nch;
      nSamplesPerSec  = (uint)Fs;
      nBlockAlign     = 2; // Convert.ToUInt16(bps / 8); // sizeof(short);
			nAvgBytesPerSec = Convert.ToUInt32(nBlockAlign * Fs);
      wBitsPerSample  = bps;
    }
    /// <summary>
    /// Write all but data which is to be written directly after calling this.
    /// </summary>
    /// <param name="writer"></param>
    public void Write(BinaryWriter writer)
    {
      // begin format block
      CKID.Write(writer);            // 08
      // ------------------------------ bytes written
      writer.Write(wFormatTag);      // 02 : 0x0001 (PCM) --- two bytes written
      writer.Write(nChannels);       // 04 : 1
      writer.Write(nSamplesPerSec);  // 08 : 44100 or whatever
      writer.Write(nAvgBytesPerSec); // 12 : 
      writer.Write(nBlockAlign);     // 14 : 2 // see wBitsPerSample, as we're aligning to two bytes
      writer.Write(wBitsPerSample);  // 16 : 0x0000 is two 8bit bytes = 16bit or 16
    }
  }
  
}
