namespace HaloOnline.Research.Core.Imports.Structs
{
    /// <summary>
    /// Describes an entry in the descriptor table. This structure is valid only on x86-based systems.
    /// </summary>
    public struct LdtEntry
    {
        /// <summary>
        /// The low-order part of the address of the last byte in the segment.
        /// </summary>
        public ushort LimitLow;

        /// <summary>
        /// The low-order part of the base address of the segment.
        /// </summary>
        public ushort BaseLow;

        /// <summary>
        /// The high-order portion of the descriptor.
        /// </summary>
        public LdtHighBits HighWord;

        /// <summary>
        /// The base address of the segment.
        /// </summary>
        public uint BaseAddress
        {
            get { return (uint)((HighWord.BaseHi << 24) | (HighWord.BaseMid << 16) | BaseLow); }
        }
    }
}
