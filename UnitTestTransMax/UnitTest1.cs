using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
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
        const string INPUT_FILE_PATH = @"..\..\InputFiles\";
        const string EXPECTED_RESULTS_FILE_PATH = @"..\..\ExpectedResult\";
        const string ACTUAL_RESULTS_FILE_PATH = @"..\..\ActualResult\";

        [TestMethod]
        public void ValidateUsage()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                string[] args = new string[0];
                Program.Main(args);
                Assert.IsTrue(sw.ToString().Contains("Incorrect usage"));
            }
        }

        [TestMethod]
        public void ValidateInputFile()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                string[] args = new string[] { "ThisFileDoesNotExist.txt" };
                Program.Main(args);
                Assert.IsTrue(sw.ToString().Contains("Could not find file"));
            }
        }

        [TestMethod]
        public void ValidateEmptyFile()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                string inputFileName = INPUT_FILE_PATH + "InputFileEmpty.txt";
                string[] args = new string[1] { inputFileName };
                Program.Main(args);

                Assert.IsTrue(sw.ToString().Contains("The file is empty"));
            }
        }

        [TestMethod]
        public void ValidateStudentCollectionIfMarkAbsent()
        {
            var testStudent = new Student();
            string inputFileName = INPUT_FILE_PATH + "MarksMissingInput.txt";
            var studentsList = testStudent.ReadFile(inputFileName);

            IEnumerable<Student> sortedStudentsList = testStudent.SortByMarksAndLastName(studentsList);

            string outputFileName = ACTUAL_RESULTS_FILE_PATH + testStudent.GetOuputFileName(inputFileName);
            testStudent.WriteFile(outputFileName, sortedStudentsList);

            string expectedResultFileName = EXPECTED_RESULTS_FILE_PATH + "MarksMissingExpected.txt";
            IEnumerable<Student> expectedSortedStudentsList = testStudent.ReadFile(expectedResultFileName);

            AssertCollectionsAreEqual(expectedSortedStudentsList, sortedStudentsList);

        }

        [TestMethod]
        public void ValidateStudentCollectionIfLastNamekAbsent()
        {
            var testStudent = new Student();
            string inputFileName = INPUT_FILE_PATH + "LastNameMissingInput.txt";

            var studentsList = testStudent.ReadFile(inputFileName);

            IEnumerable<Student> sortedStudentsList = testStudent.SortByMarksAndLastName(studentsList);

            string outputFileName = ACTUAL_RESULTS_FILE_PATH + testStudent.GetOuputFileName(inputFileName);

            testStudent.WriteFile(outputFileName, sortedStudentsList);

            string expectedResultFileName = EXPECTED_RESULTS_FILE_PATH + "LastNameMissingExpected.txt";
            IEnumerable<Student> expectedSortedStudentsList = testStudent.ReadFile(expectedResultFileName);

            AssertCollectionsAreEqual(expectedSortedStudentsList, sortedStudentsList);
        }

        [TestMethod]
        public void ValidateStudentCollectionIfFirstNamekAbsent()
        {
            var testStudent = new Student();
            string inputFileName = INPUT_FILE_PATH + "FirstNameMissingInput.txt";

            var studentsList = testStudent.ReadFile(inputFileName);

            IEnumerable<Student> sortedStudentsList = testStudent.SortByMarksAndLastName(studentsList);

            string outputFileName = ACTUAL_RESULTS_FILE_PATH + testStudent.GetOuputFileName(inputFileName);

            testStudent.WriteFile(outputFileName, sortedStudentsList);

            string expectedResultFileName = EXPECTED_RESULTS_FILE_PATH + "FirstNameMissingExpected.txt";
            IEnumerable<Student> expectedSortedStudentsList = testStudent.ReadFile(expectedResultFileName);

            AssertCollectionsAreEqual(expectedSortedStudentsList, sortedStudentsList);
        }

        [TestMethod]
        public void TestForGoodData()
        {
            var testStudent = new Student();
            string inputFileName = INPUT_FILE_PATH + "GoodDataInput.txt";

            var studentsList = testStudent.ReadFile(inputFileName);

            IEnumerable<Student> sortedStudentsList = testStudent.SortByMarksAndLastName(studentsList);

            string outputFileName = ACTUAL_RESULTS_FILE_PATH + testStudent.GetOuputFileName(inputFileName);

            testStudent.WriteFile(outputFileName, sortedStudentsList);

            string expectedResultFileName = EXPECTED_RESULTS_FILE_PATH + "GoodDataExpected.txt";
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
