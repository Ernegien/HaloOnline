using System.Runtime.InteropServices;

namespace HaloOnline.Research.Core.Imports.Structs
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ImageNtHeaders32
    {
        public uint Signature;
        public ImageFileHeader32 FileHeader;
        public ImageOptionalHeader32 OptionalHeader;
    }
}
