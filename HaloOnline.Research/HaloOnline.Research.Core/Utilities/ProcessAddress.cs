using System;
using System.Diagnostics;

namespace HaloOnline.Research.Core.Utilities
{
    /// <summary>
    /// Provides translation of 32-bit image addresses to process addresses.
    /// </summary>
    [DebuggerDisplay("{Value}")]
    public class ProcessAddress : IComparable<ProcessAddress>
    {
        /// <summary>
        /// The process address.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private uint Value { get; }

        /// <summary>
        /// Determins whether the static <see cref="Initialize"/> method has been called.
        /// </summary>
        private static bool IsInitialized { get; set; }

        /// <summary>
        /// The image base address.
        /// </summary>
        private static uint ImageBaseAddress { get; set; }

        /// <summary>
        /// The process base address.
        /// </summary>
        private static uint ProcessBaseAddress { get; set; }

        /// <summary>
        /// Provides the class with the information necessary for address translation.
        /// </summary>
        /// <param name="imageBase">The image base address.</param>
        /// <param name="processBase">The process base address.</param>
        public static void Initialize(uint imageBase, uint processBase)
        {
            ImageBaseAddress = imageBase;
            ProcessBaseAddress = processBase;
            IsInitialized = true;
        }

        /// <summary>
        /// Initializes a new instance of the ProcessAddress class.
        /// </summary>
        /// <param name="address">The process address.</param>
        public ProcessAddress(uint address)
        {
            Value = address;
        }

        /// <summary>
        /// Casts a uint representing a process address into a ProcessAddress.
        /// </summary>
        /// <param name="x"></param>
        public static implicit operator ProcessAddress(uint x)
        {
            if (!IsInitialized)
                throw new FieldAccessException("ProcessAddress.Initialize must be called first.");

            return new ProcessAddress(x);
        }

        /// <summary>
        /// Casts a ProcessAddress value to a uint.
        /// </summary>
        /// <param name="x"></param>
        public static implicit operator uint(ProcessAddress x)
        {
            if (!IsInitialized)
                throw new FieldAccessException("ProcessAddress.Initialize must be called first.");

            return x.Value;
        }

        /// <summary>
        /// Converts a process address to an image address.
        /// </summary>
        /// <param name="processAddress"></param>
        /// <returns></returns>
        public static uint ToImageAddress(uint processAddress)
        {
            return processAddress + ImageBaseAddress - ProcessBaseAddress;
        }

        /// <summary>
        /// Converts the process address to an image address.
        /// </summary>
        /// <returns></returns>
        public uint ToImageAddress()
        {
            return Value + ImageBaseAddress - ProcessBaseAddress;
        }

        /// <summary>
        /// Converts an image address to a process address.
        /// </summary>
        /// <param name="imageAddress"></param>
        /// <returns></returns>
        public static uint FromImageAddress(uint imageAddress)
        {
            return imageAddress - ImageBaseAddress + ProcessBaseAddress;
        }

        /// <summary>
        /// Performs a value comparison between two process addresses.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(ProcessAddress other)
        {
            return other == null ? 1 : Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Returns the hexidecimal string representation of the process address.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"0x{Convert.ToString(Value, 16).PadLeft(8, '0')}";
        }
    }
}
