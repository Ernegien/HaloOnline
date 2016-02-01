using System;
using System.Diagnostics;

namespace HaloOnline.Research.Core.Utilities
{
    /// <summary>
    /// Represents a module memory address and provides translation services to/from image addresses.
    /// </summary>
    [DebuggerDisplay("{Value}")]
    public class ModuleAddress : IComparable<ModuleAddress>
    {
        /// <summary>
        /// The module address context.
        /// </summary>
        public ModuleAddressContext Context { get; }

        /// <summary>
        /// The module address.
        /// </summary>
        public uint Value { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleAddress"/> class.
        /// </summary>
        /// <param name="context">The module address context.</param>
        /// <param name="moduleAddress">The module address.</param>
        public ModuleAddress(ModuleAddressContext context, uint moduleAddress)
        {
            if (!IsValidModuleAddress(context, moduleAddress))
                throw new ArgumentOutOfRangeException();

            Context = context;
            Value = moduleAddress;
        }

        /// <summary>
        /// Converts the current module address to an image address.
        /// </summary>
        /// <returns>The image address.</returns>
        public uint ToImageAddress()
        {
            return ToImageAddress(Context, Value);
        }

        /// <summary>
        /// Converts a module address to an image address.
        /// </summary>
        /// <param name="context">The module address context.</param>
        /// <param name="moduleAddress">The module address.</param>
        /// <returns>The image address.</returns>
        public static uint ToImageAddress(ModuleAddressContext context, uint moduleAddress)
        {
            if (!IsValidModuleAddress(context, moduleAddress))
                throw new ArgumentOutOfRangeException();

            return moduleAddress + context.ImageBaseAddress - context.BaseAddress;
        }

        /// <summary>
        /// Converts an image address to a module address.
        /// </summary>
        /// <param name="context">The module address context.</param>
        /// <param name="imageAddress">The image address.</param>
        /// <returns>The module address.</returns>
        public static ModuleAddress FromImageAddress(ModuleAddressContext context, uint imageAddress)
        {
            uint moduleAddress = imageAddress - context.ImageBaseAddress + context.BaseAddress;
            
            if (!IsValidModuleAddress(context, moduleAddress))
                throw new ArgumentOutOfRangeException();

            return new ModuleAddress(context, moduleAddress);
        }

        /// <summary>
        /// Determines if the specified module address is valid under the given context.
        /// </summary>
        /// <param name="context">The module address context.</param>
        /// <param name="moduleAddress">The module address.</param>
        /// <returns>Returns true if the module address is valid.</returns>
        public static bool IsValidModuleAddress(ModuleAddressContext context, uint moduleAddress)
        {
            return moduleAddress >= context.BaseAddress && moduleAddress <= context.BaseAddress + context.Size;
        }

        /// <summary>
        /// Performs a value comparison between two module addresses.
        /// </summary>
        /// <param name="other">The other module address to compare to.</param>
        /// <returns></returns>
        public int CompareTo(ModuleAddress other)
        {
            return other == null ? 1 : Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Returns the hexidecimal string representation of the module address.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"0x{Convert.ToString(Value, 16).PadLeft(8, '0')}";
        }

        /// <summary>
        /// Casts a <see cref="ModuleAddress"/> value to a UInt32.
        /// </summary>
        /// <param name="address">The module address value.</param>
        public static implicit operator uint(ModuleAddress address)
        {
            return address.Value;
        }
    }
}
