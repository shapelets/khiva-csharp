// Copyright (c) 2019 Shapelets.io
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

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
        public static class Khiva
        {

            /**
             * Khiva Backend.
             */
            [Flags]
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

            public static Backend ActualBackend
            {
                get {
                    interop.DLLLibrary.get_backend(out int backend);
                    return (Backend)backend;
                }
                set
                {
                    int backend = (int)value;
                    interop.DLLLibrary.set_backend(ref backend);
                }
            }

            public static Backend SupportedBackends
            {
                get
                {
                    interop.DLLLibrary.get_backends(out int backends);
                    return (Backend)backends;
                }
            }

            /**
                * Prints information from the current backend.
                */
            public static void PrintBackendInfo()
            {
                StringBuilder backendInfo = new StringBuilder(256);
                interop.DLLLibrary.backend_info(ref backendInfo);
                Console.WriteLine(backendInfo.ToString());
            }

            /**
                * Gets information from the current backend.
                *
                * @return String with information from the active backend.
                */
            public static String BackendInfo
            {
                get
                {
                    StringBuilder backendInfo = new StringBuilder(256);
                    interop.DLLLibrary.backend_info(ref backendInfo);
                    return backendInfo.ToString();
                }     
            }


            /**
                * Sets the Khiva device.
                *
                * @param device Device selected.
                */

            public static int Device
            {
                set
                {
                    interop.DLLLibrary.set_device(ref value);
                }
                get
                {
                    interop.DLLLibrary.get_device_id(out int device_id);
                    return device_id;
                }
            }   

            public static int DeviceCount
            {
                get
                {
                    interop.DLLLibrary.get_device_count(out int device_count);
                    return device_count;
                }
            }

            /**
            * Gets the vesion of the library.
            *
            * @return A string with the khiva version.
            */
            public static String Version
            {
                get{
                    StringBuilder versionName = new StringBuilder(256);
                    interop.DLLLibrary.version(ref versionName);
                    return versionName.ToString();
                }   
            }
        }
}

