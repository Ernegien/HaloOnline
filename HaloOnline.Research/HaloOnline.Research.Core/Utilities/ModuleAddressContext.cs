using System;

namespace HaloOnline.Research.Core.Utilities
{
    /// <summary>
    /// Suppliments the <see cref="ModuleAddress"/> class with the context necessary for translation to/from image addresses.
    /// </summary>
    public class ModuleAddressContext
    {
        /// <summary>
        /// The image base address.
        /// </summary>
        public uint ImageBaseAddress { get; set; }

        /// <summary>
        /// The module base memory address.
        /// </summary>
        public uint BaseAddress { get; set; }

        /// <summary>
        /// The module base memory address.
        /// </summary>
        public uint Size { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleAddressContext"/> class.
        /// </summary>
        /// <param name="imageBaseAddress">The image base address.</param>
        /// <param name="baseAddress">The module base address.</param>
        /// <param name="size">The module size.</param>
        public ModuleAddressContext(uint imageBaseAddress, uint baseAddress, uint size)
        {
            if (imageBaseAddress + size <= imageBaseAddress || baseAddress + size <= baseAddress)
                throw new ArgumentOutOfRangeException();

            ImageBaseAddress = imageBaseAddress;
            BaseAddress = baseAddress;
            Size = size;
        }
    }
}
