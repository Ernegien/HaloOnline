using System;
using System.Windows.Forms;

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
            var servers = Properties.Settings.Default.UpdateServers;
            foreach (var server in servers)
            {
                
            }



        }
    }
}
