using System;
using NUnit.Framework;
using WordLadder;

namespace WordLadderTests
{
    public class InputDataTests
    {
        [Test]
        public void ValidateInputs_Verify()
        {
            // arrange
            var inputData = new InputData
            {
                StartWord = "same",
                EndWord = "cost",
                DictionaryFile = "dictionaryfile",
                ResultFile = "resultfile"
            };

            // act & assert
            Assert.DoesNotThrow(()=>inputData.ValidateInputs());
        }
        
        [Test]
        public void ValidateInputs_ThrowsException_WhenStartWord_IsNull()
        {
            // arrange
            var inputData = new InputData
            {
                StartWord = null,
                EndWord = "cost",
                DictionaryFile = "dictionaryfile",
                ResultFile = "resultfile"
            };

            // act & assert
            Assert.Throws<ArgumentNullException>(()=>inputData.ValidateInputs());
        }
        
        [Test]
        public void ValidateInputs_ThrowsException_WhenStartWord_IsEmpty()
        {
            // arrange
            var inputData = new InputData
            {
                StartWord = string.Empty,
                EndWord = "cost",
                DictionaryFile = "dictionaryfile",
                ResultFile = "resultfile"
            };

            // act & assert
            Assert.Throws<ArgumentNullException>(()=>inputData.ValidateInputs());
        }
        
        [Test]
        public void ValidateInputs_ThrowsException_WhenEndWord_IsNull()
        {
            // arrange
            var inputData = new InputData
            {
                StartWord = "same",
                EndWord = null,
                DictionaryFile = "dictionaryfile",
                ResultFile = "resultfile"
            };

            // act & assert
            Assert.Throws<ArgumentNullException>(()=>inputData.ValidateInputs());
        }
        
        [Test]
        public void ValidateInputs_ThrowsException_WhenEndWord_IsEmpty()
        {
            // arrange
            var inputData = new InputData
            {
                StartWord = "same",
                EndWord = string.Empty,
                DictionaryFile = "dictionaryfile",
                ResultFile = "resultfile"
            };

            // act & assert
            Assert.Throws<ArgumentNullException>(()=>inputData.ValidateInputs());
        }
        
        [Test]
        public void ValidateInputs_ThrowsException_WhenDictionaryFile_IsNull()
        {
            // arrange
            var inputData = new InputData
            {
                StartWord = "same",
                EndWord = "cost",
                DictionaryFile = null,
                ResultFile = "resultfile"
            };

            // act & assert
            Assert.Throws<ArgumentNullException>(()=>inputData.ValidateInputs());
        }
        
        [Test]
        public void ValidateInputs_ThrowsException_WhenDictionaryFile_IsEmpty()
        {
            // arrange
            var inputData = new InputData
            {
                StartWord = "same",
                EndWord = "cost",
                DictionaryFile = string.Empty,
                ResultFile = "resultfile"
            };

            // act & assert
            Assert.Throws<ArgumentNullException>(()=>inputData.ValidateInputs());
        }
        
        [Test]
        public void ValidateInputs_ThrowsException_WhenResultFile_IsNull()
        {
            // arrange
            var inputData = new InputData
            {
                StartWord = "same",
                EndWord = "cost",
                DictionaryFile = "DictionaryFile",
                ResultFile = null
            };

            // act & assert
            Assert.Throws<ArgumentNullException>(()=>inputData.ValidateInputs());
        }
        
        [Test]
        public void ValidateInputs_ThrowsException_WhenResultFile_IsEmpty()
        {
            // arrange
            var inputData = new InputData
            {
                StartWord = "same",
                EndWord = "cost",
                DictionaryFile = "DictionaryFile",
                ResultFile = string.Empty
            };

            // act & assert
            Assert.Throws<ArgumentNullException>(()=>inputData.ValidateInputs());
        }
        
        [Test]
        public void ValidateInputs_ThrowsException_WhenStartAndEndWord_LengthNotMatch()
        {
            // arrange
            var inputData = new InputData
            {
                StartWord = "same",
                EndWord = "coast",
                DictionaryFile = "DictionaryFile",
                ResultFile = "resultfile"
            };

            // act & assert
            Assert.Throws<ArgumentException>(()=>inputData.ValidateInputs());
        }
    }
}