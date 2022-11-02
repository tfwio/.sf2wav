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
using sf_gen = on.soundfont_2_41.SoundFont2.PGEN;
namespace on.snd_module
{
  class SoundFont2_Index
  {
    public SoundFontModel Model { get; set; }
    public SoundFont2 AudioModule { get { return Model.sf2; } }
    public FileInfo SoundFontFile { get; set; }
    public bool InfoToFile { get; set; }
    
    /// <summary>
    /// 1: Sample Chunk<br/>
    /// 2: Bank Preset<br/>
    /// 3: Preset Instrument<br/>
    /// </summary>
    internal readonly string[][] ColumnHeaders = new string[3][] {
      new string[] { "ckID", "ckSize", "pos" },
      new string[] { "bnk:lib", "Name" },
      new string[] { "Name", "Rate", "Length", "Type", "Key", "Fine" }
    };
    
    const string infotxt = ".sf2nfo";

    public SoundFont2_Index(string filePath, bool andIndexToFile)
    {
      Initialize(filePath);
      InfoToFile = andIndexToFile;
      // if (andIndexToFile) DoIndex();
    }
    
    void Initialize(string filePath)
    {
      SoundFontFile = new FileInfo(filePath);
      Model = new SoundFontModel(filePath);
      
      // Load the SoundFont banks' headers and sort them
      for (int i = 0; i < Model.sf2.hyde.phdr.Count - 1; i++) Model.Bank.Add(new SFBank(Model, i));
      
      Model.Bank.Sort(SFBank.SortBankPreset);
    }
    
    public void DoIndex()
    {
      DoIndex(null,null,null,null,null);
    }

    /// <summary>
    /// This appears to be a primary assertion point where we load, parse and output data to a file.
    /// 
    /// </summary>
    /// <param name="onBank"></param>
    /// <param name="onPreset"></param>
    /// <param name="onPresetValue"></param>
    /// <param name="onSample"></param>
    /// <param name="statusMethod"></param>
    public void DoIndex (
      Action<SFBank> onBank,
      Action<sf_gen> onPreset,
      Action<string[]> onPresetValue,
      Action<SoundFont2.SHDR> onSample,
      Action<string> statusMethod)
    {
      var fn = string.Format("{0}{1}", Path.GetFileNameWithoutExtension(SoundFontFile.Name), infotxt);
      var infoFileName = Path.Combine(SoundFontFile.Directory.FullName,fn);
      
      if (InfoToFile && File.Exists(infoFileName)) File.Delete(infoFileName);
      
      var fs = InfoToFile ? new FileStream(infoFileName, FileMode.CreateNew) : null;
      var writer = InfoToFile ? new StreamWriter(fs) : null;
      
      Load_SF2_META(InfoToFile, writer);
      
      if (InfoToFile) writer.WriteLine();
      Console.WriteLine("Indexing Presets\n----------------\n");
      
      if (InfoToFile && Model.Bank.Count > 0)
      {
        writer.WriteLine("- Banks");
        foreach (var b in Model.Bank) writer.WriteLine("  - {0}: \"{1}\"", b.BankPresetString, b.PresetName);
        writer.WriteLine();
      }
      
      
      Load_SF2_Samples(InfoToFile, writer, onSample, statusMethod); // load/print samples (first)
      Load_SF2_Banks(InfoToFile, writer, onBank, onPreset, onPresetValue, statusMethod); // so then what does this do?
      
      
      if (writer != null)
      {
        writer.Dispose();
        writer = null;
        fs.Dispose();
        fs = null;
      }
    }
    void DumpSample(SoundFont2.SHDR input)
    {
      //Model.sf2.sdta;
    }
    void Load_SF2_Samples(bool infoToFile, TextWriter text_writer, Action<SoundFont2.SHDR> onSample, Action<string> status)
    {
      Console.WriteLine("Indexing Samples\n----------------\n");

      for (int f = 0; f < Model.sf2.hyde.shdr.Count - 1; f++)
        Model.Sample.Add(Model.sf2.ToItem(f));
      // Index our samples
      // =================
      if (infoToFile) text_writer.WriteLine("- Samples");
      
      for (int f = 0; f < Model.sf2.hyde.shdr.Count - 1; f++)
      {
        var shdr = AudioModule.hyde.shdr[f];
        onSample?.Invoke(shdr);
        if (infoToFile) text_writer.Write(
          $"  - Sample:  \"{shdr.Name}\"\n"+
          $"    - Rate:   {shdr.GetSampleRate()}\n" +
          $"    - Length: {shdr.GetSampleLength()}\n" +
          $"    - Type:   {shdr.GetSampleLink()}\n" +
          $"    - Pitch:  {Model.sf2.nn[shdr.Pitch]}\n" +
          $"    - Retune: {shdr.PitchC: ##,###,###,##0} (cents)\n" +
          $"    - StartA: {shdr.LenA:   ##,###,###,##0}\n" +
          $"    - StartB: {shdr.LenB:   ##,###,###,##0}\n" +
          $"    - LoopA:  {shdr.LoopA:  ##,###,###,##0}\n" +
          $"    - LoopB:  {shdr.LoopB:  ##,###,###,##0}\n\n"
         );
        status?.Invoke($"Sample: {f + 1}/{Model.sf2.hyde.shdr.Count - 1}");
      }
    }

    void ToTheBank(
      int gen_index,
      bool write_text_to_file,
      TextWriter text_writer,
      SFBank sfbank,
      Action<sf_gen> onPreset,
      Action<string[]> onPresetValue)
    {

      for (int k = sfbank.PresetGen[gen_index].A;
               k < sfbank.PresetGen[gen_index].B;
               k++)
      {
        var sfGen = Model.sf2.hyde.pgen[k];

        if (write_text_to_file) text_writer.WriteLine("    # ----");

        // callback if exist
        onPreset?.Invoke(sfGen);

        if (sfGen.Generator == SoundFont2.SFGenConst.instrument)
        {
          for (int l = sfbank.Inst[k].R.A; l < sfbank.Inst[k].R.B; l++)
          {
            if (write_text_to_file) text_writer.WriteLine($"    - {GetInstrumentString(l)}");
            if (onPresetValue != null)
            {
              var data = GetInstrumentArray(l);
              onPresetValue(data);
            }
          }
        }
        else
        {
          var data = $" - { GetGeneratorStringPGenValue(sfGen.Generator, k, sfGen)}";
          if (write_text_to_file) text_writer.WriteLine(data);
          onPresetValue?.Invoke(new string[] {
              sfGen.Generator.ToString(),
              data,
              SoundFont2.GetGenValueType(sfGen.Generator).ToString()
            });
        }
      }
    }

    void Load_SF2_Banks(
      bool infoToFile,
      TextWriter writer,
      Action<SFBank> onBank,
      Action<sf_gen> onPreset,
      Action<string[]> onPresetValue,
      Action<string> status = null)
    {
      if (Model.Bank.Count > 0)
      {
        if (infoToFile) writer.WriteLine("- BANKS");
        int sfbank_id = 0;
        foreach (var sfbank in Model.Bank)
        {
          if (infoToFile) writer.WriteLine("  - {0}: \"{1}\"", sfbank.BankPresetString, sfbank.PresetName);
          
          onBank?.Invoke(sfbank);
          
          foreach ( int sfgen_index in sfbank.Banks )
          {
            ToTheBank(sfgen_index, infoToFile, writer, sfbank, onPreset, onPresetValue);
          }
          status?.Invoke($"Bank: {++sfbank_id}/{Model.Bank.Count}");
        }
        if (infoToFile) writer.WriteLine();
      }
    }
    void Load_SF2_META(bool infoToFile, TextWriter writer)
    {
      if (infoToFile)
      {
        writer.WriteLine ("- meta");
        writer.WriteLine ("  - Name:     \"{0}\"", Model.Name);
        writer.WriteLine ("  - Engine:   \"{0}\"", IOHelper.GetZerodStr(Model.sf2.nfo.isng.StrValue));
      }
      using (var fs_sf2 = new FileStream(SoundFontFile.FullName,FileMode.Open))
        using (var bread = new BinaryReader(fs_sf2))
          foreach (var mox in Model.sf2.nfo.nfosub)
      {
        if (mox.Value.Type.ToString4() == "ifil") continue; // this is file-version tag
        if (mox.Value.Type.ToString4() == "isng") continue;
        if (mox.Value.Type.ToString4() == "INAM") continue;
        
        fs_sf2.Seek(mox.Key+8, SeekOrigin.Begin);
        var StrValue = bread.ReadChars(mox.Value.Length);
        var str = IOHelper.GetZerodStr(StrValue);
        if (string.IsNullOrEmpty(str)) continue; // skip blank entries
        
        if (infoToFile) writer.WriteLine ( "  - {0}:     \"{1}\"", mox.Value.Type, IOHelper.GetZerodStr(StrValue) );
      }
    }
    
    string GetInstrumentString(int indexIGEN)
    {
      var gent = AudioModule.hyde.igen[indexIGEN].Generator;
      var valu = GetInstrumentGeneratorValue(indexIGEN);
      var valt = AudioModule.GetGenValueType(indexIGEN);
      
      return string.Format( "{0} : {1} # {2}", gent, valu, valt );
    }
    string[] GetInstrumentArray(int indexIGEN)
    {
      var gent = AudioModule.hyde.igen[indexIGEN].Generator;
      var valu = GetInstrumentGeneratorValue(indexIGEN);
      var valt = AudioModule.GetGenValueType(indexIGEN);
      
      return new string[]{gent.ToString(), valu, valt.ToString()};
    }
    string GetInstrumentGeneratorValue(int indexIGEN)
    {
      var igen = Model.sf2.hyde.igen[indexIGEN];
      var tgen = (SFGenConst)igen.gen;
      return GetGeneratorStringIGenValue(tgen,indexIGEN,igen);
    }
    string GetGeneratorStringIGenValue(SFGenConst value, int index, SoundFont2.IGEN gen)
    {
      switch (value)
      {
        case SFGenConst.sampleID:
          return string.Format("“{0}”", IOHelper.GetZerodStr(AudioModule.hyde.shdr[gen.Hi].iName));
        case SFGenConst.sampleModes:
          return string.Format("{0:00#} - {1:00#}", gen.Hi, gen.Lo);
        //return string.Format("{0}", sf2str.IGEN_Range2String(AudioModule,index));
        case SFGenConst.keyRange:
          return string.Format("{0} # {1}", string.Format("{0} to {1}", gen.Hi, gen.Lo), string.Format("{0} to {1}", AudioModule.nn[gen.Hi], AudioModule.nn[gen.Lo]));
        case SFGenConst.overridingRootKey:
          return string.Format("{0}:{1}", value, gen.Hi);
        case SFGenConst.exclusiveClass:
        case SFGenConst.delayModEnv:
        case SFGenConst.attackModEnv:
        case SFGenConst.decayModEnv:
        case SFGenConst.holdModEnv:
        case SFGenConst.sustainModEnv:
        case SFGenConst.releaseModEnv:
        case SFGenConst.releaseVolEnv:
        case SFGenConst.pan:
          return $"{gen.SHORT:X4}";
        case SFGenConst.delayVibLFO:
        case SFGenConst.delayModLFO:			// timecents
        case SFGenConst.freqModLFO:		  // freq:cents
        case SFGenConst.freqVibLFO:		  // freq:cents
        case SFGenConst.modLfoToFilterFc:// degree:cents
        case SFGenConst.modLfoToPitch:		// degree:cents
        case SFGenConst.modLfoToVolume:	  // degree:cents
          return $"amt: {gen.WORD}u, {gen.SHORT}i";
        default:
          return $"amt: 0x{gen.SHORT:X4}, {gen.WORD:X4}u, {gen.SHORT}i";
      }
    }
    string GetGeneratorStringPGenValue(SFGenConst value, int index, SoundFont2.PGEN gen)
    {
      switch (value)
      {
        case SFGenConst.sampleID:
          return $"“{IOHelper.GetZerodStr(AudioModule.hyde.shdr[gen.Hi].iName)}”";
        case SFGenConst.sampleModes:
          return $"{gen.Hi:00#} - {gen.Lo:00#}";
        //return string.Format("{0}", sf2str.IGEN_Range2String(AudioModule,index));
        case SFGenConst.keyRange:
          return $"{ AudioModule.nn[gen.Hi]}:{AudioModule.nn[gen.Lo]} ({gen.Hi:000}:{gen.Lo}:000)";
        case SFGenConst.overridingRootKey:
          return string.Format("{0}:{1}", value, gen.Hi);
        case SFGenConst.exclusiveClass:
        case SFGenConst.delayModEnv:
        case SFGenConst.attackModEnv:
        case SFGenConst.decayModEnv:
        case SFGenConst.holdModEnv:
        case SFGenConst.sustainModEnv:
        case SFGenConst.releaseModEnv:
        case SFGenConst.releaseVolEnv:
        case SFGenConst.pan:
          return $"{gen.SHORT:X4}";
        case SFGenConst.delayVibLFO:
        case SFGenConst.delayModLFO:			// timecents
        case SFGenConst.freqModLFO:		  // freq:cents
        case SFGenConst.freqVibLFO:		  // freq:cents
        case SFGenConst.modLfoToFilterFc:// degree:cents
        case SFGenConst.modLfoToPitch:		// degree:cents
        case SFGenConst.modLfoToVolume:	// degree:cents
          return $"amt: {gen.WORD}u, {gen.SHORT}i";
        default:
          return $"amt: 0x{gen.SHORT:X4}, {gen.WORD:X4}u, {gen.SHORT}i";
      }
    }
  }
  
}
