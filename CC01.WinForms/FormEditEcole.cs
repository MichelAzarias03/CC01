using System;
using CC01.BO;
using CC01.DAL;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CC01.WinForms
{
    public partial class FormEditEcole : Form
    {
        private Action callBack;
        private EcoleBLO ecoleBLO;
        private Ecole oldEcole;
        public FormEditEcole()
        {
            InitializeComponent();
            ecoleBLO = new EcoleBLO(ConfigurationManager.AppSettings["DbFolder"]);
            oldEcole = ecoleBLO.GetEcole();
            if (oldEcole != null)
            {
                txtNom.Text = oldEcole.Nom;
                txtIdentifiant.Text = oldEcole.Identifiant;
                pictureBox1.ImageLocation = oldEcole.Logo;
            }
        }
        public FormEditEcole(Action callBack):this()
        {
            this.callBack = callBack;
        }

        public FormEditEcole(Ecole ecole, Action callback):this(callback)
        {
            this.oldEcole = ecole;
            txtIdentifiant.Text = ecole.Identifiant;
            txtNom.Text = ecole.Nom;
            if (ecole.Logo != null)
                pictureBox1.Image = Image.FromStream(new MemoryStream(1));
        }

        private void btnEnregistrer_Click(object sender, EventArgs e)
        {
            try
            {
                checkForm();

                Ecole newEcole = new Ecole
                (
                    txtNom.Text.ToUpper(),
                    txtIdentifiant.Text,
                    pictureBox1.ImageLocation
                );

                ecoleBLO.CreateEcole(oldEcole, newEcole);

                MessageBox.Show
                (
                    "Save done !",
                    "Confirmation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                Close();
            }
            catch (TypingException ex)
            {
                MessageBox.Show
               (
                   ex.Message,
                   "Typing error",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Warning
               );
            }
            catch (Exception ex)
            {
                ex.WriteToFile();
                MessageBox.Show
               (
                   "An error occurred! Please try again later.",
                   "Erreur",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Error
               );
            }
        }
        private void checkForm()
        {
            string text = string.Empty;
            txtNom.BackColor = Color.White;
            txtIdentifiant.BackColor = Color.White;
            if (string.IsNullOrWhiteSpace(txtNom.Text))
            {
                text += "- Please enter the name ! \n";
                txtIdentifiant.BackColor = Color.Pink;
            }

            if (!string.IsNullOrEmpty(text))
                throw new TypingException(text);
        }

        private void btnAnnuler_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Choose a picture";
            ofd.Filter = "Image files|*.jpg;*.jpeg;*.png;*.gif";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.ImageLocation = ofd.FileName;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pictureBox1.ImageLocation = null;
        }
    }
}
