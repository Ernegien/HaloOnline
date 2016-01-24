using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using HaloOnline.Research.Core.Imports;
using HaloOnline.Research.Core.Imports.Structs;
using HaloOnline.Research.Core.Imports.Types;
using HaloOnline.Research.Core.Utilities;

namespace HaloOnline.Research.Launcher.Forms
{
    public partial class MainForm : Form
    {
        private const string AlphaBuildName = "eldorado.exe";
        private const string ReleaseBuildName = "halo_online.exe";

        public string HaloExePath { get; private set; }

        public MainForm()
        {
            InitializeComponent();

            cmbBuild.Items.Add("Current");
            cmbBuild.SelectedIndex = 0;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LocateHaloExecutable();

            // TODO: detect current build

            // TODO: populate available builds

        }

        private void LocateHaloExecutable()
        {
            bool exitRequested = false;

            // first check for eldorado or halo_online.exe in the current directory
            HaloExePath = FindHaloExeInDirectory(Environment.CurrentDirectory);

            // next check for what's specified in the app config
            if (HaloExePath == null && File.Exists(Properties.Settings.Default.DefaultHaloExePath))
            {
                HaloExePath = Properties.Settings.Default.DefaultHaloExePath;
            }

            // if automatic detection fails, keep prompting user to locate it manually until they find it or give up
            while (HaloExePath == null && !exitRequested)
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Filter = $"Halo Online (*.exe)|{AlphaBuildName};{ReleaseBuildName}";

                    var result = ofd.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        if (IsValidHaloExeName(Path.GetFileName(ofd.FileName)))
                        {
                            HaloExePath = ofd.FileName;
                        }
                        else
                        {
                            MessageBox.Show($"Please find {AlphaBuildName} or {ReleaseBuildName} or click Cancel to exit.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else if (result == DialogResult.Cancel)
                    {
                        MessageBox.Show("Unable to locate Halo Online executable. Click OK to exit.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        exitRequested = true;
                        Application.Exit();
                    }
                }
            }
        }

        /// <summary>
        /// Checks if the specified name is a valid Halo Online executable name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private bool IsValidHaloExeName(string name)
        {
            return name.Equals(AlphaBuildName, StringComparison.InvariantCultureIgnoreCase) ||
                   name.Equals(ReleaseBuildName, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Finds the Halo executable path in the specified directory.
        /// </summary>
        /// <param name="directory">The directory to search in.</param>
        /// <returns>Returns the path if a single instance is found, otherwise null.</returns>
        private string FindHaloExeInDirectory(string directory)
        {
            bool alphaBuildFound = File.Exists(Path.Combine(directory, AlphaBuildName));
            bool releaseBuildFound = File.Exists(Path.Combine(directory, ReleaseBuildName));

            if (alphaBuildFound && releaseBuildFound)
            {
                MessageBox.Show("Please ensure there is only one Halo Online executable in the directory specified.", "Multiple instances found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            if (alphaBuildFound)
            {
                return Path.Combine(directory, AlphaBuildName);
            }

            if (releaseBuildFound)
            {
                return Path.Combine(directory, ReleaseBuildName);
            }
            
            return null;
        }

        private Tuple<uint, uint> CreateSuspendedProcess(string filePath, string arguments = "")
        {
            ProcessInformation processInfo;
            ProcessStartupInfo startupInfo = new ProcessStartupInfo();

            Kernel32.CreateProcess(filePath + " " + arguments, ProcessCreationFlags.CreateSuspended, Path.GetDirectoryName(filePath), ref startupInfo, out processInfo);

            return new Tuple<uint, uint>(processInfo.ProcessId, processInfo.ThreadId);
        }

        private void InjectDll(uint processId, string dllPath)
        {
            var targetProcessHandle = Kernel32.OpenProcess(ProcessAccessFlags.All, false, processId);
            var loadLibraryExportAddress = Kernel32.GetProcAddress(Kernel32.GetModuleHandle("kernel32.dll"), "LoadLibraryA");
            var dllMemoryAddress = Kernel32.VirtualAllocEx(targetProcessHandle, UIntPtr.Zero, (uint) dllPath.Length,
                MemoryAllocationType.Reserve | MemoryAllocationType.Commit, MemoryProtect.ReadWrite);

            using (ProcessStream ps = new ProcessStream(targetProcessHandle))
            {
                ps.Write((uint)dllMemoryAddress.ToUInt64(), Encoding.Default.GetBytes(dllPath));
            }

            Kernel32.CreateRemoteThread(targetProcessHandle, loadLibraryExportAddress, dllMemoryAddress);
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            Tuple<uint, uint> pInfo = CreateSuspendedProcess(HaloExePath, "--account 123 --sign-in-code 123 -width 1280 -height 720 -window");
            InjectDll(pInfo.Item1, Path.Combine(Environment.CurrentDirectory, Properties.Settings.Default.ExtensionLibraryName));
            Kernel32.ResumeThread(pInfo.Item2);
        }
    }
}
