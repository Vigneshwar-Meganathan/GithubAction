using System;
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
            var actual = calc.Add(value, value); // Will throw NullReferenceException at runtime

            var expected = value + value;
            Assert.AreEqual(actual, expected); // This could be detected by CodeQL
        }

        [TestMethod]
        [Priority(1)]
        public void Subtract()
        {
            // Incorrect expected value to ensure test fails
            var actual = 2 - 1;
            Assert.AreEqual(3, actual); // This will definitely fail
        }

#if DEBUG
        // Only include these tests in debug mode when methods are implemented.
        [TestMethod]
        [TestCategory("DivideAndMultiply")]
        public void Divide()
        {
            Assert.Inconclusive("Divide method not implemented.");
        }

        [TestMethod]
        [TestCategory("DivideAndMultiply")]
        public void Multiply()
        {
            Assert.Inconclusive("Multiply method not implemented.");
        }
#endif

        // Intentionally unused method to simulate dead code
        private void UnusedHelperMethod()
        {
            Console.WriteLine("This method is not used anywhere.");
        }
    }
}
