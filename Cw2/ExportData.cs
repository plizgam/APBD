using Newtonsoft.Json;
using System.IO;
using System.Xml.Serialization;


namespace Cw2
{
    class ExportData
    {

        public static void SaveToXML(StudentArray students, string path)
        {
            var writer = new FileStream(path, FileMode.Create);
            var names = new XmlSerializerNamespaces();
            names.Add("", "");
            var serializer = new XmlSerializer(typeof(StudentArray),
                                               new XmlRootAttribute("uczelnia"));
            serializer.Serialize(writer, students, names);
            writer.Close();
        }


        public static void SaveToJSON(StudentArray students, string path)
        {

            var serializer = new JsonSerializer();
            var stringWriter = new StringWriter();
            using (var writer = new JsonTextWriter(stringWriter))
            {
                writer.QuoteName = false;
                serializer.Serialize(writer, students);
            }
            File.WriteAllText(path, stringWriter.ToString());

         }
    }
}
