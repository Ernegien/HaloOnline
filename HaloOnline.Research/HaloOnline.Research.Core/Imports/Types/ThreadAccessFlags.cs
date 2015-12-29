using System;

namespace HaloOnline.Research.Core.Imports.Types
{
    /// <summary>
    /// Thread security access rights.
    /// </summary>
    [Flags]
    public enum ThreadAccessFlags : uint
    {
        /// <summary>
        /// Required to terminate a thread using TerminateThread.
        /// </summary>
        Terminate = 1 << 0,

        /// <summary>
        /// Required to suspend or resume a thread.
        /// </summary>
        SuspendResume = 1 << 1,

        /// <summary>
        /// Required to read the context of a thread using GetThreadContext.
        /// </summary>
        GetContext = 1 << 3,

        /// <summary>
        /// Required to write the context of a thread using SetThreadContext.
        /// </summary>
        SetContext = 1 << 4,

        /// <summary>
        /// Required to set certain information in the thread object.
        /// </summary>
        SetInformation = 1 << 5,

        /// <summary>
        /// Required to read certain information from the thread object, such as the exit code.
        /// </summary>
        QueryInformation = 1 << 6,

        /// <summary>
        /// Required to set the impersonation token for a thread using SetThreadToken.
        /// </summary>
        SetThreadToken = 1 << 7,

        /// <summary>
        /// Required to use a thread's security information directly without calling it by using a communication mechanism that provides impersonation services.
        /// </summary>
        Impersonate = 1 << 8,

        /// <summary>
        /// Required for a server thread that impersonates a client.
        /// </summary>
        DirectImpersonation = 1 << 9,

        /// <summary>
        /// Required to set certain information in the thread object. A handle that has the SetInformation access right is automatically granted SetLimitedInformation.
        /// </summary>
        SetLimitedInformation = 1 << 10,

        /// <summary>
        /// Required to read certain information from the thread objects. A handle that has the QueryInformation access right is automatically granted QueryLimitedInformation.
        /// </summary>
        QueryLimitedInformation = 1 << 11,

        /// <summary>
        /// Enables the use of the thread handle in any of the wait functions.
        /// </summary>
        Synchronize = 1 << 20,

        /// <summary>
        /// All possible access rights for a thread object.
        /// </summary>
        All = 0x1F03FF
    }
}
