using System;
using System.Diagnostics;

namespace HaloOnline.Research.Core.Utilities
{
    /// <summary>
    /// Provides translation of image addresses to process addresses.
    /// </summary>
    [DebuggerDisplay("{Value}")]
    public class ProcessAddress
    {
        /// <summary>
        /// The image address.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private uint ImageAddress { get; }

        /// <summary>
        /// The process address.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private uint Value => ImageAddress - ImageBaseAddress + ProcessBaseAddress;

        /// <summary>
        /// Determins whether the static <see cref="Initialize"/> method has been called.
        /// </summary>
        private static bool Initialized { get; set; }

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
            Initialized = true;
        }

        /// <summary>
        /// Initializes a new instance of the ProcessAddress class.
        /// </summary>
        /// <param name="imageAddress">The static address according to the executable image.</param>
        public ProcessAddress(uint imageAddress)
        {
            ImageAddress = imageAddress;
        }

        /// <summary>
        /// Casts a uint representing a process address into a ProcessAddress.
        /// </summary>
        /// <param name="x"></param>
        public static implicit operator ProcessAddress(uint x)
        {
            if (!Initialized)
                throw new FieldAccessException("ProcessAddress.Initialize must be called first.");

            return new ProcessAddress(x + ImageBaseAddress - ProcessBaseAddress);
        }

        /// <summary>
        /// Casts a ProcessAddress to a uint representing the actual process address.
        /// </summary>
        /// <param name="x"></param>
        public static implicit operator uint(ProcessAddress x)
        {
            if (!Initialized)
                throw new FieldAccessException("ProcessAddress.Initialize must be called first.");

            return x.Value;
        }
    }
}
