using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

/**
 * Class to change internal properties of the Khiva library.
 */
namespace KhivaCsharp
{
    namespace library
    {
        public class Library
        {
            #if (__unix__)
                                            const String khivaPath = "/usr/local/lib/libkhiva_c.so";
            #elif (__APPLE__)
                                             const String khivaPath = "/usr/local/lib/libkhiva_c.dylib";
            #else
                        const String khivaPath = "C:\\Program Files\\Khiva\\v0\\lib\\khiva_c.dll";
            #endif
            public Library()
            {
               
            }


            /**

             * Khiva Backend.

             */
            public enum Backend
            {
                /**
                 * DEFAULT Backend
                 */
                KHIVA_BACKEND_DEFAULT = 0,
                /**
                 * CPU Backend.
                 */
                KHIVA_BACKEND_CPU = 1,
                /**
                 * CUDA Backend.
                 */
                KHIVA_BACKEND_CUDA = 2,
                /**
                 * OPENCL Backend.
                 */
                KHIVA_BACKEND_OPENCL = 4
            }

            private int ordinal;

            public void SetBackend(int ordinal)
            {
                this.ordinal = ordinal;
            }

            /**
            * Gets the ordinal from the Khiva Backend.
            *
            * @return The ordinal of the Khiva Backend.
            */

            public int GetKhivaOrdinal()
            {
                return ordinal;
            }

            /**
             * Gets the backend from the ordinal.
             *
             * @param ordinal Integer representing the Backend ordinal.
             * @return The corresponding Khiva BACKEND.
             */
            public static Backend GetBackendFromOrdinal(int ordinal)
            {
                switch (ordinal)
                {
                    case 0:
                        return Backend.KHIVA_BACKEND_DEFAULT;
                    case 1:
                        return Backend.KHIVA_BACKEND_CPU;
                    case 2:
                        return Backend.KHIVA_BACKEND_CUDA;
                    case 4:
                        return Backend.KHIVA_BACKEND_OPENCL;
                    default:
                        return Backend.KHIVA_BACKEND_DEFAULT;
                }
            }

            [DllImport(khivaPath, CallingConvention = CallingConvention.Cdecl)]
            private extern static void backend_info(IntPtr backendInfo);

            [DllImport(khivaPath, CallingConvention = CallingConvention.Cdecl)]
            private extern static void set_backend(int backend);

            [DllImport(khivaPath, CallingConvention = CallingConvention.Cdecl)]
            private extern static void set_device(int device);

            [DllImport(khivaPath, CallingConvention = CallingConvention.Cdecl)]
            private extern static int get_backends();

            [DllImport(khivaPath, CallingConvention = CallingConvention.Cdecl)]
            private extern static int get_device_id();

            [DllImport(khivaPath, CallingConvention = CallingConvention.Cdecl)]
            private extern static int get_backend();

            [DllImport(khivaPath, CallingConvention = CallingConvention.Cdecl)]
            private extern static int get_device_count();

            [DllImport(khivaPath, CallingConvention = CallingConvention.Cdecl)]
            private extern static void version(out IntPtr version);

            public static String Version()
            {
                IntPtr version_name = new IntPtr(40);
                version(out version_name);
                return version_name.ToString();
            }

            public static int GetBackend()
            {
                return get_backend();
            }
        }
    }
}

