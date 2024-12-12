using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

[TestClass]
public class ProgramTests
{
    [TestMethod]
    public void TestProgramOutput()
    {
        // Set up console redirection
        using (StringWriter sw = new StringWriter())
        {
            Console.SetOut(sw);

            // Execute the Main method of the program
            StoredProcedure_1.Main(null);

            // Get the output
            string output = sw.ToString();

            // Define expected output lines
            string[] expectedOutputLines = new string[]
            {
                "Name, Position, Salary",
                "Bob Jones, Marketing Manager, 75000",
                "Jane Smith, HR Manager, 70000",
                "John Doe, Software Engineer, 60000"
            };

            // Split the output into lines
            string[] outputLines = output.Trim().Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            // Assert each line matches the expected format
            Assert.AreEqual(expectedOutputLines.Length, outputLines.Length, "Number of output lines does not match.");

            for (int i = 0; i < expectedOutputLines.Length; i++)
            {
                Assert.AreEqual(expectedOutputLines[i], outputLines[i], $"Output line {i + 1} does not match expected format.");
            }
        }
    }
}