using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace khiva.interop
{
    public static class DLLLibrary
    {
#if (_WINDOWS)
        public const String khivaPath = "C:\\Program Files\\Khiva\\v0\\lib\\khiva_c.dll";
#else
            public const String khivaPath = "libkhiva_c";
#endif

        [DllImport(khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void backend_info(ref StringBuilder backendInfo);

        [DllImport(khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void set_backend(ref int backend);

        [DllImport(khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void set_device(ref int device);

        [DllImport(khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void get_backends(ref int backend);

        [DllImport(khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void get_device_id(ref int device_id);

        [DllImport(khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void get_backend(ref int backend);

        [DllImport(khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void get_device_count(ref int device_count);

        [DllImport(khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void version(ref StringBuilder version);
    }
}
