using System;
using System.Runtime.InteropServices;
using HaloOnline.Research.Core.Imports.Types;

namespace HaloOnline.Research.Core.Imports.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MemoryBasicInformation
    {
        public IntPtr BaseAddress;
        public IntPtr AllocationBase;
        public MemoryProtect AllocationProtect;
        public uint RegionSize;
        public MemoryState State;
        public MemoryProtect Protect;
        public MemoryType Type;
    }
}
