// Copyright (c) 2019 Shapelets.io
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace khiva.interop
{
    public static class DLLArray
    {
        /**
            * @brief Creates an Array object.
            *
            * @param data Data used in order to create the array.
            * @param ndims Number of dimensions of the data.
            * @param dims Cardinality of dimensions of the data.
            * @param result Array created.
            * @param type Data type.
            */
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] float[] arr, [In] uint ndims, [In] long[] dims,[Out] out IntPtr result, [In] int type);
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] float[,] arr, [In] uint ndims, [In] long[] dims,[Out] out IntPtr result, [In] int type);
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] float[,,] arr, [In] uint ndims, [In] long[] dims,[Out] out IntPtr result, [In] int type);
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] float[,,,] arr, [In] uint ndims, [In] long[] dims,[Out] out IntPtr result, [In] int type);

        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] double[] arr, [In] uint ndims, [In] long[] dims,[Out] out IntPtr result, [In] int type);
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] double[,] arr, [In] uint ndims, [In] long[] dims,[Out] out IntPtr result, [In] int type);
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] double[,,] arr, [In] uint ndims, [In] long[] dims,[Out] out IntPtr result, [In] int type);
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] double[,,,] arr, [In] uint ndims, [In] long[] dims,[Out] out IntPtr result, [In] int type);

        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] int[] arr, [In] uint ndims, [In] long[] dims,[Out] out IntPtr result, [In] int type);
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] int[,] arr, [In] uint ndims, [In] long[] dims,[Out] out IntPtr result, [In] int type);
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] int[,,] arr, [In] uint ndims, [In] long[] dims,[Out] out IntPtr result, [In] int type);
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] int[,,,] arr, [In] uint ndims, [In] long[] dims,[Out] out IntPtr result, [In] int type);

        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] uint[] arr, [In] uint ndims, [In] long[] dims,[Out] out IntPtr result, [In] int type);
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] uint[,] arr, [In] uint ndims, [In] long[] dims,[Out] out IntPtr result, [In] int type);
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] uint[,,] arr, [In] uint ndims, [In] long[] dims,[Out] out IntPtr result, [In] int type);
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] uint[,,,] arr, [In] uint ndims, [In] long[] dims,[Out] out IntPtr result, [In] int type);

        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] byte[] arr, [In] uint ndims, [In] long[] dims,[Out] out IntPtr result, [In] int type);
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] byte[,] arr, [In] uint ndims, [In] long[] dims,[Out] out IntPtr result, [In] int type);
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] byte[,,] arr, [In] uint ndims, [In] long[] dims,[Out] out IntPtr result, [In] int type);
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] byte[,,,] arr, [In] uint ndims, [In] long[] dims,[Out] out IntPtr result, [In] int type);

        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] long[] arr, [In] uint ndims, [In] long[] dims,[Out] out IntPtr result, [In] int type);
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] long[,] arr, [In] uint ndims, [In] long[] dims,[Out] out IntPtr result, [In] int type);
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] long[,,] arr, [In] uint ndims, [In] long[] dims,[Out] out IntPtr result, [In] int type);
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] long[,,,] arr, [In] uint ndims, [In] long[] dims,[Out] out IntPtr result, [In] int type);

        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] ulong[] arr, [In] uint ndims, [In] long[] dims,[Out] out IntPtr result, [In] int type);
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] ulong[,] arr, [In] uint ndims, [In] long[] dims,[Out] out IntPtr result, [In] int type);
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] ulong[,,] arr, [In] uint ndims, [In] long[] dims,[Out] out IntPtr result, [In] int type);
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] ulong[,,,] arr, [In] uint ndims, [In] long[] dims,[Out] out IntPtr result, [In] int type);

        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] short[] arr, [In] uint ndims, [In] long[] dims,[Out] out IntPtr result, [In] int type);
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] short[,] arr, [In] uint ndims, [In] long[] dims,[Out] out IntPtr result, [In] int type);
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] short[,,] arr, [In] uint ndims, [In] long[] dims,[Out] out IntPtr result, [In] int type);
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] short[,,,] arr, [In] uint ndims, [In] long[] dims,[Out] out IntPtr result, [In] int type);

        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] ushort[] arr, [In] uint ndims, [In] long[] dims,[Out] out IntPtr result, [In] int type);
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] ushort[,] arr, [In] uint ndims, [In] long[] dims,[Out] out IntPtr result, [In] int type);
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] ushort[,,] arr, [In] uint ndims, [In] long[] dims,[Out] out IntPtr result, [In] int type);
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] ushort[,,,] arr, [In] uint ndims, [In] long[] dims,[Out] out IntPtr result, [In] int type);

        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] ref IntPtr data, [In] ref uint ndims, [In] ref long[] dims, [In,Out] ref IntPtr result, [In] ref int type);

        /**
            * @brief Retrieves the data from the device to the host.
            *
            * @param array The Array that contains the data to be retrieved.
            * @param data Pointer to previously allocated memory in the host.
            */
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void get_data([In]ref IntPtr array,[Out] out IntPtr data);

        /**
            * @brief Gets the Array dimensions.
            *
            * @param array Array from which to get the dimensions.
            * @param dims The dimensions.
            */
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void get_dims([In] IntPtr array,[Out] long[] dims);


        /**
            * @brief Displays an Array.
            *
            * @param array The array to display.
            */
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void display([In] IntPtr array);

        /**
            * @brief Decreases the references count of the given array.
            *
            * @param array The Array to release.
            */
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void delete_array([In, Out] ref IntPtr array);

        /**
            * @brief Gets the type of the array.
            *
            * @param array The array to obtain the type information from.
            * @param type Value of the Dtype enumeration.
            */
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void get_type([In] IntPtr array, [Out] out int t);

        /**
            * @brief Adds two arrays.
            *
            * @param lhs Left-hand side KHIVA array for the operation.
            * @param rhs Right-hand side KHIVA array for the operation.
            * @param result KHIVA Array with the result of this operation.
            */
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void khiva_add([In] IntPtr lhs, [In] IntPtr rhs, [Out] out IntPtr result);



        /**
            * @brief Multiplies two arrays.
            *
            * @param lhs Left-hand side KHIVA array for the operation.
            * @param rhs Right-hand side KHIVA array for the operation.
            * @param result KHIVA Array with the result of this operation.
            */
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void khiva_mul([In] IntPtr lhs, [In] IntPtr rhs, [Out] out IntPtr result);

        /**
            * @brief Subtracts two arrays.
            *
            * @param lhs Left-hand side KHIVA array for the operation.
            * @param rhs Right-hand side KHIVA array for the operation.
            * @param result KHIVA Array with the result of this operation.
            */
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void khiva_sub([In] IntPtr lhs, [In] IntPtr rhs, [Out] out IntPtr result);

        /**
            * @brief Divides lhs by rhs (element-wise).
            *
            * @param lhs Left-hand side KHIVA array for the operation.
            * @param rhs Right-hand side KHIVA array for the operation.
            * @param result KHIVA Array with the result of this operation.
            */
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void khiva_div([In] IntPtr lhs, [In] IntPtr rhs, [Out] out IntPtr result);

        /**
            * @brief Performs the modulo operation of lhs by rhs.
            *
            * @param lhs Left-hand side KHIVA array for the operation.
            * @param rhs Right-hand side KHIVA array for the operation.
            * @param result KHIVA Array with the result of this operation.
            */
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void khiva_mod([In] IntPtr lhs, [In] IntPtr rhs, [Out] out IntPtr result);

        /**
            * @brief Powers lhs with rhs.
            *
            * @param lhs Left-hand side KHIVA array for the operation. Base.
            * @param rhs Right-hand side KHIVA array for the operation. Exponent.
            * @param result KHIVA Array with the result of this operation.
            */
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void khiva_pow([In] IntPtr lhs, [In] IntPtr rhs, [Out] out IntPtr result);

        /**
            * @brief Compares (element-wise) if lhs is lower than rhs.
            *
            * @param lhs Left-hand side KHIVA array for the operation.
            * @param rhs Right-hand side KHIVA array for the operation.
            * @param result KHIVA Array with the result of this operation.
            */
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void khiva_lt([In] IntPtr lhs, [In] IntPtr rhs, [Out] out IntPtr result);

        /**
            * @brief Compares (element-wise) if lhs is greater than rhs.
            *
            * @param lhs Left-hand side KHIVA array for the operation.
            * @param rhs Right-hand side KHIVA array for the operation.
            * @param result KHIVA Array with the result of this operation.
            */
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void khiva_gt([In] IntPtr lhs, [In] IntPtr rhs, [Out] out IntPtr result);

        /**
            * @brief Compares (element-wise) if lhs is lower or equal than rhs.
            *
            * @param lhs Left-hand side KHIVA array for the operation.
            * @param rhs Right-hand side KHIVA array for the operation.
            * @param result KHIVA Array with the result of this operation.
            */
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void khiva_le([In] IntPtr lhs, [In] IntPtr rhs, [Out] out IntPtr result);

        /**
            * @brief Compares (element-wise) if lhs is greater or equal than rhs.
            *
            * @param lhs Left-hand side KHIVA array for the operation.
            * @param rhs Right-hand side KHIVA array for the operation.
            * @param result KHIVA Array with the result of this operation.
            */
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void khiva_ge([In] IntPtr lhs, [In] IntPtr rhs, [Out] out IntPtr result);

        /**
            * @brief Compares (element-wise) if rhs is equal to rhs.
            *
            * @param lhs Left-hand side KHIVA array for the operation.
            * @param rhs Right-hand side KHIVA array for the operation.
            * @param result KHIVA Array with the result of this operation.
            */
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void khiva_eq([In] IntPtr lhs, [In] IntPtr rhs, [Out] out IntPtr result);

        /**
            * @brief Compares (element-wise) if lhs is not equal to rhs.
            *
            * @param lhs Left-hand side KHIVA array for the operation.
            * @param rhs Right-hand side KHIVA array for the operation.
            * @param result KHIVA Array with the result of this operation.
            */
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void khiva_ne([In] IntPtr lhs, [In] IntPtr rhs, [Out] out IntPtr result);

        /**
            * @brief Performs an AND operation (element-wise) with lhs and rhs.
            *
            * @param lhs Left-hand side KHIVA array for the operation.
            * @param rhs Right-hand side KHIVA array for the operation.
            * @param result KHIVA Array with the result of this operation.
            */
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void khiva_bitand([In] IntPtr lhs, [In] IntPtr rhs, [Out] out IntPtr result);

        /**
            * @brief Performs an OR operation (element-wise) with lhs and rhs.
            *
            * @param lhs Left-hand side KHIVA array for the operation.
            * @param rhs Right-hand side KHIVA array for the operation.
            * @param result KHIVA Array with the result of this operation.
            */
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void khiva_bitor([In] IntPtr lhs, [In] IntPtr rhs, [Out] out IntPtr result);

        /**
            * @brief Performs an eXclusive-OR operation (element-wise) with lhs and rhs.
            *
            * @param lhs Left-hand side KHIVA array for the operation.
            * @param rhs Right-hand side KHIVA array for the operation.
            * @param result KHIVA Array with the result of this operation.
            */
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void khiva_bitxor([In] IntPtr lhs, [In] IntPtr rhs, [Out] out IntPtr result);

        /**
            * @brief Performs a left bit shift operation (element-wise) to array as many times as specified in the parameter n.
            *
            * @param array KHIVA Array to shift.
            * @param n Number of bits to be shifted.
            * @param result KHIVA Array with the result of this operation.
            */
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void khiva_bitshiftl([In] IntPtr array, [In] int n, [Out] out IntPtr result);

        /**
            * @brief Performs a right bit shift operation (element-wise) to array as many times as specified in the parameter n.
            *
            * @param array KHIVA Array to shift.
            * @param n Number of bits to be shifted.
            * @param result KHIVA Array with the result of this operation.
            */
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void khiva_bitshiftr([In] IntPtr array, [In] int n, [Out] out IntPtr result);

        /**
            * @brief Logical NOT operation to array.
            *
            * @param array KHIVA Array to negate.
            * @param result KHIVA Array with the result of this operation.
            */
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void khiva_not([In] IntPtr array, [Out] out IntPtr result);

        /**
            * @brief Transposes array.
            *
            * @param array KHIVA Array to transpose.
            * @param conjugate If true a conjugate transposition is performed.
            * @param result KHIVA Array with the result of this operation.
            */
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void khiva_transpose([In] IntPtr array, [In] bool conjugate, [Out] out IntPtr result);

        /**
            * @brief Retrieves a given column of array.
            *
            * @param array KHIVA Array.
            * @param index The column to be retrieved.
            * @param result KHIVA Array with the result of this operation.
            */
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void khiva_col([In] IntPtr array, [In] int index, [Out] out IntPtr result);

        /**
            * @brief Retrieves a subset of columns of array, starting at first and finishing at last, both inclusive.
            *
            * @param array KHIVA Array.
            * @param first Start of the subset of columns to be retrieved.
            * @param last End of the subset of columns to be retrieved.
            * @param result KHIVA Array with the result of this operation.
            */
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void khiva_cols([In] IntPtr array, [In] int first, [In] int last, [Out] out IntPtr result);

        /**
            * @brief Retrieves a given row of array.
            *
            * @param array KHIVA Array.
            * @param index The row to be retrieved.
            * @param result KHIVA Array with the result of this operation.
            */
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void khiva_row([In] IntPtr array, [In] int index, [Out] out IntPtr result);

        /**
            * @brief Retrieves a subset of rows of array, starting at first and finishing at last, both inclusive.
            *
            * @param array KHIVA Array.
            * @param first Start of the subset of rows to be retrieved.
            * @param last End of the subset of rows to be retrieved.
            * @param result KHIVA Array with the result of this operation.
            */
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void khiva_rows([In] IntPtr array, [In] int first, [In] int last, [Out] out IntPtr result);

        /**
            * @brief Creates a KHIVA array from an ArrayFire array.
            *
            * @param arrayfire ArrayFire array reference.
            * @param result KHIVA Array.
            */
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void from_arrayfire([In] IntPtr arrayfire, [Out] out IntPtr result);

        /**
            * @brief Performs a matrix multiplication of lhs and rhs.
            *
            * @param lhs Left-hand side KHIVA array for the operation.
            * @param rhs Right-hand side KHIVA array for the operation.
            * @param result KHIVA Array with the result of this operation.
            */
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void khiva_matmul([In] IntPtr lhs, [In] IntPtr rhs, [Out] out IntPtr result);

        /**
            * @brief Performs a deep copy of array.
            *
            * @param array KHIVA Array.
            * @param result KHIVA Array which contains a copy of array.
            */
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void copy([In] IntPtr array, [Out] out IntPtr result);

        /**
            * @brief Changes the type of array.
            *
            * @param array KHIVA Array.
            * @param type Target type of the output array.
            * @param result KHIVA Array with the result of this operation.
            */
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void khiva_as([In] IntPtr array, [In] int type, [Out] out IntPtr result);

    }
}
