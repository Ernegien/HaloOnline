using System;

namespace HaloOnline.Research.Core.Utilities
{
    /// <summary>
    /// Represents a minimum and maximum range of values of type T.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Range<T> where T : IComparable<T>
    {
        /// <summary>
        /// Minimum value of the range
        /// </summary>
        public T Minimum { get; }

        /// <summary>
        /// Maximum value of the range
        /// </summary>
        public T Maximum { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Range{T}"/> class.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public Range(T min, T max)
        {
            if (min == null)
                throw new ArgumentNullException();

            if (max == null)
                throw new ArgumentNullException();

            if (Minimum.CompareTo(Maximum) > 0)
            {
                throw new ArgumentException("Minimum value must be less than or equal to the maximum value.");
            }

            Minimum = min;
            Maximum = max;
        }

        /// <summary>
        /// Determines if this Range is inside the bounds of another range
        /// </summary>
        /// <param name="range">The parent range to test on</param>
        /// <returns>True if range is inclusive, else false</returns>
        public bool IsContainedWithin(Range<T> range)
        {
            return range.Contains(Minimum) && range.Contains(Maximum);
        }

        /// <summary>
        /// Determines if the provided value is inside the range
        /// </summary>
        /// <param name="value">The value to test</param>
        /// <returns>True if the value is inside Range, else false</returns>
        public bool Contains(T value)
        {
            return (Minimum.CompareTo(value) <= 0) && (value.CompareTo(Maximum) <= 0);
        }

        /// <summary>
        /// Determines if another range is inside the bounds of this range
        /// </summary>
        /// <param name="range">The child range to test</param>
        /// <returns>True if range is inside, else false</returns>
        public bool Contains(Range<T> range)
        {
            return Contains(range.Minimum) && Contains(range.Maximum);
        }

        /// <summary>
        /// Presents the Range in readable format
        /// </summary>
        /// <returns>String representation of the Range</returns>
        public override string ToString() { return $"[{Minimum} - {Maximum}]"; }
    }
}
