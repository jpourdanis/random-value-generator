using System;
using System.Collections.Generic;
using System.IO;
using RandomValueGenerator.Utilities;

namespace RandomValueGenerator
{
    public interface IRandomValueGenerator
    {
        #region Void

        /// <summary>
        /// Setting up the <see cref="IDictionaryDao"/> of your choice.
        /// </summary>
        /// <param name="dictionaryDao"><see cref="IDictionaryDao"/></param>
        /// <list type="Exceptions">
        /// <exception cref="ArgumentNullException">Throws when <see cref="IDictionaryDao"/> is null.</exception>
        /// </list>
        void SetDictionaryDao(IDictionaryDao dictionaryDao);

        #endregion

        #region String

        /// <summary>
        /// Returns a random word from a <see cref="IDictionaryDao"/>.
        /// </summary>
        /// <returns>The word.</returns>
        string GetWord();

        /// <summary>
        /// Returns a random word with the <see cref="CaseSensitivityMode"/> of your choice.
        /// </summary>
        /// <param name="caseSensitivityMode"></param>
        /// <returns>The word.</returns>
        string GetWord(CaseSensitivityMode caseSensitivityMode);

        /// <summary>
        /// Returns a random uppercase word.
        /// </summary>
        /// <returns>The word.</returns>
        string GetUpperCaseWord();

        /// <summary>
        /// Returns a random lowercase word.
        /// </summary>
        /// <returns>The word.</returns>
        string GetLowerCaseWord();

        /// <summary>
        /// Returns a list of random words.
        /// </summary>
        /// <param name="numberOfWords">The number of words.</param>
        /// <returns>The word list.</returns>
        /// <list type="Exceptions">
        /// <exception cref="ArgumentException">Throws when <see cref="numberOfWords"/> is negative number.</exception>
        /// </list>
        IList<string> GetWords(int numberOfWords);

        /// <summary>
        /// Returns a list of random words with the <see cref="CaseSensitivityMode"/> of your choice.
        /// </summary>
        /// <param name="numberOfWords">The number of words.</param>
        /// <param name="caseSensitivityMode"><see cref="CaseSensitivityMode"/></param>
        /// <returns>The word list.</returns>
        /// <list type="Exceptions">
        /// <exception cref="ArgumentException">Throws when <see cref="numberOfWords"/> is negative number.</exception>
        /// </list>
        IList<string> GetWords(int numberOfWords, CaseSensitivityMode caseSensitivityMode);

        /// <summary>
        /// Returns a random sentence with separator of your choice.
        /// </summary>
        /// <param name="numberOfWords">The number of words.</param>
        /// <param name="separator">The separator of words.</param>
        /// <returns>The sentence.</returns>
        /// <list type="Exceptions">
        /// <exception cref="ArgumentException">Throws when <see cref="numberOfWords"/> is negative number.</exception>
        /// <exception cref="ArgumentNullException">Throws when <see cref="separator"/> is null.</exception>
        /// </list>
        string GetSentence(int numberOfWords, string separator);

        /// <summary>
        /// Returns a random sentence with separator and <see cref="CaseSensitivityMode"/> of your choice.
        /// </summary>
        /// <param name="numberOfWords">The number of words.</param>
        /// <param name="separator">The separator of words.</param>
        /// <param name="caseSensitivityMode"><see cref="CaseSensitivityMode"/></param>
        /// <returns>The sentence.</returns>
        /// <list type="Exceptions">
        /// <exception cref="ArgumentException">Throws when <see cref="numberOfWords"/> is negative number.</exception>
        /// <exception cref="ArgumentNullException">Throws when <see cref="separator"/> is null.</exception>
        /// </list>
        string GetSentence(int numberOfWords, string separator, CaseSensitivityMode caseSensitivityMode);

        /// <summary>
        /// Returns a random string with length of your choice.
        /// </summary>
        /// <param name="stringLength">The string length.</param>
        /// <returns>The string.</returns>
        /// <list type="Exceptions">
        /// <exception cref="ArgumentException">Throws when <see cref="stringLength"/> is negative number.</exception>
        /// </list>
        string Get(int stringLength);

        /// <summary>
        /// Returns a random string from an alphabet and length of your choice.
        /// </summary>
        /// <param name="alphabet">The alphabet.</param>
        /// <param name="stringLength">The string length.</param>
        /// <returns>The string.</returns>
        /// <list type="Exceptions">
        /// <exception cref="ArgumentException">Throws when <see cref="stringLength"/> is negative number.</exception>
        /// <exception cref="ArgumentException">Throws when <see cref="alphabet"/> is null,empty or whitespace.</exception>
        /// </list>
        string Get(string alphabet, int stringLength);

        /// <summary>
        /// Returns a random string from an alphabet and between a length of choice.
        /// </summary>
        /// <param name="alphabet">The alphabet.</param>
        /// <param name="minLength">The minimum length of the string.</param>
        /// <param name="maxLength">The maximum length of the string.</param>
        /// <returns>The string.</returns>
        /// <list type="Exceptions">
        /// <exception cref="ArgumentException">Throws when <see cref="minLength"/> is negative number.</exception>
        /// <exception cref="ArgumentException">Throws when <see cref="maxLength"/> is negative number.</exception>
        /// <exception cref="ArgumentException">Throws when <see cref="maxLength"/> is bigger than <see cref="minLength"/>.</exception>
        /// <exception cref="ArgumentException">Throws when <see cref="alphabet"/> is null,empty or whitespace.</exception>
        /// </list>
        string Get(string alphabet, int minLength, int maxLength);

        /// <summary>
        /// Returns the default Latin letters with numbers as alphabet.
        /// </summary>
        /// <returns>The Latin letters and numbers.</returns>
        string GetDefaultAlphabet();

        /// <summary>
        /// Returns the default Latin letters [a-z][A-Z].
        /// </summary>
        /// <returns>The Latin letters.</returns>
        string GetDefaultLetters();

        /// <summary>
        /// Returns the default digits [0-9].
        /// </summary>
        /// <returns>The default digits.</returns>
        string GetDefaultDigits();

        /// <summary>
        /// Returns the default separator of a words in a sentence [Whitespace].
        /// </summary>
        /// <returns>The separator.</returns>
        string GetDefaultSeparator();

        /// <summary>
        /// Returns all keyboard symbols both [ Alt + numpad] except those of [Whitespace] and beep noise.
        /// </summary>
        /// <returns>The separator.</returns>
        string GetDefaultSymbols();

        /// <summary>
        /// Return a random email address.
        /// </summary>
        /// <returns>The email address.</returns>
        string GetEmailAddress();

        /// <summary>
        /// Returns a random whitespace.
        /// </summary>
        /// <returns>The whitespace.</returns>
        string GetNullOrEmptyOrWhiteSpace();

        #endregion

        #region Integer

        /// <summary>
        /// Returns a random integer between a minimum and maximum value.
        /// </summary>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        /// <returns>The integer.</returns>
        /// <list type="Exceptions">
        /// <exception cref="ArgumentException">Throws when <see cref="maxValue"/> is bigger than <see cref="minValue"/>.</exception>
        /// </list>
        int Get(int minValue, int maxValue);

        /// <summary>
        /// Returns a positive random integer.
        /// </summary>
        /// <param name="maxValue">The maximum value.</param>
        /// <returns>The integer.</returns>
        /// <list type="Exceptions">
        /// <exception cref="ArgumentException">Throws when <see cref="maxValue"/> is negative.</exception>
        /// </list>
        int GetPositive(int maxValue);

        /// <summary>
        /// Returns a negative random integer.
        /// </summary>
        /// <param name="maxValue">The maximum value.</param>
        /// <returns>The integer.</returns>
        /// <list type="Exceptions">
        /// <exception cref="ArgumentException">Throws when <see cref="maxValue"/> is positive.</exception>
        /// </list>
        int GetNegative(int maxValue);

        #endregion

        #region Decimal

        /// <summary>
        /// Returns a random positive decimal between a minimum and maximum value.
        /// </summary>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        /// <param name="numberOfDecimals">The number of decimals digits after comma.</param>
        /// <returns>The decimal.</returns>
        /// <list type="Exceptions">
        /// <exception cref="ArgumentException">Throws when <see cref="maxValue"/> is bigger than <see cref="minValue"/>.</exception>
        /// </list>
        decimal GetDecimal(int minValue, int maxValue, byte numberOfDecimals);

        #endregion

        #region Guid

        /// <summary>
        /// Returns a random GUID.
        /// </summary>
        /// <returns>The GUID.</returns>
        Guid GetGuid();

        #endregion

        #region FileInfo

        /// <summary>
        /// Returns a text file with random words on specific directory path.
        /// </summary>
        /// <param name="numberOfWords">The number of words.</param>
        /// <param name="directoryPath">The directory path.</param>
        /// <returns>The text file.</returns>
        /// <list type="Exceptions">
        /// <exception cref="ArgumentException">Throws when <see cref="numberOfWords"/> is negative number.</exception>
        /// <exception cref="ArgumentException">Throws when <see cref="directoryPath"/> is null,empty or whitespace.</exception>
        /// <exception cref="DirectoryNotFoundException">Throws when <see cref="directoryPath"/> cannot be found.</exception>
        /// </list>
        FileInfo GetTextFile(int numberOfWords, string directoryPath);

        /// <summary>
        /// Returns a text file with random words on specific directory path with separator for words of your choice.
        /// </summary>
        /// <param name="numberOfWords">The number of words.</param>
        /// <param name="separator">The separator of words.</param>
        /// <param name="directoryPath">The directory path.</param>
        /// <returns>The text file.</returns>
        /// <list type="Exceptions">
        /// <exception cref="ArgumentException">Throws when <see cref="numberOfWords"/> is negative number.</exception>
        /// <exception cref="ArgumentException">Throws when <see cref="directoryPath"/> is null,empty or whitespace.</exception>
        /// <exception cref="DirectoryNotFoundException">Throws when <see cref="directoryPath"/> cannot be found.</exception>
        /// <exception cref="ArgumentNullException">Throws when <see cref="separator"/> is null.</exception>
        /// </list>
        FileInfo GetTextFile(int numberOfWords, string separator, string directoryPath);

        #endregion

        #region Boolean

        /// <summary>
        /// Returns a random Boolean value.
        /// </summary>
        /// <returns>The Boolean value.</returns>
        bool GetBoolean();

        #endregion

        #region DateTime

        /// <summary>
        /// Returns a random past UTC date time.
        /// </summary>
        /// <returns>The DateTime.</returns>
        DateTime GetPastUtcDateTime();

        /// <summary>
        /// Returns a random past UTC date time from giving datetime.
        /// </summary>
        /// <param name="dateTime">The datetime.</param>
        /// <returns>The past DateTime.</returns>
        DateTime GetPastUtcDateTime(DateTime dateTime);

        /// <summary>
        /// Returns a random future UTC date time.
        /// </summary>
        /// <returns>The DateTime.</returns>
        DateTime GetFutureUtcDateTime();

        /// <summary>
        /// Returns a random future UTC date time from giving datetime.
        /// </summary>
        /// <param name="dateTime">The datetime.</param>
        /// <returns>The future DateTime.</returns>
        DateTime GetFutureUtcDateTime(DateTime dateTime);

        #endregion

        #region DateTimeOffset

        /// <summary>
        /// Returns a random past UTC date time offset.
        /// </summary>
        /// <returns>The DateTimeOffset.</returns>
        DateTimeOffset GetPastUtcDateTimeOffset();

        /// <summary>
        /// Returns a random future UTC date time offset.
        /// </summary>
        /// <returns>The DateTimeOffset.</returns>
        DateTimeOffset GetFutureUtcDateTimeOffset();

        #endregion

        #region Generic

        /// <summary>
        /// Returns an object of a list randomly.
        /// </summary>
        /// <typeparam name="T">The type of object.</typeparam>
        /// <param name="elements">The list of objects.</param>
        /// <returns>The object.</returns>
        /// <list type="Exceptions">
        /// <exception cref="ArgumentNullException">Throws when <see cref="elements"/> is null.</exception>
        /// <exception cref="ArgumentException">Throws when any of <see cref="elements"/> is empty.</exception>
        /// </list>
        T GetElement<T>(IList<T> elements);

        #endregion

        #region Char

        /// <summary>
        /// Returns a random character of an alphabet.
        /// </summary>
        /// <param name="alphabet">The alphabet.</param>
        /// <returns>The letter.</returns>
        /// <list type="Exceptions">
        /// <exception cref="ArgumentException">Throws when <see cref="alphabet"/> is null,empty or whitespace.</exception>
        /// <exception cref="ArgumentException">Throws when <see cref="alphabet"/> contains non letter characters.</exception>
        /// </list>
        char GetLetter(string alphabet);

        /// <summary>
        /// Returns a random letter of Latin letter list.
        /// </summary>
        /// <returns>The letter.</returns>
        char GetLetter();

        #endregion

        #region Byte

        /// <summary>
        /// Returns a random digit of digit's list [0-9].
        /// </summary>
        /// <returns>The digit.</returns>
        byte GetDigit();

        /// <summary>
        /// Returns a different number of <see cref="number"/>.
        /// </summary>
        /// <param name="number">A number.</param>
        /// <returns>The different number.</returns>
        byte GetDifferentNumber(byte number);

        #endregion
    }
}