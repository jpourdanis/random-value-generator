# random-value-generator

Random-value-generator is a C# class library for creating random values of many types.

Read interface for more info.

# code example

string result = RandomValueGenerator.Get(alphabet, stringLength);

# unit test example

        [Test]
        public void TestGetRandomStringWithAlphabet()
        {
            int stringLength = Random.Next(1, 100);

            string alphabet = RandomValueGenerator.Get(10);

            string result = RandomValueGenerator.Get(alphabet, stringLength);

            Assert.AreEqual(result.Length, stringLength);

            Assert.IsTrue(result.All(x => alphabet.Contains(x)));
        }
