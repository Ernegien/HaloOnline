using System;

namespace HaloOnline.Research.Core.Imports.Types
{
    [Flags]
    public enum ProcessCreationFlags : uint
    {
        ZeroFlag = 0x00000000,
        CreateBreakawayFromJob = 0x01000000,
        CreateDefaultErrorMode = 0x04000000,
        CreateNewConsole = 0x00000010,
        CreateNewProcessGroup = 0x00000200,
        CreateNoWindow = 0x08000000,
        CreateProtectedProcess = 0x00040000,
        CreatePreserveCodeAuthzLevel = 0x02000000,
        CreateSeparateWowVdm = 0x00001000,
        CreateSharedWowVdm = 0x00001000,
        CreateSuspended = 0x00000004,
        CreateUnicodeEnvironment = 0x00000400,
        DebugOnlyThisProcess = 0x00000002,
        DebugProcess = 0x00000001,
        DetachedProcess = 0x00000008,
        ExtendedStartupinfoPresent = 0x00080000,
        InheritParentAffinity = 0x00010000
    }
}
