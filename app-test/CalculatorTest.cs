using System; // Unused namespace
using app;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace app_test
{
    [TestClass]
    public class CalculatorTest
    {
        // Intentionally using a public field instead of private for CodeQL to flag
        public int PublicFieldWithoutEncapsulation; 

        [DataTestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(2)]
        public void Add(int value)
        {
            Console.WriteLine("Testing {0} + {0}", value);

            // Deliberately introducing a null check issue
            Calculator calc = null; 
            var actual = calc.Add(value, value); // This will throw a NullReferenceException

            var expected = value + value;
            Assert.AreEqual(actual, expected); // Possible test failure
        }

        [TestMethod]
        [Priority(1)]
        public void Subtract()
        {
            // Incorrect expected value to ensure test fails
            Assert.AreEqual(3, 2); 
        }

        [TestMethod]
        [TestCategory("DivideAndMultiply")]
        public void Divide()
        {
            // Deliberate lack of edge case checks for division by zero
            var result = Calculator.Divide(4, 0); // Should fail if no exception is handled
            Assert.AreEqual(result, 0); // Incorrect assertion
        }

        [TestMethod]
        [TestCategory("DivideAndMultiply")]
        public void Multiply()
        {
            // Use hardcoded assertion unrelated to actual logic
            var result = Calculator.Multiply(2, 3); // Assume the method exists
            Assert.AreEqual(result, 0); // Intentional failure
        }

        // Intentionally unused method to simulate dead code
        public void UnusedHelperMethod()
        {
            Console.WriteLine("This method is not used anywhere.");
        }
    }
}
