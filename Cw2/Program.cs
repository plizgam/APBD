using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Cw2
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var path =  $@"{GetApplicationRoot()}\Data\dane.csv";

                var lines = File.ReadLines(path);
                var hash = new HashSet<Student>(new OwnComparer());


                foreach (var line in lines)
                {
                    String[] data = line.Split(",");

                    bool nullElement = false;
                    foreach (var item in data)
                    {
                        if (String.IsNullOrWhiteSpace(item))
                            nullElement = true;
                    }

                    if (!nullElement)
                    {
                        var student = new Student()
                        {
                            firstName = data[0],
                            lastName = data[1],
                            studyName = data[2],
                            studyMode = data[3],
                            index = data[4],
                            birthDate = DateTime.Parse(data[5])
                        };


                        if (!hash.Add(student) || nullElement)
                        {
                            WriteToLog($"Błąd przy dodowaniu studenta. Dane: {line}");
                        }
                    }        
                }

            }
            catch(ArgumentException e)
            {
                string error = "Podana ścieżka jest niepoprawna";
                throw new ArgumentException(WriteToLog(error).ToString(), e);              
            }
            catch(FileNotFoundException e)
            {
                string error = "Plik nazwa nie istnieje";
                 throw new FileNotFoundException(WriteToLog(error).ToString(), e);
            }


            //SaveToXML(students, GetApplicationRoot() + "");

        }

        public static void SaveToXML(HashSet<Student> students, string path)
        {
            FileStream writer = new FileStream(@"data.xml", FileMode.Create);
            XmlSerializer serializer = new XmlSerializer(typeof(List<Student>),
                                       new XmlRootAttribute("uczelnia"));
            serializer.Serialize(writer, students);
            serializer.Serialize(writer, students);
        }


        public static string WriteToLog(string message)
        {
            using(var file = new FileStream($@"{GetApplicationRoot()}\Logs\log.txt", FileMode.Append , FileAccess.Write))
            {             
                var streamWriter = new StreamWriter(file);
                streamWriter.WriteLine($"{message} | {DateTime.UtcNow}");           
                streamWriter.Close();
            }
            return message;
        }


        public static string GetApplicationRoot()
        {
            var exePath = Path.GetDirectoryName(System.Reflection
                              .Assembly.GetExecutingAssembly().CodeBase);
            Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
            var appRoot = appPathMatcher.Match(exePath).Value;
            return appRoot;
        }
    }
}