﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cw2
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                var path =  $@"{GetApplicationRoot()}\Data\dane.csv";
                Console.WriteLine(path);

                var lines = File.ReadLines(path);
                var hash = new HashSet<Student>(new OwnComparer());


                foreach (var line in lines)
                {
                    String[] data = line.Split(",");


                    var student = new Student()
                    {
                        firstName = data[0],
                        lastName = data[1],
                        studyName = data[2],
                        studyMode = data[3],
                        index = data[4],
                        birthDate = DateTime.Parse(data[5])
                    };


                    if (!hash.Add(student))
                    {
                        Console.WriteLine("Lol");
                        await WriteToLog($"Błąd przy dodowaniu studenta. Dane: {line}");
                    }
                    
                }

                var today = DateTime.UtcNow;
                Console.WriteLine(today);
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


        public static async Task<string> WriteToLog(string message)
        {
            using(var file = new FileStream($@"{GetApplicationRoot()}\Logs\log.txt", FileMode.OpenOrCreate , FileAccess.Write))
            {
               
                var streamWriter = new StreamWriter(file);
                await streamWriter.WriteLineAsync($"{message} | {DateTime.UtcNow}");
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