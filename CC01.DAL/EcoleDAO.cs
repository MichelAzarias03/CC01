using System;
using System.Collections.Generic;
using System.Linq;
using CC01.BO;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CC01.DAL
{
    public class EcoleDAO
    {
        private Ecole ecoles=new Ecole();
        private const string FILE_NAME = @"ecole.json";
        private readonly string dbFolder;
        private FileInfo file;
        public EcoleDAO(string dbFolder)
        {
            this.dbFolder = dbFolder;
            file = new FileInfo(Path.Combine(this.dbFolder, FILE_NAME));
            if (!file.Directory.Exists)
            {
                file.Directory.Create();
            }
            if (!file.Exists)
            {
                file.Create().Close();
                file.Refresh();
            }
            if (file.Length > 0)
            {
                using (StreamReader sr = new StreamReader(file.FullName))
                {
                    string json = sr.ReadToEnd();
                    ecoles = JsonConvert.DeserializeObject<Ecole>(json);
                }
            }
        }

        public void Add(Ecole ecole)
        {
            using (StreamWriter sw = new StreamWriter(file.FullName, false))
            {
                string json = JsonConvert.SerializeObject(ecole);
                sw.WriteLine(json);
            }
        }

        public Ecole Get()
        {
            return ecoles;
        }
       
    }
}
