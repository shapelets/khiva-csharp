// Copyright (c) 2019 Shapelets.io
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System;
using System.Runtime.InteropServices;

namespace Khiva.Interop
{
    /// <summary>
    /// Khiva KhivaArray Class.
    /// </summary>
    public static class DLLKhivaArray
    {
        /// <summary>
        /// Creates an KhivaArray object.
        /// </summary>
        /// <param name="arr">Data used in order to create the array.</param>
        /// <param name="nDims">Number of dimensions of the data.</param>
        /// <param name="dims">Cardinality of dimensions of the data.</param>
        /// <param name="result">KhivaArray created.</param>
        /// <param name="type">Data type.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void create_array([In] void* arr, [In] ref uint nDims, [In] long[] dims,
            [Out] out IntPtr result, [In] ref int type);

        /// <summary>
        /// Retrieves the data from the device to the host.
        /// </summary>
        /// <param name="array">The KhivaArray that contains the data to be retrieved.</param>
        /// <param name="data">Pointer to previously allocated memory in the host.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void get_data([In] ref IntPtr array, [In, Out] IntPtr data);

        /// <summary>
        /// Gets the KhivaArray dimensions.
        /// </summary>
        /// <param name="array">KhivaArray from which to get the dimensions.</param>
        /// <param name="dims">The dimensions.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void get_dims([In] ref IntPtr array, [Out] long[] dims);

        /// <summary> Displays an KhivaArray.</summary>
        ///
        /// <param name="array">The array to display.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void display([In] ref IntPtr array);

        /// <summary> Decreases the references count of the given array.</summary>
        ///
        /// <param name="array">The KhivaArray to release.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void delete_array([In, Out] ref IntPtr array);

        /// <summary> Gets the type of the array.</summary>
        ///
        /// <param name="array">The array to obtain the type information from.</param>
        /// <param name="type">Value of the DType enumeration.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void get_type([In] ref IntPtr array, [Out] out int type);

        /// <summary> Adds two arrays.</summary>
        ///
        /// <param name="lhs">Left</param>-hand side KHIVA array for the operation.
        /// <param name="rhs">Right</param>-hand side KHIVA array for the operation.
        /// <param name="result">KHIVA KhivaArray with the result of this operation.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void khiva_add([In] ref IntPtr lhs, [In] ref IntPtr rhs, [Out] out IntPtr result);

        /// <summary> Multiplies two arrays. </summary>
        /// <param name="lhs">Left</param>-hand side KHIVA array for the operation.
        /// <param name="rhs">Right</param>-hand side KHIVA array for the operation.
        /// <param name="result">KHIVA KhivaArray with the result of this operation.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void khiva_mul([In] ref IntPtr lhs, [In] ref IntPtr rhs, [Out] out IntPtr result);

        /// <summary> 
        /// Subtracts two arrays.
        /// </summary>
        /// <param name="lhs">Left</param>-hand side KHIVA array for the operation.
        /// <param name="rhs">Right</param>-hand side KHIVA array for the operation.
        /// <param name="result">KHIVA KhivaArray with the result of this operation.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void khiva_sub([In] ref IntPtr lhs, [In] ref IntPtr rhs, [Out] out IntPtr result);

        /// <summary> Divides lhs by rhs (element-wise). </summary>
        /// <param name="lhs">Left</param>-hand side KHIVA array for the operation.
        /// <param name="rhs">Right</param>-hand side KHIVA array for the operation.
        /// <param name="result">KHIVA KhivaArray with the result of this operation.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void khiva_div([In] ref IntPtr lhs, [In] ref IntPtr rhs, [Out] out IntPtr result);

        /// <summary> Performs the modulo operation of lhs by rhs.</summary>
        /// <param name="lhs">Left</param>-hand side KHIVA array for the operation.
        /// <param name="rhs">Right</param>-hand side KHIVA array for the operation.
        /// <param name="result">KHIVA KhivaArray with the result of this operation.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void khiva_mod([In] ref IntPtr lhs, [In] ref IntPtr rhs, [Out] out IntPtr result);

        /// <summary> Powers lhs with rhs.</summary>
        /// <param name="lhs">Left</param>-hand side KHIVA array for the operation. Base.
        /// <param name="rhs">Right</param>-hand side KHIVA array for the operation. Exponent.
        /// <param name="result">KHIVA KhivaArray with the result of this operation.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void khiva_pow([In] ref IntPtr lhs, [In] ref IntPtr rhs, [Out] out IntPtr result);

        /// <summary> Compares (element-wise) if lhs is lower than rhs.</summary>
        /// <param name="lhs">Left</param>-hand side KHIVA array for the operation.
        /// <param name="rhs">Right</param>-hand side KHIVA array for the operation.
        /// <param name="result">KHIVA KhivaArray with the result of this operation.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void khiva_lt([In] ref IntPtr lhs, [In] ref IntPtr rhs, [Out] out IntPtr result);

        /// <summary> Compares (element-wise) if lhs is greater than rhs.</summary>
        /// <param name="lhs">Left</param>-hand side KHIVA array for the operation.
        /// <param name="rhs">Right</param>-hand side KHIVA array for the operation.
        /// <param name="result">KHIVA KhivaArray with the result of this operation.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void khiva_gt([In] ref IntPtr lhs, [In] ref IntPtr rhs, [Out] out IntPtr result);

        /// <summary> Compares (element-wise) if lhs is lower or equal than rhs.</summary>
        /// <param name="lhs">Left</param>-hand side KHIVA array for the operation.
        /// <param name="rhs">Right</param>-hand side KHIVA array for the operation.
        /// <param name="result">KHIVA KhivaArray with the result of this operation.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void khiva_le([In] ref IntPtr lhs, [In] ref IntPtr rhs, [Out] out IntPtr result);

        /// <summary> Compares (element-wise) if lhs is greater or equal than rhs.</summary>
        /// <param name="lhs">Left</param>-hand side KHIVA array for the operation.
        /// <param name="rhs">Right</param>-hand side KHIVA array for the operation.
        /// <param name="result">KHIVA KhivaArray with the result of this operation.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void khiva_ge([In] ref IntPtr lhs, [In] ref IntPtr rhs, [Out] out IntPtr result);

        /// <summary> Compares (element-wise) if rhs is equal to rhs.</summary>
        /// <param name="lhs">Left</param>-hand side KHIVA array for the operation.
        /// <param name="rhs">Right</param>-hand side KHIVA array for the operation.
        /// <param name="result">KHIVA KhivaArray with the result of this operation.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void khiva_eq([In] ref IntPtr lhs, [In] ref IntPtr rhs, [Out] out IntPtr result);

        /// <summary> Compares (element-wise) if lhs is not equal to rhs.</summary>
        /// <param name="lhs">Left</param>-hand side KHIVA array for the operation.
        /// <param name="rhs">Right</param>-hand side KHIVA array for the operation.
        /// <param name="result">KHIVA KhivaArray with the result of this operation.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void khiva_ne([In] ref IntPtr lhs, [In] ref IntPtr rhs, [Out] out IntPtr result);

        /// <summary> Performs an AND operation (element-wise) with lhs and rhs.</summary>
        /// <param name="lhs">Left</param>-hand side KHIVA array for the operation.
        /// <param name="rhs">Right</param>-hand side KHIVA array for the operation.
        /// <param name="result">KHIVA KhivaArray with the result of this operation.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void khiva_bitand([In] ref IntPtr lhs, [In] ref IntPtr rhs, [Out] out IntPtr result);

        /// <summary> Performs an OR operation (element-wise) with lhs and rhs.</summary>
        /// <param name="lhs">Left</param>-hand side KHIVA array for the operation.
        /// <param name="rhs">Right</param>-hand side KHIVA array for the operation.
        /// <param name="result">KHIVA KhivaArray with the result of this operation.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void khiva_bitor([In] ref IntPtr lhs, [In] ref IntPtr rhs, [Out] out IntPtr result);

        /// <summary> Performs an eXclusive-OR operation (element-wise) with lhs and rhs.</summary>
        /// <param name="lhs">Left</param>-hand side KHIVA array for the operation.
        /// <param name="rhs">Right</param>-hand side KHIVA array for the operation.
        /// <param name="result">KHIVA KhivaArray with the result of this operation.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void khiva_bitxor([In] ref IntPtr lhs, [In] ref IntPtr rhs, [Out] out IntPtr result);

        /// <summary> Performs a left bit shift operation (element-wise) to array as many times as specified in the parameter n.</summary>
        /// <param name="array">KHIVA KhivaArray to shift.</param>
        /// <param name="n">Number of bits to be shifted.</param>
        /// <param name="result">KHIVA KhivaArray with the result of this operation.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void khiva_bitshiftl([In] ref IntPtr array, [In] ref int n, [Out] out IntPtr result);

        /// <summary> Performs a right bit shift operation (element-wise) to array as many times as specified in the parameter n.</summary>
        /// <param name="array">KHIVA KhivaArray to shift.</param>
        /// <param name="n">Number of bits to be shifted.</param>
        /// <param name="result">KHIVA KhivaArray with the result of this operation.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void khiva_bitshiftr([In] ref IntPtr array, [In] ref int n, [Out] out IntPtr result);

        /// <summary> Logical NOT operation to array.</summary>
        /// <param name="array">KHIVA KhivaArray to negate.</param>
        /// <param name="result">KHIVA KhivaArray with the result of this operation.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void khiva_not([In] ref IntPtr array, [Out] out IntPtr result);

        /// <summary> Transposes array.</summary>
        /// <param name="array">KHIVA KhivaArray to transpose.</param>
        /// <param name="conjugate">If true a conjugate transposition is performed.</param>
        /// <param name="result">KHIVA KhivaArray with the result of this operation.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void khiva_transpose([In] ref IntPtr array, [In] ref bool conjugate,
            [Out] out IntPtr result);

        /// <summary> Retrieves a given column of array.</summary>
        /// <param name="array">KHIVA KhivaArray.</param>
        /// <param name="index">The column to be retrieved.</param>
        /// <param name="result">KHIVA KhivaArray with the result of this operation.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void khiva_col([In] ref IntPtr array, [In] ref int index, [Out] out IntPtr result);

        /// <summary> Retrieves a subset of columns of array, starting at first and finishing at last, both inclusive.</summary>
        /// <param name="array">KHIVA KhivaArray.</param>
        /// <param name="first">Start of the subset of columns to be retrieved.</param>
        /// <param name="last">End of the subset of columns to be retrieved.</param>
        /// <param name="result">KHIVA KhivaArray with the result of this operation.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void khiva_cols([In] ref IntPtr array, [In] ref int first, [In] ref int last,
            [Out] out IntPtr result);

        /// <summary> Retrieves a given row of array.</summary>
        /// <param name="array">KHIVA KhivaArray.</param>
        /// <param name="index">The row to be retrieved.</param>
        /// <param name="result">KHIVA KhivaArray with the result of this operation.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void khiva_row([In] ref IntPtr array, [In] ref int index, [Out] out IntPtr result);

        /// <summary> Retrieves a subset of rows of array, starting at first and finishing at last, both inclusive.</summary>
        /// <param name="array">KHIVA KhivaArray.</param>
        /// <param name="first">Start of the subset of rows to be retrieved.</param>
        /// <param name="last">End of the subset of rows to be retrieved.</param>
        /// <param name="result">KHIVA KhivaArray with the result of this operation.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void khiva_rows([In] ref IntPtr array, [In] ref int first, [In] ref int last,
            [Out] out IntPtr result);

        /// <summary> Performs a matrix multiplication of lhs and rhs.</summary>
        /// <param name="lhs">Left</param>-hand side KHIVA array for the operation.
        /// <param name="rhs">Right</param>-hand side KHIVA array for the operation.
        /// <param name="result">KHIVA KhivaArray with the result of this operation.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void khiva_matmul([In] ref IntPtr lhs, [In] ref IntPtr rhs, [Out] out IntPtr result);

        /// <summary> Performs a deep copy of array.</summary>
        /// <param name="array">KHIVA KhivaArray.</param>
        /// <param name="result">KHIVA KhivaArray which contains a copy of array.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void copy([In] ref IntPtr array, [Out] out IntPtr result);

        /// <summary> Changes the type of array.</summary>
        /// <param name="array">KHIVA KhivaArray.</param>
        /// <param name="type">Target type of the output array.</param>
        /// <param name="result">KHIVA KhivaArray with the result of this operation.</param>
        [DllImport(DLLLibrary.KhivaPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void khiva_as([In] ref IntPtr array, [In] ref int type, [Out] out IntPtr result);
    }
}