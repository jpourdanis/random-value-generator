using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using RandomValueGenerator.Utilities;

namespace RandomValueGenerator
{
    public class RandomValueGenerator : IRandomValueGenerator
    {
        #region Private Static Readonly Fields

        /// <summary>
        /// <see cref="Random"/>
        /// </summary>
        private static readonly Random Random = new Random();

        /// <summary>
        /// The alphabet with all Latin letters and digits.
        /// </summary>
        private static readonly string Alphabet = string.Format("{0}{1}{2}", Letters, Digits,Symbols);

        #endregion

        #region Private Static Const Fields

        /// <summary>
        /// All Latin letters.
        /// </summary>
        private const string Letters = "qwertyuioplkjhgfdsazxcvbnmQWERTYUIOPLKJHGFDSAZXCVBNM";

        /// <summary>
        /// All numeric digits.
        /// </summary>
        private const string Digits = "0123456789";

        /// <summary>
        /// The default separator of a sentence.
        /// </summary>
        private const string Separator = " ";

        /// <summary>
        /// All keyboard symbols.
        /// </summary>
        private const string Symbols = "☺☻♥♦♣♠◘○◙♂♀♪♫☼►◄↕‼¶§▬↨↑↓→←∟↔▲▼!#$%&½¼¡«»░▒▓│┤╡╢╖╕╣║╗╝╜╛┐└┴┬├─┼╞╟╚╔╩╦╠═╬╧╨╤╥╙╘╒╓╫╪┘┌█▄▌▐▀αßΓπΣσµτΦΘΩδ∞φε∩≡±≥≤⌠⌡÷≈°∙·√ⁿ²■{|}~⌂€‚ƒ„…†‡ˆ‰Š‹ŒŽ‘’“”–—˜™š›œžŸ¡¢£¤¥¦§¨©ª«¬­®¯°±²³´µ¶·¸¹º»¾¿ÀÁÂ_ÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖ×ØÙÚÛÜÝÞàáâãäåæçèéêëìíîïðñòóôõö÷øùúûüýþÿ";


        #endregion

        #region IRandomValueGenerator Implementation

        #region Void

        /// <summary>
        /// <see cref="IRandomValueGenerator.SetDictionaryDao"/>
        /// </summary>
        /// <param name="dictionaryDao">..</param>
        public void SetDictionaryDao(IDictionaryDao dictionaryDao)
        {
            if (dictionaryDao == null)
            {
                throw new ArgumentNullException("dictionaryDao");
            }

            DictionaryDao = dictionaryDao;
        }

        #endregion

        #region String

        /// <summary>
        /// <see cref="IRandomValueGenerator.GetDefaultAlphabet"/>
        /// </summary>
        /// <returns>..</returns>
        public string GetDefaultAlphabet()
        {
            return Alphabet;
        }

        /// <summary>
        /// <see cref="IRandomValueGenerator.GetDefaultLetters"/>
        /// </summary>
        /// <returns>..</returns>
        public string GetDefaultLetters()
        {
            return Letters;
        }

        /// <summary>
        /// <see cref="IRandomValueGenerator.GetDefaultDigits"/>
        /// </summary>
        /// <returns>..</returns>
        public string GetDefaultDigits()
        {
            return Digits;
        }

        /// <summary>
        /// <see cref="IRandomValueGenerator.GetDefaultSeparator"/>
        /// </summary>
        /// <returns>..</returns>
        public string GetDefaultSeparator()
        {
            return Separator;
        }

        /// <summary>
        /// <see cref="IRandomValueGenerator.GetDefaultSymbols"/>
        /// </summary>
        /// <returns>..</returns>
        public string GetDefaultSymbols()
        {
            return Symbols;
        }

        /// <summary>
        /// <see cref="IRandomValueGenerator.GetEmailAddress"/>
        /// </summary>
        /// <returns>..</returns>
        public string GetEmailAddress()
        {
            string alphabet = string.Format("{0}{1}", Letters, Digits);

            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append(GetLetter());

            stringBuilder.Append(Get(alphabet, 20));

            stringBuilder.Append("@");

            stringBuilder.Append(GetLetter());

            stringBuilder.Append(Get(alphabet, 20));

            stringBuilder.Append(".");

            stringBuilder.Append(Get(Letters, 1, 4));

            return stringBuilder.ToString();
        }

        /// <summary>
        /// <see cref="IRandomValueGenerator.GetWord()"/>
        /// </summary>
        /// <returns>..</returns>
        public string GetWord()
        {
            return DictionaryDao.GetWord();
        }

        /// <summary>
        /// <see cref="IRandomValueGenerator.GetUpperCaseWord"/>
        /// </summary>
        /// <returns>..</returns>
        public string GetUpperCaseWord()
        {
            return GetWord(CaseSensitivityMode.UpperCase);
        }

        /// <summary>
        /// <see cref="IRandomValueGenerator.GetLowerCaseWord"/>
        /// </summary>
        /// <returns>..</returns>
        public string GetLowerCaseWord()
        {
            return GetWord(CaseSensitivityMode.LowerCase);
        }

        /// <summary>
        /// <see cref="IRandomValueGenerator.GetWord(CaseSensitivityMode)"/>
        /// </summary>
        /// <param name="caseSensitivityMode">..</param>
        /// <returns>..</returns>
        public string GetWord(CaseSensitivityMode caseSensitivityMode)
        {
            string word = GetWord();

            IDictionary<CaseSensitivityMode, string> dictionary = new Dictionary<CaseSensitivityMode, string>();

            dictionary.Add(CaseSensitivityMode.Default, word);

            dictionary.Add(CaseSensitivityMode.UpperCase, word.ToUpper());

            dictionary.Add(CaseSensitivityMode.LowerCase, word.ToLower());

            return dictionary[caseSensitivityMode];
        }

        /// <summary>
        /// <see cref="IRandomValueGenerator.GetWords(int)"/>
        /// </summary>
        /// <param name="numberOfWords">..</param>
        /// <returns>..</returns>
        public IList<string> GetWords(int numberOfWords)
        {
            if (numberOfWords < 0)
            {
                throw new ArgumentException("The number of words cannot be negative!");
            }

            return GetWords(numberOfWords, CaseSensitivityMode.Default);
        }

        /// <summary>
        /// <see cref="IRandomValueGenerator.GetWords(int, CaseSensitivityMode)"/>
        /// </summary>
        /// <param name="numberOfWords">..</param>
        /// <param name="caseSensitivityMode">..</param>
        /// <returns>..</returns>
        public IList<string> GetWords(int numberOfWords, CaseSensitivityMode caseSensitivityMode)
        {
            if (numberOfWords < 0)
            {
                throw new ArgumentException("The number of words cannot be negative!");
            }

            IList<string> words = new List<string>();

            for (int i = 0; i < numberOfWords; i++)
            {
                words.Add(GetWord(caseSensitivityMode));
            }

            return words;
        }

        /// <summary>
        /// <see cref="IRandomValueGenerator.GetNullOrEmptyOrWhiteSpace"/>
        /// </summary>
        /// <returns>..</returns>
        public string GetNullOrEmptyOrWhiteSpace()
        {
            return GetElement(new List<string> { null, string.Empty, "\r \n \t" });
        }

        /// <summary>
        /// <see cref="IRandomValueGenerator.GetSentence(int, string, CaseSensitivityMode)"/>
        /// </summary>
        /// <param name="numberOfWords">..</param>
        /// <param name="separator">..</param>
        /// <param name="caseSensitivityMode">..</param>
        /// <returns>..</returns>
        public string GetSentence(int numberOfWords, string separator, CaseSensitivityMode caseSensitivityMode)
        {
            if (numberOfWords < 0)
            {
                throw new ArgumentException("The number of words cannot be negative!");
            }

            if (separator == null)
            {
                throw new ArgumentNullException("separator");
            }

            IList<string> words = GetWords(numberOfWords, caseSensitivityMode);

            return string.Join(separator, words);
        }

        /// <summary>
        /// <see cref="IRandomValueGenerator.GetSentence(int, string)"/>
        /// </summary>
        /// <param name="numberOfWords">..</param>
        /// <param name="separator">..</param>
        /// <returns>..</returns>
        public string GetSentence(int numberOfWords, string separator)
        {
            if (numberOfWords < 0)
            {
                throw new ArgumentException("The number of words cannot be negative!");
            }

            if (separator == null)
            {
                throw new ArgumentNullException("separator");
            }

            return GetSentence(numberOfWords, separator, CaseSensitivityMode.Default);
        }

        /// <summary>
        /// <see cref="IRandomValueGenerator.Get(int)"/>
        /// </summary>
        /// <param name="stringLength">..</param>
        /// <returns>..</returns>
        public string Get(int stringLength)
        {
            return Get(Alphabet, stringLength);
        }

        /// <summary>
        /// <see cref="IRandomValueGenerator.Get(string, int)"/>
        /// </summary>
        /// <param name="alphabet">..</param>
        /// <param name="stringLength">..</param>
        /// <returns>..</returns>
        public string Get(string alphabet, int stringLength)
        {
            if (stringLength < 0)
            {
                throw new ArgumentException("The string length cannot be negative!");
            }

            if (string.IsNullOrWhiteSpace(alphabet))
            {
                throw new ArgumentException("The alphabet cannot be null, empty or whitespace!");
            }

            return Get(alphabet, stringLength, stringLength);
        }

        /// <summary>
        /// <see cref="IRandomValueGenerator.Get(string, int, int)"/>
        /// </summary>
        /// <param name="alphabet">..</param>
        /// <param name="minLength">..</param>
        /// <param name="maxLength">..</param>
        /// <returns>..</returns>
        public string Get(string alphabet, int minLength, int maxLength)
        {
            if (minLength < 0)
            {
                throw new ArgumentException("The string minimum length cannot be negative!");
            }

            if (maxLength < 0)
            {
                throw new ArgumentException("The string maximum length cannot be negative!");
            }

            if (maxLength < minLength)
            {
                throw new ArgumentException("Maximum length is less than minimum length!");
            }

            if (string.IsNullOrWhiteSpace(alphabet))
            {
                throw new ArgumentException("The alphabet cannot be null, empty or whitespace!");
            }

            int randomSize = Get(minLength, maxLength);

            return Enumerable.Range(0, randomSize).Aggregate(new StringBuilder(), (current, next) => current.Append(alphabet[Random.Next(alphabet.Length)])).ToString();
        }

        #endregion

        #region Integer

        /// <summary>
        /// <see cref="IRandomValueGenerator.Get(int, int)"/>
        /// </summary>
        /// <param name="minValue">..</param>
        /// <param name="maxValue">..</param>
        /// <returns>..</returns>
        public int Get(int minValue, int maxValue)
        {
            if (maxValue < minValue)
            {
                throw new ArgumentException("Maximum value is less than minimum value!");
            }

            return Random.Next(minValue, maxValue);
        }

        /// <summary>
        /// <see cref="IRandomValueGenerator.GetPositive(int)"/>
        /// </summary>
        /// <param name="maxValue">..</param>
        /// <returns>..</returns>
        public int GetPositive(int maxValue)
        {
            if (maxValue < 0)
            {
                throw new ArgumentException("Maximum value cannot be negative!");
            }

            return Random.Next(maxValue);
        }

        /// <summary>
        /// <see cref="IRandomValueGenerator.GetNegative(int)"/>
        /// </summary>
        /// <param name="maxValue">..</param>
        /// <returns>..</returns>
        public int GetNegative(int maxValue)
        {
            if (maxValue >= 0)
            {
                throw new ArgumentException("Maximum value should be less than zero!");
            }

            return -Random.Next(1, Math.Abs(maxValue));
        }
        
        #endregion

        #region Decimal

        /// <summary>
        /// <see cref="IRandomValueGenerator.GetDecimal(int,int,byte)"/>
        /// </summary>
        /// <param name="minValue">..</param>
        /// <param name="maxValue">..</param>
        /// <param name="numberOfDecimals">..</param>
        /// <returns>..</returns>
        public decimal GetDecimal(int minValue, int maxValue, byte numberOfDecimals)
        {
            if (maxValue < minValue)
            {
                throw new ArgumentException("Maximum value is less than minimum value!");
            }

            string numberBeforeComa = Get(minValue, maxValue).ToString();

            string numberAfterComa = Math.Floor(Random.NextDouble() * Math.Pow(10, Random.Next(numberOfDecimals + 1))).ToString(new string('0', numberOfDecimals));

            return decimal.Parse(numberBeforeComa + "," + numberAfterComa);
        }
        
        #endregion

        #region Guid

        /// <summary>
        /// <see cref="IRandomValueGenerator.GetGuid"/>
        /// </summary>
        /// <returns>..</returns>
        public Guid GetGuid()
        {
            Guid randomGuid;

            do
            {
                byte[] bytes = new byte[16];

                Random.NextBytes(bytes);

                randomGuid = new Guid(bytes);
            }
            while (randomGuid == Guid.Empty);

            return randomGuid;
        }

        #endregion

        #region FileInfo

        /// <summary>
        /// <see cref="IRandomValueGenerator.GetTextFile(int, string)"/>
        /// </summary>
        /// <param name="numberOfWords">..</param>
        /// <param name="directoryPath">..</param>
        /// <returns>..</returns>
        public FileInfo GetTextFile(int numberOfWords, string directoryPath)
        {
            if (numberOfWords < 0)
            {
                throw new ArgumentException("The number of words cannot be negative!");
            }

            if (string.IsNullOrWhiteSpace(directoryPath))
            {
                throw new ArgumentException("The directory cannot be null, empty or whitespace!");
            }

            if (!Directory.Exists(directoryPath))
            {
                throw new DirectoryNotFoundException("The provided directory path does not exist!");
            }

            return GetTextFile(numberOfWords, Separator, directoryPath);
        }

        /// <summary>
        /// <see cref="IRandomValueGenerator.GetTextFile(int, string, string)"/>
        /// </summary>
        /// <param name="numberOfWords">..</param>
        /// <param name="separator">..</param>
        /// <param name="directoryPath">..</param>
        /// <returns>..</returns>
        public FileInfo GetTextFile(int numberOfWords, string separator, string directoryPath)
        {
            if (numberOfWords < 0)
            {
                throw new ArgumentException("The number of words cannot be negative!");
            }

            if (separator == null)
            {
                throw new ArgumentNullException("separator");
            }

            if (string.IsNullOrWhiteSpace(directoryPath))
            {
                throw new ArgumentException("The directory cannot be null, empty or whitespace!");
            }

            if (!Directory.Exists(directoryPath))
            {
                throw new DirectoryNotFoundException("The provided directory path does not exist!");
            }

            string information = GetSentence(numberOfWords, separator);

            string fileName = string.Format("{0:dd-MM-yyyy-H-mm-ss-fff}{1}", DateTime.Now, ".txt");

            string filePath = Path.Combine(directoryPath, fileName);

            FileInfo fileInfo = new FileInfo(filePath);

            if (!fileInfo.Exists)
            {
                File.WriteAllText(filePath, information);

                fileInfo.Refresh();
            }

            return fileInfo;
        }
        
        #endregion

        #region Boolean

        /// <summary>
        /// <see cref="IRandomValueGenerator.GetBoolean"/>
        /// </summary>
        /// <returns>..</returns>
        public bool GetBoolean()
        {
            return Random.NextDouble() < 0.5;
        }
        
        #endregion

        #region DateTime

        /// <summary>
        /// <see cref="IRandomValueGenerator.GetPastUtcDateTime()"/>
        /// </summary>
        /// <returns>..</returns>
        public DateTime GetPastUtcDateTime()
        {
            return DateTime.UtcNow.AddDays(-Get(1, 100));
        }

        /// <summary>
        /// <see cref="IRandomValueGenerator.GetPastUtcDateTime(DateTime)"/>
        /// </summary>
        /// <param name="dateTime">..</param>
        /// <returns>..</returns>
        public DateTime GetPastUtcDateTime(DateTime dateTime)
        {
            return dateTime.AddDays(-Get(1, 100));
        }

        /// <summary>
        /// <see cref="IRandomValueGenerator.GetFutureUtcDateTime()"/>
        /// </summary>
        /// <returns>..</returns>
        public DateTime GetFutureUtcDateTime()
        {
            return DateTime.UtcNow.AddDays(Get(1, 100));
        }

        /// <summary>
        /// <see cref="IRandomValueGenerator.GetFutureUtcDateTime(DateTime)"/>
        /// </summary>
        /// <param name="dateTime">..</param>
        /// <returns>..</returns>
        public DateTime GetFutureUtcDateTime(DateTime dateTime)
        {
            return dateTime.AddDays(Get(1, 100));
        }

        #endregion

        #region DateTimeOffset

        /// <summary>
        /// <see cref="IRandomValueGenerator.GetPastUtcDateTimeOffset"/>
        /// </summary>
        /// <returns>..</returns>
        public DateTimeOffset GetPastUtcDateTimeOffset()
        {
            return DateTimeOffset.UtcNow.AddDays(-Get(1, 100));
        }

        /// <summary>
        /// <see cref="IRandomValueGenerator.GetFutureUtcDateTimeOffset"/>
        /// </summary>
        /// <returns>..</returns>
        public DateTimeOffset GetFutureUtcDateTimeOffset()
        {
            return DateTimeOffset.UtcNow.AddDays(Get(1, 100));
        }

        #endregion

        #region Generic

        /// <summary>
        /// <see cref="IRandomValueGenerator.GetElement{T}"/>
        /// </summary>
        /// <typeparam name="T">..</typeparam>
        /// <param name="elements">..</param>
        /// <returns>..</returns>
        public T GetElement<T>(IList<T> elements)
        {
            if (elements == null)
            {
                throw new ArgumentNullException("elements");
            }

            if (!elements.Any())
            {
                throw new ArgumentException("The elements collection is empty.");
            }

            return Shuffle(elements).First();
        }
        
        #endregion

        #region Char

        /// <summary>
        /// <see cref="IRandomValueGenerator.GetLetter(string)"/>
        /// </summary>
        /// <param name="alphabet">..</param>
        /// <returns>..</returns>
        public char GetLetter(string alphabet)
        {
            if (string.IsNullOrWhiteSpace(alphabet))
            {
                throw new ArgumentException("The alphabet cannot be null, empty or whitespace!");
            }

            if (alphabet.Any(o => !char.IsLetter(o)))
            {
                throw new ArgumentException("The alphabet contains non letter characters!");
            }

            return GetElement(alphabet.ToCharArray());
        }

        /// <summary>
        /// <see cref="IRandomValueGenerator.GetLetter()"/>
        /// </summary>
        /// <returns>..</returns>
        public char GetLetter()
        {
            return GetLetter(Letters);
        }

        #endregion

        #region Byte

        /// <summary>
        /// <see cref="IRandomValueGenerator.GetDigit"/>
        /// </summary>
        /// <returns>..</returns>
        public byte GetDigit()
        {
            char[] digits = Digits.ToCharArray();

            return (byte)(Convert.ToByte(GetElement(digits)) - Convert.ToByte(digits.First()));
        }

        /// <summary>
        /// <see cref="IRandomValueGenerator.GetDifferentNumber"/>
        /// </summary>
        /// <param name="number">..</param>
        /// <returns>..</returns>
        public byte GetDifferentNumber(byte number)
        {
            byte randomNumber;

            do
            {
                randomNumber = (byte)Random.Next();
            }
            while (randomNumber == number);

            return randomNumber;
        }
        
        #endregion

        #endregion

        #region Private Methods

        /// <summary>
        /// This method shuffles the provided <paramref name="elements"/>.
        /// </summary>
        /// 
        /// <typeparam name="T">The type.</typeparam>
        /// 
        /// <param name="elements">The elements.</param>
        /// 
        /// <returns>The shuffled <paramref name="elements"/>.</returns>
        /// 
        /// <exception cref="ArgumentNullException">Throws when <paramref name="elements"/> is null.</exception>
        private IList<T> Shuffle<T>(IList<T> elements)
        {
            if (elements == null)
            {
                throw new ArgumentNullException("elements");
            }

            return elements.OrderBy(o => Random.Next()).ToList();
        }

        #endregion

        #region Private Properties

        /// <summary>
        /// <see cref="IDictionaryDao"/>
        /// </summary>
        private IDictionaryDao DictionaryDao { get; set; }

        #endregion
    }
}