// Copyright (c) 2019 Shapelets.io
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System;
using System.Text;
using Khiva.Interop;

namespace Khiva
{
    /// <summary>
    /// Class to change internal properties of the Khiva library.
    /// </summary>
    public static class Library
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
            // ReSharper disable once UnusedMember.Global
            KhivaBackendDefault = 0,

            /// <summary>
            /// CPU Backend.
            /// </summary>
            KhivaBackendCpu = 1,

            /// <summary>
            /// CUDA Backend.
            /// </summary>
            KhivaBackendCuda = 2,

            /// <summary>
            /// OPENCL Backend.
            /// </summary>
            KhivaBackendOpencl = 4
        }

        /// <summary>
        /// Getters and setters for the Khiva backend
        /// </summary>
        public static Backend CurrentBackend
        {
            get
            {
                DLLLibrary.get_backend(out var backend);
                return (Backend) backend;
            }
            set
            {
                var backend = (int) value;
                DLLLibrary.set_backend(ref backend);
            }
        }

        /// <summary>
        /// Supported Khiva backends 
        /// </summary>
        public static Backend SupportedBackends
        {
            get
            {
                DLLLibrary.get_backends(out var backends);
                return (Backend) backends;
            }
        }

        /// <summary>
        /// Prints information of the current backend.
        /// </summary>
        public static void PrintBackendInfo()
        {
            var backendInfo = new StringBuilder(256);
            DLLLibrary.backend_info(ref backendInfo);
            Console.WriteLine(backendInfo.ToString());
        }

        /// <summary>
        /// Information getter for the current backend.
        /// </summary>
        public static string BackendInfo
        {
            get
            {
                var backendInfo = new StringBuilder(256);
                DLLLibrary.backend_info(ref backendInfo);
                return backendInfo.ToString();
            }
        }

        /// <summary>
        /// Getter and setter for the Khiva device
        /// </summary>
        public static int Device
        {
            set => DLLLibrary.set_device(ref value);
            get
            {
                DLLLibrary.get_device_id(out var deviceId);
                return deviceId;
            }
        }

        /// <summary>
        /// Getter for the device count
        /// </summary>
        public static int DeviceCount
        {
            get
            {
                DLLLibrary.get_device_count(out var deviceCount);
                return deviceCount;
            }
        }

        /// <summary>
        /// Getter for the Khiva version
        /// </summary>
        public static string Version
        {
            get
            {
                var versionName = new StringBuilder(256);
                DLLLibrary.version(ref versionName);
                return versionName.ToString();
            }
        }
    }
}