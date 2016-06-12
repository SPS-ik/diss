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

        public void Save(List<AgentOrientedSubsystem> aosList, string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                // сериализуем весь массив people
                formatter.Serialize(fs, aosList);
            }
        }

        public List<AgentOrientedSubsystem> Restore(string fileName)
        {
            List<AgentOrientedSubsystem> aosList;
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                aosList = (List<AgentOrientedSubsystem>)formatter.Deserialize(fs);
            }
            return aosList;
        }

    }
}
