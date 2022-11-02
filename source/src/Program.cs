#define USE_WIN

using System;
using System.Windows.Forms;
using on.snd_module;


namespace sf2wav
{
  /// <summary>
  /// Class with program entry point.
  /// </summary>
  internal sealed class Program
  {
    /// <summary>
    /// Program entry point.
    /// </summary>
    [STAThread]
    private static void Main(string[] args)
    {
      #if USE_WIN
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
			#else
			if (args.Length == 0)
			{
				Console.WriteLine("No input specified\n.");
				Console.WriteLine("Press a Key to continue.");
				Console.ReadKey();
				return;
			}
			var file = new System.IO.FileInfo(args[0]);

			if (file.Exists)
			{
				var sf2_index = new SoundFont2_Index(file.FullName, true);
			}
			#endif
    }
    
  }
}
