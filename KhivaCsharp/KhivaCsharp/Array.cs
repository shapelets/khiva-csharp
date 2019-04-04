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
                this.DeleteArray();
                this.Reference = IntPtr.Zero;
            }
            /// <summary>
            /// Destroy KhivaArray
            /// </summary>
            ~KhivaArray()
            {
                CleanUp();
            }

            /// <summary>
            /// Creates an KhivaArray object of 1 dimension of type float
            /// </summary>
            /// <param name="arr">Data used in order to create the array.</param>
            public KhivaArray(float[] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.f32;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.Length, 1, 1, 1 };
                interop.DLLArray.create_array(arr, ref ndims, dims, out reference, ref type);
            }

            /// <summary>
            /// Creates an KhivaArray object of 2 dimensions of type float
            /// </summary>
            /// <param name="arr">Data used in order to create the array.</param>
            public KhivaArray(float[,] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.f32;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.GetLength(1), arr.GetLength(0), 1, 1 };
                interop.DLLArray.create_array(arr, ref ndims, dims, out reference, ref type);
            }

            /// <summary>
            /// Creates an KhivaArray object of 3 dimensions of type float
            /// </summary>
            /// <param name="arr">Data used in order to create the array.</param>
            public KhivaArray(float[,,] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.f32;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.GetLength(1), arr.GetLength(0), arr.GetLength(2), 1 };
                interop.DLLArray.create_array(arr, ref ndims, dims, out reference, ref type);
            }

            /// <summary>
            /// Creates an KhivaArray object of 4 dimensions of type float
            /// </summary>
            /// <param name="arr">Data used in order to create the array.</param>
            public KhivaArray(float[,,,] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.f32;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.GetLength(1), arr.GetLength(0), arr.GetLength(2), arr.GetLength(3) };
                interop.DLLArray.create_array(arr, ref ndims, dims, out reference, ref type);
            }

            /// <summary>
            /// Creates an KhivaArray object of 1 dimension of type double
            /// </summary>
            /// <param name="arr">Data used in order to create the array.</param>
            public KhivaArray(double[] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.f64;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.Length, 1, 1, 1 };
                interop.DLLArray.create_array(arr, ref ndims, dims, out reference, ref type);
            }

            /// <summary>
            /// Creates an KhivaArray object of 2 dimensions of type double
            /// </summary>
            /// <param name="arr">Data used in order to create the array.</param>
            public KhivaArray(double[,] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.f64;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.GetLength(1), arr.GetLength(0), 1, 1 };
                interop.DLLArray.create_array(arr, ref ndims, dims, out reference, ref type);
            }

            /// <summary>
            /// Creates an KhivaArray object of 3 dimensions of type double
            /// </summary>
            /// <param name="arr">Data used in order to create the array.</param>
            public KhivaArray(double[,,] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.f64;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.GetLength(1), arr.GetLength(0), arr.GetLength(2), 1 };
                interop.DLLArray.create_array(arr, ref ndims, dims, out reference, ref type);
            }

            /// <summary>
            /// Creates an KhivaArray object of 4 dimensions of type double
            /// </summary>
            /// <param name="arr">Data used in order to create the array.</param>
            public KhivaArray(double[,,,] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.f64;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.GetLength(1), arr.GetLength(0), arr.GetLength(2), arr.GetLength(3) };
                interop.DLLArray.create_array(arr, ref ndims, dims, out reference, ref type);
            }

            public KhivaArray(Complex[] arr, bool doublePrecision)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.Length, 1, 1, 1 };
                double[] complexArrDouble = null;
                float[] complexArrFloat = null;
                if (!doublePrecision)
                {
                    type = (int)Dtype.c32;
                    complexArrFloat = new float[arr.Length * 2];
                    Flatten1D<float>(complexArrFloat, arr);
                    interop.DLLArray.create_array(complexArrFloat, ref ndims, dims, out reference, ref type);
                }
                else
                {
                    type = (int)Dtype.c64;
                    complexArrDouble = new double[arr.Length * 2];
                    Flatten1D<double>(complexArrDouble, arr);
                    interop.DLLArray.create_array(complexArrDouble, ref ndims, dims, out reference, ref type);
                }
            }

            private void Flatten1D<T>(T[] complexArr, Complex[] arr)
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    complexArr[2 * i] = (T)Convert.ChangeType(arr[i].Real, typeof(T));
                    complexArr[2 * i + 1] = (T)Convert.ChangeType(arr[i].Imaginary, typeof(T));
                }
            }

            public KhivaArray(Complex[,] arr, bool doublePrecision)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.GetLength(1), arr.GetLength(0), 1, 1 };
                double[] complexArrDouble = null;
                float[] complexArrFloat = null;
                if (!doublePrecision)
                {
                    type = (int)Dtype.c32;
                    complexArrFloat = new float[arr.Length * 2];
                    Flatten2D<float>(complexArrFloat, arr);
                    interop.DLLArray.create_array(complexArrFloat, ref ndims, dims, out reference, ref type);
                }
                else
                {
                    type = (int)Dtype.c64;
                    complexArrDouble = new double[arr.Length * 2];
                    Flatten2D<double>(complexArrDouble, arr);
                    interop.DLLArray.create_array(complexArrDouble, ref ndims, dims, out reference, ref type);
                }
            }

            private void Flatten2D<T>(T[] complexArr, Complex[,] arr)
            {
                for (int i = 0; i < arr.GetLength(0); i++)
                {
                    for (int j = 0; j < arr.GetLength(1); j++)
                    {
                        complexArr[2 * i * arr.GetLength(1) + 2 * j] = (T)Convert.ChangeType(arr[i, j].Real, typeof(T));
                        complexArr[2 * i * arr.GetLength(1) + 2 * j + 1] = (T)Convert.ChangeType(arr[i, j].Imaginary, typeof(T));
                    }
                }
            }

            public KhivaArray(Complex[,,] arr, bool doublePrecision)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.GetLength(1), arr.GetLength(0), arr.GetLength(2), 1 };
                double[] complexArrDouble = null;
                float[] complexArrFloat = null;
                if (!doublePrecision)
                {
                    type = (int)Dtype.c32;
                    complexArrFloat = new float[arr.Length * 2];
                    Flatten3D<float>(complexArrFloat, arr);
                    interop.DLLArray.create_array(complexArrFloat, ref ndims, dims, out reference, ref type);
                }
                else
                {
                    type = (int)Dtype.c64;
                    complexArrDouble = new double[arr.Length * 2];
                    Flatten3D<double>(complexArrDouble, arr);
                    interop.DLLArray.create_array(complexArrDouble, ref ndims, dims, out reference, ref type);
                }
            }

            private void Flatten3D<T>(T[] complexArr, Complex[,,] arr)
            {
                for (int i = 0; i < arr.GetLength(0); i++)
                {
                    for (int j = 0; j < arr.GetLength(1); j++)
                    {
                        for (int k = 0; k < arr.GetLength(2); k++)
                        {
                            complexArr[2 * i * (arr.GetLength(1) * arr.GetLength(2)) + 2 * j * arr.GetLength(2) + 2 * k] = (T)Convert.ChangeType(arr[i, j, k].Real, typeof(T));
                            complexArr[2 * i * (arr.GetLength(1) * arr.GetLength(2)) + 2 * j * arr.GetLength(2) + 2 * k + 1] = (T)Convert.ChangeType(arr[i, j, k].Imaginary, typeof(T));
                        }
                    }
                }
            }

            public KhivaArray(Complex[,,,] arr, bool doublePrecision)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.GetLength(1), arr.GetLength(0), arr.GetLength(2), arr.GetLength(3) };
                double[] complexArrDouble = null;
                float[] complexArrFloat = null;
                if (!doublePrecision)
                {
                    type = (int)Dtype.c32;
                    complexArrFloat = new float[arr.Length * 2];
                    Flatten4D<float>(complexArrFloat, arr);
                    interop.DLLArray.create_array(complexArrFloat, ref ndims, dims, out reference, ref type);
                }
                else
                {
                    type = (int)Dtype.c64;
                    complexArrDouble = new double[arr.Length * 2];
                    Flatten4D<double>(complexArrDouble, arr);
                    interop.DLLArray.create_array(complexArrDouble, ref ndims, dims, out reference, ref type);
                }
            }

            private void Flatten4D<T>(T[] complexArr, Complex[,,,] arr)
            {
                for (int i = 0; i < arr.GetLength(0); i++)
                {
                    for (int j = 0; j < arr.GetLength(1); j++)
                    {
                        for (int k = 0; k < arr.GetLength(2); k++)
                        {
                            for (int z = 0; z < arr.GetLength(3); z++)
                            {
                                complexArr[2 * i * (arr.GetLength(1) * arr.GetLength(2) * arr.GetLength(3)) + 2 * j * (arr.GetLength(2) * arr.GetLength(3)) + 2 * k * arr.GetLength(3) + 2 * z] = (T)Convert.ChangeType(arr[i, j, k, z].Real, typeof(T));
                                complexArr[2 * i * (arr.GetLength(1) * arr.GetLength(2) * arr.GetLength(3)) + 2 * j * (arr.GetLength(2) * arr.GetLength(3)) + 2 * k * arr.GetLength(3) + 2 * z + 1] = (T)Convert.ChangeType(arr[i, j, k, z].Imaginary, typeof(T));
                            }
                        }
                    }
                }
            }

            public KhivaArray(bool[] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.b8;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.Length, 1, 1, 1 };
                byte[] byteArr = new byte[arr.GetLength(0)];
                BoolToByte1D(byteArr, arr);
                interop.DLLArray.create_array(byteArr, ref ndims, dims, out reference, ref type);
            }

            private void BoolToByte1D(byte[] byteArr, bool[] arr)
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    byteArr[i] = Convert.ToByte(arr[i]);
                }
            }

            public KhivaArray(bool[,] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.b8;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.GetLength(1), arr.GetLength(0), 1, 1 };
                byte[,] byteArr = new byte[arr.GetLength(0), arr.GetLength(1)];
                BoolToByte2D(byteArr, arr);
                interop.DLLArray.create_array(byteArr, ref ndims, dims, out reference, ref type);
            }

            private void BoolToByte2D(byte[,] byteArr, bool[,] arr)
            {
                for (int i = 0; i < arr.GetLength(0); i++)
                {
                    for (int j = 0; j < arr.GetLength(1); j++)
                    {
                        byteArr[i, j] = Convert.ToByte(arr[i, j]);
                    }
                }
            }

            public KhivaArray(bool[,,] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.b8;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.GetLength(1), arr.GetLength(0), arr.GetLength(2), 1 };
                byte[,,] byteArr = new byte[arr.GetLength(0), arr.GetLength(1), arr.GetLength(2)];
                BoolToByte3D(byteArr, arr);
                interop.DLLArray.create_array(byteArr, ref ndims, dims, out reference, ref type);
            }

            private void BoolToByte3D(byte[,,] byteArr, bool[,,] arr)
            {
                for (int i = 0; i < arr.GetLength(0); i++)
                {
                    for (int j = 0; j < arr.GetLength(1); j++)
                    {
                        for (int k = 0; k < arr.GetLength(2); k++)
                        {
                            byteArr[i, j, k] = Convert.ToByte(arr[i, j, k]);
                        }
                    }
                }
            }

            public KhivaArray(bool[,,,] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.b8;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.GetLength(1), arr.GetLength(0), arr.GetLength(2), arr.GetLength(3) };
                byte[,,,] byteArr = new byte[arr.GetLength(0), arr.GetLength(1), arr.GetLength(2), arr.GetLength(3)];
                BoolToByte4D(byteArr, arr);
                interop.DLLArray.create_array(byteArr, ref ndims, dims, out reference, ref type);
            }

            private void BoolToByte4D(byte[,,,] byteArr, bool[,,,] arr)
            {
                for (int i = 0; i < arr.GetLength(0); i++)
                {
                    for (int j = 0; j < arr.GetLength(1); j++)
                    {
                        for (int k = 0; k < arr.GetLength(2); k++)
                        {
                            for (int z = 0; z < arr.GetLength(3); z++)
                            {
                                byteArr[i, j, k, z] = Convert.ToByte(arr[i, j, k, z]);
                            }
                        }
                    }
                }
            }

            public KhivaArray(int[] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.s32;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.Length, 1, 1, 1 };
                interop.DLLArray.create_array(arr, ref ndims, dims, out reference, ref type);
            }

            public KhivaArray(int[,] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.s32;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.GetLength(1), arr.GetLength(0), 1, 1 };
                interop.DLLArray.create_array(arr, ref ndims, dims, out reference, ref type);
            }

            public KhivaArray(int[,,] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.s32;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.GetLength(1), arr.GetLength(0), arr.GetLength(2), 1 };
                interop.DLLArray.create_array(arr, ref ndims, dims, out reference, ref type);
            }

            public KhivaArray(int[,,,] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.s32;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.GetLength(1), arr.GetLength(0), arr.GetLength(2), arr.GetLength(3) };
                interop.DLLArray.create_array(arr, ref ndims, dims, out reference, ref type);
            }

            public KhivaArray(uint[] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.u32;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.Length, 1, 1, 1 };
                interop.DLLArray.create_array(arr, ref ndims, dims, out reference, ref type);
            }

            public KhivaArray(uint[,] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.u32;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.GetLength(1), arr.GetLength(0), 1, 1 };
                interop.DLLArray.create_array(arr, ref ndims, dims, out reference, ref type);
            }

            public KhivaArray(uint[,,] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.u32;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.GetLength(1), arr.GetLength(0), arr.GetLength(2), 1 };
                interop.DLLArray.create_array(arr, ref ndims, dims, out reference, ref type);
            }

            public KhivaArray(uint[,,,] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.u32;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.GetLength(1), arr.GetLength(0), arr.GetLength(2), arr.GetLength(3) };
                interop.DLLArray.create_array(arr, ref ndims, dims, out reference, ref type);
            }

            public KhivaArray(byte[] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.u8;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.Length, 1, 1, 1 };
                interop.DLLArray.create_array(arr, ref ndims, dims, out reference, ref type);
            }

            public KhivaArray(byte[,] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.u8;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.GetLength(1), arr.GetLength(0), 1, 1 };
                interop.DLLArray.create_array(arr, ref ndims, dims, out reference, ref type);
            }

            public KhivaArray(byte[,,] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.u8;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.GetLength(1), arr.GetLength(0), arr.GetLength(2), 1 };
                interop.DLLArray.create_array(arr, ref ndims, dims, out reference, ref type);
            }

            public KhivaArray(byte[,,,] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.u8;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.GetLength(1), arr.GetLength(0), arr.GetLength(2), arr.GetLength(3) };
                interop.DLLArray.create_array(arr, ref ndims, dims, out reference, ref type);
            }

            public KhivaArray(long[] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.s64;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.Length, 1, 1, 1 };
                interop.DLLArray.create_array(arr, ref ndims, dims, out reference, ref type);
            }

            public KhivaArray(long[,] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.s64;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.GetLength(1), arr.GetLength(0), 1, 1 };
                interop.DLLArray.create_array(arr, ref ndims, dims, out reference, ref type);
            }

            public KhivaArray(long[,,] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.s64;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.GetLength(1), arr.GetLength(0), arr.GetLength(2), 1 };
                interop.DLLArray.create_array(arr, ref ndims, dims, out reference, ref type);
            }

            public KhivaArray(long[,,,] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.s64;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.GetLength(1), arr.GetLength(0), arr.GetLength(2), arr.GetLength(3) };
                interop.DLLArray.create_array(arr, ref ndims, dims, out reference, ref type);
            }

            public KhivaArray(ulong[] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.u64;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.Length, 1, 1, 1 };
                interop.DLLArray.create_array(arr, ref ndims, dims, out reference, ref type);
            }

            public KhivaArray(ulong[,] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.u64;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.GetLength(1), arr.GetLength(0), 1, 1 };
                interop.DLLArray.create_array(arr, ref ndims, dims, out reference, ref type);
            }

            public KhivaArray(ulong[,,] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.u64;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.GetLength(1), arr.GetLength(0), arr.GetLength(2), 1 };
                interop.DLLArray.create_array(arr, ref ndims, dims, out reference, ref type);
            }

            public KhivaArray(ulong[,,,] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.u64;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.GetLength(1), arr.GetLength(0), arr.GetLength(2), arr.GetLength(3) };
                interop.DLLArray.create_array(arr, ref ndims, dims, out reference, ref type);
            }

            public KhivaArray(short[] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.s16;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.Length, 1, 1, 1 };
                interop.DLLArray.create_array(arr, ref ndims, dims, out reference, ref type);
            }

            public KhivaArray(short[,] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.s16;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.GetLength(1), arr.GetLength(0), 1, 1 };
                interop.DLLArray.create_array(arr, ref ndims, dims, out reference, ref type);
            }

            public KhivaArray(short[,,] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.s16;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.GetLength(1), arr.GetLength(0), arr.GetLength(2), 1 };
                interop.DLLArray.create_array(arr, ref ndims, dims, out reference, ref type);
            }

            public KhivaArray(short[,,,] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.s16;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.GetLength(1), arr.GetLength(0), arr.GetLength(2), arr.GetLength(3) };
                interop.DLLArray.create_array(arr, ref ndims, dims, out reference, ref type);
            }

            public KhivaArray(ushort[] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.u16;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.Length, 1, 1, 1 };
                interop.DLLArray.create_array(arr, ref ndims, dims, out reference, ref type);
            }

            public KhivaArray(ushort[,] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.u16;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.GetLength(1), arr.GetLength(0), 1, 1 };
                interop.DLLArray.create_array(arr, ref ndims, dims, out reference, ref type);
            }

            public KhivaArray(ushort[,,] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.u16;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.GetLength(1), arr.GetLength(0), arr.GetLength(2), 1 };
                interop.DLLArray.create_array(arr, ref ndims, dims, out reference, ref type);
            }

            public KhivaArray(ushort[,,,] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.u16;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.GetLength(1), arr.GetLength(0), arr.GetLength(2), arr.GetLength(3) };
                interop.DLLArray.create_array(arr, ref ndims, dims, out reference, ref type);
            }

            public KhivaArray(IntPtr reference)
            {
                this.reference = reference;
            }

            public KhivaArray(KhivaArray other)
            {
                this.reference = other.Reference;
            }

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

            /// <summary>
            /// Gets the data stored in a 1D array.
            /// </summary>
            /// <typeparam name="T">The data type to be returned.</typeparam>
            /// <returns>The data to an array of its type.</returns>
            public T[] GetData1D<T>()
            {
                if (!CheckType(typeof(T)))
                {
                    throw new Exception("Type does mismatch");
                }
                long[] dims = Dims;
                CheckNdims(dims, 1);
                T[] data = new T[dims[0]];
                GCHandle gchArr = new GCHandle();
                try
                {
                    if (typeof(T) == typeof(bool))
                    {
                        byte[] byteData = new byte[dims[0]];
                        gchArr = GCHandle.Alloc(byteData, GCHandleType.Pinned);
                        IntPtr dataPtr = gchArr.AddrOfPinnedObject();
                        interop.DLLArray.get_data(ref reference, dataPtr);
                        this.Reference = reference;
                        ByteToGeneric1D<T>(data, byteData);
                    }
                    else if (typeof(T) == typeof(Complex))
                    {
                        if (ArrayType == Dtype.c64)
                        {
                            double[] complexData = new double[dims[0] * 2];
                            gchArr = GCHandle.Alloc(complexData, GCHandleType.Pinned);
                            IntPtr dataPtr = gchArr.AddrOfPinnedObject();
                            interop.DLLArray.get_data(ref reference, dataPtr);
                            this.Reference = reference;
                            ToGenericComplex1D<T, double>(data, complexData);
                        }
                        else
                        {
                            float[] complexData = new float[dims[0] * 2];
                            gchArr = GCHandle.Alloc(complexData, GCHandleType.Pinned);
                            IntPtr dataPtr = gchArr.AddrOfPinnedObject();
                            interop.DLLArray.get_data(ref reference, dataPtr);
                            this.Reference = reference;
                            ToGenericComplex1D<T, float>(data, complexData);
                        }
                    }
                    else
                    {
                        gchArr = GCHandle.Alloc(data, GCHandleType.Pinned);
                        IntPtr dataPtr = gchArr.AddrOfPinnedObject();
                        interop.DLLArray.get_data(ref reference, dataPtr);
                        this.Reference = reference;
                    }
                }
                finally
                {
                    GCHandle.Alloc(gchArr.Target, GCHandleType.Weak);
                }
                return data;
            }

            private void ByteToGeneric1D<T>(T[] genericArr, byte[] arr)
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    genericArr[i] = (T)Convert.ChangeType(arr[i], typeof(T));
                }
            }

            private void ToGenericComplex1D<T, R>(T[] genericArr, R[] arr)
            {
                for (int i = 0; i < arr.Length / 2; i++)
                {
                    genericArr[i] = (T)Convert.ChangeType(new Complex(Convert.ToDouble(arr[2 * i]), Convert.ToDouble(arr[2 * i + 1])), typeof(T));
                }
            }

            /// <summary>
            /// Gets the data stored in a 2D array.
            /// </summary>
            /// <typeparam name="T">The data type to be returned.</typeparam>
            /// <returns>The data to an array of its type.</returns>
            public T[,] GetData2D<T>()
            {
                if (!CheckType(typeof(T)))
                {
                    throw new Exception("Type does mismatch");
                }
                long[] dims = Dims;
                CheckNdims(dims, 2);
                T[,] data = new T[dims[1], dims[0]];
                GCHandle gchArr = new GCHandle();
                try
                {
                    if (typeof(T) == typeof(bool))
                    {
                        byte[,] byteData = new byte[dims[1], dims[0]];
                        gchArr = GCHandle.Alloc(byteData, GCHandleType.Pinned);
                        IntPtr dataPtr = gchArr.AddrOfPinnedObject();
                        interop.DLLArray.get_data(ref reference, dataPtr);
                        this.Reference = reference;
                        ByteToGeneric2D<T>(data, byteData);
                    }
                    else if (typeof(T) == typeof(Complex))
                    {
                        if (ArrayType == Dtype.c64)
                        {
                            double[] complexData = new double[dims[0] * dims[1] * 2];
                            gchArr = GCHandle.Alloc(complexData, GCHandleType.Pinned);
                            IntPtr dataPtr = gchArr.AddrOfPinnedObject();
                            interop.DLLArray.get_data(ref reference, dataPtr);
                            this.Reference = reference;
                            ToGenericComplex2D<T, double>(data, complexData);
                        }
                        else
                        {
                            float[] complexData = new float[dims[0] * dims[1] * 2];
                            gchArr = GCHandle.Alloc(complexData, GCHandleType.Pinned);
                            IntPtr dataPtr = gchArr.AddrOfPinnedObject();
                            interop.DLLArray.get_data(ref reference, dataPtr);
                            this.Reference = reference;
                            ToGenericComplex2D<T, float>(data, complexData);
                        }
                    }
                    else
                    {
                        gchArr = GCHandle.Alloc(data, GCHandleType.Pinned);
                        IntPtr dataPtr = gchArr.AddrOfPinnedObject();
                        interop.DLLArray.get_data(ref reference, dataPtr);
                        this.Reference = reference;
                    }
                }
                finally
                {
                    GCHandle.Alloc(gchArr.Target, GCHandleType.Weak);
                }
                
                return data;
            }

            private void ByteToGeneric2D<T>(T[,] genericArr, byte[,] arr)
            {
                for (int i = 0; i < arr.GetLength(0); i++)
                {
                    for (int j = 0; j < arr.GetLength(1); j++)
                    {
                        genericArr[i, j] = (T)Convert.ChangeType(arr[i, j], typeof(T));
                    }
                }
            }

            private void ToGenericComplex2D<T, R>(T[,] genericArr, R[] arr)
            {
                for (int i = 0; i < genericArr.GetLength(0); i++)
                {
                    for (int j = 0; j < genericArr.GetLength(1); j++)
                    {
                        genericArr[i, j] = (T)Convert.ChangeType(new Complex(Convert.ToDouble(arr[2 * i * genericArr.GetLength(1) + 2 * j]), Convert.ToDouble(arr[2 * i * genericArr.GetLength(1) + 2 * j + 1])), typeof(T));
                    }
                }
            }

            /// <summary>
            /// Gets the data stored in a 3D array.
            /// </summary>
            /// <typeparam name="T">The data type to be returned.</typeparam>
            /// <returns>The data to an array of its type.</returns>
            public T[,,] GetData3D<T>()
            {
                if (!CheckType(typeof(T)))
                {
                    throw new Exception("Type does mismatch");
                }
                long[] dims = Dims;
                CheckNdims(dims, 3);
                T[,,] data = new T[dims[1], dims[0], dims[2]];
                GCHandle gchArr = new GCHandle();
                try
                {
                    if (typeof(T) == typeof(bool))
                    {
                        byte[,,] byteData = new byte[dims[1], dims[0], dims[2]];
                        gchArr = GCHandle.Alloc(byteData, GCHandleType.Pinned);
                        IntPtr dataPtr = gchArr.AddrOfPinnedObject();
                        interop.DLLArray.get_data(ref reference, dataPtr);
                        this.Reference = reference;
                        ByteToGeneric3D<T>(data, byteData);
                    }
                    else if (typeof(T) == typeof(Complex))
                    {
                        if (ArrayType == Dtype.c64)
                        {
                            double[] complexData = new double[dims[0] * dims[1] * dims[2] * 2];
                            gchArr = GCHandle.Alloc(complexData, GCHandleType.Pinned);
                            IntPtr dataPtr = gchArr.AddrOfPinnedObject();
                            interop.DLLArray.get_data(ref reference, dataPtr);
                            this.Reference = reference;
                            ToGenericComplex3D<T, double>(data, complexData);
                        }
                        else
                        {
                            float[] complexData = new float[dims[0] * dims[1] * dims[2] * 2];
                            gchArr = GCHandle.Alloc(complexData, GCHandleType.Pinned);
                            IntPtr dataPtr = gchArr.AddrOfPinnedObject();
                            interop.DLLArray.get_data(ref reference, dataPtr);
                            this.Reference = reference;
                            ToGenericComplex3D<T, float>(data, complexData);
                        }
                    }
                    else
                    {
                        gchArr = GCHandle.Alloc(data, GCHandleType.Pinned);
                        IntPtr dataPtr = gchArr.AddrOfPinnedObject();
                        interop.DLLArray.get_data(ref reference, dataPtr);
                        this.Reference = reference;
                    }
                }
                finally
                {
                    GCHandle.Alloc(gchArr.Target, GCHandleType.Weak);
                }
                return data;
            }

            private void ByteToGeneric3D<T>(T[,,] genericArr, byte[,,] arr)
            {
                for (int i = 0; i < arr.GetLength(0); i++)
                {
                    for (int j = 0; j < arr.GetLength(1); j++)
                    {
                        for (int k = 0; k < arr.GetLength(2); k++)
                        {
                            genericArr[i, j, k] = (T)Convert.ChangeType(arr[i, j, k], typeof(T));
                        }
                    }
                }
            }

            private void ToGenericComplex3D<T, R>(T[,,] genericArr, R[] arr)
            {
                for (int i = 0; i < genericArr.GetLength(0); i++)
                {
                    for (int j = 0; j < genericArr.GetLength(1); j++)
                    {
                        for (int k = 0; k < genericArr.GetLength(2); k++)
                        {
                            genericArr[i, j, k] = (T)Convert.ChangeType(new Complex(Convert.ToDouble(arr[2 * i * (genericArr.GetLength(1) + genericArr.GetLength(2)) + 2 * j * genericArr.GetLength(2) + 2 * k]), Convert.ToDouble(arr[2 * i * (genericArr.GetLength(1) + genericArr.GetLength(2)) + 2 * j * genericArr.GetLength(2) + 2 * k + 1])), typeof(T));
                        }
                    }
                }
            }

            /// <summary>
            /// Gets the data stored in a 4D array.
            /// </summary>
            /// <typeparam name="T">The data type to be returned.</typeparam>
            /// <returns>The data to an array of its type.</returns>
            public T[,,,] GetData4D<T>()
            {
                if (!CheckType(typeof(T)))
                {
                    throw new Exception("Type does mismatch");
                }
                long[] dims = Dims;
                T[,,,] data = new T[dims[1], dims[0], dims[2], dims[3]];
                GCHandle gchArr = new GCHandle();
                try
                {
                    if (typeof(T) == typeof(bool))
                    {
                        byte[,,,] byteData = new byte[dims[1], dims[0], dims[2], dims[3]];
                        gchArr = GCHandle.Alloc(byteData, GCHandleType.Pinned);
                        IntPtr dataPtr = gchArr.AddrOfPinnedObject();
                        interop.DLLArray.get_data(ref reference, dataPtr);
                        this.Reference = reference;
                        ByteToGeneric4D<T>(data, byteData);
                    }
                    else if (typeof(T) == typeof(Complex))
                    {
                        if (ArrayType == Dtype.c64)
                        {
                            double[] complexData = new double[dims[0] * dims[1] * dims[2] * dims[3] * 2];
                            gchArr = GCHandle.Alloc(complexData, GCHandleType.Pinned);
                            IntPtr dataPtr = gchArr.AddrOfPinnedObject();
                            interop.DLLArray.get_data(ref reference, dataPtr);
                            this.Reference = reference;
                            ToGenericComplex4D<T, double>(data, complexData);
                        }
                        else
                        {
                            float[] complexData = new float[dims[0] * dims[1] * dims[2] * dims[3] * 2];
                            gchArr = GCHandle.Alloc(complexData, GCHandleType.Pinned);
                            IntPtr dataPtr = gchArr.AddrOfPinnedObject();
                            interop.DLLArray.get_data(ref reference, dataPtr);
                            this.Reference = reference;
                            ToGenericComplex4D<T, float>(data, complexData);
                        }
                    }
                    else
                    {
                        gchArr = GCHandle.Alloc(data, GCHandleType.Pinned);
                        IntPtr dataPtr = gchArr.AddrOfPinnedObject();
                        interop.DLLArray.get_data(ref reference, dataPtr);
                        this.Reference = reference;
                    }
                }
                finally
                {
                    GCHandle.Alloc(gchArr.Target, GCHandleType.Weak);
                }
                return data;
            }

            private void ByteToGeneric4D<T>(T[,,,] genericArr, byte[,,,] arr)
            {
                for (int i = 0; i < arr.GetLength(0); i++)
                {
                    for (int j = 0; j < arr.GetLength(1); j++)
                    {
                        for (int k = 0; k < arr.GetLength(2); k++)
                        {
                            for (int z = 0; z < arr.GetLength(3); z++)
                            {
                                genericArr[i, j, k, z] = (T)Convert.ChangeType(arr[i, j, k, z], typeof(T));
                            }
                        }
                    }
                }
            }

            private void ToGenericComplex4D<T, R>(T[,,,] genericArr, R[] arr)
            {
                for (int i = 0; i < genericArr.GetLength(0); i++)
                {
                    for (int j = 0; j < genericArr.GetLength(1); j++)
                    {
                        for (int k = 0; k < genericArr.GetLength(2); k++)
                        {
                            for (int z = 0; z < genericArr.GetLength(3); z++)
                            {
                                genericArr[i, j, k, z] = (T)Convert.ChangeType(new Complex(Convert.ToDouble(arr[2 * i * (genericArr.GetLength(1) + genericArr.GetLength(2) + genericArr.GetLength(3)) + 2 * j * (genericArr.GetLength(2) + genericArr.GetLength(3)) + 2 * k * genericArr.GetLength(3) + 2 * z]), Convert.ToDouble(arr[2 * i * (genericArr.GetLength(1) + genericArr.GetLength(2) + genericArr.GetLength(3)) + 2 * j * (genericArr.GetLength(2) + genericArr.GetLength(3)) + 2 * k * genericArr.GetLength(3) + 2 * z + 1])), typeof(T));
                            }
                        }
                    }
                }
            }

            private bool CheckType(Type type)
            {
                switch (ArrayType) {
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
                        throw new Exception("The array must be have at most " + maxNdims + " dimensions for using this method and have " + (i + 1));
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
                    interop.DLLArray.get_type(ref reference, out int type);
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
                interop.DLLArray.khiva_add(ref lhs.reference, ref rhs.reference, out IntPtr result);
                lhs.Reference = lhs.reference;
                rhs.Reference = rhs.reference;
                return new KhivaArray(result);
            }

            /// <summary>
            /// Multiplies two arrays.
            /// </summary>
            /// <param name="lhs">Left-hand side KHIVA array for the operation.</param>
            /// <param name="rhs">Right-hand side KHIVA array for the operation.</param>
            /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
            public static KhivaArray operator *(KhivaArray lhs, KhivaArray rhs)
            {
                interop.DLLArray.khiva_mul(ref lhs.reference, ref rhs.reference, out IntPtr result);
                lhs.Reference = lhs.reference;
                rhs.Reference = rhs.reference;
                return (new KhivaArray(result));
            }

            /// <summary>
            /// Substracts two arrays.
            /// </summary>
            /// <param name="lhs">Left-hand side KHIVA array for the operation.</param>
            /// <param name="rhs">Right-hand side KHIVA array for the operation.</param>
            /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
            public static KhivaArray operator -(KhivaArray lhs, KhivaArray rhs)
            {
                interop.DLLArray.khiva_sub(ref lhs.reference, ref rhs.reference, out IntPtr result);
                lhs.Reference = lhs.reference;
                rhs.Reference = rhs.reference;
                return (new KhivaArray(result));
            }

            /// <summary>
            /// Divides two arrays.
            /// </summary>
            /// <param name="lhs">Left-hand side KHIVA array for the operation.</param>
            /// <param name="rhs">Right-hand side KHIVA array for the operation.</param>
            /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
            public static KhivaArray operator /(KhivaArray lhs, KhivaArray rhs)
            {
                interop.DLLArray.khiva_div(ref lhs.reference, ref rhs.reference, out IntPtr result);
                lhs.Reference = lhs.reference;
                rhs.Reference = rhs.reference;
                return (new KhivaArray(result));
            }

            /// <summary>
            /// Performs the modulo operation of lhs by rhs.
            /// </summary>
            /// <param name="lhs">Left-hand side KHIVA array for the operation.</param>
            /// <param name="rhs">Right-hand side KHIVA array for the operation.</param>
            /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
            public static KhivaArray operator %(KhivaArray lhs, KhivaArray rhs)
            {
                interop.DLLArray.khiva_mod(ref lhs.reference, ref rhs.reference, out IntPtr result);
                lhs.Reference = lhs.reference;
                rhs.Reference = rhs.reference;
                return (new KhivaArray(result));
            }

            /// <summary>
            /// Powers lhs with rhs.
            /// </summary>
            /// <param name="lhs">Left-hand side KHIVA array for the operation.</param>
            /// <param name="rhs">Right-hand side KHIVA array for the operation.</param>
            /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
            public static KhivaArray Pow(KhivaArray lhs, KhivaArray rhs)
            {
                interop.DLLArray.khiva_pow(ref lhs.reference, ref rhs.reference, out IntPtr result);
                lhs.Reference = lhs.reference;
                rhs.Reference = rhs.reference;
                return (new KhivaArray(result));
            }

            /// <summary>
            /// Performs an AND operation (element-wise) with lhs and rhs.
            /// </summary>
            /// <param name="lhs">Left-hand side KHIVA array for the operation.</param>
            /// <param name="rhs">Right-hand side KHIVA array for the operation.</param>
            /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
            public static KhivaArray operator &(KhivaArray lhs, KhivaArray rhs)
            {
                interop.DLLArray.khiva_bitand(ref lhs.reference, ref rhs.reference, out IntPtr result);
                lhs.Reference = lhs.reference;
                rhs.Reference = rhs.reference;
                return (new KhivaArray(result));
            }

            /// <summary>
            /// Performs an OR operation (element-wise) with lhs and rhs.
            /// </summary>
            /// <param name="lhs">Left-hand side KHIVA array for the operation.</param>
            /// <param name="rhs">Right-hand side KHIVA array for the operation.</param>
            /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
            public static KhivaArray operator |(KhivaArray lhs, KhivaArray rhs)
            {
                interop.DLLArray.khiva_bitor(ref lhs.reference, ref rhs.reference, out IntPtr result);
                lhs.Reference = lhs.reference;
                rhs.Reference = rhs.reference;
                return (new KhivaArray(result));
            }

            /// <summary>
            /// Performs an eXclusive-OR operation (element-wise) with lhs and rhs.
            /// </summary>
            /// <param name="lhs">Left-hand side KHIVA array for the operation.</param>
            /// <param name="rhs">Right-hand side KHIVA array for the operation.</param>
            /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
            public static KhivaArray operator ^(KhivaArray lhs, KhivaArray rhs)
            {
                interop.DLLArray.khiva_bitxor(ref lhs.reference, ref rhs.reference, out IntPtr result);
                lhs.Reference = lhs.reference;
                rhs.Reference = rhs.reference;
                return (new KhivaArray(result));
            }

            /// <summary>
            /// Performs a left bit shift operation (element-wise) to array as many times as specified in the parameter n.
            /// </summary>
            /// <param name="lhs">array KHIVA KhivaArray to shift.</param>
            /// <param name="shift">Number of bits to be shifted.</param>
            /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
            public static KhivaArray operator <<(KhivaArray lhs, int shift)
            {
                interop.DLLArray.khiva_bitshiftl(ref lhs.reference, ref shift, out IntPtr result);
                lhs.Reference = lhs.reference;
                return (new KhivaArray(result));
            }

            /// <summary>
            /// Performs a right bit shift operation (element-wise) to array as many times as specified in the parameter n.
            /// </summary>
            /// <param name="lhs">array KHIVA KhivaArray to shift.</param>
            /// <param name="shift">Number of bits to be shifted.</param>
            /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
            public static KhivaArray operator >>(KhivaArray lhs, int shift)
            {
                interop.DLLArray.khiva_bitshiftr(ref lhs.reference, ref shift, out IntPtr result);
                lhs.Reference = lhs.reference;
                return (new KhivaArray(result));
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
                if (rhs.ArrayType == Dtype.c32 | rhs.ArrayType == Dtype.c64)
                {
                    if (maxDim == 1)
                    {
                        Complex[] tss = new Complex[dims[0]];
                        if (rhs.ArrayType == Dtype.c32)
                        {
                            zeros = new KhivaArray(tss, false);
                        }
                        else
                        {
                            zeros = new KhivaArray(tss, true);
                        }
                    }
                    else if (maxDim == 2)
                    {
                        Complex[,] tss = new Complex[dims[0], dims[1]];
                        if (rhs.ArrayType == Dtype.c32)
                        {
                            zeros = new KhivaArray(tss, false);
                        }
                        else
                        {
                            zeros = new KhivaArray(tss, true);
                        }
                    }
                    else if (maxDim == 3)
                    {
                        Complex[,,] tss = new Complex[dims[0], dims[1], dims[2]];
                        if (rhs.ArrayType == Dtype.c32)
                        {
                            zeros = new KhivaArray(tss, false);
                        }
                        else
                        {
                            zeros = new KhivaArray(tss, true);
                        }
                    }
                    else
                    {
                        Complex[,,,] tss = new Complex[dims[0], dims[1], dims[2], dims[3]];
                        if (rhs.ArrayType == Dtype.c32)
                        {
                            zeros = new KhivaArray(tss, false);
                        }
                        else
                        {
                            zeros = new KhivaArray(tss, true);
                        }
                    }
                }
                else
                {
                    if (maxDim == 1)
                    {
                        int[] tss = new int[dims[0]];
                        zeros = new KhivaArray(tss);
                    } else if (maxDim == 2)
                    {
                        int[,] tss = new int[dims[0], dims[1]];
                        zeros = new KhivaArray(tss);
                    }
                    else if (maxDim == 3)
                    {
                        int[,,] tss = new int[dims[0], dims[1], dims[2]];
                        zeros = new KhivaArray(tss);
                    }
                    else
                    {
                        int[,,,] tss = new int[dims[0], dims[1], dims[2], dims[3]];
                        zeros = new KhivaArray(tss);
                    }
                }
                interop.DLLArray.khiva_sub(ref zeros.reference, ref rhs.reference, out IntPtr result);
                zeros.Reference = zeros.reference;
                rhs.Reference = rhs.reference;
                return (new KhivaArray(result));
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
                interop.DLLArray.khiva_not(ref lhs.reference, out IntPtr result);
                lhs.Reference = lhs.reference;
                return (new KhivaArray(result));
            }

            /// <summary>
            /// Compares (element-wise) if lhs is lower than rhs.
            /// </summary>
            /// <param name="lhs">Left-hand side KHIVA array for the operation.</param>
            /// <param name="rhs">Right-hand side KHIVA array for the operation.</param>
            /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
            public static KhivaArray operator <(KhivaArray lhs, KhivaArray rhs)
            {
                interop.DLLArray.khiva_lt(ref lhs.reference, ref rhs.reference, out IntPtr result);
                lhs.Reference = lhs.reference;
                rhs.Reference = rhs.reference;
                return (new KhivaArray(result));
            }

            /// <summary>
            /// Compares (element-wise) if lhs is greater than rhs.
            /// </summary>
            /// <param name="lhs">Left-hand side KHIVA array for the operation.</param>
            /// <param name="rhs">Right-hand side KHIVA array for the operation.</param>
            /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
            public static KhivaArray operator >(KhivaArray lhs, KhivaArray rhs)
            {
                interop.DLLArray.khiva_gt(ref lhs.reference, ref rhs.reference, out IntPtr result);
                lhs.Reference = lhs.reference;
                rhs.Reference = rhs.reference;
                return (new KhivaArray(result));
            }

            /// <summary>
            /// Compares (element-wise) if lhs is lower or equal than rhs.
            /// </summary>
            /// <param name="lhs">Left-hand side KHIVA array for the operation.</param>
            /// <param name="rhs">Right-hand side KHIVA array for the operation.</param>
            /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
            public static KhivaArray operator <=(KhivaArray lhs, KhivaArray rhs)
            {
                interop.DLLArray.khiva_le(ref lhs.reference, ref rhs.reference, out IntPtr result);
                lhs.Reference = lhs.reference;
                rhs.Reference = rhs.reference;
                return (new KhivaArray(result));
            }

            /// <summary>
            /// Compares (element-wise) if lhs is greater or equal than rhs.
            /// </summary>
            /// <param name="lhs">Left-hand side KHIVA array for the operation.</param>
            /// <param name="rhs">Right-hand side KHIVA array for the operation.</param>
            /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
            public static KhivaArray operator >=(KhivaArray lhs, KhivaArray rhs)
            {
                interop.DLLArray.khiva_ge(ref lhs.reference, ref rhs.reference, out IntPtr result);
                lhs.Reference = lhs.reference;
                rhs.Reference = rhs.reference;
                return (new KhivaArray(result));
            }

            /// <summary>
            /// Compares (element-wise) if rhs is equal to rhs.
            /// </summary>
            /// <param name="lhs">Left-hand side KHIVA array for the operation.</param>
            /// <param name="rhs">Right-hand side KHIVA array for the operation.</param>
            /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
            public static KhivaArray operator ==(KhivaArray lhs, KhivaArray rhs)
            {
                interop.DLLArray.khiva_eq(ref lhs.reference, ref rhs.reference, out IntPtr result);
                lhs.Reference = lhs.reference;
                rhs.Reference = rhs.reference;
                return (new KhivaArray(result));
            }

            /// <summary>
            /// Compares (element-wise) if lhs is not equal to rhs.
            /// </summary>
            /// <param name="lhs">Left-hand side KHIVA array for the operation.</param>
            /// <param name="rhs">Right-hand side KHIVA array for the operation.</param>
            /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
            public static KhivaArray operator !=(KhivaArray lhs, KhivaArray rhs)
            {
                interop.DLLArray.khiva_ne(ref lhs.reference, ref rhs.reference, out IntPtr result);
                lhs.Reference = lhs.reference;
                rhs.Reference = rhs.reference;
                return (new KhivaArray(result));
            }

            /// <summary>
            /// Equals object method
            /// </summary>
            /// <param name="o">Object to compare</param>
            /// <returns></returns>
            public override bool Equals(object o)
            {
                if(o.GetType() == typeof(KhivaArray))
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
                interop.DLLArray.khiva_transpose(ref reference, ref conjugate, out IntPtr result);
                this.Reference = reference;
                return (new KhivaArray(result));
            }

            /// <summary>
            /// Retrieves a given column of array.
            /// </summary>
            /// <param name="index">The column to be retrieved.</param>
            /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
            public KhivaArray Col(int index)
            {
                interop.DLLArray.khiva_col(ref reference, ref index, out IntPtr result);
                this.Reference = reference;
                return (new KhivaArray(result));
            }

            /// <summary>
            /// Retrieves a subset of columns of array, starting at first and finishing at last, both inclusive.
            /// </summary>
            /// <param name="first">Start of the subset of columns to be retrieved.</param>
            /// <param name="last">End of the subset of columns to be retrieved.</param>
            /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
            public KhivaArray Cols(int first, int last)
            {
                interop.DLLArray.khiva_cols(ref reference, ref first, ref last, out IntPtr result);
                this.Reference = reference;
                return (new KhivaArray(result));
            }

            /// <summary>
            /// Retrieves a given row of array.
            /// </summary>
            /// <param name="index">The row to be retrieved.</param>
            /// <returns> KHIVA KhivaArray with the result of this operation.</returns>
            public KhivaArray Row(int index)
            {
                interop.DLLArray.khiva_row(ref reference, ref index, out IntPtr result);
                this.Reference = reference;
                return (new KhivaArray(result));
            }

            /// <summary>
            /// Retrieves a subset of rows of array, starting at first and finishing at last, both inclusive.
            /// </summary>
            /// <param name="first">Start of the subset of rows to be retrieved.</param>
            /// <param name="last">End of the subset of rows to be retrieved.</param>
            /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
            public KhivaArray Rows(int first, int last)
            {
                interop.DLLArray.khiva_rows(ref reference, ref first, ref last, out IntPtr result);
                this.Reference = reference;
                return (new KhivaArray(result));
            }

            /// <summary>
            /// Performs a matrix multiplication of lhs and rhs.
            /// </summary>
            /// <param name="rhs">Right-hand side KHIVA array for the operation.</param>
            /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
            public KhivaArray Matmul(KhivaArray rhs)
            {
                interop.DLLArray.khiva_matmul(ref reference, ref rhs.reference, out IntPtr result);
                this.Reference = reference;
                rhs.Reference = rhs.reference;
                return (new KhivaArray(result));
            }

            /// <summary>
            /// Performs a deep copy of array.
            /// </summary>
            /// <returns>KHIVA KhivaArray which contains a copy of array.</returns>
            public KhivaArray Copy()
            {
                interop.DLLArray.copy(ref reference, out IntPtr result);
                this.Reference = reference;
                return (new KhivaArray(result));
            }

            /// <summary>
            /// Changes the type of array.
            /// </summary>
            /// <param name="type">Target type of the output array.</param>
            /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
            public KhivaArray As(int type)
            {
                interop.DLLArray.khiva_as(ref reference, ref type, out IntPtr result);
                this.Reference = reference;
                return (new KhivaArray(result));
            }


        }
    }
}