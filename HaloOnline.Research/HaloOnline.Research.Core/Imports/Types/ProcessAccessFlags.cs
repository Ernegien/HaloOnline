using System;

namespace HaloOnline.Research.Core.Imports.Types
{
    /// <summary>
    /// Process security access rights.
    /// </summary>
    [Flags]
    public enum ProcessAccessFlags : uint
    {
        /// <summary>
        /// Required to terminate a process using TerminateProcess.
        /// </summary>
        Terminate = 1 << 0,

        /// <summary>
        /// Required to create a thread.
        /// </summary>
        CreateThread = 1 << 1,

        /// <summary>
        /// Required to perform an operation on the address space of a process.
        /// </summary>
        VirtualMemoryOperation = 1 << 3,

        /// <summary>
        /// Required to read memory in a process using ReadProcessMemory.
        /// </summary>
        VirtualMemoryRead = 1 << 4,

        /// <summary>
        /// Required to write to memory in a process using WriteProcessMemory.
        /// </summary>
        VirtualMemoryWrite = 1 << 5,

        /// <summary>
        /// Required to duplicate a handle using DuplicateHandle.
        /// </summary>
        DuplicateHandle = 1 << 6,

        /// <summary>
        /// Required to create a process.
        /// </summary>
        CreateProcess = 1 << 7,

        /// <summary>
        /// Required to set memory limits using SetProcessWorkingSetSize.
        /// </summary>
        SetQuota = 1 << 8,

        /// <summary>
        /// Required to set certain information about a process, such as its priority class.
        /// </summary>
        SetInformation = 1 << 9,

        /// <summary>
        /// Required to retrieve certain information about a process, such as its token, exit code, and priority class.
        /// </summary>
        QueryInformation = 1 << 10,

        /// <summary>
        /// Required to suspend or resume a process.
        /// </summary>
        SuspendResume = 1 << 11,

        /// <summary>
        /// Required to retrieve certain information about a process.
        /// </summary>
        QueryLimitedInformation = 1 << 12,

        /// <summary>
        /// Required to wait for the process to terminate using the wait functions.
        /// </summary>
        Synchronize = 1 << 20,

        /// <summary>
        /// All possible access rights for a process object.
        /// </summary>
        All = 0x001F0FFF
    }
}
