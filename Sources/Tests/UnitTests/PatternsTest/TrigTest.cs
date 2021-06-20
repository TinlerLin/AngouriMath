/*
 * This file was auto-generated by TestGenerator
 * Do not modify it; modify TestGenerator.java and rerun it instead.
 */

/*
 * It's super important to test all following cases because they test replacements for Trigonometric functions
 * so if one is wrong your result might be wrong at all
 */


using AngouriMath;
using static AngouriMath.Entity.Number;
using System;
using System.Linq;
using HonkSharp.Functional;
using Xunit;

namespace UnitTests.PatternsTest
{
    public sealed class TestTrigTableConsts
    {
        // TODO: Remove when we implement extra precision for rounding
        internal static void AssertEqualWithoutLast3Digits(Real expected, Real actual) =>
            Assert.Equal(expected.EDecimal.RoundToExponent(expected.EDecimal.Ulp().Exponent + 3),
                         actual.EDecimal.RoundToExponent(expected.EDecimal.Ulp().Exponent + 3));

        // For MemberData to show up as individual test cases, all arguments must be serializable:
        // https://github.com/xunit/xunit/issues/1473#issuecomment-333226539
        public static readonly System.Collections.Generic.IEnumerable<object[]> TrigTestData =
            new[] { nameof(MathS.Sin), nameof(MathS.Cos), nameof(MathS.Tan), nameof(MathS.Cotan) }
            .SelectMany(_ => Enumerable.Range(1, 29), (func, i) => new object[] { func, i });

        [Theory]
        [MemberData(nameof(TrigTestData))]
        public void TrigTest(string trigFunc, int twoPiOver)
        {
            var toSimplify =
                (Entity?)typeof(MathS).GetMethod(trigFunc)?.Invoke(null, new object[] { 2 * MathS.pi / twoPiOver })
                ?? throw new Exception($"{trigFunc} not found.");
            var expected = Assert.IsAssignableFrom<Real>(toSimplify.EvalNumerical());
            var actual = Assert.IsAssignableFrom<Real>(toSimplify.InnerSimplified.EvalNumerical());
            AssertEqualWithoutLast3Digits(expected, actual);
        }
        
        [Theory]
        [InlineData("pi", true)]
        [InlineData("pi / 2", true)]
        [InlineData("pi / 3", true)]
        [InlineData("pi / 6", true)]
        [InlineData("2 pi / 3", true)]
        [InlineData("4 pi / 7", true)]
        [InlineData("-pi", true)]
        [InlineData("-pi / 2", true)]
        [InlineData("-pi / 3", true)]
        [InlineData("-pi / 6", true)]
        [InlineData("-2 pi / 3", true)]
        [InlineData("-4 pi / 7", true)]
        [InlineData("200pi - pi", true)]
        [InlineData("200pi - pi / 2", true)]
        [InlineData("200pi - pi / 3", true)]
        [InlineData("200pi - pi / 6", true)]
        [InlineData("200pi - 2 pi / 3", true)]
        [InlineData("200pi - 4 pi / 7", true)]
        [InlineData("200pi + pi", true)]
        [InlineData("200pi + pi / 2", true)]
        [InlineData("200pi + pi / 3", true)]
        [InlineData("200pi + pi / 6", true)]
        [InlineData("200pi + 2 pi / 3", true)]
        [InlineData("200pi + 4 pi / 7", true)]
        [InlineData("1 + 2", false)]
        [InlineData("pi - 2", false)]
        public void SineHalvedTest(Entity angle, bool mustBeComputed)
        {
            var sin2x = MathS.Sin(angle);
            var sinx = MathS.ExperimentalFeatures.GetSineOfHalvedAngle(angle, sin2x);
            
            if (mustBeComputed)
                sinx.ShouldBeNotNull().ShouldApproximatelyEqual(MathS.Sin(angle / 2));
            else
                sinx.ShouldBeNull();
        }
        
        [Theory]
        [InlineData("pi", true)]
        [InlineData("pi / 2", true)]
        [InlineData("pi / 3", true)]
        [InlineData("pi / 6", true)]
        [InlineData("2 pi / 3", true)]
        [InlineData("4 pi / 7", true)]
        [InlineData("-pi", true)]
        [InlineData("-pi / 2", true)]
        [InlineData("-pi / 3", true)]
        [InlineData("-pi / 6", true)]
        [InlineData("-2 pi / 3", true)]
        [InlineData("-4 pi / 7", true)]
        [InlineData("200pi - pi", true)]
        [InlineData("200pi - pi / 2", true)]
        [InlineData("200pi - pi / 3", true)]
        [InlineData("200pi - pi / 6", true)]
        [InlineData("200pi - 2 pi / 3", true)]
        [InlineData("200pi - 4 pi / 7", true)]
        [InlineData("200pi + pi", true)]
        [InlineData("200pi + pi / 2", true)]
        [InlineData("200pi + pi / 3", true)]
        [InlineData("200pi + pi / 6", true)]
        [InlineData("200pi + 2 pi / 3", true)]
        [InlineData("200pi + 4 pi / 7", true)]
        [InlineData("1 + 2", false)]
        [InlineData("pi - 2", false)]
        public void CosineHalvedTest(Entity angle, bool mustBeComputed)
        {
            var cos2x = MathS.Cos(angle);
            var cosx = MathS.ExperimentalFeatures.GetCosineOfHalvedAngle(angle, cos2x);
            
            if (mustBeComputed)
                cosx.ShouldBeNotNull().ShouldApproximatelyEqual(MathS.Cos(angle / 2));
            else
                cosx.ShouldBeNull();
        }
        
        [Theory, CombinatorialData]
        public Unit SineCosineMultiplerExpansionTest(
            [CombinatorialValues("pi", "pi / 2", "- pi / 3", "pi / 10", "3", "30 pi", "-30 pi")] Entity x, 
            [CombinatorialValues(-3, -2, -1, 0, 1, 2, 3, 4, 5)] int n,
            [CombinatorialValues(true, false)] bool testSin
            )
            => testSin switch
            {
                true =>
                    MathS.ExperimentalFeatures.ExpandSineArgumentMultiplied(
                    sinx: MathS.Sin(x),
                    cosx: MathS.Cos(x),
                    n: n
                    ).ShouldApproximatelyEqual(MathS.Sin(n * x)),
                false => 
                    MathS.ExperimentalFeatures.ExpandCosineArgumentMultiplied(
                        sinx: MathS.Sin(x),
                        cosx: MathS.Cos(x),
                        n: n
                    ).ShouldApproximatelyEqual(MathS.Cos(n * x))
            };
    }
}