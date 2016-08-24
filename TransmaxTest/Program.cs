using LINQtoCSV;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TransMax
{
    public class Student
    {
        public Student()
        {
            InstantiateCSVObjects();
        }

        public IEnumerable<Student> ReadFile(string fileName)
        {
            return _csvContext.Read<Student>(fileName, _csvFileDescription);
        }

        public IEnumerable<Student> SortByMarksAndLastName(IEnumerable<Student> students)
        {
            return students.OrderByDescending(s => s.Marks).ThenBy(s => s.LastName);
        }
        public void WriteFile(string fileName, IEnumerable<Student> students)
        {
            _csvContext.Write<Student>(students.ToList(), fileName, _csvFileDescription);
        }

        public void PrintContents(string fileName)
        {
            var fileContents = File.ReadAllLines(fileName);
            foreach (var line in fileContents)
            {
                Console.WriteLine(line);
            }
        }

        public string GetOuputFileName(string inputFileName)
        {
            return string.Concat(System.IO.Path.GetFileNameWithoutExtension(inputFileName), "-graded.txt");
        }

        private void InstantiateCSVObjects()
        {
            if (_csvContext == null)
            {
                _csvContext = new CsvContext();
            }
            if (_csvFileDescription == null)
            {
                _csvFileDescription = new CsvFileDescription()
                {
                    SeparatorChar = ',',
                    FirstLineHasColumnNames = false,
                    EnforceCsvColumnAttribute = true,
                    MaximumNbrExceptions = 1

                };
            }
        }

        [CsvColumn(FieldIndex = 1)]
        public string LastName { get; set; }

        [CsvColumn(FieldIndex = 2)]
        public string FirstName { get; set; }

        [CsvColumn(FieldIndex = 3)]
        public int Marks { get; set; }

        private static CsvContext _csvContext;
        private static CsvFileDescription _csvFileDescription;
    };


    public class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                System.Console.WriteLine("Incorrect usage! Please give the filename to be processed. eg- TransmaxTest c:\\FileToBeProcessed.txt");
                return;
            }
            else
            {
                string inputFileName = args[0];
                try
                {
                    var student = new Student();
                    IEnumerable<Student> students = null;

                    try
                    {
                        students = student.ReadFile(inputFileName);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(String.Format("Could not read from file: {0}", ex.Message));
                        throw ex;
                    }

                    if (students.Count() > 0)
                    {
                        var studentsSortByMarks = student.SortByMarksAndLastName(students);

                        string outputFileName = student.GetOuputFileName(inputFileName);

                        try
                        {
                            student.WriteFile(outputFileName, studentsSortByMarks);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(String.Format("Could not write into file: {0}", ex.Message));
                            throw ex;
                        }

                        student.PrintContents(outputFileName);
                        Console.WriteLine(String.Format("Finished: Created {0}", outputFileName));
                    }
                    else
                    {
                        Console.WriteLine("The file is empty!");
                    }
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.Message);

                }

            }
        }
    }
}
