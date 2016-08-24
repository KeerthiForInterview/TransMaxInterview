Solution Structure:
The solution has two projects
	1. Implementation project - TransMax
	2. Unit test project - TransMaxUnitTests

TransMax:
	This references a third party nuget package - LinqToCsv. (Version 1.5.0.0, minimum .Net requirement - 4.0)
LinqToCsv is used to parse the csv file and to retrieve / write the contents of the file. This package enables mapping the content of the file to a business class of our application.
This way, it is easier for us to play around with data in the objects. 

TransMaxUnitTests:
This project has test cases to validate TransMax's implementation.
The following tests have been covered:
1. Incorrect usage of the command line application: If wrong number of parameters have been used in invoking the application, the test case will expect to be guided with the right usage.
2. Giving a incorrect file name as input: The console application is expected to identify that the file does not exist and exit smoothly.
3. Giving a valid file with no contents: The console application is expected to identify that the file is empty and exit smoothly.
4. If one or more rows have marks missing: The console application is expected to assign 0 to marks for those records.
5. If last name is missing for one more rows: The console application is expected to function normally  .
6. If the first name is missing for one or more rows: The console application is expected to not be bothered with this.
7. A valid file with valid data: The rows are expected to be ordered by marks in the descending order. In case of conflict between two or more rows, they are expected to be ordered by last name in ascending order.

Assumptions:
1. Data are valid
2. There are three columns in one row( comma seperated) albeit with empty data

CI has been implemented using Appveyor.
Credentials for Appveyor:
Sign in using Github
userId - keerthi.venkatachalam@yahoo.com
pwd - password1234
