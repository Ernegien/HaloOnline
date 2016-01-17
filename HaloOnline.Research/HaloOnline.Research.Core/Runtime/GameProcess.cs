using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using HaloOnline.Research.Core.Imports;
using HaloOnline.Research.Core.Imports.Structs;
using HaloOnline.Research.Core.Imports.Types;
using HaloOnline.Research.Core.Utilities;

namespace HaloOnline.Research.Core.Runtime
{
    public class GameProcess : IDisposable
    {
        #region Properties

        /// <summary>
        /// The process name.
        /// </summary>
        public string Name => Process.ProcessName;

        /// <summary>
        /// The build date.
        /// </summary>
        public DateTime BuildDate { get; private set; }

        /// <summary>
        /// The engine version.
        /// </summary>
        public GameVersion Version
        {
            get
            {
                switch (BuildDate.Ticks)
                {
                    case 0x08d23130cd95d080: return GameVersion.Alpha;
                    case 0x08d2f4ee77aecf00: return GameVersion.Latest;
                    default: return GameVersion.Unknown;
                }
            }
        }
       
        /// <summary>
        /// The image base address.
        /// </summary>
        public uint ImageBaseAddress { get; private set; }

        /// <summary>
        /// The process base address.
        /// </summary>
        public uint ProcessBaseAddress => (uint)Process.MainModule.BaseAddress.ToInt32();

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
        public ProcessStream Memory { get; private set; }

        /// <summary>
        /// Tag data cached in memory.
        /// </summary>
        public TagCache TagCache { get; private set; }

        /// <summary>
        /// A collection of global addresses.
        /// </summary>
        public GameAddresses Addresses { get; private set; }

        #endregion

        #region Initialization & Disposal

        /// <summary>
        /// Provides access to the game process.
        /// </summary>
        /// <param name="name">The process name.</param>
        public GameProcess(string name)
        {
            Initialize(GetProcessByName(name));
        }

        /// <summary>
        /// Provides access to the game process.
        /// </summary>
        /// <param name="process">The process.</param>
        public GameProcess(Process process)
        {
            Initialize(process);
        }

        /// <summary>
        /// Initializes core components.
        /// </summary>
        private void Initialize(Process process)
        {
            // get process info
            Process = process;
            ProcessHandle = Kernel32.OpenProcess(ProcessAccessFlags.All, false, (uint)Process.Id);
            MainThreadId = User32.GetWindowThreadProcessId(Process.MainWindowHandle);
            MainThreadHandle = Kernel32.OpenThread(ThreadAccessFlags.All, false, MainThreadId);

            // look away! - get original image base address and build time from PE header - http://blogs.msdn.com/b/kstanton/archive/2004/03/31/105060.aspx
            using (FileStream fs = new FileStream(Process.MainModule.FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (BinaryReader br = new BinaryReader(fs))
            {
                fs.Position = Marshal.OffsetOf(typeof(ImageDosHeader), nameof(ImageDosHeader.e_lfanew)).ToInt32();
                int ntHeaderOffset = br.ReadInt32();
                int fileHeaderOffset = Marshal.OffsetOf(typeof(ImageNtHeaders32), nameof(ImageNtHeaders32.FileHeader)).ToInt32();
                fs.Position = ntHeaderOffset + fileHeaderOffset + Marshal.OffsetOf(typeof(ImageFileHeader32), nameof(ImageFileHeader32.TimeDateStamp)).ToInt32();
                DateTime unixEpoch = new DateTime(1970, 1, 1);
                BuildDate = unixEpoch + new TimeSpan(br.ReadUInt32() * TimeSpan.TicksPerSecond);
                int fileHeaderSize = Marshal.SizeOf(typeof(ImageFileHeader32));
                int imageBaseOffset = Marshal.OffsetOf(typeof(ImageOptionalHeader32), nameof(ImageOptionalHeader32.ImageBase)).ToInt32();
                fs.Position = ntHeaderOffset + fileHeaderOffset + fileHeaderSize + imageBaseOffset;
                ImageBaseAddress = br.ReadUInt32();
            }

            if (Version == GameVersion.Unknown)
                throw new NotSupportedException("Unknown game version.");

            // initialize access to various sub-systems
            ProcessAddress.Initialize(ImageBaseAddress, ProcessBaseAddress);
            Memory = new ProcessStream(ProcessHandle);
            TlsAddress = GetTlsAddress(MainThreadHandle);
            TagCache = new TagCache(this);
            Addresses = new GameAddresses(this);
        }

        /// <summary>
        /// Disposes of and then clears the cached values and re-initializes the core components.
        /// </summary>
        public void Reset()
        {
            Dispose();
            Initialize(Process);
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

        public List<Range<uint>> GetMemoryRegions(Range<uint> searchRange = null, uint pageSize = 0x1000)
        {
            if (searchRange == null)
                searchRange = new Range<uint>(uint.MinValue, uint.MaxValue);

            List<Range<uint>> addressRanges = new List<Range<uint>>();

            uint offset = searchRange.Minimum;
            while (offset < searchRange.Maximum - pageSize)
            {
                try
                {
                    var memoryInfo = Kernel32.VirtualQueryEx(ProcessHandle, new IntPtr(offset));
                    uint startAddress = (uint)memoryInfo.BaseAddress.ToInt64();
                    uint size = memoryInfo.RegionSize;
                    uint endAddress = startAddress + size;

                    // only count committed memory
                    if (memoryInfo.State == MemoryState.Commit)
                    {
                        // combine current range with previous if they border eachother
                        if (addressRanges.Count > 0 && addressRanges.Last().Maximum == startAddress)
                        {
                            uint previousStartAddress = addressRanges.Last().Minimum;
                            addressRanges.RemoveAt(addressRanges.Count - 1);
                            addressRanges.Add(new Range<uint>(previousStartAddress, endAddress));
                        }
                        else
                        {
                            addressRanges.Add(new Range<uint>(startAddress, endAddress));
                        }
                    }

                    // skip past the memory region found
                    offset += size;
                }
                catch
                {
                    offset += pageSize;
                }
            }

            return addressRanges;
        } 

        #endregion
    }
}
