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
        public string Nom { get; set; }

        public string Prenom {get; set;}

        public string Matricule { get; set; }

        public byte [] CarteEtudiant { get; set; }

        public Etudiant(string nom, string prenom, string matricule, byte[] carteEtudiant)
        {
            Nom = nom;
            Prenom = prenom;
            Matricule = matricule;
            CarteEtudiant = carteEtudiant;
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
