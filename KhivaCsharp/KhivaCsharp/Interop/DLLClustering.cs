using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace khiva.interop
{
    public class DLLClustering
    {
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void k_means(ref IntPtr tss, ref int k, ref IntPtr centroids, ref IntPtr labels, ref float tolerance, ref int max_iterations);
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void k_shape(ref IntPtr tss, ref int k, ref IntPtr centroids, ref IntPtr labels, ref float tolerance, ref int max_iterations);
    }
}
