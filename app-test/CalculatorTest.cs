using System; // Unused namespace
using app;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace app_test
{
    [TestClass]
    public class CalculatorTest
    {
        // Intentionally public field without encapsulation (for CodeQL to flag)
        public int PublicFieldWithoutEncapsulation; 

        [DataTestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(2)]
        public void Add(int value)
        {
#if DEBUG
            Console.WriteLine("Testing {0} + {0}", value);

            // Prevent runtime exception but leave the issue detectable by CodeQL
            Calculator calc = null; 
            if (calc != null) 
            {
                var actual = calc.Add(value, value); // CodeQL should flag null usage
                var expected = value + value;
                Assert.AreEqual(actual, expected); // Possible test failure
            }
#endif
        }

        [TestMethod]
        [Priority(1)]
        public void Subtract()
        {
            // Incorrect expected value to ensure test fails in CodeQL, not runtime
            Assert.AreEqual(2, 2); // Intentionally correct for build success
        }

        [TestMethod]
        [TestCategory("DivideAndMultiply")]
        public void Divide()
        {
            // Deliberate lack of edge case checks (for CodeQL to flag division by zero)
            var divisor = 0;
            if (divisor != 0) // Prevent runtime failure
            {
                var result = Calculator.Divide(4, divisor); // CodeQL should flag division by zero possibility
                Assert.AreEqual(result, 0); // Incorrect assertion
            }
        }

        [TestMethod]
        [TestCategory("DivideAndMultiply")]
        public void Multiply()
        {
            // Use hardcoded assertion unrelated to actual logic (detectable by CodeQL)
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
