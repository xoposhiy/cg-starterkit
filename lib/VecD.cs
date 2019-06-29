using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace bot
{
    public class VecD : IEquatable<VecD>, IFormattable
    {        
        public readonly double X, Y;

        public VecD(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double this[int dimension] => dimension == 0 ? X : Y;

        [Pure]
        public bool Equals(VecD other) => !ReferenceEquals(other, null) && X == other.X && Y == other.Y;

        public string ToString(string format, IFormatProvider formatProvider) =>
            $"{X.ToString(format, formatProvider)} {Y.ToString(format, formatProvider)}";

        public static Vec FromPolar(double len, double angle) => new Vec(len * Math.Cos(angle), len * Math.Sin(angle));

        public static Vec Parse(string s)
        {
            var parts = s.Split();
            if (parts.Length != 2) throw new FormatException(s);
            return new Vec(double.Parse(parts[0], CultureInfo.InvariantCulture), double.Parse(parts[1], CultureInfo.InvariantCulture));
        }

        public static implicit operator VecD(Vec v) => new VecD(v.X, v.Y);
        public Vec Round() => new Vec(X, Y);

        public static implicit operator VecD(string text)
        {
            var parts = text.Split();
            return new VecD(int.Parse(parts[0]), int.Parse(parts[1]));
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is VecD vd && Equals(vd);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X.GetHashCode() * 397) ^ Y.GetHashCode();
            }
        }

        public override string ToString() => $"{X} {Y}";


        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double DistTo(VecD b) => (b - this).Length();

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double SquaredDistTo(VecD b)
        {
            var dx = X - b.X;
            var dy = Y - b.Y;
            return dx * dx + dy * dy;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double ManhattanDistTo(VecD b) => Math.Abs(X - b.X) + Math.Abs(Y - b.Y);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Length() => Math.Sqrt(X * X + Y * Y);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double LengthSquared() => X * X + Y * Y;

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VecD operator -(VecD a, VecD b) => new VecD(a.X - b.X, a.Y - b.Y);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VecD operator -(VecD a) => new VecD(-a.X, -a.Y);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VecD operator +(VecD v, VecD b) => new VecD(v.X + b.X, v.Y + b.Y);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VecD operator *(VecD a, int k) => new VecD(a.X * k, a.Y * k);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VecD operator /(VecD a, int k) => new VecD(a.X / k, a.Y / k);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VecD operator *(int k, VecD a) => new VecD(a.X * k, a.Y * k);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VecD operator *(double k, VecD a) => new VecD(a.X * k, a.Y * k);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double ScalarProd(VecD p2) => X * p2.X + Y * p2.Y;

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double VectorProdLength(VecD p2) => X * p2.Y - p2.X * Y;

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VecD Translate(int shiftX, int shiftY) => new VecD(X + shiftX, Y + shiftY);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VecD MoveTowards(VecD target, int distance)
        {
            var d = target - this;
            var difLen = d.Length();
            if (difLen < distance) return target;
            var k = distance / difLen;
            return new VecD(X + k * d.X, Y + k * d.Y);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VecD Rotate90CW() => new VecD(Y, -X);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VecD Rotate90CCW() => new VecD(-Y, X);

        /// <returns>angle in (-Pi..Pi]</returns>
        public double GetAngle() => Math.Atan2(Y, X);

        public static readonly VecD[] Directions4 = {
            new VecD(1, 0),
            new VecD(0, 1),
            new VecD(-1, 0),
            new VecD(0, -1)
        };
    }
}