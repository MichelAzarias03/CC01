using System;
using CC01.BO;
using CC01.DAL;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC01.BLL
{
    public class EtudiantBLO
    {
        EtudiantDAO etudiantRepo;
        public ProductBLO(string dbFolder)
        {
            etudiantRepo = new EtudiantDAO(dbFolder);
        }
        public void CreateEtudiant(Etudiant etudiant)
        {
            etudiantRepo.Add(etudiant);
        }

        public void DeleteEtudiant(Etudiant etudiant)
        {
            etudiantRepo.Remove(etudiant);
        }


        public IEnumerable<Etudiant> GetAllProducts()
        {
            return etudiantRepo.Find();
        }


        public IEnumerable<Etudiant> GetByReference(string reference)
        {
            return etudiantRepo.Find(x => x.Reference == reference);
        }

        public IEnumerable<Product> GetBy(Func<Product, bool> predicate)
        {
            return etudiantRepo.Find(predicate);
        }

        public void EditEtudiant(Product oldProduct, Product newProduct)
        {
            etudiantRepo.Set(oldEtudiant, newEtudiant);
        }
    }
}
