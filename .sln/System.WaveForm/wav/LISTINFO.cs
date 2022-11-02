/* tfwxo * 1/18/2016 * 9:56 PM */
using System.IO;
namespace System.WaveFormat
{
  /// should probably be titled something like DsListInfo
  class LISTINFO
  {
    IFFCHUNK ListInfo = new IFFCHUNK{ Name = ListType.LIST, Tag  = ListType.INFO };

    public uint  Length { get { return ListInfo.Length; } set { ListInfo.Length = value; } }
    public WZSTR Software = new WZSTR { Tag=ListType.Software, Value="DrumSynth v2.0 \0" };
    public WZSTR Comment = new WZSTR  { Tag=ListType.Comment, Value=" \0" };
    
    internal uint GetLength() {
      uint result = 4;
      /*if (!string.IsNullOrEmpty(Comment.Value))*/  result += (uint)8 + (uint)Comment.Value;
      /*if (!string.IsNullOrEmpty(Software.Value))*/ result += (uint)8 + (uint)Software.Value;
      return result;
    }
    
    public void Write(BinaryWriter writer)
    {
      Length = GetLength(); // IFFCHUNK length before writing
      ListInfo.Write(writer); // 12 - bytes written
      Software.Write(writer); // + 04 (bytes) + software (string) 
      Comment .Write(writer); // + 04 (bytes) + comment  (string)
    }
  }
  
}
