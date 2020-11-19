using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CC01.WinForms
{
    public partial class FormParent : Form
    {
        public FormParent()
        {
            InitializeComponent();
        }

        private void fonctionnalitésToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void etudiantToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new FormListEtudiant();
            form.Show();
        }

        private void ecolesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new FormListEcole();
            form.Show();
        }

        private void etudiantToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form form = new FormEditEtudiant();
            form.Show();
        }

        private void ecoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new FormEditEcole();
            form.Show();
        }
    }
}
