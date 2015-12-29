using System;
using System.Runtime.InteropServices;
using HaloOnline.Research.Core.Imports.Types;

namespace HaloOnline.Research.Core.Imports.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MemoryBasicInformation
    {
        public UIntPtr BaseAddress;
        public UIntPtr AllocationBase;
        public MemoryProtect AllocationProtect;
        public IntPtr RegionSize;
        public uint State;  // TODO: declare type
        public MemoryProtect Protect;
        public uint Type;  // TODO: declare type
    }
}
