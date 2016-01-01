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
                Globals(game);
                Tags(game);
            }
        }

        private void Globals(GameProcess game)
        {
            var randomMath = game.MemoryStream.ReadUInt32(game.TlsAddress + 0x2AC); // contains single float 163029.641
            var cinematicGlobals = game.MemoryStream.ReadUInt32(game.TlsAddress + 0x6C);
            var directorGlobals = game.MemoryStream.ReadUInt32(game.TlsAddress + 0xC0);
            var killTriggerVolumeState = game.MemoryStream.ReadUInt32(game.TlsAddress + 0x1F8);
            var directorScripting = game.MemoryStream.ReadUInt32(game.TlsAddress + 0x238);
            var objectGlobals = game.MemoryStream.ReadUInt32(game.TlsAddress + 0x50);
            var loopObjects = game.MemoryStream.ReadUInt32(game.TlsAddress + 0x150);
            var objects = game.MemoryStream.ReadUInt32(game.TlsAddress + 0xC);
            var objectMessagingQueue = game.MemoryStream.ReadUInt32(game.TlsAddress + 0x23C);
            var objectNameList = game.MemoryStream.ReadUInt32(game.TlsAddress + 0x1F0);



        }

        private void Tags(GameProcess game)
        {
            for (uint i = 0; i < 100; i++)
            {
                var tag = game.TagCache.GetTag(i);
            }
        }
    }
}
