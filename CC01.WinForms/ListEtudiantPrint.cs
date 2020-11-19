using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC01.WinForms
{
    class ListEtudiantPrint
    {
        public string Matricule { get; set; }

        public string Nom { get; set; }

        public string Prenom { get; set; }

        public DateTime DateNaissance { get; set; }

        public string LieuNaissance { get; set; }

        public byte[] CarteEtudiant { get; set; }

        public string Email { get; set; }
        public int Contact { get; set; }
        public string Identifiant { get; set; }

        public byte[] Logo { get; set; }

        public string NomEcole { get; set; }

        public ListEtudiantPrint(string matricule, string nom, string prenom, DateTime dateNaissance,string lieuNaissance,
            byte[] carteEtudiant, string email, int contact, string identifiant, byte[] logo, string nomEcole)
        {
            Matricule = matricule;
            Nom = nom;
            Prenom = prenom;
            DateNaissance = dateNaissance;
            LieuNaissance = lieuNaissance;
            CarteEtudiant = carteEtudiant;
            Email = email;
            Contact = contact;
            Identifiant = identifiant;
            Logo = logo;
            NomEcole = nomEcole;
        }

        public ListEtudiantPrint(string matricule, string nom, string prenom, DateTime dateNaissance, string lieuNaissance, byte[] carteEtudiant, string email, int contact, object identifiant, object p, byte[] vs)
        {
            Matricule = matricule;
            Nom = nom;
            Prenom = prenom;
            DateNaissance = dateNaissance;
            LieuNaissance = lieuNaissance;
            CarteEtudiant = carteEtudiant;
            Email = email;
            Contact = contact;
        }
    }
}
