﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
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
                HashSet<Student> hash = new HashSet<Student>(new OwnComparer());


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
                            birthdate = DateTime.Parse(data[5]).ToShortDateString()
                        };


                        if (!hash.Add(student) || nullElement)
                        {
                            WriteToLog($"Błąd przy dodowaniu studenta. Dane: {line}");
                        }
                    }        
                }

                var studentsModel = new Models.StudentArray
                {
                    students = hash,
                    author = "Miłosz Pliżga",
                    createdAt = DateTime.Today.ToShortDateString()
                };


                SaveToXML(studentsModel, GetApplicationRoot() + @"\GeneratedFiles\");

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
        }

        public static void SaveToXML(Models.StudentArray students, string path)
        {
            var writer = new FileStream(path + "data.xml", FileMode.Create);
            var names = new XmlSerializerNamespaces();
            names.Add("", "");
            var serializer = new XmlSerializer(typeof(Models.StudentArray),
                                       new XmlRootAttribute("uczelnia"));     
            serializer.Serialize(writer, students, names);

            writer.Close();
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