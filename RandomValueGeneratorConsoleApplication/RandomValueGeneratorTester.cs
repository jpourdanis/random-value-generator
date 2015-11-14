using RandomValueGenerator;
using NUnit.Framework;

namespace RandomValueGenerator_nu
{
    [TestFixture]
    public class RandomValueGeneratorTester
    {
        [Test]
        public void TestGetForExludedList()
        {

            string result = RandomValueGenerator.Get(100);

            Assert.AreSame(result.GetType().ToString(),"string");

        }

        IRandomValueGenerator RandomValueGenerator { get; set; }
    }
}


