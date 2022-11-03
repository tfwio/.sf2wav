/*
 * Created by SharpDevelop.
 * User: xo
 * Date: 7/19/2016
 * Time: 11:55 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using on.snd_module;
using on.soundfont_2_41;

namespace sf2wav
{
  /// <summary>
  /// Description of MainForm.
  /// </summary>
  public partial class MainForm : Form
  {
    static readonly string[]nn = MidiHelper.OctaveMacro();
    
    SoundFont2_Index Model { get; set; }
    
    FileInfo SoundFontFile { get; set; }
    
    bool IsSampleMode { get; set; }
    
    public MainForm()
    {
      InitializeComponent();
      IsSampleMode = false;
      lvRight.View = View.Details;
      lvRight.FullRowSelect = true;
      lvLeft.View = View.Details;
      label1.ApplyDragDropMethod( (sender,e)=>{ if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy; }, Event_Label_FileName);
      lvLeft.Columns.Add("Bank:Preset");
      lvLeft.Columns.Add("Name");
      lvLeft.FullRowSelect = true;
      WindowsInterop.WindowsTheme.HandleTheme(lvLeft);
      WindowsInterop.WindowsTheme.HandleTheme(lvRight);
      ColsPreset();
    }

    #region Column Header Initialization
    void ColsPreset()
    {
      lvRight.Columns.Clear();
      // Column-Headers for Presets
      lvRight.Columns.Add("Name");
      lvRight.Columns.Add("Value");
      lvRight.Columns.Add("ValueType");
    }
    
    void ColsSample()
    {
      lvRight.Columns.Clear();
      // Column-Headers for Presets
      lvRight.Columns.Add("Name");
      lvRight.Columns.Add("Rate");
      lvRight.Columns.Add("Size");
      lvRight.Columns.Add("Tune");
      lvRight.Columns.Add("Cent");
      lvRight.Columns.Add("Link-Mode");
    }


    void AutoresizeColumns()
    {
      lvLeft.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
      lvRight.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
    }
    
    #endregion

    void OnSampleHeader(SoundFont2.SHDR shdr)
    {
      lvRight.Items.Add(
        new ListViewItem(
          new string[]{
            IOHelper.GetZerodStr(shdr.iName),
            shdr.GetSampleRate(),
            shdr.GetSampleLength(),
            shdr.GetSampleLink(),
            nn[shdr.Pitch],
            shdr.PitchC.ToString("##,###,##0")
          })
       );
    }
    
    internal void Event_Label_FileName(object sender, DragEventArgs e)
    {
      var target = sender as Label;
      if (target == null) throw new Exception("Something went very wrong!");
      if (e.Data.GetDataPresent(DataFormats.FileDrop))
      {
        var strFiles = (string[])e.Data.GetData(DataFormats.FileDrop);
        SoundFontFile = new FileInfo(strFiles[0]);
        target.Text = Path.GetFileNameWithoutExtension(SoundFontFile.Name);
        strFiles = null;
        On_LoadSF2(null, null);
      }
    }
    
    void OnBank(SFBank bank)
    {
			var info = new string[] { bank.BankPresetString, string.Format("{0}", bank.PresetName) };
			lvLeft.Items.Add(new ListViewItem( info ));
      if (IsSampleMode) return;
      lvRight.Items.Add(new ListViewItem( info ){ ForeColor = System.Drawing.Color.Red });
    }
    
    void OnPreset(SoundFont2.PGEN pgen)
    {
      if (IsSampleMode) return;
      lvRight.Items.Add(new ListViewItem("--- Preset Layer ---"){ IndentCount=1 });
    }
    
    void OnPresetValue(string[] data)
    {
      if (IsSampleMode) return;
      var item = new ListViewItem(data){ IndentCount=2 };
      if (data[0] == "sampleID") item.BackColor = Color.Silver;
      if (data[0] == "keyRange") item.BackColor = Color.Pink;
      lvRight.Items.Add(item);
    }
    
    void OnSample(SoundFont2.SHDR data)
    {
      if (!IsSampleMode) return;
      var item = new string[]{
        data.Name,
        data.SampleRate.ToStringK(),
        data.ToString_SHDR_Length(),
        Model.AudioModule.nn[data.Pitch],
        data.PitchC.ToString(),
        data.GetSampleLink()
      };
      lvRight.Items.Add(new ListViewItem(item));
    }
    
    void On_LoadSF2(object sender, EventArgs e)
    {
      Model = new SoundFont2_Index(SoundFontFile.FullName,checkBox1.Checked);
      LoadInfo();
    }
    

    System.Threading.Thread loader;

    void LoadSamples(object sender, EventArgs e)
    {
      IsSampleMode = true;
      
      if (InvokeRequired) BeginInvoke((Action)LoadInfo);
      else LoadInfo();
    }
    
    /// <summary>
    /// LoadMethod proceeds to create a text representation
    /// of banks presets and samples.
    /// </summary>
    void LoadMethod()
    {
      if (!Model.SoundFontFile.Exists)
      {
        Text = $"sf2wav";
        return;
      }

      lvLeft.Visible = (lvRight.Visible = false);
      lvRight.Items.Clear();
      lvLeft.Items.Clear();

      if (IsSampleMode) ColsSample(); else ColsPreset();

      Model.DoIndex(OnBank, OnPreset, OnPresetValue, OnSample, (msg) => Text = $"Processed {msg}");
      lvLeft.Visible = (lvRight.Visible = true);
      AutoresizeColumns();
      Text = $"sf2wav — [{Model.SoundFontFile.Name}]";
    }

    #region Thread Dispatcher/Loader(s)

    void ThreadReset(Action mthd)
    {
      if (loader==null)
      {
        loader = new System.Threading.Thread(new System.Threading.ThreadStart(mthd));
      }
      else if (loader.IsAlive)
      {
        loader.Abort();
        loader.Join();
        loader = null;
      }
      loader = new System.Threading.Thread(new System.Threading.ThreadStart(mthd));
    }

    void LoadBanks(object sender, EventArgs e)
    {
      IsSampleMode = false;

      if (InvokeRequired) BeginInvoke((Action)LoadInfo);
      else LoadInfo();
    }
    void LoadInfo()
    {
      ThreadReset(LoadMethod);
      loader.Start();
    }
    #endregion

    void DumpSamplesAction()
    {
      // FIXME: add ability to change the surname or `[name1]-[sampleName].wav`
      var sf2 = Model.Model.sf2;
      var start = sf2.riff.sdta_offset;
      var sfname = Model.SoundFontFile.Name;
      var sname = Path.Combine(
        Model.SoundFontFile.Directory.FullName,
        Path.GetFileNameWithoutExtension(Model.SoundFontFile.FullName)
        );

      using (var fsr = new FileStream(Model.SoundFontFile.FullName, FileMode.Open, FileAccess.Read))
      using (var br = new BinaryReader(fsr))
        for (int f = 0; f < sf2.hyde.shdr.Count - 1; f++)
        {
          var shdr = sf2.hyde.shdr[f];
          using (var fs = new FileStream($"{sname}-{shdr.Name}.wav", FileMode.OpenOrCreate, FileAccess.Write))
          using (var b = new BinaryWriter(fs))
          {
            int len = (shdr.LenB - shdr.LenA);
            var wav = new System.WaveFormat.WaveWriter(sfname, shdr.LenB - shdr.LenA, shdr.SampleRate, 1, 16);
            wav.WriteHeader(b);
            byte[] bytes = new byte[2];
            fsr.Position = start + (shdr.LenA * 2);
            for (int j = 0; j < len; j++)
            {
              b.Write(br.ReadBytes(2));
            }
            wav.WriteTerminal(b);
          }
        }
    }
    void DumpSamples()
    {
      ThreadReset(DumpSamplesAction);
      loader.Start();
    }
    private void DumpSelectedSamples(object sender, EventArgs e)
    {
      if (InvokeRequired) BeginInvoke((Action)DumpSamples);
      else DumpSamples();
    }
  }
}
