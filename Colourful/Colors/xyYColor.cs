﻿#region License

// Copyright (C) Tomáš Pažourek, 2014
// All rights reserved.
// 
// Distributed under MIT license as a part of project Colourful.
// https://github.com/tompazourek/Colourful

#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Vector = System.Collections.Generic.IReadOnlyList<double>;

namespace Colourful
{
    /// <summary>
    /// CIE xyY color space (derived from <see cref="XYZColor"/> color space)
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "xy"), SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "xy"), SuppressMessage("Microsoft.Naming", "CA1708:IdentifiersShouldDifferByMoreThanCase")]
    public class xyYColor : IColorVector
    {
        #region Constructor

        /// <param name="x">x (usually from 0 to 1) chromaticity coordinate</param>
        /// <param name="y">y (usually from 0 to 1) chromaticity coordinate</param>
        /// <param name="Y">Y (usually from 0 to 1)</param>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Y"), SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y"), SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x"), SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Y"), SuppressMessage("Microsoft.Naming", "CA1708:IdentifiersShouldDifferByMoreThanCase")]
        public xyYColor(double x, double y, double Y)
            : this(new ChromaticityCoordinates(x, y), Y)
        {
        }

        /// <param name="chromaticity">Chromaticity coordinates (x and y together)</param>
        /// <param name="Y">Y (usually from 0 to 1)</param>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Y"), SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Y")]
        public xyYColor(ChromaticityCoordinates chromaticity, double Y)
        {
            if (chromaticity == null)
                throw new ArgumentNullException("chromaticity");

            Chromaticity = chromaticity;
            this.Y = Y;
        }

        #endregion

        #region Channels

        /// <remarks>
        /// Ranges usually from 0 to 1.
        /// </remarks>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "x"), SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
        public double x
        {
            get { return Chromaticity.x; }
        }

        /// <remarks>
        /// Ranges usually from 0 to 1.
        /// </remarks>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "y"), SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y")]
        public double y
        {
            get { return Chromaticity.y; }
        }

        /// <remarks>
        /// Ranges usually from 0 to 1.
        /// </remarks>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Y")]
        public double Y { get; private set; }

        /// <remarks>
        /// Chromaticity coordinates (identical to x and y)
        /// </remarks>
        public ChromaticityCoordinates Chromaticity { get; private set; }

        /// <summary>
        /// <see cref="IColorVector"/>
        /// </summary>
        public Vector Vector
        {
            get { return new[] { x, y, Y }; }
        }

        #endregion

        #region Equality

        public bool Equals(xyYColor other)
        {
            if (other == null) throw new ArgumentNullException("other");
            return x.Equals(other.x) && y.Equals(other.y) && Y.Equals(other.Y);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((xyYColor) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = x.GetHashCode();
                hashCode = (hashCode * 397) ^ y.GetHashCode();
                hashCode = (hashCode * 397) ^ Y.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(xyYColor left, xyYColor right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(xyYColor left, xyYColor right)
        {
            return !Equals(left, right);
        }

        #endregion

        #region Overrides

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "xyY [x={0:0.##}, y={1:0.##}, Y={2:0.##}]", x, y, Y);
        }

        #endregion
    }
}