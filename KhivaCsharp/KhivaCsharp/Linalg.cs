﻿// Copyright (c) 2019 Shapelets.io
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace khiva
{
    namespace linalg
    {
        public static class Linalg
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
             * @return result Contains the solution to the linear equation problem minimizing the norm 2.
             */
            public static array.Array Lls(array.Array a, array.Array b)
            {
                IntPtr aReference = a.Reference;
                IntPtr bReference = b.Reference;
                interop.DLLLinalg.lls(ref aReference, ref bReference, out IntPtr result);
                a.Reference = aReference;
                b.Reference = bReference;
                return (new array.Array(result));
            }
        }
    }
}
