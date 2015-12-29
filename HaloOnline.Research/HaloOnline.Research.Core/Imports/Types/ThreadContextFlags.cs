using System;

namespace HaloOnline.Research.Core.Imports.Types
{
    /// <summary>
    /// A value indicating which portions of the Context structure should be initialized. 
    /// This parameter influences the size of the initialized Context structure.
    /// </summary>
    [Flags]
    public enum ThreadContextFlags : uint
    {
        i386 = 0x10000,
        i486 = 0x10000,
        Control = i386 | 0x01,
        Integer = i386 | 0x02,
        Segments = i386 | 0x04,
        FloatingPoint = i386 | 0x08,
        DebugRegisters = i386 | 0x10,
        ExtendedRegisters = i386 | 0x20,
        Full = Control | Integer | Segments,
        All = Control | Integer | Segments | FloatingPoint | DebugRegisters | ExtendedRegisters
    }
}
