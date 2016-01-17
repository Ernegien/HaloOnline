using System;
using System.Text;
using System.Windows.Forms;
using HaloOnline.Research.Core.Imports;
using HaloOnline.Research.Core.Imports.Structs;
using HaloOnline.Research.Core.Imports.Types;
using HaloOnline.Research.Core.Utilities;
using HaloOnline.Research.Launcher.Properties;

namespace HaloOnline.Research.Launcher.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {


        }

        // TODO: revisit this, not sure why environment is being set via .net instead of within createprocess
        private uint CreateSuspendedProcess(string filePath, string arguments = "", string binDirectory = "")
        {
            if (!string.IsNullOrWhiteSpace(binDirectory))
            {
                var path = Environment.GetEnvironmentVariable("PATH") + $";{binDirectory}";
                Environment.SetEnvironmentVariable("PATH", path);
            }

            ProcessStartupInfo startupInfo = new ProcessStartupInfo();
            ProcessInformation processInfo;
            Kernel32.CreateProcess(filePath + " " + arguments, ProcessCreationFlags.CreateSuspended, null, ref startupInfo, out processInfo);

            return processInfo.ProcessId;
        }

        private void InjectDll(uint processId, string dllPath)
        {
            var targetProcessHandle = Kernel32.OpenProcess(ProcessAccessFlags.All, false, processId);
            var loadLibraryExportAddress = Kernel32.GetProcAddress(Kernel32.GetModuleHandle("kernel32.dll"), "LoadLibraryA");
            var dllMemoryAddress = Kernel32.VirtualAllocEx(targetProcessHandle, IntPtr.Zero, (uint) dllPath.Length,
                MemoryAllocationType.Reserve | MemoryAllocationType.Commit, MemoryProtect.ReadWrite);

            using (ProcessStream ps = new ProcessStream(targetProcessHandle))
            {
                ps.Write(dllMemoryAddress.ToInt64(), Encoding.Default.GetBytes(dllPath));
            }

            Kernel32.CreateRemoteThread(targetProcessHandle, loadLibraryExportAddress, dllMemoryAddress);
        }
    }
}
