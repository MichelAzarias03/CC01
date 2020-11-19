using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC01.WinForms
{
    class ListEcolePrint
    {
        public string Nom { get; set; }
        public string Identifiant { get; set; }
        public byte[] Logo { get; set; }

        public ListEcolePrint(string nom,string identifiant, byte[] logo)
        {
            Nom = nom;
            Identifiant = identifiant;
            Logo = logo;
        }
    }
}
