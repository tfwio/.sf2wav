/*
 * Created by SharpDevelop.
 * User: xo
 * Date: 7/19/2016
 * Time: 11:55 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace sf2wav
{
  partial class MainForm
  {
    /// <summary>
    /// Designer variable used to keep track of non-visual components.
    /// </summary>
    private System.ComponentModel.IContainer components = null;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ListView lvRight;
    private System.Windows.Forms.ListView lvLeft;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.Button btnBanks;
    private System.Windows.Forms.CheckBox checkBox1;
    private System.Windows.Forms.Button btnSamples;
    private System.Windows.Forms.ImageList imageList1;
    private System.Windows.Forms.SplitContainer splitContainer2;
    private System.Windows.Forms.ListView listView1;
    
    /// <summary>
    /// Disposes resources used by the form.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing) {
        if (components != null) {
          components.Dispose();
        }
      }
      base.Dispose(disposing);
    }
    
    /// <summary>
    /// This method is required for Windows Forms designer support.
    /// Do not change the method contents inside the source code editor. The Forms designer might
    /// not be able to load this method if it was changed manually.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
      this.label1 = new System.Windows.Forms.Label();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.splitContainer2 = new System.Windows.Forms.SplitContainer();
      this.lvLeft = new System.Windows.Forms.ListView();
      this.listView1 = new System.Windows.Forms.ListView();
      this.lvRight = new System.Windows.Forms.ListView();
      this.imageList1 = new System.Windows.Forms.ImageList(this.components);
      this.btnBanks = new System.Windows.Forms.Button();
      this.checkBox1 = new System.Windows.Forms.CheckBox();
      this.btnSamples = new System.Windows.Forms.Button();
      this.button1 = new System.Windows.Forms.Button();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
      this.splitContainer2.Panel1.SuspendLayout();
      this.splitContainer2.Panel2.SuspendLayout();
      this.splitContainer2.SuspendLayout();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.label1.Location = new System.Drawing.Point(10, 10);
      this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(605, 27);
      this.label1.TabIndex = 1;
      this.label1.Text = "[ Drag-Drop SoundFont 2 File Here ]";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // splitContainer1
      // 
      this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitContainer1.Location = new System.Drawing.Point(10, 39);
      this.splitContainer1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.splitContainer1.Name = "splitContainer1";
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.lvRight);
      this.splitContainer1.Size = new System.Drawing.Size(605, 305);
      this.splitContainer1.SplitterDistance = 229;
      this.splitContainer1.SplitterWidth = 3;
      this.splitContainer1.TabIndex = 2;
      // 
      // splitContainer2
      // 
      this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer2.Location = new System.Drawing.Point(0, 0);
      this.splitContainer2.Name = "splitContainer2";
      this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer2.Panel1
      // 
      this.splitContainer2.Panel1.Controls.Add(this.lvLeft);
      // 
      // splitContainer2.Panel2
      // 
      this.splitContainer2.Panel2.Controls.Add(this.listView1);
      this.splitContainer2.Size = new System.Drawing.Size(229, 305);
      this.splitContainer2.SplitterDistance = 152;
      this.splitContainer2.TabIndex = 0;
      // 
      // lvLeft
      // 
      this.lvLeft.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.lvLeft.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lvLeft.Location = new System.Drawing.Point(0, 0);
      this.lvLeft.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.lvLeft.Name = "lvLeft";
      this.lvLeft.Size = new System.Drawing.Size(229, 152);
      this.lvLeft.TabIndex = 0;
      this.lvLeft.UseCompatibleStateImageBehavior = false;
      // 
      // listView1
      // 
      this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.listView1.Location = new System.Drawing.Point(0, 0);
      this.listView1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.listView1.Name = "listView1";
      this.listView1.Size = new System.Drawing.Size(229, 149);
      this.listView1.TabIndex = 0;
      this.listView1.UseCompatibleStateImageBehavior = false;
      // 
      // lvRight
      // 
      this.lvRight.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.lvRight.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lvRight.Location = new System.Drawing.Point(0, 0);
      this.lvRight.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.lvRight.Name = "lvRight";
      this.lvRight.Size = new System.Drawing.Size(373, 305);
      this.lvRight.SmallImageList = this.imageList1;
      this.lvRight.TabIndex = 0;
      this.lvRight.UseCompatibleStateImageBehavior = false;
      // 
      // imageList1
      // 
      this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
      this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
      this.imageList1.Images.SetKeyName(0, "arrow_LeftRight.png");
      this.imageList1.Images.SetKeyName(1, "arrow_Right.png");
      this.imageList1.Images.SetKeyName(2, "arrow_UpDown.png");
      this.imageList1.Images.SetKeyName(3, "CentiBels.png");
      this.imageList1.Images.SetKeyName(4, "Cents.png");
      this.imageList1.Images.SetKeyName(5, "DeciBels.png");
      this.imageList1.Images.SetKeyName(6, "Effects.png");
      // 
      // btnBanks
      // 
      this.btnBanks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btnBanks.Location = new System.Drawing.Point(242, 348);
      this.btnBanks.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.btnBanks.Name = "btnBanks";
      this.btnBanks.Size = new System.Drawing.Size(98, 24);
      this.btnBanks.TabIndex = 3;
      this.btnBanks.Text = "BANKS";
      this.btnBanks.UseVisualStyleBackColor = true;
      this.btnBanks.Click += new System.EventHandler(this.LoadBanks);
      // 
      // checkBox1
      // 
      this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.checkBox1.Location = new System.Drawing.Point(10, 352);
      this.checkBox1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.checkBox1.Name = "checkBox1";
      this.checkBox1.Size = new System.Drawing.Size(114, 18);
      this.checkBox1.TabIndex = 4;
      this.checkBox1.Text = "Export *.sf2nfo";
      this.checkBox1.UseVisualStyleBackColor = true;
      // 
      // btnSamples
      // 
      this.btnSamples.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btnSamples.Location = new System.Drawing.Point(345, 348);
      this.btnSamples.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.btnSamples.Name = "btnSamples";
      this.btnSamples.Size = new System.Drawing.Size(98, 24);
      this.btnSamples.TabIndex = 3;
      this.btnSamples.Text = "SAMPLES";
      this.btnSamples.UseVisualStyleBackColor = true;
      this.btnSamples.Click += new System.EventHandler(this.LoadSamples);
      // 
      // button1
      // 
      this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.button1.Location = new System.Drawing.Point(460, 348);
      this.button1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(155, 24);
      this.button1.TabIndex = 3;
      this.button1.Text = "DUMP SAMPLES";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.DumpSelectedSamples);
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(624, 381);
      this.Controls.Add(this.checkBox1);
      this.Controls.Add(this.button1);
      this.Controls.Add(this.btnSamples);
      this.Controls.Add(this.btnBanks);
      this.Controls.Add(this.splitContainer1);
      this.Controls.Add(this.label1);
      this.Font = new System.Drawing.Font("Montserrat Light", 9F);
      this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
      this.MinimumSize = new System.Drawing.Size(640, 420);
      this.Name = "MainForm";
      this.Text = "sf2 info";
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
      this.splitContainer1.ResumeLayout(false);
      this.splitContainer2.Panel1.ResumeLayout(false);
      this.splitContainer2.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
      this.splitContainer2.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    private System.Windows.Forms.Button button1;
  }
}
