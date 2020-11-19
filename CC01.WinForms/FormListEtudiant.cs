using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;
using System.Data;
using CC01.BLL;
using CC01.BO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CC01.WinForms
{
    public partial class FormListEtudiant : Form
    {
        private EtudiantBLO etudiantBLO;
        private EcoleBLO ecoleBLO;
        public FormListEtudiant()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            etudiantBLO = new EtudiantBLO(ConfigurationManager.AppSettings["DbFolder"]);
            ecoleBLO = new EcoleBLO(ConfigurationManager.AppSettings["DbFolder"]);
        }

        private void btnAnnuler_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void loadData()
        {
            string value = txtSearch.Text.ToLower();
            var etudiants = productBLO.GetBy
            (
                x =>
                x.Matricule.ToLower().Contains(value) ||
                x.Nom.ToLower().Contains(value)
            ).OrderBy(x => x.Matricule).ToArray();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = etudiants;
            dataGridView1.ClearSelection();
        }

        private void btnCreer_Click(object sender, EventArgs e)
        {
            Form form = new FormEditEtudiant(loadData);
            form.Show();
        }

        private void FormListEtudiant_Load(object sender, EventArgs e)
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
                    Form f = new FormProductEdit
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
                        etudiantBLO.DeleteEtudiant(dataGridView1.SelectedRows[i].DataBoundItem as Etudiant);
                    }
                    loadData();
                }
            }
        }

        private void btnImprimer_Click(object sender, EventArgs e)
        {
            List<ListEtudiantPrint> listEtudiantPrints = new List<ListEtudiantPrint>();
            Ecole ecole = ecoleBLO.GetEcole();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                Etudiant e = dataGridView1.Rows[i].DataBoundItem as Etudiant;
                items.Add
               (
                   new ListEtudiantPrint
                   (
                       e.Matricule,
                       e.Nom,
                       e.Prenom,
                       e.DateNaissance,
                       e.LieuNaissance,
                       e.CarteEtudiant,
                       e.Email,
                       e.Contact,
                       Ecole?.Identifiant,
                       Ecole?.NomEcole
                       !string.IsNullOrEmpty(ecole?.Logo) ? File.ReadAllBytes(ecole?.Logo) : null
                   )
               ) ;
            }
            Form f = new FormPreview("ProductListRpt.rdlc", items);
            f.Show();
        }
    }
}
