// Copyright (c) 2019 Shapelets.io
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace khiva.interop
{
    /// <summary>
    /// Class to change internal properties of the Khiva library.
    /// </summary>
    public static class DLLLibrary
    {
        /// <summary>
        /// Khiva DLL path.
        /// </summary>
        public const String khivaPath = "khiva_c";
        
         /// <summary> Gets information from the active backend.</summary>
        [DllImport(khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void backend_info([In, Out] ref StringBuilder backendInfo);

        
         /// <summary> Sets the backend.
         ///</summary>
         /// <param name="backend">The desired backend.</param>
        [DllImport(khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void set_backend([In, Out] ref int backend);

        
         /// <summary> Gets the active backend.
         ///</summary>
         /// <param name="backend">The active backend.</param>
        [DllImport(khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void set_device([In, Out] ref int device);

        
         /// <summary> Gets the available backends.
         ///</summary>
         /// <param name="backends">The available backends.</param>
        [DllImport(khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void get_backends([Out] out int backend);

        
         /// <summary> Sets the device.
         ///</summary>
         /// <param name="device">The desired device.</param>
        [DllImport(khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void get_device_id([Out] out int device_id);

        
         /// <summary> Gets the active device.
         ///</summary>
         /// <param name="device">The active device.</param>
        [DllImport(khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void get_backend([Out] out int backend);

        
         /// <summary> Gets the devices count.
         ///</summary>
         /// <param name="device_count">The devices count.</param>
        [DllImport(khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void get_device_count([Out] out int device_count);

        
         /// <summary> Returns a string with the current version of the library.
         ///</summary>
         /// <param name="v">A previously malloced character array where to copy the version.</param>
        [DllImport(khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void version([In, Out] ref StringBuilder version);
    }
}
