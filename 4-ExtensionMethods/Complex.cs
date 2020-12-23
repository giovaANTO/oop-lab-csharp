using System;
using System.Collections.Generic;

namespace ExtensionMethods
{
    /// <inheritdoc cref="IComplex" />
    public class Complex : IComplex
    {
        private const double Tolerance = 1E-7;

        private readonly double im;
        private readonly double re;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Complex" /> class.
        /// </summary>
        /// <param name="re">the real part.</param>
        /// <param name="im">the imaginary part.</param>
        public Complex(double re, double im)
        {
            this.re = re;
            this.im = im;
        }

        /// <inheritdoc cref="IComplex.Real" />
        public double Real => this.re;

        /// <inheritdoc cref="IComplex.Imaginary" />
        public double Imaginary => this.im;

        /// <inheritdoc cref="IComplex.Modulus" />
        public double Modulus => Math.Sqrt(Math.Pow(this.Real,2) + Math.Pow(this.Imaginary, 2));

        /// <inheritdoc cref="IComplex.Phase" />
        public double Phase => Math.Atan2(this.Real, this.Imaginary);

        /// <inheritdoc cref="IComplex.ToString" />
        public override string ToString()
        {
            string sign = this.Imaginary >= 0 ? "+" : "-";
            return String.Format("{0}{1}i{2}",this.Real,sign,Math.Abs(this.Imaginary));
        }

        /// <inheritdoc cref="IEquatable{T}.Equals(T)" />
        public bool Equals(IComplex other)
        {
            return other != null
                   && Math.Abs(other.Real - Real) < Tolerance
                   && Math.Abs(other.Imaginary - Imaginary) < Tolerance;
        }

        public override bool Equals(object obj)
        {
            if (obj is Complex complex)
            {
                return this.Equals(complex);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(re, im);
        }
    }
}
