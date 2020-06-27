using System.Transactions;
using Xunit;

namespace UnitTests
{
    public class TestClassLibrary
    {
        [Fact]
       
        public void Test1()
        {

            double a = 3.3;
            double b = 6.7;
            double sum = ClassLibrary.Class1.Add(a, b);
            Assert.Equal((a + b).ToString(), sum.ToString());

        }

    }

}
