﻿using LanguageExt.TypeClasses;

namespace LanguageExt
{
    /// <summary>
    /// A ratio between two values.
    /// </summary>
    /// <remarks>
    /// This is used in the definition of Fractional.
    /// </remarks>
    public struct Ratio<A> : Typeclass
    {
        /// <summary>
        /// The numerator of the ratio, in non-reduced form.
        /// </summary>
        public A Numerator;

        /// <summary>
        /// The denominator of the ratio, in non-reduced form.
        /// </summary>
        public A Denominator;

        public Ratio(A num, A den)
        {
            Numerator = num;
            Denominator = den;
        }
    }
}
