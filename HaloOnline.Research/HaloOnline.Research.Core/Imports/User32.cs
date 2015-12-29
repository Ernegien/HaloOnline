using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace HaloOnline.Research.Core.Imports
{
    /// <summary>
    /// Unmanaged User32 imports.
    /// </summary>
    public class User32
    {
        #region Unmanaged

        /// <summary>
        /// Retrieves the identifier of the thread that created the specified window.
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="processId"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "GetWindowThreadProcessId", SetLastError = true)]
        private static extern uint UnmanagedGetWindowThreadProcessId(IntPtr hWnd, IntPtr processId);

        /// <summary>
        /// Retrieves the status of the specified virtual key.
        /// </summary>
        /// <param name="nVirtKey"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "GetKeyState", SetLastError = true)]
        private static extern short UnmanagedGetKeyState(int nVirtKey);

        #endregion

        #region Managed

        /// <summary>
        /// Retrieves the identifier of the thread that created the specified window.
        /// </summary>
        /// <param name="windowHandle"></param>
        /// <returns></returns>
        public static uint GetWindowThreadProcessId(IntPtr windowHandle)
        {
            uint id = UnmanagedGetWindowThreadProcessId(windowHandle, IntPtr.Zero);
            if (id == 0)
            {
                throw new Win32Exception();
            }
            return id;
        }

        /// <summary>
        /// Retrieves the status of the specified virtual key.
        /// </summary>
        /// <param name="virtualKey"></param>
        /// <returns></returns>
        public static short GetKeyState(int virtualKey)
        {
            return UnmanagedGetKeyState(virtualKey);
        }

        #endregion
    }
}
