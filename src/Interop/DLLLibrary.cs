// Copyright (c) 2019 Shapelets.io
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System.Runtime.InteropServices;
using System.Text;

namespace Khiva.Interop
{
    /// <summary>
    /// Class to change internal properties of the Khiva library.
    /// </summary>
    public static class DLLLibrary
    {
        /// <summary>
        /// Khiva DLL path.
        /// </summary>
        public const string KhivaPath = "khiva_c";

        /// <summary> Gets information from the active backend.</summary>
        [DllImport(KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void backend_info([In, Out] ref StringBuilder backendInfo);

        /// <summary> Sets the backend.
        ///</summary>
        /// <param name="backend">The desired backend.</param>
        [DllImport(KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void set_backend([In, Out] ref int backend);

        /// <summary> Gets the active backend.
        ///</summary>
        /// <param name="backend">The active backend.</param>
        [DllImport(KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void get_backend([Out] out int backend);

        /// <summary> Gets the available backends.
        ///</summary>
        /// <param name="backends">The available backends.</param>
        [DllImport(KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void get_backends([Out] out int backends);

        /// <summary> Sets the device.
        ///</summary>
        /// <param name="device">The desired device.</param>
        [DllImport(KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void set_device([In, Out] ref int device);

        /// <summary> Gets the active device.
        ///</summary>
        /// <param name="deviceId">The active device.</param>
        [DllImport(KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void get_device_id([Out] out int deviceId);

        /// <summary> Gets the devices count.
        ///</summary>
        /// <param name="deviceCount">The devices count.</param>
        [DllImport(KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void get_device_count([Out] out int deviceCount);

        /// <summary> Returns a string with the current version of the library.
        ///</summary>
        /// <param name="version">A previously malloced character array where to copy the version.</param>
        [DllImport(KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void version([In, Out] ref StringBuilder version);
    }
}