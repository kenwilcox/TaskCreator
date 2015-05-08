using System;
using System.Windows.Forms;
using TaskCreator;

namespace TaskCreatorDemo
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            Maker.MakeLocalTaskFromSource(fctb.Text);
            if (Maker.Error)
                MessageBox.Show(Maker.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                MessageBox.Show(Maker.Message, @"Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }
}
