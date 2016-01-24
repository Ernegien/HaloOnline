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
        /// Creates a new process and its primary thread.
        /// </summary>
        /// <param name="lpApplicationName"></param>
        /// <param name="lpCommandLine"></param>
        /// <param name="lpProcessAttributes"></param>
        /// <param name="lpThreadAttributes"></param>
        /// <param name="bInheritHandles"></param>
        /// <param name="dwCreationFlags"></param>
        /// <param name="lpEnvironment"></param>
        /// <param name="lpCurrentDirectory"></param>
        /// <param name="lpStartupInfo"></param>
        /// <param name="lpProcessInformation"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", EntryPoint = "CreateProcess", SetLastError = true)]
        private static extern bool UnmanagedCreateProcess(string lpApplicationName,
            string lpCommandLine, IntPtr lpProcessAttributes,
            IntPtr lpThreadAttributes,
            bool bInheritHandles, ProcessCreationFlags dwCreationFlags,
            IntPtr lpEnvironment, string lpCurrentDirectory,
            ref ProcessStartupInfo lpStartupInfo,
            out ProcessInformation lpProcessInformation);

        /// <summary>
        /// Retrieves the address of an exported function or variable from the specified dynamic-link library (DLL).
        /// </summary>
        /// <param name="hModule"></param>
        /// <param name="lpProcName"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", EntryPoint = "GetProcAddress", SetLastError = true)]
        private static extern UIntPtr UnmanagedGetProcAddress(IntPtr hModule, string lpProcName);

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
        /// Creates a thread that runs in the virtual address space of another process.
        /// </summary>
        /// <param name="hProcess"></param>
        /// <param name="lpThreadAttributes"></param>
        /// <param name="dwStackSize"></param>
        /// <param name="lpStartAddress"></param>
        /// <param name="lpParameter"></param>
        /// <param name="dwCreationFlags"></param>
        /// <param name="lpThreadId"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", EntryPoint = "CreateRemoteThread", SetLastError = true)]
        private static extern IntPtr UnmanagedCreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes,
            uint dwStackSize, UIntPtr lpStartAddress, UIntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

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
        [DllImport("kernel32.dll", EntryPoint = "VirtualAllocEx", SetLastError = true)]
        private static extern UIntPtr UnmanagedVirtualAllocEx(IntPtr hProcess, UIntPtr lpAddress,
           uint dwSize, MemoryAllocationType flAllocationType, MemoryProtect flProtect);

        /// <summary>
        /// Releases and/or decommits a region of memory within the virtual address space of a specified process.
        /// </summary>
        /// <param name="hProcess"></param>
        /// <param name="lpAddress"></param>
        /// <param name="dwSize"></param>
        /// <param name="dwFreeType"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", EntryPoint = "VirtualFreeEx", SetLastError = true)]
        private static extern bool UnmanagedVirtualFreeEx(IntPtr hProcess, UIntPtr lpAddress,
           uint dwSize, MemoryAllocationType dwFreeType);

        /// <summary>
        /// Adds a directory to the search path used to locate DLLs for the application.
        /// </summary>
        /// <param name="lpPathName"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", EntryPoint = "SetDllDirectory", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnmanagedSetDllDirectory(string lpPathName);

        /// <summary>
        /// Retrieves a module handle for the specified module. The module must have been loaded by the calling process.
        /// </summary>
        /// <param name="lpModuleName"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", EntryPoint = "GetModuleHandle", SetLastError = true)]
        private static extern IntPtr UnmanagedGetModuleHandle(string lpModuleName);

        #endregion

        #region Managed

        /// <summary>
        /// Opens an existing local process object.
        /// </summary>
        /// <param name="access"></param>
        /// <param name="inheritHandle"></param>
        /// <param name="processId"></param>
        /// <exception cref="Win32Exception"></exception>
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
        /// Creates a new process and its primary thread.
        /// </summary>
        /// <param name="commandLine"></param>
        /// <param name="creationFlags"></param>
        /// <param name="currentDirectory"></param>
        /// <param name="startupInfo"></param>
        /// <param name="processInformation"></param>
        /// <exception cref="Win32Exception"></exception>
        public static void CreateProcess(string commandLine, ProcessCreationFlags creationFlags,
            string currentDirectory, ref ProcessStartupInfo startupInfo, out ProcessInformation processInformation)
        {
            if (!UnmanagedCreateProcess(null, commandLine, IntPtr.Zero, IntPtr.Zero, false, creationFlags, IntPtr.Zero, currentDirectory, ref startupInfo, out processInformation))
            {
                throw new Win32Exception();
            }
        }

        /// <summary>
        /// Retrieves the address of an exported function or variable from the specified dynamic-link library (DLL).
        /// </summary>
        /// <param name="moduleHandle"></param>
        /// <param name="processName"></param>
        /// <exception cref="Win32Exception"></exception>
        /// <returns>The address of the exported function or variable.</returns>
        public static UIntPtr GetProcAddress(IntPtr moduleHandle, string processName)
        {
            UIntPtr address = UnmanagedGetProcAddress(moduleHandle, processName);
            if (address == null)
            {
                throw new Win32Exception();
            }
            return address;
        }

        /// <summary>
        /// Opens an existing thread object.
        /// </summary>
        /// <param name="access"></param>
        /// <param name="inheritHandle"></param>
        /// <param name="threadId"></param>
        /// <exception cref="Win32Exception"></exception>
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
        /// Creates a thread that runs in the virtual address space of another process.
        /// </summary>
        /// <param name="processHandle"></param>
        /// <param name="startAddress"></param>
        /// <param name="parameter"></param>
        /// <returns>Returns a handle to the new thread.</returns>
        /// <exception cref="Win32Exception"></exception>
        public static IntPtr CreateRemoteThread(IntPtr processHandle, UIntPtr startAddress, UIntPtr parameter)
        {
            IntPtr handle = UnmanagedCreateRemoteThread(processHandle, IntPtr.Zero, 0, startAddress, parameter, 0, IntPtr.Zero);
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
        /// <exception cref="Win32Exception"></exception>
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
        /// <exception cref="Win32Exception"></exception>
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
        /// <exception cref="Win32Exception"></exception>
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
        /// <exception cref="Win32Exception"></exception>
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
        /// <exception cref="Win32Exception"></exception>
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
        /// <exception cref="Win32Exception"></exception>
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
        /// <exception cref="Win32Exception"></exception>
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
        /// <exception cref="Win32Exception"></exception>
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
        /// <exception cref="Win32Exception"></exception>
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
        /// <exception cref="Win32Exception"></exception>
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
        /// <exception cref="Win32Exception"></exception>
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
        /// <exception cref="Win32Exception"></exception>
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
        /// <exception cref="Win32Exception"></exception>
        public static void VirtualFreeEx(IntPtr processHandle, UIntPtr address, uint size, MemoryAllocationType type)
        {
            if (!UnmanagedVirtualFreeEx(processHandle, address, size, type))
            {
                throw new Win32Exception();
            }
        }

        /// <summary>
        /// Adds a directory to the search path used to locate DLLs for the application.
        /// </summary>
        /// <param name="pathName"></param>
        /// <exception cref="Win32Exception"></exception>
        /// <returns></returns>
        public static void SetDllDirectory(string pathName)
        {
            if (!UnmanagedSetDllDirectory(pathName))
            {
                throw new Win32Exception();
            }
        }

        /// <summary>
        /// Retrieves a module handle for the specified module. The module must have been loaded by the calling process.
        /// </summary>
        /// <param name="moduleName"></param>
        /// <exception cref="Win32Exception"></exception>
        /// <returns></returns>
        public static IntPtr GetModuleHandle(string moduleName)
        {
            IntPtr handle = UnmanagedGetModuleHandle(moduleName);
            if (handle == null)
            {
                throw new Win32Exception();
            }
            return handle;
        }

        #endregion
    }
}
