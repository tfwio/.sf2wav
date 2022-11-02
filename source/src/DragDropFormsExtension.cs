// tfwio ;)
using System;
namespace System.Windows.Forms
{
	/// <summary>
	/// Apply DragDrop actions indirectly using delegtes or EventHandlers,
	/// or directly with Action definitions (see Example).
	/// </summary>
	/// <seealso cref="ApplyDefaultDragDrop(TextBox)"/>
	/// <example>
	/// <code><pre>
	/// this.ApplyDragDropMethod(
	///   (sender,e)=>{
	///     if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
	///   },
	///   (sender,e)=>{
	///     if (e.Data.GetDataPresent(DataFormats.FileDrop))
	///     {
	///       var strFiles = (string[])e.Data.GetData(DataFormats.FileDrop);
	///       var file = new FileInfo(strFiles[0]);
	///       strFiles = null;
	///       Gogo(file);
	///     }
	///   });</pre></code>
	/// </example>
	static class DragDropFormsExtension
	{
		/// <seealso cref="ApplyDefaultDragDrop(TextBox)"/>
		/// <example>
		/// <code><pre>
		/// this.ApplyDragDropMethod(
		///   (sender,e)=>{
		///     if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
		///   },
		///   (sender,e)=>{
		///     if (e.Data.GetDataPresent(DataFormats.FileDrop))
		///     {
		///       var strFiles = (string[])e.Data.GetData(DataFormats.FileDrop);
		///       var file = new FileInfo(strFiles[0]);
		///       strFiles = null;
		///       Gogo(file);
		///     }
		///   });</pre></code>
		/// </example>
		static public void ApplyDragDropMethod(this Control tInput, DragEventHandler TInputDragEnter, DragEventHandler TInputDragDrop)
		{
			// dragdrop
			tInput.AllowDrop = true;
			tInput.DragEnter += TInputDragEnter;
			tInput.DragDrop += TInputDragDrop;
		}

		/// <seealso cref="ApplyDragDropMethod(Control,DragEventHandler,DragEventHandler)"/>
		static public void ApplyDefaultDragDrop(this TextBox tInput)
		{
			tInput.ApplyDragDropMethod(Event_TInputDragEnter, Event_TInputDragDrop);
		}

		internal static void Event_TInputDragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
				e.Effect = DragDropEffects.Copy;
		}

		internal static void Event_Label_FileName(object sender, DragEventArgs e)
		{
			var target = sender as Label;
			if (target == null)
				throw new Exception("Something went very wrong!");
			if (e.Data.GetDataPresent(DataFormats.FileDrop)) {
				
			  var strFiles = (string[])e.Data.GetData(DataFormats.FileDrop);
			  var fpath = strFiles[0];
			  var fname = System.IO.Path.GetFileName(fpath);
			  target.Text = fname;
				strFiles = null;
			}
		}
		internal static void Event_TInputDragDrop(object sender, DragEventArgs e)
		{
			var tInput = sender as TextBox;
			if (tInput == null)
				throw new Exception("Something went very wrong!");
			if (e.Data.GetDataPresent(DataFormats.FileDrop)) {
				string[] strFiles = (string[])e.Data.GetData(DataFormats.FileDrop);
				tInput.Text = strFiles[0];
				strFiles = null;
			}
		}
	}
}




