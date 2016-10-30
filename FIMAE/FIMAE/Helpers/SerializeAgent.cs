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
using System.Xml;
using System.Xml.Serialization;

namespace FIMAE.Helpers
{
    class SerializeAgent
    {
        BinaryFormatter formatter = new BinaryFormatter();

        public void Save(Fimas fimas, string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, fimas);
            }
        }

        public void SaveToXml(Fimas fimas, string fileName)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Fimas));

            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            using (XmlWriter writer = XmlWriter.Create(fs))
            {
                xmlSerializer.Serialize(writer, fimas);
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

        public Fimas RestoreFromXml(string fileName)
        {

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Fimas));
            Fimas fimas;
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                fimas = (Fimas)xmlSerializer.Deserialize(fs);
            }
            return fimas;
        }
    }
}
