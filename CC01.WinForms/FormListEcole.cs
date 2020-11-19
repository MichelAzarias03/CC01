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
            var ecoles = ecoleBLO.GetBy
            (
                x =>
                x.Identifiant.ToLower().Contains(value) ||
                x.Nom.ToLower().Contains(value)
            ).OrderBy(x => x.Identifiant).ToArray();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = ecoles;
            dataGridView1.ClearSelection();
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
                    Form f = new FormEcoleEdit
                    (
                        dataGridView1.SelectedRows[i].DataBoundItem as Product,
                        loadData
                    );
                    f.ShowDialog();
                }
            }
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEdit_Click(sender, e);
        }

        private void btnSupprimer_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                if (
                    MessageBox.Show
                    (
                        "Do you really want to delete this product(s)?",
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
            List<ListEcolePrint> listEcolePrints = new List<ListEcolePrint>();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                Ecole e = dataGridView1.Rows[i].DataBoundItem as Ecole;
                items.Add
               (
                   new ListEcolePrint
                   (
                       e.Nom,
                       e.Identifiant,
                       e.Logo,
                   )
               );
            }
            Form f = new FormPreview("ProductListRpt.rdlc", items);
            f.Show();
        }
        
    }
}
