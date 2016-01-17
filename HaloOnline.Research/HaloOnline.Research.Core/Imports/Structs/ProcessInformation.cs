using System;

namespace HaloOnline.Research.Core.Imports.Structs
{
    public struct ProcessInformation
    {
        public IntPtr ProcessHandle;
        public IntPtr ThreadHandle;
        public uint ProcessId;
        public uint ThreadId;
    }
}
