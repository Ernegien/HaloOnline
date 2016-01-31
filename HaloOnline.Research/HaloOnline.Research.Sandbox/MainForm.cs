using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using HaloOnline.Research.Core.Imports;
using HaloOnline.Research.Core.Imports.Types;
using HaloOnline.Research.Core.Logging;
using HaloOnline.Research.Core.Runtime;
using HaloOnline.Research.Core.Utilities;

namespace HaloOnline.Research.Sandbox
{
    public partial class MainForm : Form
    {
        public const string GameName = "eldorado";   //darkloaded

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {          
            using (GameProcess game = new GameProcess(GameName))
            {

                //LogProcessInfo(game);

                //ProcessScanner scanner = new ProcessScanner(game.Process);
                //var results = scanner.SimpleScan(new byte[] { 0x8B, 0x87, 0x60, 0x02, 0x00, 0x00, 0x6A});

                Globals(game);
                //Tags(game);
                //Research(game);
            }
        }

        private void LogProcessInfo(GameProcess game)
        {
            List<ProcessModule> modules = game.Process.Modules.Cast<ProcessModule>().OrderBy(x => x.BaseAddress.ToInt64()).ToList();
            foreach (ProcessModule module in modules)
            {
                Logger.Instance.Trace(string.Format("0x{0}-0x{1} ({2})",
                    Convert.ToString(module.BaseAddress.ToInt64(), 16).PadLeft(8, '0'),
                    Convert.ToString(module.BaseAddress.ToInt64() + module.ModuleMemorySize, 16).PadLeft(8, '0'),
                    module.ModuleName));
            }
        }

        private void Globals(GameProcess game)
        {
            // alpha

            var playerMappingGlobals = game.Memory.ReadUInt32(game.TlsAddress + 0x5C);


            var chudGlobals2 = game.Memory.ReadUInt32(game.TlsAddress + 0x420);
            var hudOpacity = game.Memory.ReadSingle(chudGlobals2 + 0x24C);

            var chudGlobals = game.Memory.ReadUInt32(game.TlsAddress + 0x424);
            var firstHudItemOpacity = game.Memory.ReadSingle(chudGlobals + 0x184);
            var secondHudItemOpacity = game.Memory.ReadSingle(chudGlobals + 0x188);

            var cortanaEffectChudGlobals = game.Memory.ReadUInt32(game.TlsAddress + 0x440);

            var chudWidgets1 = game.Memory.ReadUInt32(game.TlsAddress + 0x428);
            
            // aspect ratio

            var oldInfo = Kernel32.VirtualQueryEx(game.ProcessHandle, new UIntPtr(0x1691F00));
            Kernel32.VirtualProtectEx(game.ProcessHandle, new UIntPtr(0x1691F00), 0x1000, MemoryProtect.ReadWrite);


            game.Memory.WriteSingle(0x1691F00, 21.0f / 9);


            // ms30 stuff below
            //var randomMath = game.Memory.ReadUInt32(game.TlsAddress + 0x2AC); // contains single float 163029.641
            //var cinematicGlobals = game.Memory.ReadUInt32(game.TlsAddress + 0x6C);
            //var directorGlobals = game.Memory.ReadUInt32(game.TlsAddress + 0xC0);
            //var killTriggerVolumeState = game.Memory.ReadUInt32(game.TlsAddress + 0x1F8);
            //var directorScripting = game.Memory.ReadUInt32(game.TlsAddress + 0x238);
            //var objectGlobals = game.Memory.ReadUInt32(game.TlsAddress + 0x50);
            //var loopObjects = game.Memory.ReadUInt32(game.TlsAddress + 0x150);
            //var objects = game.Memory.ReadUInt32(game.TlsAddress + 0xC);
            //var objectMessagingQueue = game.Memory.ReadUInt32(game.TlsAddress + 0x23C);
            //var objectNameList = game.Memory.ReadUInt32(game.TlsAddress + 0x1F0);
            //var resourceGlobals = game.Memory.ReadUInt32(game.TlsAddress + 0x2D8);


        }

        private void Tags(GameProcess game)
        {
            for (uint i = 0; i < 100; i++)
            {
                var tag = game.TagCache.GetTag(i);
            }
        }

        private void Research(GameProcess game)
        {
            var fmodPatchAddress = new DefaultDictionary<GameVersion, uint>(game.Version)
            {
                [GameVersion.Alpha] = ModuleAddress.FromImageAddress(game.ModuleContext, 0x140DA75),
                [GameVersion.Latest] = ModuleAddress.FromImageAddress(game.ModuleContext, 0xFAA9E5)
            };
            game.Memory.WriteByte(fmodPatchAddress, 0x2);
        }
    }
}
