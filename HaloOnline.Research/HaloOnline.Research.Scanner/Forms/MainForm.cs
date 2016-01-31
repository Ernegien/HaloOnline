using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HaloOnline.Research.Core.Runtime;
using HaloOnline.Research.Core.Utilities;

namespace HaloOnline.Research.Scanner.Forms
{
    // TODO: give the next iteration the ability to reconnect to different processes without crashing/restarting the scanning program
    // TODO: provide more advanced search options, save/load features, image/process address views

    public partial class MainForm : Form
    {
        private GameProcess Game { get; set; }
        private ProcessScanner Scanner { get; set; }

        public MainForm()
        {
            InitializeComponent();
        }

        public static byte[] HexStringToByteArray(string hex)
        {
            hex = hex.Replace(" ", string.Empty);
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var processSelection = new ProcessSelectionForm();
            if (processSelection.ShowDialog() == DialogResult.OK)
            {
                Game = new GameProcess(processSelection.ProcessName);
                Scanner = new ProcessScanner(Game.Process);
            }
            else Application.Exit();
        }

        private void btnScan_Click(object sender, EventArgs e)
        {            
            txtResults.Clear();
            lblStatus.ForeColor = Color.Red;
            byte[] patternBytes;
            try
            {
                patternBytes = HexStringToByteArray(txtPattern.Text.Trim());
            }
            catch
            {
                lblStatus.Text = "Invalid pattern format.";
                return;
            }

            byte[] maskBytes = null;
            try
            {
                if (txtMask.TextLength > 0)
                {
                    maskBytes = HexStringToByteArray(txtMask.Text.Trim());
                }
            }
            catch
            {
                lblStatus.Text = "Invalid mask format.";
                return;
            }

            if (maskBytes != null && patternBytes.Length != maskBytes.Length)
            {
                lblStatus.Text = "Mask length must match pattern length when specified.";
                return;
            }

            // TODO: in the better implementation check for too short of a pattern, or not enough mask bits, issuing a warning that too many results may be returned

            var results = Scanner.SimpleScan(patternBytes, maskBytes);
            StringBuilder sb = new StringBuilder();
            foreach (var result in results)
            {
                uint imageAddress = ModuleAddress.ToImageAddress(Game.ModuleContext, result);
                uint processAddress = result;

                sb.AppendLine(string.Format("0x{0} | 0x{1}", Convert.ToString(imageAddress, 16).ToUpperInvariant().PadLeft(8, '0'),
                    Convert.ToString(processAddress, 16).ToUpperInvariant().PadLeft(8, '0')));
            }
            txtResults.Text = sb.ToString();

            lblStatus.ResetForeColor();
            lblStatus.Text = $"{results.Count} result{(results.Count == 1 ? string.Empty : "s")} found.";
        }
    }
}
