using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using HaloOnline.Research.Core.Imports.Structs;
using HaloOnline.Research.Core.Imports.Types;

namespace HaloOnline.Research.Core.Imports
{
    /// <summary>
    /// Unmanaged Kernel32 imports.
    /// </summary>
    public unsafe class Kernel32
    {
        #region Unmanaged

        /// <summary>
        /// Opens an existing local process object.
        /// </summary>
        /// <param name="dwDesiredAccess"></param>
        /// <param name="bInheritHandle"></param>
        /// <param name="dwProcessId"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", EntryPoint = "OpenProcess", SetLastError = true)]
        private static extern IntPtr UnmanagedOpenProcess(ProcessAccessFlags dwDesiredAccess, bool bInheritHandle, uint dwProcessId);

        /// <summary>
        /// Opens an existing thread object.
        /// </summary>
        /// <param name="dwDesiredAccess"></param>
        /// <param name="bInheritHandle"></param>
        /// <param name="dwThreadId"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", EntryPoint = "OpenThread", SetLastError = true)]
        private static extern IntPtr UnmanagedOpenThread(ThreadAccessFlags dwDesiredAccess, bool bInheritHandle, uint dwThreadId);

        /// <summary>
        /// Closes an open object handle.
        /// </summary>
        /// <param name="hObject"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", EntryPoint = "CloseHandle", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnmanagedCloseHandle(IntPtr hObject);

        /// <summary>
        /// Retrieves the context of the specified thread.
        /// </summary>
        /// <param name="hThread"></param>
        /// <param name="lpThreadContext"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", EntryPoint = "GetThreadContext", SetLastError = true)]
        private static extern bool UnmanagedGetThreadContext(IntPtr hThread, ref ThreadContext lpThreadContext);

        /// <summary>
        /// Suspends the specified thread.
        /// </summary>
        /// <param name="hThread"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", EntryPoint = "SuspendThread", SetLastError = true)]
        private static extern int UnmanagedSuspendThread(IntPtr hThread);

        /// <summary>
        /// Resumes the specified thread.
        /// </summary>
        /// <param name="hThread"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", EntryPoint = "ResumeThread", SetLastError = true)]
        private static extern int UnmanagedResumeThread(IntPtr hThread);

        /// <summary>
        /// Retrieves a descriptor table entry for the specified selector and thread.
        /// </summary>
        /// <param name="hThread"></param>
        /// <param name="dwSelector"></param>
        /// <param name="lpSelectorEntry"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", EntryPoint = "GetThreadSelectorEntry", SetLastError = true)]
        private static extern bool UnmanagedGetThreadSelectorEntry(
            IntPtr hThread,
            uint dwSelector,
            out LdtEntry lpSelectorEntry
            );

        /// <summary>
        /// Reads data from an area of memory in a specified process.
        /// The entire area to be read must be accessible or the operation fails.
        /// </summary>
        /// <param name="hProcess"></param>
        /// <param name="lpBaseAddress"></param>
        /// <param name="lpBuffer"></param>
        /// <param name="dwSize"></param>
        /// <param name="lpNumberOfBytesRead"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", EntryPoint = "ReadProcessMemory", SetLastError = true)]
        private static extern bool UnmanagedReadProcessMemory(
            IntPtr hProcess,
            UIntPtr lpBaseAddress,
            [Out] byte* lpBuffer,
            int dwSize,
            out int lpNumberOfBytesRead
        );

        /// <summary>
        /// Writes data to an area of memory in a specified process.
        /// The entire area to be written to must be accessible or the operation fails.
        /// </summary>
        /// <param name="hProcess"></param>
        /// <param name="lpBaseAddress"></param>
        /// <param name="lpBuffer"></param>
        /// <param name="nSize"></param>
        /// <param name="lpNumberOfBytesWritten"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", EntryPoint = "WriteProcessMemory", SetLastError = true)]
        private static extern bool UnmanagedWriteProcessMemory(
            IntPtr hProcess,
            UIntPtr lpBaseAddress,
            byte* lpBuffer,
            int nSize,
            out int lpNumberOfBytesWritten);

        /// <summary>
        /// Retrieves information about a range of pages within the virtual address space of a specified process.
        /// </summary>
        /// <param name="hProcess"></param>
        /// <param name="lpAddress"></param>
        /// <param name="lpBuffer"></param>
        /// <param name="dwLength"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", EntryPoint = "VirtualQueryEx", SetLastError = true)]
        private static extern int UnmanagedVirtualQueryEx(IntPtr hProcess, UIntPtr lpAddress, 
            out MemoryBasicInformation lpBuffer, uint dwLength);

        /// <summary>
        /// Changes the protection on a region of committed pages in the virtual address space of a specified process.
        /// </summary>
        /// <param name="hProcess"></param>
        /// <param name="lpAddress"></param>
        /// <param name="dwSize"></param>
        /// <param name="flNewProtect"></param>
        /// <param name="lpflOldProtect"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", EntryPoint = "VirtualProtectEx", SetLastError = true)]
        private static extern bool UnmanagedVirtualProtectEx(IntPtr hProcess, UIntPtr lpAddress,
           uint dwSize, MemoryProtect flNewProtect, ref MemoryProtect lpflOldProtect);

        /// <summary>
        /// Reserves or commits a region of memory within the virtual address space of a specified process. 
        /// The function initializes the memory it allocates to zero, unless MEM_RESET is used.
        /// </summary>
        /// <param name="hProcess"></param>
        /// <param name="lpAddress"></param>
        /// <param name="dwSize"></param>
        /// <param name="flAllocationType"></param>
        /// <param name="flProtect"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", EntryPoint = "VirtualAllocEx", SetLastError = true, ExactSpelling = true)]
        static extern UIntPtr UnmanagedVirtualAllocEx(IntPtr hProcess, UIntPtr lpAddress,
           uint dwSize, MemoryAllocationType flAllocationType, MemoryProtect flProtect);

        /// <summary>
        /// Releases and/or decommits a region of memory within the virtual address space of a specified process.
        /// </summary>
        /// <param name="hProcess"></param>
        /// <param name="lpAddress"></param>
        /// <param name="dwSize"></param>
        /// <param name="dwFreeType"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", EntryPoint = "VirtualFreeEx", SetLastError = true, ExactSpelling = true)]
        static extern bool UnmanagedVirtualFreeEx(IntPtr hProcess, UIntPtr lpAddress,
           uint dwSize, MemoryAllocationType dwFreeType);

        #endregion

        #region Managed

        /// <summary>
        /// Opens an existing local process object.
        /// </summary>
        /// <param name="access"></param>
        /// <param name="inheritHandle"></param>
        /// <param name="processId"></param>
        /// <returns></returns>
        public static IntPtr OpenProcess(ProcessAccessFlags access, bool inheritHandle, uint processId)
        {
            IntPtr handle = UnmanagedOpenProcess(access, inheritHandle, processId);
            if (handle == null)
            {
                throw new Win32Exception();
            }
            return handle;
        }

        /// <summary>
        /// Opens an existing thread object.
        /// </summary>
        /// <param name="access"></param>
        /// <param name="inheritHandle"></param>
        /// <param name="threadId"></param>
        /// <returns></returns>
        public static IntPtr OpenThread(ThreadAccessFlags access, bool inheritHandle, uint threadId)
        {
            IntPtr handle = UnmanagedOpenThread(access, inheritHandle, threadId);
            if (handle == null)
            {
                throw new Win32Exception();
            }
            return handle;
        }

        /// <summary>
        /// Suspends the specified thread id.
        /// </summary>
        /// <param name="id"></param>
        public static void SuspendThread(uint id)
        {
            IntPtr handle = OpenThread(ThreadAccessFlags.SuspendResume, false, id);
            SuspendThread(handle);
            CloseHandle(handle);
        }

        /// <summary>
        /// Suspends the specified thread handle.
        /// </summary>
        /// <param name="handle"></param>
        public static void SuspendThread(IntPtr handle)
        {
            int suspendCount = UnmanagedSuspendThread(handle);
            if (suspendCount == -1)
            {
                throw new Win32Exception();
            }
        }

        /// <summary>
        /// Resumes the specified thread id.
        /// </summary>
        /// <param name="id"></param>
        public static void ResumeThread(uint id)
        {
            IntPtr handle = OpenThread(ThreadAccessFlags.SuspendResume, false, id);
            ResumeThread(handle);
            CloseHandle(handle);
        }

        /// <summary>
        /// Resumes the specified thread handle.
        /// </summary>
        /// <param name="handle"></param>
        public static void ResumeThread(IntPtr handle)
        {
            int suspendCount = UnmanagedResumeThread(handle);
            if (suspendCount == -1)
            {
                throw new Win32Exception();
            }
        }

        /// <summary>
        /// Retrieves a descriptor table entry for the specified selector and thread.
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static LdtEntry GetThreadSelectorEntry(IntPtr handle, uint selector)
        {
            LdtEntry entry;
            if (!UnmanagedGetThreadSelectorEntry(handle, selector, out entry))
            {
                throw new Win32Exception();
            }
            return entry;
        }

        /// <summary>
        /// Closes an open object handle.
        /// </summary>
        /// <param name="handle"></param>
        public static void CloseHandle(IntPtr handle)
        {
            if (!UnmanagedCloseHandle(handle))
            {
                throw new Win32Exception();
            }
        }


        /// <summary>
        /// Retrieves the context of the specified thread.
        /// </summary>
        /// <param name="handle"></param>
        public static ThreadContext GetThreadContext(IntPtr handle)
        {
            ThreadContext context = new ThreadContext { ThreadContextFlags = ThreadContextFlags.All };
            if (!UnmanagedGetThreadContext(handle, ref context))
            {
                throw new Win32Exception();
            }
            return context;
        }

        /// <summary>
        /// Reads data from an area of memory in a specified process.
        /// The entire area to be read must be accessible or the operation fails.
        /// </summary>
        /// <param name="hProcess"></param>
        /// <param name="lpBaseAddress"></param>
        /// <param name="lpBuffer"></param>
        /// <param name="dwSize"></param>
        /// <param name="lpNumberOfBytesRead"></param>
        public static void ReadProcessMemory(
            IntPtr hProcess,
            UIntPtr lpBaseAddress,
            [Out] byte* lpBuffer,
            int dwSize,
            out int lpNumberOfBytesRead
            )
        {
            if (!UnmanagedReadProcessMemory(hProcess, lpBaseAddress, lpBuffer, dwSize, out lpNumberOfBytesRead))
            {
                throw new Win32Exception();
            }
        }

        /// <summary>
        /// Writes data to an area of memory in a specified process.
        /// The entire area to be written to must be accessible or the operation fails.
        /// </summary>
        /// <param name="hProcess"></param>
        /// <param name="lpBaseAddress"></param>
        /// <param name="lpBuffer"></param>
        /// <param name="nSize"></param>
        /// <param name="lpNumberOfBytesWritten"></param>
        public static void WriteProcessMemory(
            IntPtr hProcess,
            UIntPtr lpBaseAddress,
            byte* lpBuffer,
            int nSize,
            out int lpNumberOfBytesWritten)
        {
            if (!UnmanagedWriteProcessMemory(hProcess, lpBaseAddress, lpBuffer, nSize, out lpNumberOfBytesWritten))
            {
                throw new Win32Exception();
            }
        }

        /// <summary>
        /// Retrieves information about a range of pages within the virtual address space of a specified process.
        /// </summary>
        /// <param name="processHandle"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public static MemoryBasicInformation VirtualQueryEx(IntPtr processHandle, UIntPtr address)
        {
            MemoryBasicInformation memInfo;
            if (UnmanagedVirtualQueryEx(processHandle, address, out memInfo, (uint)sizeof(MemoryBasicInformation)) == 0)
            {
                throw new Win32Exception();
            }
            return memInfo;
        }

        /// <summary>
        /// Changes the protection on a region of committed pages in the virtual address space of a specified process.
        /// </summary>
        /// <param name="processHandle"></param>
        /// <param name="address"></param>
        /// <param name="size"></param>
        /// <param name="newProtect"></param>
        /// <returns>Returns the previous memory access protection.</returns>
        public static MemoryProtect VirtualProtectEx(IntPtr processHandle, UIntPtr address,
            uint size, MemoryProtect newProtect)
        {
            MemoryProtect oldProtect = new MemoryProtect();
            if (!UnmanagedVirtualProtectEx(processHandle, address, size, newProtect, ref oldProtect))
            {
                throw new Win32Exception();
            }
            return oldProtect;
        }

        /// <summary>
        /// Reserves or commits a region of memory within the virtual address space of a specified process. 
        /// The function initializes the memory it allocates to zero, unless MEM_RESET is used.
        /// </summary>
        /// <param name="processHandle"></param>
        /// <param name="address"></param>
        /// <param name="size"></param>
        /// <param name="type"></param>
        /// <param name="protect"></param>
        /// <returns></returns>
        public static UIntPtr VirtualAllocEx(IntPtr processHandle, UIntPtr address, uint size, MemoryAllocationType type, MemoryProtect protect)
        {
            UIntPtr addr = UnmanagedVirtualAllocEx(processHandle, address, size, type, protect);
            if (addr == UIntPtr.Zero)
            {
                throw new Win32Exception();
            }
            return addr;
        }

        /// <summary>
        /// Releases and/or decommits a region of memory within the virtual address space of a specified process.
        /// </summary>
        /// <param name="processHandle"></param>
        /// <param name="address"></param>
        /// <param name="size"></param>
        /// <param name="type"></param>
        public static void VirtualFreeEx(IntPtr processHandle, UIntPtr address, uint size, MemoryAllocationType type)
        {
            if (!UnmanagedVirtualFreeEx(processHandle, address, size, type))
            {
                throw new Win32Exception();
            }
        }

        #endregion
    }
}
