using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WordLadder
{
    public interface IDictionaryHandler
    {
        /// <summary>
        /// Loads given <paramref name="dictionaryFile"/> to memory.
        /// </summary>
        /// <param name="dictionaryFile">File name of dictionary. <see cref="string"/></param>
        /// /// <returns>True if load success else false.c<see cref="bool"/></returns>
        Task<bool> LoadDictionary(string dictionaryFile);
        
        /// <summary>
        /// Finds given <paramref name="word"/> exists in the dictionary
        /// </summary>
        /// <param name="word">Word to find. <see cref="string"/></param>
        /// <returns>True if found else false.c<see cref="bool"/></returns>
        Task<bool> IsWordExists(string word);
        
        /// <summary>
        /// Gets list of wards for the <paramref name="wordLength"/>
        /// </summary>
        /// <param name="wordLength">Length of the word. <see cref="int"/></param>
        /// <returns>Collections of words matching the word length. <see cref="IReadOnlyCollection{T}"/></returns>
        Task<IReadOnlyCollection<string>> GetWordsByLength(int wordLength);
    }
    public class DictionaryHandler : IDictionaryHandler
    {
        private List<string> _dictionaryWords = new List<string>();

        public async Task<bool> LoadDictionary(string dictionaryFile)
        {
            dictionaryFile.ThrowIfNullOrWhiteSpace(nameof(dictionaryFile));
            var dictionaryData = await File.ReadAllTextAsync(dictionaryFile);
            _dictionaryWords = dictionaryData.Split(new[] {"\r\n", "\r", "\n"}, StringSplitOptions.None)
                .Select(x=> x.ToUpperInvariant()).ToList();
            return true;
        }

        public Task<bool> IsWordExists(string word)
        {
            word.ThrowIfNullOrWhiteSpace(nameof(word));
            return Task.FromResult(_dictionaryWords.Any() && _dictionaryWords.Contains(word.ToUpperInvariant()));
        }

        public Task<IReadOnlyCollection<string>> GetWordsByLength(int wordLength)
        {
            if (wordLength <= 0)
            {
                throw new ArgumentException("Word length is less than or equal to zero");
            }
            var data = _dictionaryWords.GroupBy(x => x.Length).ToList();
            var wordsList = data.Where(x => x.Key == wordLength).ToList()[0].ToArray();
            Array.Sort(wordsList);
            return Task.FromResult<IReadOnlyCollection<string>>(wordsList);
        }
    }
}