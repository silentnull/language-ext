﻿using LanguageExt;
using LanguageExt.TypeClass;
using System;
using Xunit;
using static LanguageExt.Prelude;
using static LanguageExt.TypeClass.Prelude;

namespace LanguageExtTests
{
    public class EitherApply
    {
        Func<int, int, int> add = (a, b) => a + b;

        [Fact]
        public void ApplyRightArgs()
        {
            var either = Right<string,Func<int,int,int>>(add)
                .Apply(Right<string, int>(3))
                .Apply(Right<string, int>(4));

            Assert.Equal(Right<string, int>(7), either);
        }

        [Fact]
        public void ApplyRightArgsF()
        {
            var either = 
                apply(
                    apply(
                        Right<string, Func<int, int, int>>(add),
                        Right<string, int>(3)
                    ),
                    Right<string, int>(4));
            Assert.Equal(Right<string, int>(7), either);
        }

        [Fact]
        public void ApplyRightArgsF2()
        {
            var either = apply(
                    Right<string, Func<int, int, int>>(add),
                    Right<string, int>(3),
                    Right<string, int>(4)
                );
            Assert.Equal(Right<string, int>(7), either);
        }

        private APPL GeneralApply<APPL>(Applicative<Func<int, int, int>> f, Applicative<int> a, Applicative<int> b)
            where APPL : struct, Applicative<int> =>
                (APPL)apply<APPL, int, int, int>(f, a, b);

        [Fact]
        public void ApplyLeftArgs()
        {
            var opt  = Some(add);
            var none = Option<int>.None;
            var four = Pure<Option<int>,int>(4);  // Some(4);

            var res = GeneralApply<Option<int>>(opt, none, four);

            Assert.Equal(None, res);

            var either = Right<string, Func<int, int, int>>(add)
                .Apply(Left<string, int>("left"))
                .Apply(Right<string, int>(4));

            Assert.Equal(Left<string, int>("left"), either);
        }

        [Fact]
        public void ApplyLeftArgsF()
        {
            var either =
                apply(
                    apply(
                        Right<string, Func<int, int, int>>(add),
                        Left<string, int>("left")
                    ),
                    Right<string, int>(4));

            Assert.Equal(Left<string, int>("left"), either);
        }

        [Fact]
        public void ApplyLeftArgsF2()
        {
            var either = apply(
                    Right<string, Func<int, int, int>>(add),
                    Left<string, int>("left"),
                    Right<string, int>(4)
                );

            Assert.Equal(Left<string, int>("left"), either);
        }

        [Fact]
        public void ApplicativeLawHolds()
        {
            var first = Right<string, Func<int, int, int>>(add)
                .Apply(Right<string, int>(3))
                .Apply(Right<string, int>(4));

            var second = Right<string, int>(3)
                .ParMap(add)
                .Apply(Right<string, int>(4));

            Assert.Equal(first, second);
        }

        [Fact]
        public void ApplicativeLawHoldsF()
        {
            var first = apply(
                    apply(
                        Right<string, Func<int, int, int>>(add),
                        Right<string, int>(3)
                    ),
                    Right<string, int>(4));

            var second = apply(parmap(Right<string, int>(3), add), Right<string, int>(4));

            Assert.Equal(first, second);
        }

        [Fact]
        public void ApplicativeLawHoldsF2()
        {
            var first = apply(
                Right<string, Func<int, int, int>>(add),
                Right<string, int>(3),
                Right<string, int>(4)
            );

            var second = apply(parmap(Right<string, int>(3), add), Right<string, int>(4));

            Assert.Equal(first, second);
        }
    }
}
