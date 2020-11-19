using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC01.BO
{
    [Serializable]
    public class Ecole
    {

        public string Identifiant { get; set; }

        public byte[] Logo { get; set; }

        public string Nom { get; set; }

        public Ecole(string identifiant, byte[] logo, string nom)
        {
            Identifiant = identifiant;
            Logo = logo;
            Nom = nom;
        }

        public override bool Equals(object obj)
        {
            return obj is Ecole ecole &&
                   Identifiant == ecole.Identifiant;
        }

        public override int GetHashCode()
        {
            return 574969646 + EqualityComparer<string>.Default.GetHashCode(Identifiant);
        }
    }
}
