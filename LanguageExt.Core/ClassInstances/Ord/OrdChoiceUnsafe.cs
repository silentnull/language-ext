﻿using System;
using LanguageExt;
using LanguageExt.TypeClasses;
using static LanguageExt.TypeClass;
using System.Diagnostics.Contracts;

namespace LanguageExt.ClassInstances
{
    /// <summary>
    /// Compare the equality of any type in the Choice type-class.  Taking into
    /// account the ordering of both possible bound values.
    /// </summary>
    public struct OrdChoiceUnsafe<OrdA, OrdB, ChoiceAB, CH, A, B> : Ord<CH>
        where ChoiceAB : struct, ChoiceUnsafe<CH, A, B>
        where OrdA     : struct, Ord<A>
        where OrdB     : struct, Ord<B>
    {
        public static readonly OrdChoiceUnsafe<OrdA, OrdB, ChoiceAB, CH, A, B> Inst = default(OrdChoiceUnsafe<OrdA, OrdB, ChoiceAB, CH, A, B>);

        /// <summary>
        /// Compare two values
        /// </summary>
        /// <param name="x">Left hand side of the compare operation</param>
        /// <param name="y">Right hand side of the compare operation</param>
        /// <returns>
        /// if x greater than y : 1
        /// if x less than y    : -1
        /// if x equals y       : 0
        /// </returns>
        [Pure]
        public int Compare(CH x, CH y) =>
            default(ChoiceAB).MatchUnsafe( x,
                Left: a =>
                    default(ChoiceAB).MatchUnsafe(y, 
                        Left: b => compare<OrdA, A>(a, b),
                        Right: _ => 1),
                Right: a =>
                    default(ChoiceAB).MatchUnsafe(y, 
                        Left: _ => -1,
                        Right: b => compare<OrdB, B>(a, b)));

        /// <summary>
        /// Equality test
        /// </summary>
        /// <param name="x">The left hand side of the equality operation</param>
        /// <param name="y">The right hand side of the equality operation</param>
        /// <returns>True if x and y are equal</returns>
        [Pure]
        public bool Equals(CH x, CH y) =>
            default(ChoiceAB).MatchUnsafe(x,
                Left: a =>
                    default(ChoiceAB).MatchUnsafe(y, 
                        Left: b => equals<OrdA, A>(a, b),
                        Right: _ => false),
                Right: a =>
                    default(ChoiceAB).MatchUnsafe(y, 
                        Left: _ => false,
                        Right: b => equals<OrdB, B>(a, b)));


        /// <summary>
        /// Get the hash-code of the provided value
        /// </summary>
        /// <returns>Hash code of x</returns>
        [Pure]
        public int GetHashCode(CH x) =>
            default(ChoiceAB).MatchUnsafe(x,
                Left: a => a.IsNull() ? 0 : a.GetHashCode(),
                Right: b => b.IsNull() ? 0 : b.GetHashCode());
    }

    /// <summary>
    /// Compare the equality of any type in the Choice type-class.  Taking into
    /// account only the 'success bound value' of B.
    /// </summary>
    public struct OrdChoiceUnsafe<OrdB, ChoiceAB, CH, A, B> : Ord<CH>
        where ChoiceAB : struct, ChoiceUnsafe<CH, A, B>
        where OrdB     : struct, Ord<B>
    {
        public static readonly OrdChoiceUnsafe<OrdB, ChoiceAB, CH, A, B> Inst = default(OrdChoiceUnsafe<OrdB, ChoiceAB, CH, A, B>);

        /// <summary>
        /// Compare two values
        /// </summary>
        /// <param name="x">Left hand side of the compare operation</param>
        /// <param name="y">Right hand side of the compare operation</param>
        /// <returns>
        /// if x greater than y : 1
        /// if x less than y    : -1
        /// if x equals y       : 0
        /// </returns>
        [Pure]
        public int Compare(CH x, CH y) =>
            default(OrdChoiceUnsafe<OrdDefault<A>, OrdB, ChoiceAB, CH, A, B>).Compare(x, y);

        /// <summary>
        /// Equality test
        /// </summary>
        /// <param name="x">The left hand side of the equality operation</param>
        /// <param name="y">The right hand side of the equality operation</param>
        /// <returns>True if x and y are equal</returns>
        [Pure]
        public bool Equals(CH x, CH y) =>
            default(OrdChoiceUnsafe<OrdDefault<A>, OrdB, ChoiceAB, CH, A, B>).Equals(x, y);

        /// <summary>
        /// Get the hash-code of the provided value
        /// </summary>
        /// <returns>Hash code of x</returns>
        [Pure]
        public int GetHashCode(CH x) =>
            default(OrdChoiceUnsafe<OrdDefault<A>, OrdB, ChoiceAB, CH, A, B>).GetHashCode(x);
    }

    /// <summary>
    /// Compare the equality of any type in the Choice type-class.  Taking into
    /// account only the 'success bound value' of B.
    /// </summary>
    public struct OrdChoiceUnsafe<ChoiceAB, CH, A, B> : Ord<CH>
        where ChoiceAB : struct, ChoiceUnsafe<CH, A, B>
    {
        public static readonly OrdChoiceUnsafe<ChoiceAB, CH, A, B> Inst = default(OrdChoiceUnsafe<ChoiceAB, CH, A, B>);

        /// <summary>
        /// Compare two values
        /// </summary>
        /// <param name="x">Left hand side of the compare operation</param>
        /// <param name="y">Right hand side of the compare operation</param>
        /// <returns>
        /// if x greater than y : 1
        /// if x less than y    : -1
        /// if x equals y       : 0
        /// </returns>
        [Pure]
        public int Compare(CH x, CH y) =>
            default(OrdChoiceUnsafe<OrdDefault<A>, OrdDefault<B>, ChoiceAB, CH, A, B>).Compare(x, y);

        /// <summary>
        /// Equality test
        /// </summary>
        /// <param name="x">The left hand side of the equality operation</param>
        /// <param name="y">The right hand side of the equality operation</param>
        /// <returns>True if x and y are equal</returns>
        [Pure]
        public bool Equals(CH x, CH y) =>
            default(OrdChoiceUnsafe<OrdDefault<A>, OrdDefault<B>, ChoiceAB, CH, A, B>).Equals(x, y);

        /// <summary>
        /// Get the hash-code of the provided value
        /// </summary>
        /// <returns>Hash code of x</returns>
        [Pure]
        public int GetHashCode(CH x) =>
            default(OrdChoiceUnsafe<OrdDefault<A>, OrdDefault<B>, ChoiceAB, CH, A, B>).GetHashCode(x);
    }
}
