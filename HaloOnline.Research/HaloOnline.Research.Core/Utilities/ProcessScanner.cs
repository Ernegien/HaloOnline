using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace HaloOnline.Research.Core.Utilities
{
    /// <summary>
    /// 
    /// </summary>
    public class ProcessScanner
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Process Process { get; }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private ProcessStream Memory { get; }

        public ProcessScanner(Process process)
        {
            if (process == null)
                throw new ArgumentNullException();

            Process = process;
            Memory = new ProcessStream(process.Handle);
        }

        // TODO: this is just to get me on my feet researching, will replace with something far more advanced later
        /// <summary>
        /// Performs a pattern scan through just the main process address space.
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="patternMask"></param>
        /// <returns></returns>
        public List<uint> SimpleScan(byte[] pattern, byte[] patternMask = null)
        {
            if (pattern == null)
                throw new ArgumentNullException(nameof(pattern));

            if (patternMask != null && pattern.Length != patternMask.Length)
                throw new ArgumentException("If a pattern mask is specified its length must be equal to the pattern length.");

            List<uint> results = new List<uint>();

            // get some info about the main module's memory and read it all into one giant buffer of love
            uint startAddress = (uint)Process.MainModule.BaseAddress.ToInt32();
            uint endAddress = startAddress + (uint)Process.MainModule.ModuleMemorySize;   // TODO: optimize this to only search through the text segment for code
            uint size = endAddress - startAddress;
            byte[] mainModuleMemory = Memory.ReadBytes(startAddress, (int)size);

            // scan for pattern
            for (int i = 0; i < size - pattern.Length; i++)
            {
                for (int j = 0; j < pattern.Length; j++)
                {
                    byte maskByte = patternMask?[j] ?? byte.MaxValue;

                    // break early if it's not a match
                    if ((mainModuleMemory[i + j] & maskByte) != (pattern[j] & maskByte))
                    {
                        break;
                    }

                    // otherwise keep going if it's not a full match
                    if (j < pattern.Length - 1) continue;

                    // full match
                    results.Add((uint)(startAddress + i));

                    // skip ahead the pattern length
                    // TODO: provide the option for the caller to search for overlapping patterns in the advanced implementation
                    i += j;
                }
            }

            return results;
        }
    }
}