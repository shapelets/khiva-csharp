﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace khiva.interop
{
    public static class DLLLinalg
    {
        /**
         * @brief Calculates the minimum norm least squares solution \f$x\f$ \f$(\left\lVert{A·x - b}\right\rVert^2)\f$ to
         * \f$A·x = b\f$. This function uses the singular value decomposition function of Arrayfire. The actual formula that
         * this function computes is \f$x = V·D\dagger·U^T·b\f$. Where \f$U\f$ and \f$V\f$ are orthogonal matrices and
         * \f$D\dagger\f$ contains the inverse values of the singular values contained in D if they are not zero, and zero
         * otherwise.
         *
         * @param a A coefficient matrix containing the coefficients of the linear equation problem to solve.
         * @param b A vector with the measured values.
         * @param result Contains the solution to the linear equation problem minimizing the norm 2.
         */
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void lls([In] ref IntPtr a, [In] ref IntPtr b, [Out] out IntPtr result);
    }
}
