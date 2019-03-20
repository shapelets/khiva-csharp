using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

/**
 * Class to change internal properties of the Khiva library.
 */
namespace khiva
{
    namespace library
    {
        public class Library
        {

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

            private static int ordinal;

            public static void SetBackend(int new_ordinal)
            {
                ordinal = new_ordinal;
            }

            /**
            * Gets the ordinal from the Khiva Backend.
            *
            * @return The ordinal of the Khiva Backend.
            */

            public static int GetKhivaOrdinal()
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

            /**
             * Prints information from the current backend.
             */
            public static void PrintBackendInfo()
            {
                StringBuilder str = new StringBuilder(268);
                Interop.DLLLibrary.backend_info(ref str);
                Console.WriteLine(str.ToString());
            }

            /**
             * Gets information from the current backend.
             *
             * @return String with information from the active backend.
             */
            public static String GetBackendInfo()
            {

                StringBuilder str = new StringBuilder(268);
                Interop.DLLLibrary.backend_info(ref str);
                return str.ToString();
            }

            /**
             * Sets the Khiva backend.
             *
             * @param khivaBE selected backend.
             */
            public static void SetKhivaBackend(Backend khivaBE)
            {
                int backend = (int)khivaBE;
                Interop.DLLLibrary.set_backend(ref backend);
            }



            /**
             * Sets the Khiva device.
             *
             * @param device Device selected.
             */

            public static void SetKhivaDevice(int device)
            {
                Interop.DLLLibrary.set_device(ref device);
            }

            /**
             * Gets the available backends.
             *
             * @return The available backends.
             */
            public static int GetKhivaBackends()
            {
                int backends = 0;
                Interop.DLLLibrary.get_backends(ref backends);
                return backends;
            }

            /**
             * Get the device id.
             *
             * @return The device id.
             */
            public static int GetKhivaDeviceID()
            {
                int device_id = 0;
                Interop.DLLLibrary.get_device_id(ref device_id);
                return device_id;
            }

            /**
             * Gets the active backend.
             *
             * @return The active backend.
             */
            public static Backend GetKhivaBackend()
            {
                int backend = 0;
                Interop.DLLLibrary.get_backend(ref backend);
                return GetBackendFromOrdinal(backend);
            }

            /**
             * Gets the devices count.
             *
             * @return The devices count.
             */
            public static int GetKhivaDeviceCount()
            {
                int device_count = 0;
                Interop.DLLLibrary.get_device_count(ref device_count);
                return device_count;
            }

            /**
            * Gets the vesion of the library.
            *
            * @return A string with the khiva version.
            */
            public static String GetKhivaVersion()
            {
                StringBuilder version_name = new StringBuilder(40);
                Interop.DLLLibrary.version(ref version_name);
                return version_name.ToString();
            }
        }
    }
}

