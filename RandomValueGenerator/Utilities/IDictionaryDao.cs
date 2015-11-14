namespace RandomValueGenerator.Utilities
{
    /// <summary>
    /// The dictionary data access object.
    /// </summary>
    public interface IDictionaryDao
    {
        /// <summary>
        /// Returns a random word.
        /// </summary>
        /// <returns>The word.</returns>
        string GetWord();
    }
}