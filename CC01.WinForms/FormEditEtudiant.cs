using System;
using CC01.BO;
using CC01.DAL;
using System.IO;
using System.Configuration;
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
    public partial class FormEditEtudiant : Form
    {
        private Action callBack;
        private Etudiant oldEtudiant;
        public FormEditEtudiant()
        {
            InitializeComponent();
        }
        public FormEditEtudiant(Action callBack):this()
        {
            this.callBack = callBack;
        }
        public FormEditEtudiant(Etudiant etudiant, Action callBack):this(callBack)
        {
            this.oldEtudiant = etudiant;
            txtNom.Text = etudiant.Nom;
            txtPrenom.Text = etudiant.Prenom;
            txtMatricule.Text = etudiant.Matricule;
            txtLieu.Text = etudiant.LieuNaissance;
            txtEmail.Text = etudiant.Email;
            txtContact.Text = etudiant.Contact.ToString();
            if (etudiant.CarteEtudiant != null)
                pictureBox1.Image = Image.FromStream(new MemoryStream(etudiant.CarteEtudiant));
        }

        private void FormEditEtudiant_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnEnregistrer_Click(object sender, EventArgs e)
        {
            try
            {
                checkForm();

                Etudiant newEtudiant=new Etudiant
                (
                    txtMatricule.Text.ToUpper(),
                    txtNom.Text,
                    txtPrenom.Text,
                    txtLieu.Text,
                    !string.IsNullOrEmpty(pictureBox1.ImageLocation) ? File.ReadAllBytes(pictureBox1.ImageLocation) : this.oldEtudiant?.CarteEtudiant,
                    txtEmail.Text,
                    int.Parse(txtContact.Text),
                    dateTimePicker1.Value.Date
                );

                EtudiantBLO etudiantBLO = new EtudiantBLO(ConfigurationManager.AppSettings["DbFolder"]);

                if (this.oldEtudiant == null)
                    etudiantBLO.CreateEtudiant(newEtudiant);
                else
                    etudiantBLO.EditEtudiant(oldEtudiant, newEtudiant);

                MessageBox.Show
                (
                    "Save done !",
                    "Confirmation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                if (callBack != null)
                    callBack();

                if (oldEtudiant != null)
                    Close();

                txtMatricule.Clear();
                txtNom.Clear();
                txtPrenom.Clear();
                txtEmail.Clear();
                txtContact.Clear();
                txtLieu.Clear();

                txtMatricule.Focus();

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
            catch (DuplicateNameException ex)
            {
                MessageBox.Show
               (
                   ex.Message,
                   "Duplicate error",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Warning
               );
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show
               (
                   ex.Message,
                   "Not found error",
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
            txtMatricule.BackColor = Color.White;
            txtNom.BackColor = Color.White;
            if (string.IsNullOrWhiteSpace(txtMatricule.Text))
            {
                text += "- Please enter the reference ! \n";
                txtMatricule.BackColor = Color.Pink;
            }
            if (string.IsNullOrWhiteSpace(txtNom.Text))
            {
                text += "- Please enter the name ! \n";
                txtNom.BackColor = Color.Pink;
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

        private void btnCodeBar_Click(object sender, EventArgs e)
        {
            Zen.Barcode.CodeQrBarcodeDraw codeQr = Zen.Barcode.BarcodeDrawFactory.CodeQr;
            pictureBox2.Image = codeQr.Draw(txtMatricule.Text, 25);
            //Zen.Barcode.Code128BarcodeDraw barcode = Zen.Barcode.BarcodeDrawFactory.Code128WithChecksum;
            //pictureBox2.Image = barcode.Draw(txtMatricule.Text, 50);
        }
    }
}
