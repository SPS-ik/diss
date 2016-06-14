using FIMAE.FIMAS;
using FIMAE.FIMAS.ExpertSystem;
using FIMAE.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace FIMAE.Helpers
{
    class Serializer
    {
        BinaryFormatter formatter = new BinaryFormatter();

        public void Save(Fimas fimas, string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, fimas);
            }
        }

        public Fimas Restore(string fileName)
        {
            Fimas fimas;
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                fimas = (Fimas)formatter.Deserialize(fs);
            }
            return fimas;
        }

    }
}
