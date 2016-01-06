using System;
using System.Diagnostics;
using System.Windows.Forms;
using HaloOnline.Research.Core.Runtime;
using HaloOnline.Research.Core.Utilities;

namespace HaloOnline.Research.Sandbox
{
    public partial class MainForm : Form
    {
        public const string GameName = "darkloaded";   //eldorado

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            using (GameProcess game = new GameProcess(GameName))
            {
                GameScanner scanner = new GameScanner(game);
                var results = scanner.SimpleScan(new byte[] { 0x8B, 0x87, 0x60, 0x02, 0x00, 0x00, 0x6A});

                Globals(game);
                Tags(game);
                Research(game);
            }
        }

        private void Globals(GameProcess game)
        {
            // ms30 stuff below
            var randomMath = game.Memory.ReadUInt32(game.TlsAddress + 0x2AC); // contains single float 163029.641
            var cinematicGlobals = game.Memory.ReadUInt32(game.TlsAddress + 0x6C);
            var directorGlobals = game.Memory.ReadUInt32(game.TlsAddress + 0xC0);
            var killTriggerVolumeState = game.Memory.ReadUInt32(game.TlsAddress + 0x1F8);
            var directorScripting = game.Memory.ReadUInt32(game.TlsAddress + 0x238);
            var objectGlobals = game.Memory.ReadUInt32(game.TlsAddress + 0x50);
            var loopObjects = game.Memory.ReadUInt32(game.TlsAddress + 0x150);
            var objects = game.Memory.ReadUInt32(game.TlsAddress + 0xC);
            var objectMessagingQueue = game.Memory.ReadUInt32(game.TlsAddress + 0x23C);
            var objectNameList = game.Memory.ReadUInt32(game.TlsAddress + 0x1F0);
            var resourceGlobals = game.Memory.ReadUInt32(game.TlsAddress + 0x2D8);

            
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
            var fmodPatchAddress = new DefaultDictionary<GameVersion, ProcessAddress>(game.Version)
            {
                [GameVersion.Alpha] = new ProcessAddress(0x140DA75),
                [GameVersion.Latest] = new ProcessAddress(0xFAA9E5)
            };
            game.Memory.WriteByte((ProcessAddress)fmodPatchAddress, 0x2);
        }
    }
}
