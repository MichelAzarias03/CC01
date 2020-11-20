using CC01.BO;
using CC01.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CC01.WinForms
{
    public partial class FormListEcole : Form
    {
        private EcoleBLO ecoleBLO;
        public FormListEcole()
        {
            InitializeComponent();
            ecoleBLO = new EcoleBLO(ConfigurationManager.AppSettings["DbFolder"]);
        }
        private void loadData()
        {
            string value = txtSearch.Text.ToLower();
            var ecoles = ecoleBLO.GetEcole();
        }
        private void btnAnnuler_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnCreer_Click(object sender, EventArgs e)
        {
            Form form = new FormEditEcole(loadData);
            form.Show();
        }

        private void FormListEcole_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void txtRecherch_TextChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnModifier_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
                {
                    Form f = new FormEditEcole
                    (
                    );
                    f.ShowDialog();
                }
            }
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnModifier_Click(sender, e);
        }

        private void btnSupprimer_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                if (
                    MessageBox.Show
                    (
                        "Do you really want to delete this school(s)?",
                        "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question
                    ) == DialogResult.Yes
                )
                {
                    for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
                    {
                        ecoleBLO.DeleteEcole(dataGridView1.SelectedRows[i].DataBoundItem as Ecole);
                    }
                    loadData();
                }
            }
        }

        private void btnImprimer_Click(object sender, EventArgs e)
        {
            List<ListEcolePrint> items = new List<ListEcolePrint>();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                Ecole ecole = dataGridView1.Rows[i].DataBoundItem as Ecole;
                items.Add
               (
                   new ListEcolePrint
                   (
                       ecole.Nom,
                       ecole.Identifiant,
                       !string.IsNullOrEmpty(ecole.Logo) ? File.ReadAllBytes(ecole.Logo) : null
                   )
               );
            }
            Form f = new FormPreview("ProductListRpt.rdlc", items);
            f.Show();
        }
        
    }
}
