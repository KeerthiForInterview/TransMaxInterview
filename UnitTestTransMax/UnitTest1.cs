using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using TransMax;

namespace TransMaxUnitTests
{

    /*
     * The following class has unit tests to test the functionality of the TransMax.Student class 
     */
    [TestClass]
    public class StudentTests
    {
        private int StartConsoleApplication(string arguments)
        {
            // Initialize process here
            Process proc = new Process();
            proc.StartInfo.FileName = @"..\..\..\TransmaxTest\bin\Release\TransmaxTest.exe";
            // add arguments as whole string
            proc.StartInfo.Arguments = arguments;

            // use it to start from testing environment
            proc.StartInfo.UseShellExecute = false;

            // redirect outputs to have it in testing console
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.RedirectStandardError = true;

            // set working directory
            proc.StartInfo.WorkingDirectory = Environment.CurrentDirectory;

            // start and wait for exit
            proc.Start();
            proc.WaitForExit();

            // get output to testing console.
            Console.WriteLine(proc.StandardOutput.ReadToEnd());
            Console.Write(proc.StandardError.ReadToEnd());

            // return exit code
            return proc.ExitCode;
        }

        [TestMethod]
        public void ValidateUsage()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                StartConsoleApplication(null);
                Assert.IsTrue(sw.ToString().Contains("Incorrect usage"));
            }
        }

        [TestMethod]
        public void ValidateInputFile()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                StartConsoleApplication("ThisFileDoesNotExist.txt");
                Assert.IsTrue(sw.ToString().Contains("Could not find file"));
            }
        }

        [TestMethod]
        public void ValidateEmptyFile()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                string inputFileName = @"InputFiles\InputFileEmpty.txt";
                StartConsoleApplication(inputFileName);

                Assert.IsTrue(sw.ToString().Contains("The file is empty"));
            }
        }

        [TestMethod]
        public void ValidateStudentCollectionIfMarkAbsent()
        {
            var testStudent = new Student();
            string inputFileName = @"InputFiles\MarksMissingInput.txt";
            var studentsList = testStudent.ReadFile(inputFileName);

            IEnumerable<Student> sortedStudentsList = testStudent.SortByMarksAndLastName(studentsList);

            string outputFileName = @"ActualResult\" + testStudent.GetOuputFileName(inputFileName);
            testStudent.WriteFile(outputFileName, sortedStudentsList);

            string expectedResultFileName = @"ExpectedResult\MarksMissingExpected.txt";
            IEnumerable<Student> expectedSortedStudentsList = testStudent.ReadFile(expectedResultFileName);

            AssertCollectionsAreEqual(expectedSortedStudentsList, sortedStudentsList);

        }

        [TestMethod]
        public void ValidateStudentCollectionIfLastNamekAbsent()
        {
            var testStudent = new Student();
            string inputFileName = @"InputFiles\LastNameMissingInput.txt";

            var studentsList = testStudent.ReadFile(inputFileName);

            IEnumerable<Student> sortedStudentsList = testStudent.SortByMarksAndLastName(studentsList);

            string outputFileName = @"ActualResult\" + testStudent.GetOuputFileName(inputFileName);

            testStudent.WriteFile(outputFileName, sortedStudentsList);

            string expectedResultFileName = @"ExpectedResult\LastNameMissingExpected.txt";
            IEnumerable<Student> expectedSortedStudentsList = testStudent.ReadFile(expectedResultFileName);

            AssertCollectionsAreEqual(expectedSortedStudentsList, sortedStudentsList);
        }

        [TestMethod]
        public void ValidateStudentCollectionIfFirstNamekAbsent()
        {
            var testStudent = new Student();
            string inputFileName = @"InputFiles\FirstNameMissingInput.txt";

            var studentsList = testStudent.ReadFile(inputFileName);

            IEnumerable<Student> sortedStudentsList = testStudent.SortByMarksAndLastName(studentsList);

            string outputFileName = @"ActualResult\" + testStudent.GetOuputFileName(inputFileName);

            testStudent.WriteFile(outputFileName, sortedStudentsList);

            string expectedResultFileName = @"ExpectedResult\FirstNameMissingExpected.txt";
            IEnumerable<Student> expectedSortedStudentsList = testStudent.ReadFile(expectedResultFileName);

            AssertCollectionsAreEqual(expectedSortedStudentsList, sortedStudentsList);
        }

        [TestMethod]
        public void TestForGoodData()
        {
            var testStudent = new Student();
            string inputFileName = @"InputFiles\GoodDataInput.txt";

            var studentsList = testStudent.ReadFile(inputFileName);

            IEnumerable<Student> sortedStudentsList = testStudent.SortByMarksAndLastName(studentsList);

            string outputFileName = @"ActualResult\" + testStudent.GetOuputFileName(inputFileName);

            testStudent.WriteFile(outputFileName, sortedStudentsList);

            string expectedResultFileName = @"ExpectedResult\GoodDataExpected.txt";
            IEnumerable<Student> expectedSortedStudentsList = testStudent.ReadFile(expectedResultFileName);

            AssertCollectionsAreEqual(expectedSortedStudentsList, sortedStudentsList);
        }

        private void AssertCollectionsAreEqual(IEnumerable<Student> expected, IEnumerable<Student> actual)
        {
            int count = expected.Count();

            Assert.AreEqual(count, actual.Count());

            for (int i = 0; i < count; i++)
            {
                Assert.AreEqual(expected.ElementAt(i).FirstName, actual.ElementAt(i).FirstName);
                Assert.AreEqual(expected.ElementAt(i).LastName, actual.ElementAt(i).LastName);
                Assert.AreEqual(expected.ElementAt(i).Marks, actual.ElementAt(i).Marks);
            }
        }
    }
}
