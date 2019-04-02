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

namespace khiva
{
    /// <summary>
    /// Class to change internal properties of the Khiva library.
    /// </summary>
    public static class Khiva
    {

        /// <summary>
        /// Khiva Backend.
        /// </summary>
        [Flags]
        public enum Backend
        {
            /// <summary>
            /// DEFAULT Backend
            /// </summary>
            KHIVA_BACKEND_DEFAULT = 0,
            /// <summary>
            /// CPU Backend.
            /// </summary>
            KHIVA_BACKEND_CPU = 1,
            /// <summary>
            /// CUDA Backend.
            /// </summary>
            KHIVA_BACKEND_CUDA = 2,
            /// <summary>
            /// OPENCL Backend.
            /// </summary>
            KHIVA_BACKEND_OPENCL = 4
        }

        /// <summary>
        /// Getters and setters for the Khiva backend
        /// </summary>
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

        /// <summary>
        /// Supported Khiva backends 
        /// </summary>
        public static Backend SupportedBackends
        {
            get
            {
                interop.DLLLibrary.get_backends(out int backends);
                return (Backend)backends;
            }
        }

        /// <summary>
        /// Prints information from the current backend.
        /// </summary>
        public static void PrintBackendInfo()
        {
            StringBuilder backendInfo = new StringBuilder(256);
            interop.DLLLibrary.backend_info(ref backendInfo);
            Console.WriteLine(backendInfo.ToString());
        }

        /// <summary>
        /// Getter for the information from the current backend.
        /// </summary>
        public static String BackendInfo
        {
            get
            {
                StringBuilder backendInfo = new StringBuilder(256);
                interop.DLLLibrary.backend_info(ref backendInfo);
                return backendInfo.ToString();
            }
        }


        
        /// <summary>
        /// Getter and setter for the Khiva device
        /// </summary>
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

        /// <summary>
        /// Getter for the device count
        /// </summary>
        public static int DeviceCount
        {
            get
            {
                interop.DLLLibrary.get_device_count(out int device_count);
                return device_count;
            }
        }

        /// <summary>
        /// Getter for the Khiva version
        /// </summary>
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

