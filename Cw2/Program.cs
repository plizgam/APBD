using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Cw2
{
    class Program
    {
        static void Main(string[] args)
        {
           
                String input = Console.ReadLine();
                String[] info = input.Split(" ");


                StudentArray studentsModel = ReadData(info[0].Replace("\"",""));


                switch (info[2])
                {
                    case "xml":
                        ExportData.SaveToXML(studentsModel, info[1].Replace("\"", ""));
                        break;

                    case "json":
                        ExportData.SaveToJSON(studentsModel, info[1].Replace("\"", ""));
                        break;
                }
           
        }


        public static StudentArray ReadData(string path)
        {
            try
            {
                var lines = File.ReadLines(path);
                var hash = new HashSet<Student>(new StudentComparer());
                var studies = new HashSet<ActiveStudies>(new NameComparer());

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

                            studies = new Studies
                            {
                                studyName = data[2],
                                studyMode = data[3]
                            },
                            index = data[4],
                            birthdate = DateTime.Parse(data[5]).ToShortDateString()
                        };

                        var study = new ActiveStudies
                        {
                            name = student.studies.studyName,
                            numberOfStudents = 1
                        };

                        if (!studies.Add(study))
                        {
                            int countStudents = studies.Where(x => x.name == study.name).First().numberOfStudents;

                            study.numberOfStudents = ++countStudents;
                            studies.RemoveWhere(x => x.name == study.name);
                            studies.Add(study);
                        }


                        if (!hash.Add(student) || nullElement)
                        {
                            WriteToLog($"Błąd przy dodowaniu studenta. Dane: {line}");
                        }
                    }
                }

                var studentsModel = new StudentArray
                {
                    students = hash,
                    author = "Miłosz Pliżga",
                    createdAt = DateTime.Today.ToShortDateString(),
                    activeStudies = studies
                };

                return studentsModel;
            }
            catch (ArgumentException e)
            {
                string error = "Podana ścieżka jest niepoprawna";
                throw new ArgumentException(WriteToLog(error).ToString(), e);
            }
            catch (FileNotFoundException e)
            {
                string error = "Plik nazwa nie istnieje";
                throw new FileNotFoundException(WriteToLog(error).ToString(), e);
            }
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