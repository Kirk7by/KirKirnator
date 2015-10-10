using ExpertCore.elements;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ExpertCore.Serialyze
{
    class Serialyzable
    {
        //Реализует сериализацию отправленной коллекции в файл
        public void SerializableCollections(List<Heroes> cats)
        {
            using (Stream fStream = new FileStream(@"./База данных.xml", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                XmlSerializer xmlFormat = new XmlSerializer(typeof(List<Heroes>));
                xmlFormat.Serialize(fStream, cats);
                fStream.Close();
        
            }
        }
/*
        //Реализует десериализацию 
        public List<serializableCollections> DeSerializableCollections()
        {
            List<serializableCollections> cats;
            using (Stream fStream = new FileStream(@"./MyMediaList.xml", FileMode.Open))
            {
                XmlSerializer xmlFormat = new XmlSerializer(typeof(List<serializableCollections>));
                cats = (List<serializableCollections>)xmlFormat.Deserialize(fStream);
                fStream.Close();
            }
            return cats;
        }
        */
    }
}
