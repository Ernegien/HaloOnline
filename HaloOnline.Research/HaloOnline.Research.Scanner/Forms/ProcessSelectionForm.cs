using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace HaloOnline.Research.Scanner.Forms
{
    public partial class ProcessSelectionForm : Form
    {
        public string ProcessName { get; private set; }

        public ProcessSelectionForm()
        {
            InitializeComponent();

            var processes = Process.GetProcesses().OrderBy(process => process.ProcessName);
            foreach (var process in processes)
            {
                lbProcesses.Items.Add(process.ProcessName);
            }
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            ProcessName = (string)lbProcesses.SelectedItem;
            DialogResult = DialogResult.OK;
        }

        private void lbProcesses_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            btnOk.Enabled = true;
        }
    }
}
