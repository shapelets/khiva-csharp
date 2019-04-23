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
using System.Numerics;
using System.Runtime.InteropServices;

namespace khiva
{
    namespace array
    {
        /// <summary>
        /// Khiva KhivaArray Class.
        /// </summary>
        public class KhivaArray : IDisposable
        {

            private IntPtr reference;

            /// <summary>
            /// KHIVA array available types.
            /// </summary>
            public enum Dtype
            {
                /// <summary>
                /// Floating point of single precision. khiva.dtype.
                /// </summary>
                f32,
                /// <summary>
                /// Complex floating point of single precision. khiva.dtype.
                /// </summary>
                c32,
                /// <summary>
                /// Floating point of double precision. khiva.dtype.
                /// </summary>
                f64,
                /// <summary>
                /// Complex floating point of double precision. khiva.dtype.
                /// </summary>
                c64,
                /// <summary>
                /// Boolean. khiva.dtype.
                /// </summary>
                b8,
                /// <summary>
                /// 32 bits Int. khiva.dtype.
                /// </summary>
                s32,
                /// <summary>
                /// 32 bits Unsigned Int. khiva.dtype.
                /// </summary>
                u32,
                /// <summary>
                /// 8 bits Unsigned Int. khiva.dtype.
                /// </summary>
                u8,
                /// <summary>
                /// 64 bits Integer. khiva.dtype.
                /// </summary>
                s64,
                /// <summary>
                /// 64 bits Unsigned Int. khiva.dtype.
                /// </summary>
                u64,
                /// <summary>
                /// 16 bits Int. khiva.dtype.
                /// </summary>
                s16,
                /// <summary>
                /// 16 bits Unsigned Int. khiva.dtype.
                /// </summary>
                u16
            }

            /// <summary>
            /// Dispose the KhivaArray
            /// </summary>
            public void Dispose()
            {
                CleanUp();
                GC.SuppressFinalize(this);
            }
            /// <summary>
            /// Clean up the array
            /// </summary>
            private void CleanUp()
            {
                if (Reference != IntPtr.Zero)
                {
                    this.DeleteArray();
                    this.Reference = IntPtr.Zero;
                }
            }
            /// <summary>
            /// Destroy KhivaArray
            /// </summary>
            ~KhivaArray()
            {
                CleanUp();
            }

            /// <summary>
            /// Creates empty KhivaArray
            /// </summary>
            protected KhivaArray()
            {
                Reference = IntPtr.Zero;
            }

            /// <summary>
            /// Creates KhivaArray of zeros
            /// </summary>
            /// <typeparam name="T">Type of the elements of the array</typeparam>
            /// <param name="dims">Cardinality of dimensions of the data.</param>
            /// <param name="ndims">Number of dimensions of the data.</param>
            /// <param name="doublePrecision">If Complex array has double precision. Default to false</param>
            /// <returns>KhivaArray created</returns>
            public static KhivaArray CreateZeros<T>(long[] dims, uint ndims, bool doublePrecision = false) where T:unmanaged
            {
                if(ndims == 1)
                {
                    T[] values = new T[dims[0]];
                    return Create(values, doublePrecision);
                }
                else if (ndims == 2)
                {
                    T[,] values = new T[dims[0], dims[1]];
                    return Create(values, doublePrecision);
                }
                else if (ndims == 3)
                {
                    T[,,] values = new T[dims[0], dims[1], dims[2]];
                    return Create(values, doublePrecision);
                }
                else if (ndims == 4)
                {
                    T[,,,] values = new T[dims[0], dims[1], dims[2], dims[3]];
                    return Create(values, doublePrecision);
                }
                else
                {
                    throw new Exception("Number of dimensions must be between 1 and 4");
                }
            }

            /// <summary>
            /// Creates a khiva array object from reference
            /// </summary>
            /// <param name="reference">Reference from which create the array</param>
            /// <returns>KhivaArray created</returns>
            public static KhivaArray Create(IntPtr reference)
            {
                if (reference == null)
                {
                    throw new Exception("Null elems object provided");
                }
                var arr = new KhivaArray();
                arr.Reference = reference;
                return arr;
            }

            /// <summary>
            /// Creates a khiva array object from copy
            /// </summary>
            /// <param name="other">KhivaArray to copy</param>
            /// <returns>KhivaArray created</returns>
            public static KhivaArray Create(KhivaArray other)
            {
                if (other.Reference == null)
                {
                    throw new Exception("Null elems object provided");
                }
                return other.Copy();
            }

            /// <summary>
            /// Creates a khiva array object
            /// </summary>
            /// <typeparam name="T">Type of the elements of the array</typeparam>
            /// <param name="values">1 dimensional array with the data</param>
            /// <param name="doublePrecision">If Complex array has double precision. Default to false</param>
            /// <returns>KhivaArray created</returns>
            public unsafe static KhivaArray Create<T>(T[] values, bool doublePrecision = false) where T:unmanaged
            {
                if (values == null)
                {
                    throw new Exception("Null elems object provided");
                }
                fixed (T* data = &values[0])
                    return Create<T>(1, new long[] { values.Length, 1, 1, 1 }, data, doublePrecision);
            }

            /// <summary>
            /// Creates a khiva array object
            /// </summary>
            /// <typeparam name="T">Type of the elements of the array</typeparam>
            /// <param name="values">2 dimensional array with the data</param>
            /// <param name="doublePrecision">If Complex array has double precision. Default to false</param>
            /// <returns>KhivaArray created</returns>
            public unsafe static KhivaArray Create<T>(T[,] values, bool doublePrecision = false) where T : unmanaged
            {
                if (values == null)
                {
                    throw new Exception("Null elems object provided");
                }
                fixed (T* data = &values[0,0])
                    return Create<T>(2, new long[] { values.GetLength(1), values.GetLength(0), 1, 1 }, data, doublePrecision);
            }

            /// <summary>
            /// Creates a khiva array object
            /// </summary>
            /// <typeparam name="T">Type of the elements of the array</typeparam>
            /// <param name="values">3 dimensional array with the data</param>
            /// <param name="doublePrecision">If Complex array has double precision. Default to false</param>
            /// <returns>KhivaArray created</returns>
            public unsafe static KhivaArray Create<T>(T[,,] values, bool doublePrecision = false) where T : unmanaged
            {
                if (values == null)
                {
                    throw new Exception("Null elems object provided");
                }
                fixed (T* data = &values[0,0,0])
                    return Create<T>(3, new long[] { values.GetLength(1), values.GetLength(0), values.GetLength(2), 1 }, data, doublePrecision);
            }

            /// <summary>
            /// Creates a khiva array object
            /// </summary>
            /// <typeparam name="T">Type of the elements of the array</typeparam>
            /// <param name="values">4 dimensional array with the data</param>
            /// <param name="doublePrecision">If Complex array has double precision. Default to false</param>
            /// <returns>KhivaArray created</returns>
            public unsafe static KhivaArray Create<T>(T[,,,] values, bool doublePrecision = false) where T : unmanaged
            {
                if (values == null)
                {
                    throw new Exception("Null elems object provided");
                }
                fixed (T* data = &values[0,0,0,0])
                    return Create<T>(4, new long[] { values.GetLength(1), values.GetLength(0), values.GetLength(2), values.GetLength(3) }, data, doublePrecision);
            }

            private unsafe static KhivaArray Create<T>(uint ndims, long[] dims, T* values, bool doublePrecision = false) where T : unmanaged
            {
                KhivaArray arr = new KhivaArray();
                long totalLength = (long)(dims[0] * dims[1] * dims[2] * dims[3]);
                var type = (int)GetDtypeFromT<T>(doublePrecision);
                if(typeof(T) == typeof(Complex))
                {
                    fixed (Complex* data = new Complex[totalLength])
                    {
                        for (int i = 0; i < totalLength; i++)
                        {
                            data[i] = (Complex)Convert.ChangeType(values[i], typeof(Complex));
                        }
                        if (type == (int)Dtype.c32)
                        {  
                            float* flatten = FlatComplex<float>(data, totalLength);
                            interop.DLLArray.create_array(flatten, ref ndims, dims, out arr.reference, ref type);
                        }

                        else if (type == (int)Dtype.c64)
                        {
                            double* flatten = FlatComplex<double>(data, totalLength);
                            interop.DLLArray.create_array(flatten, ref ndims, dims, out arr.reference, ref type);
                        }
                    }   
                }
                else
                {
                    interop.DLLArray.create_array(values, ref ndims, dims, out arr.reference, ref type);
                }

                arr.Reference = arr.reference;
                return arr;
            }

            private unsafe static T* FlatComplex<T>(Complex* data, long totalLength) where T : unmanaged
            {
                fixed(T* flatten = new T[totalLength * 2])
                {
                    for (int i = 0; i<totalLength; i++)
                    {
                        flatten[2 * i] = (T)Convert.ChangeType(data[i].Real, typeof(T));
                        flatten[(2 * i) + 1] = (T)Convert.ChangeType(data[i].Imaginary, typeof(T));
                    }
                    return flatten;
                }
            }

            /// <summary>
            /// Get the data of a 4 dimensional khiva array
            /// </summary>
            /// <typeparam name="T">The type of the elements of the array</typeparam>
            /// <returns>1 dimensional array containing the data of the khiva array</returns>
            public T[] GetData1D<T>()
            {
                if (!CheckType(typeof(T)))
                {
                    throw new Exception("ArrayType does mismatch");
                }
                CheckNdims(Dims, 2);
                T[] data = new T[Dims[0]];
                try
                {
                    if (ArrayType == Dtype.c32)
                    {
                       var values = GetData1DAux<float>(Dims[0]*2);
                        for(int i = 0; i < Dims[0]; i++)
                        {
                            data[i] = (T)Convert.ChangeType(new Complex(values[2*i], values[2*i + 1]), typeof(T));
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
                T[] data = new T[lastDim];
                try
                {
                    GCHandle gchArr = GCHandle.Alloc(data, GCHandleType.Pinned);
                    IntPtr dataPtr = gchArr.AddrOfPinnedObject();
                    interop.DLLArray.get_data(ref reference, dataPtr);
                    Reference = reference;
                }
                finally
                {
                    GCHandle.Alloc(data, GCHandleType.Weak);
                }
                return data;
            }

            /// <summary>
            /// Get the data of a 2 dimensional khiva array
            /// </summary>
            /// <typeparam name="T">The type of the elements of the array</typeparam>
            /// <returns>2 dimensional array containing the data of the khiva array</returns>
            public T[,] GetData2D<T>()
            {
                if (!CheckType(typeof(T)))
                {
                    throw new Exception("ArrayType does mismatch");
                }
                CheckNdims(Dims, 2);
                T[,] data = new T[Dims[1], Dims[0]];
                try
                {
                    if (ArrayType == Dtype.c32)
                    {
                        var values = GetData2DAux<float>(Dims[0] * 2);
                        for (int i = 0; i < data.GetLength(0); i++)
                        {
                            for (int j = 0; j < data.GetLength(1); j++)
                            {
                                data[i,j] = (T)Convert.ChangeType(new Complex(values[i, 2*j], values[i, 2*j + 1]), typeof(T));
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
                T[,] data = new T[Dims[1], lastDim];
                try
                {
                    GCHandle gchArr = GCHandle.Alloc(data, GCHandleType.Pinned);
                    IntPtr dataPtr = gchArr.AddrOfPinnedObject();
                    interop.DLLArray.get_data(ref reference, dataPtr);
                    Reference = reference;
                }
                finally
                {
                    GCHandle.Alloc(data, GCHandleType.Weak);
                }
                return data;
            }

            /// <summary>
            /// Get the data of a 3 dimensional khiva array
            /// </summary>
            /// <typeparam name="T">The type of the elements of the array</typeparam>
            /// <returns>3 dimensional array containing the data of the khiva array</returns>
            public T[,,] GetData3D<T>()
            {
                if (!CheckType(typeof(T)))
                {
                    throw new Exception("ArrayType does mismatch");
                }
                CheckNdims(Dims, 3);
                T[,,] data = new T[Dims[1], Dims[0], Dims[2]];
                try
                {
                    if (ArrayType == Dtype.c32)
                    {
                        var values = GetData3DAux<float>(Dims[2] * 2);
                        for (int i = 0; i < data.GetLength(0); i++)
                        for (int j = 0; j < data.GetLength(1); j++)
                        for (int k = 0; k < data.GetLength(2); k++) { 
                            data[i,j,k] = (T)Convert.ChangeType(new Complex(values[i,j,2*k], values[i,j,2 * k + 1]), typeof(T));
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
                T[,,] data = new T[Dims[1], Dims[0], lastDim];
                try
                {
                    GCHandle gchArr = GCHandle.Alloc(data, GCHandleType.Pinned);
                    IntPtr dataPtr = gchArr.AddrOfPinnedObject();
                    interop.DLLArray.get_data(ref reference, dataPtr);
                    Reference = reference;
                }
                finally
                {
                    GCHandle.Alloc(data, GCHandleType.Weak);
                }
                return data;
            }

            /// <summary>
            /// Get the data of a 4 dimensional khiva array
            /// </summary>
            /// <typeparam name="T">The type of the elements of the array</typeparam>
            /// <returns>4 dimensional array containing the data of the khiva array</returns>
            public T[,,,] GetData4D<T>()
            {
                if (!CheckType(typeof(T)))
                {
                    throw new Exception("ArrayType does mismatch");
                }
                T[,,,] data = new T[Dims[1], Dims[0], Dims[2], Dims[3]];
                try
                {
                    if (ArrayType == Dtype.c32)
                    {
                        var values = GetData4DAux<float>(Dims[3] * 2);
                        for (int i = 0; i < data.GetLength(0); i++)
                        for (int j = 0; j < data.GetLength(1); j++)
                        for (int k = 0; k < data.GetLength(2); k++)
                        for (int z = 0; z < data.GetLength(3); z++) { 
                            data[i,j,k,z] = (T)Convert.ChangeType(new Complex(values[i,j,k,2 * z], values[i,j,k,2 * z + 1]), typeof(T));
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
                T[,,,] data = new T[Dims[1], Dims[0], Dims[2], lastDim];
                try
                {
                    GCHandle gchArr = GCHandle.Alloc(data, GCHandleType.Pinned);
                    IntPtr dataPtr = gchArr.AddrOfPinnedObject();
                    interop.DLLArray.get_data(ref reference, dataPtr);
                    Reference = reference;
                }
                finally
                {
                    GCHandle.Alloc(data, GCHandleType.Weak);
                }
                return data;
            }

            private static Dtype GetDtypeFromT<T>(bool doublePrecision)
            {
                var type = typeof(T);
                if (type == typeof(double))
                {
                    return Dtype.f64;
                }
                else if (type == typeof(float))
                {
                    return Dtype.f32;
                }
                else if (type == typeof(int))
                {
                    return Dtype.s32;
                }
                else if (type == typeof(uint))
                {
                    return Dtype.u32;
                }
                else if (type == typeof(bool))
                {
                    return Dtype.b8;
                }
                else if (type == typeof(long))
                {
                    return Dtype.s64;
                }
                else if (type == typeof(ulong))
                {
                    return Dtype.u64;
                }
                else if (type == typeof(short))
                {
                    return Dtype.s16;
                }
                else if (type == typeof(ushort))
                {
                    return Dtype.u16;
                }
                else if (type == typeof(byte))
                {
                    return Dtype.u8;
                }
                else if (type == typeof(Complex))
                {
                    if (doublePrecision)
                    {
                        return Dtype.c64;
                    }
                    else
                    {
                        return Dtype.c32;
                    }
                }
                else
                {
                    throw new Exception("Type not supported");
                }
            }

            /// <summary>
            /// Getters and setters of the Reference parameter
            /// </summary>
            public IntPtr Reference
            {
                get
                {
                    return reference;
                }
                set
                {
                    reference = value;
                }
            }

            private bool CheckType(Type type)
            {
                switch (ArrayType)
                {
                    case Dtype.b8:
                        return type == typeof(bool);
                    case Dtype.c32:
                        return type == typeof(Complex);
                    case Dtype.c64:
                        return type == typeof(Complex);
                    case Dtype.f32:
                        return type == typeof(float);
                    case Dtype.f64:
                        return type == typeof(double);
                    case Dtype.s16:
                        return type == typeof(short);
                    case Dtype.s32:
                        return type == typeof(int);
                    case Dtype.s64:
                        return type == typeof(long);
                    case Dtype.u16:
                        return type == typeof(ushort);
                    case Dtype.u32:
                        return type == typeof(uint);
                    case Dtype.u64:
                        return type == typeof(ulong);
                    case Dtype.u8:
                        return type == typeof(byte);
                    default:
                        return false;
                }
            }

            private void CheckNdims(long[] dims, int maxNdims)
            {
                int i = 3;
                while (i > maxNdims - 1)
                {
                    if (dims[i] > 1)
                    {
                        throw new Exception("The array must be have at most " + maxNdims + " dimensions for using this method but have " + (i + 1));
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
                    long[] dims = new long[4];
                    interop.DLLArray.get_dims(ref reference, dims);
                    this.Reference = reference;
                    return dims;
                }
            }

            /// <summary>
            /// Displays an KhivaArray.
            /// </summary>
            public void Display()
            {
                interop.DLLArray.display(ref reference);
                this.Reference = reference;
            }

            /// <summary>
            /// Decreases the references count of the given array.
            /// </summary>
            public void DeleteArray()
            {
                interop.DLLArray.delete_array(ref reference);
                this.Reference = reference;
            }

            /// <summary>
            /// Gets the type of the array.
            /// </summary>
            public Dtype ArrayType
            {
                get
                {
                    int type;
                    interop.DLLArray.get_type(ref reference, out type);
                    this.Reference = reference;
                    return (Dtype)type;
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
                IntPtr result;
                interop.DLLArray.khiva_add(ref lhs.reference, ref rhs.reference, out result);
                lhs.Reference = lhs.reference;
                rhs.Reference = rhs.reference;
                return (Create(result));
            }

            /// <summary>
            /// Multiplies two arrays.
            /// </summary>
            /// <param name="lhs">Left-hand side KHIVA array for the operation.</param>
            /// <param name="rhs">Right-hand side KHIVA array for the operation.</param>
            /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
            public static KhivaArray operator *(KhivaArray lhs, KhivaArray rhs)
            {
                IntPtr result;
                interop.DLLArray.khiva_mul(ref lhs.reference, ref rhs.reference, out result);
                lhs.Reference = lhs.reference;
                rhs.Reference = rhs.reference;
                return (Create(result));
            }

            /// <summary>
            /// Substracts two arrays.
            /// </summary>
            /// <param name="lhs">Left-hand side KHIVA array for the operation.</param>
            /// <param name="rhs">Right-hand side KHIVA array for the operation.</param>
            /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
            public static KhivaArray operator -(KhivaArray lhs, KhivaArray rhs)
            {
                IntPtr result;
                interop.DLLArray.khiva_sub(ref lhs.reference, ref rhs.reference, out result);
                lhs.Reference = lhs.reference;
                rhs.Reference = rhs.reference;
                return (Create(result));
            }

            /// <summary>
            /// Divides two arrays.
            /// </summary>
            /// <param name="lhs">Left-hand side KHIVA array for the operation.</param>
            /// <param name="rhs">Right-hand side KHIVA array for the operation.</param>
            /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
            public static KhivaArray operator /(KhivaArray lhs, KhivaArray rhs)
            {
                IntPtr result;
                interop.DLLArray.khiva_div(ref lhs.reference, ref rhs.reference, out result);
                lhs.Reference = lhs.reference;
                rhs.Reference = rhs.reference;
                return (Create(result));
            }

            /// <summary>
            /// Performs the modulo operation of lhs by rhs.
            /// </summary>
            /// <param name="lhs">Left-hand side KHIVA array for the operation.</param>
            /// <param name="rhs">Right-hand side KHIVA array for the operation.</param>
            /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
            public static KhivaArray operator %(KhivaArray lhs, KhivaArray rhs)
            {
                IntPtr result;
                interop.DLLArray.khiva_mod(ref lhs.reference, ref rhs.reference, out result);
                lhs.Reference = lhs.reference;
                rhs.Reference = rhs.reference;
                return (Create(result));
            }

            /// <summary>
            /// Powers lhs with rhs.
            /// </summary>
            /// <param name="lhs">Left-hand side KHIVA array for the operation.</param>
            /// <param name="rhs">Right-hand side KHIVA array for the operation.</param>
            /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
            public static KhivaArray Pow(KhivaArray lhs, KhivaArray rhs)
            {
                IntPtr result;
                interop.DLLArray.khiva_pow(ref lhs.reference, ref rhs.reference, out result);
                lhs.Reference = lhs.reference;
                rhs.Reference = rhs.reference;
                return (Create(result));
            }

            /// <summary>
            /// Performs an AND operation (element-wise) with lhs and rhs.
            /// </summary>
            /// <param name="lhs">Left-hand side KHIVA array for the operation.</param>
            /// <param name="rhs">Right-hand side KHIVA array for the operation.</param>
            /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
            public static KhivaArray operator &(KhivaArray lhs, KhivaArray rhs)
            {
                IntPtr result;
                interop.DLLArray.khiva_bitand(ref lhs.reference, ref rhs.reference, out result);
                lhs.Reference = lhs.reference;
                rhs.Reference = rhs.reference;
                return (Create(result));
            }

            /// <summary>
            /// Performs an OR operation (element-wise) with lhs and rhs.
            /// </summary>
            /// <param name="lhs">Left-hand side KHIVA array for the operation.</param>
            /// <param name="rhs">Right-hand side KHIVA array for the operation.</param>
            /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
            public static KhivaArray operator |(KhivaArray lhs, KhivaArray rhs)
            {
                IntPtr result;
                interop.DLLArray.khiva_bitor(ref lhs.reference, ref rhs.reference, out result);
                lhs.Reference = lhs.reference;
                rhs.Reference = rhs.reference;
                return (Create(result));
            }

            /// <summary>
            /// Performs an eXclusive-OR operation (element-wise) with lhs and rhs.
            /// </summary>
            /// <param name="lhs">Left-hand side KHIVA array for the operation.</param>
            /// <param name="rhs">Right-hand side KHIVA array for the operation.</param>
            /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
            public static KhivaArray operator ^(KhivaArray lhs, KhivaArray rhs)
            {
                IntPtr result;
                interop.DLLArray.khiva_bitxor(ref lhs.reference, ref rhs.reference, out result);
                lhs.Reference = lhs.reference;
                rhs.Reference = rhs.reference;
                return (Create(result));
            }

            /// <summary>
            /// Performs a left bit shift operation (element-wise) to array as many times as specified in the parameter n.
            /// </summary>
            /// <param name="lhs">array KHIVA KhivaArray to shift.</param>
            /// <param name="shift">Number of bits to be shifted.</param>
            /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
            public static KhivaArray operator <<(KhivaArray lhs, int shift)
            {
                IntPtr result;
                interop.DLLArray.khiva_bitshiftl(ref lhs.reference, ref shift, out result);
                lhs.Reference = lhs.reference;
                return (Create(result));
            }

            /// <summary>
            /// Performs a right bit shift operation (element-wise) to array as many times as specified in the parameter n.
            /// </summary>
            /// <param name="lhs">array KHIVA KhivaArray to shift.</param>
            /// <param name="shift">Number of bits to be shifted.</param>
            /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
            public static KhivaArray operator >>(KhivaArray lhs, int shift)
            {
                IntPtr result;
                interop.DLLArray.khiva_bitshiftr(ref lhs.reference, ref shift, out result);
                lhs.Reference = lhs.reference;
                return (Create(result));
            }
            
            /// <summary>
            /// Unary minus of one array.
            /// </summary>
            /// <param name="rhs">Right-hand side KHIVA array for the operation.</param>
            /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
            public static KhivaArray operator -(KhivaArray rhs)
            {
                KhivaArray zeros;
                long[] dims = rhs.Dims;
                uint maxDim = GetMaxDim(dims);
                if (rhs.ArrayType == Dtype.c32)
                {
                    zeros = CreateZeros<Complex>(dims, maxDim);
                }
                else if (rhs.ArrayType == Dtype.c64)
                {
                    zeros = CreateZeros<Complex>(dims, maxDim, true);
                }
                else
                {
                    zeros = CreateZeros<int>(dims, maxDim);
                }
                IntPtr result;
                IntPtr zRef = zeros.Reference;
                interop.DLLArray.khiva_sub(ref zRef, ref rhs.reference, out result);
                zeros.Reference = zeros.reference;
                rhs.Reference = rhs.reference;
                return (Create(result));
            }
            

            private static uint GetMaxDim(long[] dims)
            {
                uint i = 3;
                while (i > 0)
                {
                    if (dims[i] > 1)
                    {
                        return (i + 1);
                    }
                    i--;
                }
                return (i + 1);
            }

            /// <summary>
            /// Logical NOT operation to array.
            /// </summary>
            /// <param name="lhs">KHIVA KhivaArray to negate.</param>
            /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
            public static KhivaArray operator !(KhivaArray lhs)
            {
                IntPtr result;
                interop.DLLArray.khiva_not(ref lhs.reference, out result);
                lhs.Reference = lhs.reference;
                return (Create(result));
            }

            /// <summary>
            /// Compares (element-wise) if lhs is lower than rhs.
            /// </summary>
            /// <param name="lhs">Left-hand side KHIVA array for the operation.</param>
            /// <param name="rhs">Right-hand side KHIVA array for the operation.</param>
            /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
            public static KhivaArray operator <(KhivaArray lhs, KhivaArray rhs)
            {
                IntPtr result;
                interop.DLLArray.khiva_lt(ref lhs.reference, ref rhs.reference, out result);
                lhs.Reference = lhs.reference;
                rhs.Reference = rhs.reference;
                return (Create(result));
            }

            /// <summary>
            /// Compares (element-wise) if lhs is greater than rhs.
            /// </summary>
            /// <param name="lhs">Left-hand side KHIVA array for the operation.</param>
            /// <param name="rhs">Right-hand side KHIVA array for the operation.</param>
            /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
            public static KhivaArray operator >(KhivaArray lhs, KhivaArray rhs)
            {
                IntPtr result;
                interop.DLLArray.khiva_gt(ref lhs.reference, ref rhs.reference, out result);
                lhs.Reference = lhs.reference;
                rhs.Reference = rhs.reference;
                return (Create(result));
            }

            /// <summary>
            /// Compares (element-wise) if lhs is lower or equal than rhs.
            /// </summary>
            /// <param name="lhs">Left-hand side KHIVA array for the operation.</param>
            /// <param name="rhs">Right-hand side KHIVA array for the operation.</param>
            /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
            public static KhivaArray operator <=(KhivaArray lhs, KhivaArray rhs)
            {
                IntPtr result;
                interop.DLLArray.khiva_le(ref lhs.reference, ref rhs.reference, out result);
                lhs.Reference = lhs.reference;
                rhs.Reference = rhs.reference;
                return (Create(result));
            }

            /// <summary>
            /// Compares (element-wise) if lhs is greater or equal than rhs.
            /// </summary>
            /// <param name="lhs">Left-hand side KHIVA array for the operation.</param>
            /// <param name="rhs">Right-hand side KHIVA array for the operation.</param>
            /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
            public static KhivaArray operator >=(KhivaArray lhs, KhivaArray rhs)
            {
                IntPtr result;
                interop.DLLArray.khiva_ge(ref lhs.reference, ref rhs.reference, out result);
                lhs.Reference = lhs.reference;
                rhs.Reference = rhs.reference;
                return (Create(result));
            }

            /// <summary>
            /// Compares (element-wise) if rhs is equal to rhs.
            /// </summary>
            /// <param name="lhs">Left-hand side KHIVA array for the operation.</param>
            /// <param name="rhs">Right-hand side KHIVA array for the operation.</param>
            /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
            public static KhivaArray operator ==(KhivaArray lhs, KhivaArray rhs)
            {
                IntPtr result;
                interop.DLLArray.khiva_eq(ref lhs.reference, ref rhs.reference, out result);
                lhs.Reference = lhs.reference;
                rhs.Reference = rhs.reference;
                return (Create(result));
            }

            /// <summary>
            /// Compares (element-wise) if lhs is not equal to rhs.
            /// </summary>
            /// <param name="lhs">Left-hand side KHIVA array for the operation.</param>
            /// <param name="rhs">Right-hand side KHIVA array for the operation.</param>
            /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
            public static KhivaArray operator !=(KhivaArray lhs, KhivaArray rhs)
            {
                IntPtr result;
                interop.DLLArray.khiva_ne(ref lhs.reference, ref rhs.reference, out result);
                lhs.Reference = lhs.reference;
                rhs.Reference = rhs.reference;
                return (Create(result));
            }

            /// <summary>
            /// Equals object method
            /// </summary>
            /// <param name="o">Object to compare</param>
            /// <returns></returns>
            public override bool Equals(object o)
            {
                if (o.GetType() == typeof(KhivaArray))
                {
                    KhivaArray arr = (KhivaArray)o;
                    return reference.Equals(arr.reference);
                }
                else
                {
                    return false;
                }
            }

            /// <summary>
            /// GetHashCode object method
            /// </summary>
            /// <returns>Hashcode of the KhivaArray</returns>
            public override int GetHashCode()
            {
                return reference.GetHashCode();
            }

            /// <summary>
            /// Transposes array.
            /// </summary>
            /// <param name="conjugate">If true a conjugate transposition is performed.</param>
            /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
            public KhivaArray Transpose(bool conjugate = false)
            {
                IntPtr result;
                interop.DLLArray.khiva_transpose(ref reference, ref conjugate, out result);
                this.Reference = reference;
                return (Create(result));
            }

            /// <summary>
            /// Retrieves a given column of array.
            /// </summary>
            /// <param name="index">The column to be retrieved.</param>
            /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
            public KhivaArray Col(int index)
            {
                IntPtr result;
                interop.DLLArray.khiva_col(ref reference, ref index, out result);
                this.Reference = reference;
                return (Create(result));
            }

            /// <summary>
            /// Retrieves a subset of columns of array, starting at first and finishing at last, both inclusive.
            /// </summary>
            /// <param name="first">Start of the subset of columns to be retrieved.</param>
            /// <param name="last">End of the subset of columns to be retrieved.</param>
            /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
            public KhivaArray Cols(int first, int last)
            {
                IntPtr result;
                interop.DLLArray.khiva_cols(ref reference, ref first, ref last, out result);
                this.Reference = reference;
                return (Create(result));
            }

            /// <summary>
            /// Retrieves a given row of array.
            /// </summary>
            /// <param name="index">The row to be retrieved.</param>
            /// <returns> KHIVA KhivaArray with the result of this operation.</returns>
            public KhivaArray Row(int index)
            {
                IntPtr result;
                interop.DLLArray.khiva_row(ref reference, ref index, out result);
                this.Reference = reference;
                return (Create(result));
            }

            /// <summary>
            /// Retrieves a subset of rows of array, starting at first and finishing at last, both inclusive.
            /// </summary>
            /// <param name="first">Start of the subset of rows to be retrieved.</param>
            /// <param name="last">End of the subset of rows to be retrieved.</param>
            /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
            public KhivaArray Rows(int first, int last)
            {
                IntPtr result;
                interop.DLLArray.khiva_rows(ref reference, ref first, ref last, out result);
                this.Reference = reference;
                return (Create(result));
            }

            /// <summary>
            /// Performs a matrix multiplication of lhs and rhs.
            /// </summary>
            /// <param name="rhs">Right-hand side KHIVA array for the operation.</param>
            /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
            public KhivaArray Matmul(KhivaArray rhs)
            {
                IntPtr result;
                interop.DLLArray.khiva_matmul(ref reference, ref rhs.reference, out result);
                this.Reference = reference;
                rhs.Reference = rhs.reference;
                return (Create(result));
            }

            /// <summary>
            /// Performs a deep copy of array.
            /// </summary>
            /// <returns>KHIVA KhivaArray which contains a copy of array.</returns>
            public KhivaArray Copy()
            {
                IntPtr result;
                interop.DLLArray.copy(ref reference, out result);
                this.Reference = reference;
                return (Create(result));
            }

            /// <summary>
            /// Changes the type of array.
            /// </summary>
            /// <param name="type">Target type of the output array.</param>
            /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
            public KhivaArray As(int type)
            {
                IntPtr result;
                interop.DLLArray.khiva_as(ref reference, ref type, out result);
                this.Reference = reference;
                return (Create(result));
            }
        }
    }
}