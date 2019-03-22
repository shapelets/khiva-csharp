using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace khiva.interop
{
    public static class DLLDimensionality
    {
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void paa(ref IntPtr a, ref int bins, ref IntPtr result);

        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void pip(ref IntPtr a, ref int number_ips, ref IntPtr result);

        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void pla_bottom_up(ref IntPtr ts, ref float max_error, ref IntPtr result);

        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void pla_sliding_window(ref IntPtr ts, ref float max_error, ref IntPtr result);

        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void ramer_douglas_peucker(ref IntPtr points, ref double epsilon, ref IntPtr res_points);

        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void sax(ref IntPtr a, ref int alphabet_size, ref IntPtr result);

        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void visvalingam(ref IntPtr points, ref int num_points, ref IntPtr res_points);
    }
}
