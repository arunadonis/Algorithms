using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WordLadder
{
    class Program
    {
        private static readonly IDictionaryHandler _dictionaryHandler = new DictionaryHandler();
        private static readonly IWordLadderAlgorithm _wordLadderAlgorithm = new WordLadderAlgorithm();
        private static async Task Main()
        {
            // get user inputs for start word, end word, dictionary file and result file
            var inputData = GetInputs();
            
            // validate inputs
            inputData.ValidateInputs();
            
            // load dictionary data and check given words in dictionary
            var loadSuccess  = await _dictionaryHandler.LoadDictionary(inputData.DictionaryFile);
            if (!loadSuccess)
            {
                Console.WriteLine("Loading dictionary failed");
                return;
            }
            if (!await _dictionaryHandler.IsWordExists(inputData.StartWord))
            {
                throw new ArgumentException("Start word is not found in the dictionary");
            }
            if (!await _dictionaryHandler.IsWordExists(inputData.EndWord))
            {
                throw new ArgumentException("End word is not found in the dictionary");
            }

            // get all words matching to the length of given words
            var dictionaryWords = await _dictionaryHandler.GetWordsByLength(inputData.StartWord.Length);

            // find path to the end word from start word
            var path = await _wordLadderAlgorithm.FindShortestPath(dictionaryWords, inputData.StartWord, inputData.EndWord);

            if (path.Contains(inputData.EndWord))
            {
                Console.WriteLine($"Path found from {inputData.StartWord} to {inputData.EndWord}");
                path.ToList().ForEach(Console.WriteLine);
                await File.WriteAllLinesAsync(inputData.ResultFile, path);
            }
            else
            {
                Console.WriteLine($"No path could be found from {inputData.StartWord} to {inputData.EndWord}");
            }
        }

        private static InputData GetInputs()
        {
            var inputData = new InputData();
            Console.WriteLine("Start word: ");
            inputData.StartWord = Console.ReadLine();
            Console.WriteLine("End word: ");
            inputData.EndWord = Console.ReadLine();
            Console.WriteLine("Dictionary file: ");
            inputData.DictionaryFile = Console.ReadLine();
            Console.WriteLine("Result file: ");
            inputData.ResultFile = Console.ReadLine();

            return inputData;
        }
    }
}
