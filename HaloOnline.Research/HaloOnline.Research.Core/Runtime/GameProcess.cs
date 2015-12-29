using System;
using System.Diagnostics;
using HaloOnline.Research.Core.Imports;
using HaloOnline.Research.Core.Imports.Structs;
using HaloOnline.Research.Core.Imports.Types;

namespace HaloOnline.Research.Core.Runtime
{
    public class GameProcess : IDisposable
    {
        #region Properties

        /// <summary>
        /// The process name.
        /// </summary>
        public string Name { get; }
   
        /// <summary>
        /// The main thread id of the process.
        /// </summary>
        public uint MainThreadId { get; private set; }

        /// <summary>
        /// The main thread handle of the process.
        /// </summary>
        public IntPtr MainThreadHandle { get; private set; }

        /// <summary>
        /// The process.
        /// </summary>
        public Process Process { get; private set; }

        /// <summary>
        /// The process handle.
        /// </summary>
        public IntPtr ProcessHandle { get; private set; }

        /// <summary>
        /// The TLS address.
        /// </summary>
        public uint TlsAddress { get; private set; }

        /// <summary>
        /// Process memory stream.
        /// </summary>
        public ProcessMemoryStream Memory { get; private set; }

        #endregion

        #region Initialization & Disposal

        /// <summary>
        /// Provides access to the game process.
        /// </summary>
        /// <param name="name">The process name.</param>
        public GameProcess(string name)
        {
            Name = name;
            Initialize();
        }

        /// <summary>
        /// Initializes core components.
        /// </summary>
        private void Initialize()
        {
            Process = GetProcessByName(Name);
            ProcessHandle = Kernel32.OpenProcess(ProcessAccessFlags.All, false, (uint)Process.Id);
            Memory = new ProcessMemoryStream(ProcessHandle);
            MainThreadId = User32.GetWindowThreadProcessId(Process.MainWindowHandle);
            MainThreadHandle = Kernel32.OpenThread(ThreadAccessFlags.All, false, MainThreadId);
            TlsAddress = GetTlsAddress(MainThreadHandle);
        }

        /// <summary>
        /// Disposes of and then clears the cached values and re-initializes the core components.
        /// </summary>
        public void Reset()
        {
            Dispose();
            Initialize();
        }

        /// <summary>
        /// Disposes of unmanaged resources. 
        /// </summary>
        public void Dispose()
        {
            try
            {
                Memory?.Dispose();

                if (MainThreadHandle != IntPtr.Zero)
                {
                    Kernel32.CloseHandle(MainThreadHandle);
                }

                if (ProcessHandle != IntPtr.Zero)
                {
                    Kernel32.CloseHandle(ProcessHandle);
                }
            }
            catch
            {
                // oh well
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Suspends the main process thread.
        /// </summary>
        public void SuspendMainThread()
        {
            Kernel32.SuspendThread(MainThreadHandle);
        }

        /// <summary>
        /// Resumes the main process thread.
        /// </summary>
        public void ResumeMainThread()
        {
            Kernel32.ResumeThread(MainThreadHandle);
        }

        /// <summary>
        /// Gets the TLS address of the specified thread and slot index.
        /// </summary>
        /// <param name="threadHandle"></param>
        /// <param name="slotIndex"></param>
        /// <returns></returns>
        public uint GetTlsAddress(IntPtr threadHandle, int slotIndex = 0)
        {
            Kernel32.SuspendThread(threadHandle);
            ThreadContext context = Kernel32.GetThreadContext(threadHandle);
            Kernel32.ResumeThread(threadHandle);
            LdtEntry ldt = Kernel32.GetThreadSelectorEntry(threadHandle, context.SegFs);

            uint tlsArrayPtr = ldt.BaseAddress + 0x2C;
            uint tlsArrayAddress = Memory.ReadUInt32(tlsArrayPtr);
            return Memory.ReadUInt32(tlsArrayAddress + slotIndex * sizeof(uint));
        }

        /// <summary>
        /// Searches for a process by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Process GetProcessByName(string name)
        {
            Process[] processes = Process.GetProcessesByName(name);
            switch (processes.Length)
            {
                case 0: throw new ArgumentException($"The {name} process was not found.");
                case 1: return processes[0];
                default: throw new Exception($"Multiple {name} processes found.");
            }
        }

        #endregion
    }
}
