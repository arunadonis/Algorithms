using System;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using WordLadder;

namespace WordLadderTests
{
    [TestFixture]
    public class DictionaryHandlerTests
    {
        private static readonly IDictionaryHandler _dictionaryHandler = new DictionaryHandler();
        
        [Test]
        public async Task LoadDictionary_Verify()
        {
            var status = await _dictionaryHandler.LoadDictionary("Inputs/words-english.txt");
            status.Should().BeTrue();
        }
        
        [Test]
        public async Task LoadDictionary_ThrowsException_WhenDictionaryFile_IsNull()
        {
            await _dictionaryHandler.Awaiting(x => x.LoadDictionary(null))
                .Should().ThrowAsync<ArgumentException>();
        }
        
        [Test]
        public async Task LoadDictionary_ThrowsException_WhenDictionaryFile_IsEmpty()
        {
            var dictionaryFile = string.Empty;
            await _dictionaryHandler.Awaiting(x => x.LoadDictionary(dictionaryFile))
                .Should().ThrowAsync<ArgumentException>();
        }
        
        [Test]
        public async Task IsWordExists_Verify()
        {
            await _dictionaryHandler.LoadDictionary("Inputs/words-english.txt");
            var wordFound = await _dictionaryHandler.IsWordExists("same");
            wordFound.Should().BeTrue();
        }
        
        [Test]
        public async Task IsWordExists_ReturnsFalse()
        {
            await _dictionaryHandler.LoadDictionary("Inputs/words-english.txt");
            var wordFound = await _dictionaryHandler.IsWordExists("janan");
            wordFound.Should().BeFalse();
        }
        
        [Test]
        public async Task IsWordExists_ThrowsException_WhenWord_IsNull()
        {
            await _dictionaryHandler.Awaiting(x => x.IsWordExists(null))
                .Should().ThrowAsync<ArgumentException>();
        }
        
        [Test]
        public async Task IsWordExists_ThrowsException_WhenWord_IsEmpty()
        {
            var word = string.Empty;
            await _dictionaryHandler.Awaiting(x => x.IsWordExists(word))
                .Should().ThrowAsync<ArgumentException>();
        }
        
        [Test]
        public async Task GetWordsByLength_Verify()
        {
            await _dictionaryHandler.LoadDictionary("Inputs/words-english.txt");
            var wordsByLength = await _dictionaryHandler.GetWordsByLength(3);
            wordsByLength.Should().HaveCountGreaterThan(0);
        }
        
        [Test]
        public async Task GetWordsByLength_ThrowsException_WhenWordLength_IsLessThanOrEqualToZero()
        {
            await _dictionaryHandler.Awaiting(x => x.GetWordsByLength(0))
                .Should().ThrowAsync<ArgumentException>();
            await _dictionaryHandler.Awaiting(x => x.GetWordsByLength(-1))
                .Should().ThrowAsync<ArgumentException>();
        }
    }
}