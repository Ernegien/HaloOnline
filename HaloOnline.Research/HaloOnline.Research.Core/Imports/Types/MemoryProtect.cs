using System;

namespace HaloOnline.Research.Core.Imports.Types
{
    /// <summary>
    /// Memory access rights.
    /// </summary>
    [Flags]
    public enum MemoryProtect : uint
    {
        NoAccess = 1 << 0,
        ReadOnly = 1 << 1,
        ReadWrite = 1 << 2,
        WriteCopy = 1 << 3,
        Execute = 1 << 4,
        ExecuteRead = 1 << 5,
        ExecuteReadWrite = 1 << 6,
        ExecuteWriteCopy = 1 << 7,
        Guard = 1 << 8,
        NoCache = 1 << 9,
        WriteCombine = 1 << 10
    }
}
