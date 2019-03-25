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
    public static class DLLLibrary
    {
#if (_WINDOWS)
        public const String khivaPath = "C:\\Program Files\\Khiva\\v0\\lib\\khiva_c.dll";
#else
            public const String khivaPath = "libkhiva_c";
#endif
        /**
         * @brief Gets information from the active backend.
         */
        [DllImport(khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void backend_info(ref StringBuilder backendInfo);

        /**
         * @brief Sets the backend.
         *
         * @param backend The desired backend.
         */
        [DllImport(khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void set_backend(ref int backend);

        /**
         * @brief Gets the active backend.
         *
         * @param backend The active backend.
         */
        [DllImport(khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void set_device(ref int device);

        /**
         * @brief Gets the available backends.
         *
         * @param backends The available backends.
         */
        [DllImport(khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void get_backends(ref int backend);

        /**
         * @brief Sets the device.
         *
         * @param device The desired device.
         */
        [DllImport(khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void get_device_id(ref int device_id);

        /**
         * @brief Gets the active device.
         *
         * @param device The active device.
         */
        [DllImport(khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void get_backend(ref int backend);

        /**
         * @brief Gets the devices count.
         *
         * @param device_count The devices count.
         */
        [DllImport(khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void get_device_count(ref int device_count);

        /**
         * @brief Returns a string with the current version of the library.
         *
         * @param v A previously malloced character array where to copy the version.
         */
        [DllImport(khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void version(ref StringBuilder version);
    }
}
