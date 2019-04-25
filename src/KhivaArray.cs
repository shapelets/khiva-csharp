// Copyright (c) 2019 Shapelets.io
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Khiva
{
    /// <inheritdoc />
    /// <summary>
    /// Khiva KhivaArray Class.
    /// </summary>
    public class KhivaArray : IDisposable
    {
        private IntPtr _reference;

        /// <summary>
        /// KHIVA array available types.
        /// </summary>
        public enum DType
        {
            /// <summary>
            /// Floating point of single precision. Khiva.DType.
            /// </summary>
            F32,

            /// <summary>
            /// Complex floating point of single precision. Khiva.DType.
            /// </summary>
            C32,

            /// <summary>
            /// Floating point of double precision. Khiva.DType.
            /// </summary>
            F64,

            /// <summary>
            /// Complex floating point of double precision. Khiva.DType.
            /// </summary>
            C64,

            /// <summary>
            /// Boolean. Khiva.DType.
            /// </summary>
            B8,

            /// <summary>
            /// 32 bits Int. Khiva.DType.
            /// </summary>
            S32,

            /// <summary>
            /// 32 bits Unsigned Int. Khiva.DType.
            /// </summary>
            U32,

            /// <summary>
            /// 8 bits Unsigned Int. Khiva.DType.
            /// </summary>
            U8,

            /// <summary>
            /// 64 bits Integer. Khiva.DType.
            /// </summary>
            S64,

            /// <summary>
            /// 64 bits Unsigned Int. Khiva.DType.
            /// </summary>
            U64,

            /// <summary>
            /// 16 bits Int. Khiva.DType.
            /// </summary>
            S16,

            /// <summary>
            /// 16 bits Unsigned Int. Khiva.DType.
            /// </summary>
            U16
        }

        /// <inheritdoc />
        /// <summary>
        /// Dispose the KhivaArray.
        /// </summary>
        public void Dispose()
        {
            CleanUp();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Clean up the array.
        /// </summary>
        private void CleanUp()
        {
            if (Reference == IntPtr.Zero) return;
            DeleteArray();
            Reference = IntPtr.Zero;
        }

        /// <summary>
        /// Destroy KhivaArray.
        /// </summary>
        ~KhivaArray()
        {
            CleanUp();
        }

        /// <summary>
        /// Creates empty KhivaArray.
        /// </summary>
        private KhivaArray()
        {
            Reference = IntPtr.Zero;
        }

        /// <summary>
        /// Creates KhivaArray of zeros.
        /// </summary>
        /// <typeparam name="T">Type of the elements of the array.</typeparam>
        /// <param name="dims">Cardinality of dimensions of the data.</param>
        /// <param name="nDims">Number of dimensions of the data.</param>
        /// <param name="doublePrecision">If Complex array has double precision. Default to false.</param>
        /// <returns>KhivaArray created.</returns>
        public static KhivaArray CreateZeros<T>(long[] dims, uint nDims, bool doublePrecision = false)
            where T : unmanaged
        {
            if (nDims == 1)
            {
                var values = new T[dims[0]];
                return Create(values, doublePrecision);
            }

            if (nDims == 2)
            {
                var values = new T[dims[0], dims[1]];
                return Create(values, doublePrecision);
            }

            if (nDims == 3)
            {
                var values = new T[dims[0], dims[1], dims[2]];
                return Create(values, doublePrecision);
            }

            if (nDims != 4) throw new Exception("Number of dimensions must be between 1 and 4");
            {
                var values = new T[dims[0], dims[1], dims[2], dims[3]];
                return Create(values, doublePrecision);
            }
        }

        /// <summary>
        /// Creates a khiva array object from reference.
        /// </summary>
        /// <param name="reference">Reference from which create the array.</param>
        /// <returns>KhivaArray created.</returns>
        public static KhivaArray Create(IntPtr reference)
        {
            if (reference == null)
            {
                throw new Exception("Null elems object provided");
            }

            var arr = new KhivaArray {Reference = reference};
            return arr;
        }

        /// <summary>
        /// Creates a khiva array object from copy.
        /// </summary>
        /// <param name="other">KhivaArray to copy.</param>
        /// <returns>KhivaArray created.</returns>
        public static KhivaArray Create(KhivaArray other)
        {
            if (other.Reference == null)
            {
                throw new Exception("Null elems object provided");
            }

            return other.Copy();
        }

        /// <summary>
        /// Creates a khiva array object.
        /// </summary>
        /// <typeparam name="T">Type of the elements of the array.</typeparam>
        /// <param name="values">1 dimensional array with the data.</param>
        /// <param name="doublePrecision">If Complex array has double precision. Default to false.</param>
        /// <returns>KhivaArray created.</returns>
        public static unsafe KhivaArray Create<T>(T[] values, bool doublePrecision = false) where T : unmanaged
        {
            if (values == null)
            {
                throw new Exception("Null elems object provided");
            }

            fixed (T* data = &values[0])
                return Create(1, new long[] {values.Length, 1, 1, 1}, data, doublePrecision);
        }

        /// <summary>
        /// Creates a khiva array object.
        /// </summary>
        /// <typeparam name="T">Type of the elements of the array.</typeparam>
        /// <param name="values">2 dimensional array with the data.</param>
        /// <param name="doublePrecision">If Complex array has double precision. Default to false.</param>
        /// <returns>KhivaArray created.</returns>
        public static unsafe KhivaArray Create<T>(T[,] values, bool doublePrecision = false) where T : unmanaged
        {
            if (values == null)
            {
                throw new Exception("Null elems object provided");
            }

            fixed (T* data = &values[0, 0])
                return Create(2, new long[] {values.GetLength(1), values.GetLength(0), 1, 1}, data, doublePrecision);
        }

        /// <summary>
        /// Creates a khiva array object.
        /// </summary>
        /// <typeparam name="T">Type of the elements of the array.</typeparam>
        /// <param name="values">3 dimensional array with the data.</param>
        /// <param name="doublePrecision">If Complex array has double precision. Default to false.</param>
        /// <returns>KhivaArray created</returns>
        public static unsafe KhivaArray Create<T>(T[,,] values, bool doublePrecision = false) where T : unmanaged
        {
            if (values == null)
            {
                throw new Exception("Null elems object provided");
            }

            fixed (T* data = &values[0, 0, 0])
                return Create(3, new long[] {values.GetLength(1), values.GetLength(0), values.GetLength(2), 1}, data,
                    doublePrecision);
        }

        /// <summary>
        /// Creates a khiva array object.
        /// </summary>
        /// <typeparam name="T">Type of the elements of the array.</typeparam>
        /// <param name="values">4 dimensional array with the data.</param>
        /// <param name="doublePrecision">If Complex array has double precision. Default to false.</param>
        /// <returns>KhivaArray created.</returns>
        public static unsafe KhivaArray Create<T>(T[,,,] values, bool doublePrecision = false) where T : unmanaged
        {
            if (values == null)
            {
                throw new Exception("Null elems object provided");
            }

            fixed (T* data = &values[0, 0, 0, 0])
                return Create(4,
                    new long[] {values.GetLength(1), values.GetLength(0), values.GetLength(2), values.GetLength(3)},
                    data, doublePrecision);
        }

        private static unsafe KhivaArray Create<T>(uint nDims, long[] dims, T* values, bool doublePrecision = false)
            where T : unmanaged
        {
            var arr = new KhivaArray();
            var totalLength = dims[0] * dims[1] * dims[2] * dims[3];
            var type = (int) GetDTypeFromT<T>(doublePrecision);
            if (typeof(T) == typeof(Complex))
            {
                fixed (Complex* data = new Complex[totalLength])
                {
                    for (var i = 0; i < totalLength; i++)
                    {
                        data[i] = (Complex) Convert.ChangeType(values[i], typeof(Complex));
                    }

                    // ReSharper disable once SwitchStatementMissingSomeCases
                    switch (type)
                    {
                        case (int) DType.C32:
                        {
                            var flatten = FlatComplex<float>(data, totalLength);
                            Interop.DLLKhivaArray.create_array(flatten, ref nDims, dims, out arr._reference, ref type);
                            break;
                        }
                        case (int) DType.C64:
                        {
                            var flatten = FlatComplex<double>(data, totalLength);
                            Interop.DLLKhivaArray.create_array(flatten, ref nDims, dims, out arr._reference, ref type);
                            break;
                        }
                    }
                }
            }
            else
            {
                Interop.DLLKhivaArray.create_array(values, ref nDims, dims, out arr._reference, ref type);
            }

            arr.Reference = arr._reference;
            return arr;
        }

        private static unsafe T* FlatComplex<T>(Complex* data, long totalLength) where T : unmanaged
        {
            fixed (T* flatten = new T[totalLength * 2])
            {
                for (var i = 0; i < totalLength; i++)
                {
                    flatten[2 * i] = (T) Convert.ChangeType(data[i].Real, typeof(T));
                    flatten[2 * i + 1] = (T) Convert.ChangeType(data[i].Imaginary, typeof(T));
                }

                return flatten;
            }
        }

        /// <summary>
        /// Get the data of a 4 dimensional khiva array.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <returns>1 dimensional array containing the data of the khiva array.</returns>
        public T[] GetData1D<T>()
        {
            if (!CheckType(typeof(T)))
            {
                throw new Exception("ArrayType does mismatch");
            }

            CheckNDims(Dims, 1);
            var data = new T[Dims[0]];
            try
            {
                if (ArrayType == DType.C32)
                {
                    var values = GetData1DAux<float>(Dims[0] * 2);
                    for (var i = 0; i < Dims[0]; i++)
                    {
                        data[i] = (T) Convert.ChangeType(new Complex(values[2 * i], values[2 * i + 1]), typeof(T));
                    }
                }
                else
                {
                    data = GetData1DAux<T>(Dims[0]);
                }
            }
            finally
            {
                GCHandle.Alloc(data, GCHandleType.Weak);
            }

            return data;
        }

        private T[] GetData1DAux<T>(long lastDim)
        {
            var data = new T[lastDim];
            try
            {
                var gchArr = GCHandle.Alloc(data, GCHandleType.Pinned);
                var dataPtr = gchArr.AddrOfPinnedObject();
                Interop.DLLKhivaArray.get_data(ref _reference, dataPtr);
                Reference = _reference;
            }
            finally
            {
                GCHandle.Alloc(data, GCHandleType.Weak);
            }

            return data;
        }

        /// <summary>
        /// Get the data of a 2 dimensional khiva array.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <returns>2 dimensional array containing the data of the khiva array.</returns>
        public T[,] GetData2D<T>()
        {
            if (!CheckType(typeof(T)))
            {
                throw new Exception("ArrayType does mismatch");
            }

            CheckNDims(Dims, 2);
            var data = new T[Dims[1], Dims[0]];
            try
            {
                if (ArrayType == DType.C32)
                {
                    var values = GetData2DAux<float>(Dims[0] * 2);
                    for (var i = 0; i < data.GetLength(0); i++)
                    {
                        for (var j = 0; j < data.GetLength(1); j++)
                        {
                            data[i, j] = (T) Convert.ChangeType(new Complex(values[i, 2 * j], values[i, 2 * j + 1]),
                                typeof(T));
                        }
                    }
                }
                else
                {
                    data = GetData2DAux<T>(Dims[0]);
                }
            }
            finally
            {
                GCHandle.Alloc(data, GCHandleType.Weak);
            }

            return data;
        }

        private T[,] GetData2DAux<T>(long lastDim)
        {
            var data = new T[Dims[1], lastDim];
            try
            {
                var gchArr = GCHandle.Alloc(data, GCHandleType.Pinned);
                var dataPtr = gchArr.AddrOfPinnedObject();
                Interop.DLLKhivaArray.get_data(ref _reference, dataPtr);
                Reference = _reference;
            }
            finally
            {
                GCHandle.Alloc(data, GCHandleType.Weak);
            }

            return data;
        }

        /// <summary>
        /// Get the data of a 3 dimensional khiva array.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <returns>3 dimensional array containing the data of the khiva array.</returns>
        public T[,,] GetData3D<T>()
        {
            if (!CheckType(typeof(T)))
            {
                throw new Exception("ArrayType does mismatch");
            }

            CheckNDims(Dims, 3);
            var data = new T[Dims[1], Dims[0], Dims[2]];
            try
            {
                if (ArrayType == DType.C32)
                {
                    var values = GetData3DAux<float>(Dims[2] * 2);
                    for (var i = 0; i < data.GetLength(0); i++)
                    for (var j = 0; j < data.GetLength(1); j++)
                    for (var k = 0; k < data.GetLength(2); k++)
                    {
                        data[i, j, k] =
                            (T) Convert.ChangeType(new Complex(values[i, j, 2 * k], values[i, j, 2 * k + 1]),
                                typeof(T));
                    }
                }
                else
                {
                    data = GetData3DAux<T>(Dims[2]);
                }
            }
            finally
            {
                GCHandle.Alloc(data, GCHandleType.Weak);
            }

            return data;
        }

        private T[,,] GetData3DAux<T>(long lastDim)
        {
            var data = new T[Dims[1], Dims[0], lastDim];
            try
            {
                var gchArr = GCHandle.Alloc(data, GCHandleType.Pinned);
                var dataPtr = gchArr.AddrOfPinnedObject();
                Interop.DLLKhivaArray.get_data(ref _reference, dataPtr);
                Reference = _reference;
            }
            finally
            {
                GCHandle.Alloc(data, GCHandleType.Weak);
            }

            return data;
        }

        /// <summary>
        /// Get the data of a 4 dimensional khiva array.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <returns>4 dimensional array containing the data of the khiva array.</returns>
        public T[,,,] GetData4D<T>()
        {
            if (!CheckType(typeof(T)))
            {
                throw new Exception("ArrayType does mismatch");
            }

            var data = new T[Dims[1], Dims[0], Dims[2], Dims[3]];
            try
            {
                if (ArrayType == DType.C32)
                {
                    var values = GetData4DAux<float>(Dims[3] * 2);
                    for (var i = 0; i < data.GetLength(0); i++)
                    for (var j = 0; j < data.GetLength(1); j++)
                    for (var k = 0; k < data.GetLength(2); k++)
                    for (var z = 0; z < data.GetLength(3); z++)
                    {
                        data[i, j, k, z] =
                            (T) Convert.ChangeType(new Complex(values[i, j, k, 2 * z], values[i, j, k, 2 * z + 1]),
                                typeof(T));
                    }
                }
                else
                {
                    data = GetData4DAux<T>(Dims[3]);
                }
            }
            finally
            {
                GCHandle.Alloc(data, GCHandleType.Weak);
            }

            return data;
        }

        private T[,,,] GetData4DAux<T>(long lastDim)
        {
            var data = new T[Dims[1], Dims[0], Dims[2], lastDim];
            try
            {
                var gchArr = GCHandle.Alloc(data, GCHandleType.Pinned);
                var dataPtr = gchArr.AddrOfPinnedObject();
                Interop.DLLKhivaArray.get_data(ref _reference, dataPtr);
                Reference = _reference;
            }
            finally
            {
                GCHandle.Alloc(data, GCHandleType.Weak);
            }

            return data;
        }

        private static DType GetDTypeFromT<T>(bool doublePrecision)
        {
            var type = typeof(T);
            if (type == typeof(double))
            {
                return DType.F64;
            }

            if (type == typeof(float))
            {
                return DType.F32;
            }

            if (type == typeof(int))
            {
                return DType.S32;
            }

            if (type == typeof(uint))
            {
                return DType.U32;
            }

            if (type == typeof(bool))
            {
                return DType.B8;
            }

            if (type == typeof(long))
            {
                return DType.S64;
            }

            if (type == typeof(ulong))
            {
                return DType.U64;
            }

            if (type == typeof(short))
            {
                return DType.S16;
            }

            if (type == typeof(ushort))
            {
                return DType.U16;
            }

            if (type == typeof(byte))
            {
                return DType.U8;
            }

            if (type == typeof(Complex))
            {
                return doublePrecision ? DType.C64 : DType.C32;
            }

            throw new Exception("Type not supported");
        }

        /// <summary>
        /// Getters and setters of the Reference parameter.
        /// </summary>
        public IntPtr Reference
        {
            get => _reference;
            set => _reference = value;
        }

        private bool CheckType(Type type)
        {
            switch (ArrayType)
            {
                case DType.B8:
                    return type == typeof(bool);
                case DType.C32:
                    return type == typeof(Complex);
                case DType.C64:
                    return type == typeof(Complex);
                case DType.F32:
                    return type == typeof(float);
                case DType.F64:
                    return type == typeof(double);
                case DType.S16:
                    return type == typeof(short);
                case DType.S32:
                    return type == typeof(int);
                case DType.S64:
                    return type == typeof(long);
                case DType.U16:
                    return type == typeof(ushort);
                case DType.U32:
                    return type == typeof(uint);
                case DType.U64:
                    return type == typeof(ulong);
                case DType.U8:
                    return type == typeof(byte);
                default:
                    return false;
            }
        }

        private static void CheckNDims(IReadOnlyList<long> dims, int maxNDims)
        {
            var i = 3;
            while (i > maxNDims - 1)
            {
                if (dims[i] > 1)
                {
                    throw new Exception("The array must be have at most " + maxNDims +
                                        " dimensions for using this method but have " + (i + 1));
                }

                i--;
            }
        }

        /// <summary>
        /// Gets the KhivaArray dimensions.
        /// </summary>
        public long[] Dims
        {
            get
            {
                var dims = new long[4];
                Interop.DLLKhivaArray.get_dims(ref _reference, dims);
                Reference = _reference;
                return dims;
            }
        }

        /// <summary>
        /// Displays an KhivaArray.
        /// </summary>
        public void Display()
        {
            Interop.DLLKhivaArray.display(ref _reference);
            Reference = _reference;
        }

        /// <summary>
        /// Decreases the references count of the given array.
        /// </summary>
        private void DeleteArray()
        {
            Interop.DLLKhivaArray.delete_array(ref _reference);
            Reference = _reference;
        }

        /// <summary>
        /// Gets the type of the array.
        /// </summary>
        public DType ArrayType
        {
            get
            {
                Interop.DLLKhivaArray.get_type(ref _reference, out var type);
                Reference = _reference;
                return (DType) type;
            }
        }

        /// <summary>
        /// Adds two arrays.
        /// </summary>
        /// <param name="lhs">Left-hand side KHIVA array for the operation.</param>
        /// <param name="rhs">Right-hand side KHIVA array for the operation.</param>
        /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
        public static KhivaArray operator +(KhivaArray lhs, KhivaArray rhs)
        {
            Interop.DLLKhivaArray.khiva_add(ref lhs._reference, ref rhs._reference, out var result);
            lhs.Reference = lhs._reference;
            rhs.Reference = rhs._reference;
            return Create(result);
        }

        /// <summary>
        /// Multiplies two arrays.
        /// </summary>
        /// <param name="lhs">Left-hand side KHIVA array for the operation.</param>
        /// <param name="rhs">Right-hand side KHIVA array for the operation.</param>
        /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
        public static KhivaArray operator *(KhivaArray lhs, KhivaArray rhs)
        {
            Interop.DLLKhivaArray.khiva_mul(ref lhs._reference, ref rhs._reference, out var result);
            lhs.Reference = lhs._reference;
            rhs.Reference = rhs._reference;
            return Create(result);
        }

        /// <summary>
        /// Subtracts two arrays.
        /// </summary>
        /// <param name="lhs">Left-hand side KHIVA array for the operation.</param>
        /// <param name="rhs">Right-hand side KHIVA array for the operation.</param>
        /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
        public static KhivaArray operator -(KhivaArray lhs, KhivaArray rhs)
        {
            Interop.DLLKhivaArray.khiva_sub(ref lhs._reference, ref rhs._reference, out var result);
            lhs.Reference = lhs._reference;
            rhs.Reference = rhs._reference;
            return Create(result);
        }

        /// <summary>
        /// Divides two arrays.
        /// </summary>
        /// <param name="lhs">Left-hand side KHIVA array for the operation.</param>
        /// <param name="rhs">Right-hand side KHIVA array for the operation.</param>
        /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
        public static KhivaArray operator /(KhivaArray lhs, KhivaArray rhs)
        {
            Interop.DLLKhivaArray.khiva_div(ref lhs._reference, ref rhs._reference, out var result);
            lhs.Reference = lhs._reference;
            rhs.Reference = rhs._reference;
            return Create(result);
        }

        /// <summary>
        /// Performs the modulo operation of lhs by rhs.
        /// </summary>
        /// <param name="lhs">Left-hand side KHIVA array for the operation.</param>
        /// <param name="rhs">Right-hand side KHIVA array for the operation.</param>
        /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
        public static KhivaArray operator %(KhivaArray lhs, KhivaArray rhs)
        {
            Interop.DLLKhivaArray.khiva_mod(ref lhs._reference, ref rhs._reference, out var result);
            lhs.Reference = lhs._reference;
            rhs.Reference = rhs._reference;
            return Create(result);
        }

        /// <summary>
        /// Powers lhs with rhs.
        /// </summary>
        /// <param name="lhs">Left-hand side KHIVA array for the operation.</param>
        /// <param name="rhs">Right-hand side KHIVA array for the operation.</param>
        /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
        public static KhivaArray Pow(KhivaArray lhs, KhivaArray rhs)
        {
            Interop.DLLKhivaArray.khiva_pow(ref lhs._reference, ref rhs._reference, out var result);
            lhs.Reference = lhs._reference;
            rhs.Reference = rhs._reference;
            return Create(result);
        }

        /// <summary>
        /// Performs an AND operation (element-wise) with lhs and rhs.
        /// </summary>
        /// <param name="lhs">Left-hand side KHIVA array for the operation.</param>
        /// <param name="rhs">Right-hand side KHIVA array for the operation.</param>
        /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
        public static KhivaArray operator &(KhivaArray lhs, KhivaArray rhs)
        {
            Interop.DLLKhivaArray.khiva_bitand(ref lhs._reference, ref rhs._reference, out var result);
            lhs.Reference = lhs._reference;
            rhs.Reference = rhs._reference;
            return Create(result);
        }

        /// <summary>
        /// Performs an OR operation (element-wise) with lhs and rhs.
        /// </summary>
        /// <param name="lhs">Left-hand side KHIVA array for the operation.</param>
        /// <param name="rhs">Right-hand side KHIVA array for the operation.</param>
        /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
        public static KhivaArray operator |(KhivaArray lhs, KhivaArray rhs)
        {
            Interop.DLLKhivaArray.khiva_bitor(ref lhs._reference, ref rhs._reference, out var result);
            lhs.Reference = lhs._reference;
            rhs.Reference = rhs._reference;
            return Create(result);
        }

        /// <summary>
        /// Performs an eXclusive-OR operation (element-wise) with lhs and rhs.
        /// </summary>
        /// <param name="lhs">Left-hand side KHIVA array for the operation.</param>
        /// <param name="rhs">Right-hand side KHIVA array for the operation.</param>
        /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
        public static KhivaArray operator ^(KhivaArray lhs, KhivaArray rhs)
        {
            Interop.DLLKhivaArray.khiva_bitxor(ref lhs._reference, ref rhs._reference, out var result);
            lhs.Reference = lhs._reference;
            rhs.Reference = rhs._reference;
            return Create(result);
        }

        /// <summary>
        /// Performs a left bit shift operation (element-wise) to array as many times as specified in the parameter n.
        /// </summary>
        /// <param name="lhs">array KHIVA KhivaArray to shift.</param>
        /// <param name="shift">Number of bits to be shifted.</param>
        /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
        public static KhivaArray operator <<(KhivaArray lhs, int shift)
        {
            Interop.DLLKhivaArray.khiva_bitshiftl(ref lhs._reference, ref shift, out var result);
            lhs.Reference = lhs._reference;
            return Create(result);
        }

        /// <summary>
        /// Performs a right bit shift operation (element-wise) to array as many times as specified in the parameter n.
        /// </summary>
        /// <param name="lhs">array KHIVA KhivaArray to shift.</param>
        /// <param name="shift">Number of bits to be shifted.</param>
        /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
        public static KhivaArray operator >>(KhivaArray lhs, int shift)
        {
            Interop.DLLKhivaArray.khiva_bitshiftr(ref lhs._reference, ref shift, out var result);
            lhs.Reference = lhs._reference;
            return Create(result);
        }

        /// <summary>
        /// Unary minus of one array.
        /// </summary>
        /// <param name="rhs">Right-hand side KHIVA array for the operation.</param>
        /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
        public static KhivaArray operator -(KhivaArray rhs)
        {
            KhivaArray zeros;
            var dims = rhs.Dims;
            var maxDim = GetMaxDim(dims);
            switch (rhs.ArrayType)
            {
                case DType.C32:
                    zeros = CreateZeros<Complex>(dims, maxDim);
                    break;
                case DType.C64:
                    zeros = CreateZeros<Complex>(dims, maxDim, true);
                    break;
                case DType.F32:
                case DType.F64:
                case DType.B8:
                case DType.S32:
                case DType.U32:
                case DType.U8:
                case DType.S64:
                case DType.U64:
                case DType.S16:
                case DType.U16:
                    zeros = CreateZeros<int>(dims, maxDim);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var zRef = zeros.Reference;
            Interop.DLLKhivaArray.khiva_sub(ref zRef, ref rhs._reference, out var result);
            zeros.Reference = zeros._reference;
            rhs.Reference = rhs._reference;
            return Create(result);
        }

        // ReSharper disable once SuggestBaseTypeForParameter
        private static uint GetMaxDim(long[] dims)
        {
            uint i = 3;
            while (i > 0)
            {
                if (dims[i] > 1)
                {
                    return i + 1;
                }

                i--;
            }

            return i + 1;
        }

        /// <summary>
        /// Logical NOT operation to array.
        /// </summary>
        /// <param name="lhs">KHIVA KhivaArray to negate.</param>
        /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
        public static KhivaArray operator !(KhivaArray lhs)
        {
            Interop.DLLKhivaArray.khiva_not(ref lhs._reference, out var result);
            lhs.Reference = lhs._reference;
            return Create(result);
        }

        /// <summary>
        /// Compares (element-wise) if lhs is lower than rhs.
        /// </summary>
        /// <param name="lhs">Left-hand side KHIVA array for the operation.</param>
        /// <param name="rhs">Right-hand side KHIVA array for the operation.</param>
        /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
        public static KhivaArray operator <(KhivaArray lhs, KhivaArray rhs)
        {
            Interop.DLLKhivaArray.khiva_lt(ref lhs._reference, ref rhs._reference, out var result);
            lhs.Reference = lhs._reference;
            rhs.Reference = rhs._reference;
            return Create(result);
        }

        /// <summary>
        /// Compares (element-wise) if lhs is greater than rhs.
        /// </summary>
        /// <param name="lhs">Left-hand side KHIVA array for the operation.</param>
        /// <param name="rhs">Right-hand side KHIVA array for the operation.</param>
        /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
        public static KhivaArray operator >(KhivaArray lhs, KhivaArray rhs)
        {
            Interop.DLLKhivaArray.khiva_gt(ref lhs._reference, ref rhs._reference, out var result);
            lhs.Reference = lhs._reference;
            rhs.Reference = rhs._reference;
            return Create(result);
        }

        /// <summary>
        /// Compares (element-wise) if lhs is lower or equal than rhs.
        /// </summary>
        /// <param name="lhs">Left-hand side KHIVA array for the operation.</param>
        /// <param name="rhs">Right-hand side KHIVA array for the operation.</param>
        /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
        public static KhivaArray operator <=(KhivaArray lhs, KhivaArray rhs)
        {
            Interop.DLLKhivaArray.khiva_le(ref lhs._reference, ref rhs._reference, out var result);
            lhs.Reference = lhs._reference;
            rhs.Reference = rhs._reference;
            return Create(result);
        }

        /// <summary>
        /// Compares (element-wise) if lhs is greater or equal than rhs.
        /// </summary>
        /// <param name="lhs">Left-hand side KHIVA array for the operation.</param>
        /// <param name="rhs">Right-hand side KHIVA array for the operation.</param>
        /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
        public static KhivaArray operator >=(KhivaArray lhs, KhivaArray rhs)
        {
            Interop.DLLKhivaArray.khiva_ge(ref lhs._reference, ref rhs._reference, out var result);
            lhs.Reference = lhs._reference;
            rhs.Reference = rhs._reference;
            return Create(result);
        }

        /// <summary>
        /// Compares (element-wise) if rhs is equal to rhs.
        /// </summary>
        /// <param name="lhs">Left-hand side KHIVA array for the operation.</param>
        /// <param name="rhs">Right-hand side KHIVA array for the operation.</param>
        /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
        public static KhivaArray operator ==(KhivaArray lhs, KhivaArray rhs)
        {
            if (lhs is null || rhs is null) return null;
            Interop.DLLKhivaArray.khiva_eq(ref lhs._reference, ref rhs._reference, out var result);
            lhs.Reference = lhs._reference;
            rhs.Reference = rhs._reference;
            return Create(result);
        }

        /// <summary>
        /// Compares (element-wise) if lhs is not equal to rhs.
        /// </summary>
        /// <param name="lhs">Left-hand side KHIVA array for the operation.</param>
        /// <param name="rhs">Right-hand side KHIVA array for the operation.</param>
        /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
        public static KhivaArray operator !=(KhivaArray lhs, KhivaArray rhs)
        {
            if (lhs is null || rhs is null) return null;
            Interop.DLLKhivaArray.khiva_ne(ref lhs._reference, ref rhs._reference, out var result);
            lhs.Reference = lhs._reference;
            rhs.Reference = rhs._reference;
            return Create(result);
        }

        /// <summary>
        /// Equals object method
        /// </summary>
        /// <param name="o">Object to compare</param>
        /// <returns></returns>
        public override bool Equals(object o)
        {
            if (o != null && o is KhivaArray arr)
            {
                return _reference.Equals(arr._reference);
            }

            return false;
        }

        /// <summary>
        /// GetHashCode object method
        /// </summary>
        /// <returns>Hashcode of the KhivaArray</returns>
        public override int GetHashCode()
        {
            // ReSharper disable once NonReadonlyMemberInGetHashCode
            return _reference.GetHashCode();
        }

        /// <summary>
        /// Transposes array.
        /// </summary>
        /// <param name="conjugate">If true a conjugate transposition is performed.</param>
        /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
        public KhivaArray Transpose(bool conjugate = false)
        {
            Interop.DLLKhivaArray.khiva_transpose(ref _reference, ref conjugate, out var result);
            Reference = _reference;
            return Create(result);
        }

        /// <summary>
        /// Retrieves a given column of array.
        /// </summary>
        /// <param name="index">The column to be retrieved.</param>
        /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
        public KhivaArray Col(int index)
        {
            Interop.DLLKhivaArray.khiva_col(ref _reference, ref index, out var result);
            Reference = _reference;
            return Create(result);
        }

        /// <summary>
        /// Retrieves a subset of columns of array, starting at first and finishing at last, both inclusive.
        /// </summary>
        /// <param name="first">Start of the subset of columns to be retrieved.</param>
        /// <param name="last">End of the subset of columns to be retrieved.</param>
        /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
        public KhivaArray Cols(int first, int last)
        {
            Interop.DLLKhivaArray.khiva_cols(ref _reference, ref first, ref last, out var result);
            Reference = _reference;
            return Create(result);
        }

        /// <summary>
        /// Retrieves a given row of array.
        /// </summary>
        /// <param name="index">The row to be retrieved.</param>
        /// <returns> KHIVA KhivaArray with the result of this operation.</returns>
        public KhivaArray Row(int index)
        {
            Interop.DLLKhivaArray.khiva_row(ref _reference, ref index, out var result);
            Reference = _reference;
            return Create(result);
        }

        /// <summary>
        /// Retrieves a subset of rows of array, starting at first and finishing at last, both inclusive.
        /// </summary>
        /// <param name="first">Start of the subset of rows to be retrieved.</param>
        /// <param name="last">End of the subset of rows to be retrieved.</param>
        /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
        public KhivaArray Rows(int first, int last)
        {
            Interop.DLLKhivaArray.khiva_rows(ref _reference, ref first, ref last, out var result);
            Reference = _reference;
            return Create(result);
        }

        /// <summary>
        /// Performs a matrix multiplication of lhs and rhs.
        /// </summary>
        /// <param name="rhs">Right-hand side KHIVA array for the operation.</param>
        /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
        public KhivaArray MatMul(KhivaArray rhs)
        {
            Interop.DLLKhivaArray.khiva_matmul(ref _reference, ref rhs._reference, out var result);
            Reference = _reference;
            rhs.Reference = rhs._reference;
            return Create(result);
        }

        /// <summary>
        /// Performs a deep copy of array.
        /// </summary>
        /// <returns>KHIVA KhivaArray which contains a copy of array.</returns>
        public KhivaArray Copy()
        {
            Interop.DLLKhivaArray.copy(ref _reference, out var result);
            Reference = _reference;
            return Create(result);
        }

        /// <summary>
        /// Changes the type of array.
        /// </summary>
        /// <param name="type">Target type of the output array.</param>
        /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
        public KhivaArray As(int type)
        {
            Interop.DLLKhivaArray.khiva_as(ref _reference, ref type, out var result);
            Reference = _reference;
            return Create(result);
        }
    }
}