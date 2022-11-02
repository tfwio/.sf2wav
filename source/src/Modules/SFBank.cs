#region User/License
// oio * 2005-11-12 * 04:19 PM
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
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using on.soundfont_2_41;
using PHDR = on.soundfont_2_41.SoundFont2.PHDR;
using PBAG = on.soundfont_2_41.SoundFont2.PBAG;
using PMOD = on.soundfont_2_41.SoundFont2.PMOD;
using PGEN = on.soundfont_2_41.SoundFont2.PGEN;
using INST = on.soundfont_2_41.SoundFont2.INST;
using IBAG = on.soundfont_2_41.SoundFont2.IBAG;
using IMOD = on.soundfont_2_41.SoundFont2.IMOD;
using IGEN = on.soundfont_2_41.SoundFont2.IGEN;
namespace on.snd_module
{

  public class Instrument
  {
    public SoundFont2.INST inst { get; set; }
    public Range R { get; set; }
  }
  public class SFBank
  {
    public int index;
    public short bank;
    public short preset;
    public string PresetName;
    
    #region ETC
    
    Range PHDR_PBAG_INDEX { get; set; }
    IEnumerable<int> BanksEnumerator { get { for (int i = PHDR_PBAG_INDEX.A; i < PHDR_PBAG_INDEX.B; i++) yield return i; } }

    public List<int> Banks {
      get { return banks; }
      set { banks = value; }
    } List<int> banks = new List<int>();
    
    /// <summary>BANK -> PRESET -> GENERATOR (or MODULATOR)</summary>
    public Dictionary<int,Range> PresetGen {
      get { return presets; }
      set { presets = value; }
    } Dictionary<int,Range> presets = new Dictionary<int,Range>();
    
    /// <summary>BANK -> PRESET -> GENERATOR (or MODULATOR)</summary>
    public Dictionary<int,Instrument> Inst {
      get { return generators; }
      set { generators = value; }
    } Dictionary<int,Instrument> generators = new Dictionary<int,Instrument>();
    
    /// <summary>BANK -> PRESET -> GENERATOR (or MODULATOR)</summary>
    public Dictionary<int,Range> Modulators {
      get { return modulators; }
      set { modulators = value; }
    } Dictionary<int,Range> modulators = new Dictionary<int,Range>();
    
    #endregion
    
    
    public SFBank(SoundFontModel Model, int index)
    {
      var hyde = Model.sf2.hyde;
      this.index  = index;
      this.bank   = hyde.phdr[index].bank;
      this.preset = hyde.phdr[index].preset;
      this.PresetName = IOHelper.GetZerodStr(hyde.phdr[index].presetName);
      
      PHDR_PBAG_INDEX = new Range{
        A=hyde.phdr[index].presetBagIndex,
        B=hyde.phdr[index+1].presetBagIndex
      };
      
      foreach ( int j in BanksEnumerator )
      {
        Banks.Add(j);
        PresetGen.Add(j, new Range{ A = hyde.pbag[j].gen, B = hyde.pbag[j+1].gen });
        for (int k = hyde.pbag[j].gen; k < hyde.pbag[j+1].gen; k++)
        {
          var gen = hyde.pgen[k];
          var genid = gen.Shift8;
          if (gen.Generator==SoundFont2.SFGenConst.instrument)
          {
            int i1 = hyde.inst[gen.Lo].bagIndex;
            int i2 = hyde.inst[gen.Hi].bagIndex;
            //- hyde.inst[s8].iName;
            Inst.Add( k, new Instrument(){
              inst = hyde.inst[gen.WORD],
              R =new Range{
                A =hyde.ibag[i1].gen,
                B =hyde.ibag[i2].gen }
            });
          }
        }
      }
    }
    
    public void ToListView(ListView list)
    {
      list.Items.Add(new ListViewItem( new string[]{ BankPresetString, this.PresetName }, "inst" ))
        .Tag = this.index;
    }
    
    public string BankPresetString
    {
      get { return string.Format( "{0:00#}:{1:00#}", bank, preset ); }
    }
    
    public int IntIndex
    {
      get { return bank << 8 | (int)preset; }
    }
    
    static public int SortBankPreset(SFBank a, SFBank b)
    {
      return a.IntIndex - b.IntIndex;
    }
  }
}
