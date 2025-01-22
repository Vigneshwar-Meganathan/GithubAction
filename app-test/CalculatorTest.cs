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
        public void UnusedHelperMethod()
        {
            Console.WriteLine("This method is not used anywhere.");
        }
    }
}
