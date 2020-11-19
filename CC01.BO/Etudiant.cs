using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC01.BO
{
    [Serializable]
    class Etudiant
    {
        public string Matricule { get; set; }

        public string Nom { get; set; }

        public string Prenom {get; set;}

        public DateTime DateNaissance { get; set; }

        public string LieuNaissance { get; set; }

        public byte [] CarteEtudiant { get; set; }

        public string Email { get; set; }

        public int Contact { get; set; }

       

        public Etudiant(string matricule, string nom, string prenom, string lieuNaissance, 
            byte[] carteEtudiant, string email, int contact, DateTime dateNaissance)
        {
            Matricule = matricule;
            Nom = nom;
            Prenom = prenom;
            LieuNaissance = lieuNaissance;
            CarteEtudiant = carteEtudiant;
            Email = email;
            Contact = contact;
            DateNaissance = dateNaissance;
        }

        public override bool Equals(object obj)
        {
            return obj is Etudiant etudiant &&
                   Matricule == etudiant.Matricule;
        }

        public override int GetHashCode()
        {
            return 797189699 + EqualityComparer<string>.Default.GetHashCode(Matricule);
        }
    }
}
