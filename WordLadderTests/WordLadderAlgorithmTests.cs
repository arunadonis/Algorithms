using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using WordLadder;

namespace WordLadderTests
{
    [TestFixture]
    public class WordLadderAlgorithmTests
    {
        private static readonly IWordLadderAlgorithm _wordLadderAlgorithm = new WordLadderAlgorithm();
        
        [Test]
        public async Task FindShortestPath_Verify()
        {
            var path = new List<string>() {"spin", "spit", "spot"};
            var wordsList = new[] {"spin", "abcd", "xyza", "test", "spit", "spat", "spot"};

            var result = await _wordLadderAlgorithm.FindShortestPath(
                wordsList, "spin", "spot");

            result.Should().BeEquivalentTo(path);
        }
        
        [Test]
        public async Task FindShortestPath_Verify_UsingMock()
        {
            var path = new List<string>() {"spin", "spit", "spot"};
            var mock = new Mock<IWordLadderAlgorithm>();
            mock.Setup(x => x.FindShortestPath(
                    It.IsAny<IReadOnlyCollection<string>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()))
                .ReturnsAsync(path);

            var result = await _wordLadderAlgorithm.FindShortestPath(
                new[] {"spin", "spit", "spat", "spot"}, "spin", "spot");

            result.Should().BeEquivalentTo(path);
        }

        [Test]
        public async Task FindShortestPath_ThrowsException_WhenListOfWords_IsNull()
        {
            await _wordLadderAlgorithm
                .Awaiting(x => x.FindShortestPath(null, "same", "cost"))
                .Should().ThrowAsync<ArgumentNullException>();
        }
        
        [Test]
        public async Task FindShortestPath_ThrowsException_WhenStartWord_IsNull()
        {
            await _wordLadderAlgorithm
                .Awaiting(x => x.FindShortestPath(new List<string>(), null, "cost"))
                .Should().ThrowAsync<ArgumentNullException>();
        }
        
        [Test]
        public async Task FindShortestPath_ThrowsException_WhenStartWord_IsEmpty()
        {
            await _wordLadderAlgorithm
                .Awaiting(x => x.FindShortestPath(new List<string>(), string.Empty, "cost"))
                .Should().ThrowAsync<ArgumentNullException>();
        }
        
        [Test]
        public async Task FindShortestPath_ThrowsException_WhenEndWord_IsNull()
        {
            await _wordLadderAlgorithm
                .Awaiting(x => x.FindShortestPath(new List<string>(), "same", null))
                .Should().ThrowAsync<ArgumentNullException>();
        }
        
        [Test]
        public async Task FindShortestPath_ThrowsException_WhenEndWord_IsEmpty()
        {
            await _wordLadderAlgorithm
                .Awaiting(x => x.FindShortestPath(new List<string>(), "same", string.Empty))
                .Should().ThrowAsync<ArgumentNullException>();
        }
    }
}