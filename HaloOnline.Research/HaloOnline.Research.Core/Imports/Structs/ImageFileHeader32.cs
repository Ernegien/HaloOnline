using System.Runtime.InteropServices;

namespace HaloOnline.Research.Core.Imports.Structs
{
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 20)]
    public struct ImageFileHeader32
    {
        public ushort Machine;
        public ushort NumberOfSections;
        public uint TimeDateStamp;
        public uint PointerToSymbolTable;
        public uint NumberOfSymbols;
        public ushort SizeOfOptionalHeader;
        public ushort Characteristics;
    }
}
