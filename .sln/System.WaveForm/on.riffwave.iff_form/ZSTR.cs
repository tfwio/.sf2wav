/*
 * tfooo in #DEVLEOP
 * DateTime: 12/27/2006 : 9:04 AM
 */

using System;
using System.Runtime.InteropServices;

namespace on.riffwave.iff_form
{
	[StructLayout(LayoutKind.Sequential) ]
	public struct ZSTR
	{
		public int		Length;
		public char[]	StrValue;
	}
	
}
