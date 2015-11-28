using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using NUnit.Framework;
using RandomValueGenerator;
using RandomValueGenerator.Utilities;
using Rhino.Mocks;
using Spring.Testing.NUnit;

namespace RandomValueGenerator_nu
{
    [TestFixture]
    public class RandomValueGeneratorTester : AbstractDependencyInjectionSpringContextTests
    {
        #region SetUp / TearDown

        protected override void OnSetUp()
        {
            base.OnSetUp();

            Random = new Random();
        }

        protected override void OnTearDown()
        {
            base.OnTearDown();

            Mocks.BackToRecordAll(BackToRecordOptions.All);
        }

        #endregion

        #region Tests

        #region GetPossitive

        #region Success

        [Test]
        public void TestGetPossitive()
        {
            int maximumValue = Random.Next(100);

            int result = RandomValueGenerator.GetPositive(maximumValue);

            Assert.GreaterOrEqual(result, 0);

            Assert.LessOrEqual(result, maximumValue);
        }

        #endregion

        #region Failure

        [Test]
        public void TestGetPossitiveArgumentException()
        {
            Assert.Throws<ArgumentException>(() => RandomValueGenerator.GetPositive(-Random.Next(1, 100)));
        }

        #endregion

        #endregion

        #region GetNegative

        #region Success

        [Test]
        public void TestGetNegative()
        {
            int minimumValue = -Random.Next(1, 100);

            int result = RandomValueGenerator.GetNegative(minimumValue);

            Assert.LessOrEqual(result, -1);

            Assert.GreaterOrEqual(result, minimumValue);
        }

        #endregion

        #region Failure

        [Test]
        public void TestGetNegativeArgumentException()
        {
            Assert.Throws<ArgumentException>(() => RandomValueGenerator.GetNegative(Random.Next(1, 100)));
        }

        #endregion

        #endregion

        #region Get(int minValue, int maxValue)

        #region Success

        [Test]
        public void TestGetRandomNumber()
        {
            int maxValue = Random.Next(100);

            int minValue = Random.Next(maxValue);

            int result = RandomValueGenerator.Get(minValue, maxValue);

            Assert.Less(result, maxValue);

            Assert.GreaterOrEqual(result, minValue);
        }

        #endregion

        #region Failure

        [Test]
        public void TestGetRandomNumberArgumentExceptionWhenMinValueGreaterMaxValue()
        {
            int maxValue = Random.Next(100);

            int minValue = maxValue + 1;

            Assert.Throws<ArgumentException>(() => RandomValueGenerator.Get(minValue, maxValue));
        }

        #endregion

        #endregion

        #region Get(int stringLength)

        #region Success

        [Test]
        public void TestGetRandomString()
        {
            string alphabet = RandomValueGenerator.GetDefaultAlphabet();

            int stringLength = Random.Next(100);

            string result = RandomValueGenerator.Get(stringLength);

            Assert.AreEqual(result.Length, stringLength);

            Assert.IsTrue(result.Any(o => alphabet.Contains(o)));
        }

        #endregion

        #region Failure

        [Test]
        public void TestGetRandomStringArgumentExceptionForNegativeStringLength()
        {
            Assert.Throws<ArgumentException>(() => RandomValueGenerator.Get(-Random.Next(1, 100)));
        }

        #endregion

        #endregion

        #region Get(string alphabet, int stringLength)

        #region Success

        [Test]
        public void TestGetRandomStringWithAlphabet()
        {
            int stringLength = Random.Next(1, 100);

            string alphabet = RandomValueGenerator.Get(10);

            string result = RandomValueGenerator.Get(alphabet, stringLength);

            Assert.AreEqual(result.Length, stringLength);

            Assert.IsTrue(result.All(x => alphabet.Contains(x)));
        }

        [Test]
        public void TestGetRandomStringWithAlphabetWhenStringLengthIsZero()
        {
            string alphabet = RandomValueGenerator.Get(10);

            Assert.AreEqual(string.Empty, RandomValueGenerator.Get(alphabet, 0));
        }

        #endregion

        #region Failure

        [Test]
        public void TestGetRandomStringWithAlphabetArgumentExceptionForNegativeStringLength()
        {
            string alphabet = RandomValueGenerator.Get(10);

            Assert.Throws<ArgumentException>(() => RandomValueGenerator.Get(alphabet, -Random.Next(1, 100)));
        }

        [Test]
        public void TestGetRandomStringWithAlphabetArgumentExceptionForAlphabet([Values(null, "", "\r \n \t")] string alphabet)
        {
            Assert.Throws<ArgumentException>(() => RandomValueGenerator.Get(alphabet, Random.Next(1, 100)));
        }

        #endregion

        #endregion

        #region GetTextFile(int numberOfWords, string directoryPath)

        #region Success

        [Test]
        public void TestGetTextFile()
        {
            Mocks.ReplayAll();

            #region Initialize Test Data

            IList<string> expectedWords = new List<string>();

            for (int i = 0; i < Random.Next(50); i++)
            {
                expectedWords.Add(string.Format("ExpectedWord{0}", Random.Next()));
            }

            #endregion

            Mocks.BackToRecordAll();

            #region Expectations

            using (Mocks.Ordered())
            {
                foreach (string expectedWord in expectedWords)
                {
                    Expect.Call(DictionaryDao.GetWord()).Return(expectedWord);
                }
            }

            #endregion

            Mocks.ReplayAll();

            #region Call Method

            FileInfo fileInfo = RandomValueGenerator.GetTextFile(expectedWords.Count, Environment.CurrentDirectory);

            Assert.IsTrue(fileInfo.FullName.EndsWith(".txt"));

            string resultString = File.ReadAllText(fileInfo.FullName);

            string defaultSeparator = RandomValueGenerator.GetDefaultSeparator();

            IList<string> actualWords = resultString.Split(new[] { defaultSeparator }, StringSplitOptions.RemoveEmptyEntries);

            CollectionAssert.AreEqual(expectedWords, actualWords);

            fileInfo.Delete();

            #endregion

            Mocks.VerifyAll();
        }

        #endregion

        #region Failure

        [Test]
        public void TestGetTextFileArgumentExceptionForNumberOfWords()
        {
            Assert.Throws<ArgumentException>(() => RandomValueGenerator.GetTextFile(-Random.Next(1, 100), Environment.CurrentDirectory));
        }

        [Test]
        public void TestGetTextFileArgumentExceptionForDirectoryPath([Values(null, "", "\r \n \t")] string directoryPath)
        {
            Assert.Throws<ArgumentException>(() => RandomValueGenerator.GetTextFile(Random.Next(1, 100), directoryPath));
        }

        [Test]
        public void TestGetTextFileDirectoryNotFoundException()
        {
            Assert.Throws<DirectoryNotFoundException>(() => RandomValueGenerator.GetTextFile(Random.Next(1, 100), Path.GetInvalidPathChars().ToString()));
        }

        #endregion

        #endregion

        #region GetGuid

        #region Success

        [Test]
        public void TestGetGuid()
        {
            Assert.AreNotEqual(Guid.Empty, RandomValueGenerator.GetGuid());
        }

        #endregion

        #endregion

        #region GetPastUtcDateTime

        #region Success

        [Test]
        public void TestGetPastUtcDateTime()
        {
            Assert.That(DateTime.UtcNow, Is.GreaterThan(RandomValueGenerator.GetPastUtcDateTime()));
        }

        #endregion

        #endregion

        #region GetFutureUtcDateTime

        #region Success

        [Test]
        public void TestGetFutureUtcDateTime()
        {
            Assert.That(DateTime.UtcNow, Is.LessThan(RandomValueGenerator.GetFutureUtcDateTime()));
        }

        #endregion

        #endregion

        #region GetPastUtcDateTimeOffset

        #region Success

        [Test]
        public void TestGetPastUtcDateTimeOffset()
        {
            Assert.That(DateTimeOffset.UtcNow, Is.GreaterThan(RandomValueGenerator.GetPastUtcDateTimeOffset()));
        }

        #endregion

        #endregion

        #region GetFutureUtcDateTimeOffset

        #region Success

        [Test]
        public void TestGetFutureUtcDateTimeOffset()
        {
            Assert.That(DateTimeOffset.UtcNow, Is.LessThan(RandomValueGenerator.GetFutureUtcDateTimeOffset()));
        }

        #endregion

        #endregion

        #region GetEmailAddress

        #region Success

        [Test]
        public void TestGetEmailAddress()
        {
            EmailAddressAttribute emailAddressAttribute = new EmailAddressAttribute();

            string emailAddress = RandomValueGenerator.GetEmailAddress();

            Assert.IsTrue(emailAddressAttribute.IsValid(emailAddress));
        }

        #endregion

        #endregion

        #region GetDigit

        #region Success

        [Test]
        public void TestGetDigit()
        {
            byte result = RandomValueGenerator.GetDigit();

            Assert.IsTrue(result <= 9);
        }

        #endregion

        #endregion

        #region GetLetter

        #region Success

        [Test]
        public void TestGetLetter()
        {
            char result = RandomValueGenerator.GetLetter(RandomValueGenerator.GetDefaultLetters());

            Assert.IsTrue(char.IsLetter(result));
        }

        #endregion

        #region Failure

        [Test]
        public void TestGetLetterArgumentExceptionForNullOrWhiteSpaceAlphabet([Values(null, "", "\r \n \t")] string alphabet)
        {
            Assert.Throws<ArgumentException>(() => RandomValueGenerator.GetLetter(alphabet));
        }

        [Test]
        public void TestGetLetterArgumentExceptionForNonLetterCharacters()
        {
            Assert.Throws<ArgumentException>(() => RandomValueGenerator.GetLetter(RandomValueGenerator.GetDefaultDigits()));
        }

        #endregion

        #endregion

        #region GetWord

        #region Success

        [Test]
        public void TestGetWord()
        {
            Mocks.ReplayAll();

            #region Initialize Test Data

            string expectedWord = string.Format("ExpectedWord{0}", Random.Next());

            #endregion

            Mocks.BackToRecordAll();

            #region Expectations

            using (Mocks.Ordered())
            {
                Expect.Call(DictionaryDao.GetWord()).Return(expectedWord);
            }

            #endregion

            Mocks.ReplayAll();

            #region Call Method

            string actualWord = RandomValueGenerator.GetWord();

            Assert.AreEqual(actualWord, expectedWord);

            #endregion

            Mocks.VerifyAll();
        }

        #endregion

        #endregion

        #region GetWords

        #region Success

        [Test]
        public void TestGetWords()
        {
            Mocks.ReplayAll();

            #region Initialize Test Data

            int numberOfWords = Random.Next(50);

            IList<string> expectedWords = new List<string>();

            for (int i = 0; i < numberOfWords; i++)
            {
                expectedWords.Add(string.Format("ExpectedWord{0}", Random.Next()));
            }

            #endregion

            Mocks.BackToRecordAll();

            #region Expectations

            using (Mocks.Ordered())
            {
                foreach (string expectedWord in expectedWords)
                {
                    Expect.Call(DictionaryDao.GetWord()).Return(expectedWord);
                }
            }

            #endregion

            Mocks.ReplayAll();

            #region Call Method

            IList<string> actualWords = RandomValueGenerator.GetWords(numberOfWords);

            CollectionAssert.AreEqual(expectedWords, actualWords);

            #endregion

            Mocks.VerifyAll();
        }

        #endregion

        #region Failure

        [Test]
        public void TestGetWordsArgumentExceptionForNumberOfWords()
        {
            Assert.Throws<ArgumentException>(() => RandomValueGenerator.GetWords(-Random.Next(1, 100)));
        }

        #endregion

        #endregion

        #region GetUpperCaseWord

        #region Success

        [Test]
        public void TestGetUpperCaseWord()
        {
            Mocks.ReplayAll();

            #region Initialize Test Data

            string expectedWord = "EXPECTEDWORD";

            #endregion

            Mocks.BackToRecordAll();

            #region Expectations

            using (Mocks.Ordered())
            {
                Expect.Call(DictionaryDao.GetWord()).Return(expectedWord);
            }

            #endregion

            Mocks.ReplayAll();

            #region Call Method

            string actualWord = RandomValueGenerator.GetUpperCaseWord();

            Assert.AreEqual(actualWord, expectedWord);

            Assert.IsTrue(actualWord.All(char.IsUpper));

            #endregion

            Mocks.VerifyAll();
        }

        #endregion

        #endregion

        #region GetLowerCaseWord

        #region Success

        [Test]
        public void TestGetLowerCaseWord()
        {
            Mocks.ReplayAll();

            #region Initialize Test Data

            string expectedWord = "expectedword";

            #endregion

            Mocks.BackToRecordAll();

            #region Expectations

            using (Mocks.Ordered())
            {
                Expect.Call(DictionaryDao.GetWord()).Return(expectedWord);
            }

            #endregion

            Mocks.ReplayAll();

            #region Call Method

            string actualWord = RandomValueGenerator.GetLowerCaseWord();

            Assert.AreEqual(actualWord, expectedWord);

            Assert.IsTrue(actualWord.All(char.IsLower));

            #endregion

            Mocks.VerifyAll();
        }

        #endregion

        #endregion

        #region GetSentence

        #region Success

        [Test]
        public void TestGetSentence()
        {
            Mocks.ReplayAll();

            #region Initialize Test Data

            string separator = RandomValueGenerator.GetDefaultSeparator();

            int numberOfWords = Random.Next(50);

            IList<string> expectedSentenceWords = new List<string>();

            for (int i = 0; i < numberOfWords; i++)
            {
                expectedSentenceWords.Add(string.Format("ExpectedWord{0}", Random.Next()));
            }

            string expectedSentence = string.Join(separator, expectedSentenceWords);

            #endregion

            Mocks.BackToRecordAll();

            #region Expectations

            using (Mocks.Ordered())
            {
                foreach (string expectedWord in expectedSentenceWords)
                {
                    Expect.Call(DictionaryDao.GetWord()).Return(expectedWord);
                }
            }

            #endregion

            Mocks.ReplayAll();

            #region Call Method

            string actualSentence = RandomValueGenerator.GetSentence(numberOfWords, separator);

            IList<string> actualSentenceWords = actualSentence.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);

            Assert.AreEqual(expectedSentence, actualSentence);

            CollectionAssert.AreEqual(expectedSentenceWords, actualSentenceWords);

            #endregion

            Mocks.VerifyAll();
        }

        #endregion

        #region Failure

        [Test]
        public void TestGetSentenceArgumentExceptionForNumberOfWords()
        {
            Assert.Throws<ArgumentException>(() => RandomValueGenerator.GetSentence(-Random.Next(1, 100), RandomValueGenerator.GetDefaultSeparator()));
        }

        [Test]
        public void TestGetSentenceArgumentExceptionForNullSeparator()
        {
            Assert.Throws<ArgumentNullException>(() => RandomValueGenerator.GetSentence(Random.Next(1, 100), null));
        }

        #endregion

        #endregion

        #region GetNullOrEmptyOrWhiteSpace

        #region Success

        [Test]
        public void TestGetNullOrEmptyOrWhiteSpace()
        {
            Assert.IsTrue(string.IsNullOrWhiteSpace(RandomValueGenerator.GetNullOrEmptyOrWhiteSpace()));
        }

        #endregion

        #endregion

        #region GetElement

        #region Success

        [Test]
        public void TestGetElement()
        {
            string alphabet = RandomValueGenerator.GetDefaultAlphabet();

            char symbol = RandomValueGenerator.GetElement(alphabet.ToList());

            Assert.IsTrue(alphabet.Contains(symbol));
        }

        #endregion

        #region Failure

        [Test]
        public void TestGetElementArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => RandomValueGenerator.GetElement<int>(null));
        }

        [Test]
        public void TestGetElementArgumentException()
        {
            Assert.Throws<ArgumentException>(() => RandomValueGenerator.GetElement(new List<int>()));
        }

        #endregion

        #endregion

        #region GetDecimal

        #region Success

        [Test]
        public void TestGetDecimal()
        {
            int maxValue = Random.Next(1,100);

            int minValue = Random.Next(maxValue);

            byte numberOfDecimals = (byte)Random.Next(1, 20);

            decimal result = RandomValueGenerator.GetDecimal(minValue, maxValue, numberOfDecimals);

            Assert.LessOrEqual(result, maxValue);

            Assert.GreaterOrEqual(result, minValue);

            Assert.AreEqual(numberOfDecimals, result.ToString().Split(',')[1].Length);
        }

        #endregion

        #region Failure

        [Test]
        public void TestGetDecimalArgumentExceptionWhenMinValueGreaterMaxValue()
        {
            int maxValue = Random.Next(100);

            int minValue = maxValue + 1;

            Assert.Throws<ArgumentException>(() => RandomValueGenerator.GetDecimal(minValue,maxValue,(byte) Random.Next(1,20)));
        }

        #endregion

        #endregion

        #region GetDifferentNumber

        #region Success

        [Test]
        public void TestGetDifferentNumber()
        {
            int randomNumber = Random.Next(1, byte.MaxValue);

            byte result = RandomValueGenerator.GetDifferentNumber(randomNumber);

            Assert.AreNotEqual(result,randomNumber);
        }

        #endregion

        #endregion

        #endregion

        #region Public Properties

        /// <summary>
        /// <see cref="MockRepository"/>
        /// </summary>
        public MockRepository Mocks { get; set; }

        /// <summary>
        /// <see cref="IRandomValueGenerator"/>
        /// </summary>
        public IRandomValueGenerator RandomValueGenerator { get; set; }

        #region DAOs

        /// <summary>
        /// <see cref="IDictionaryDao"/>
        /// </summary>
        public IDictionaryDao DictionaryDao { get; set; }

        #endregion

        #endregion

        #region Private Properties

        /// <summary>
        /// <see cref="Random"/>
        /// </summary>
        private Random Random { get; set; }

        #endregion

        #region Configuration

        protected override string[] ConfigLocations { get { return new[] { "~/spring-config-random-value-generator-nu.xml", "~/spring-config-nu.xml" }; } }

        #endregion
    }
}