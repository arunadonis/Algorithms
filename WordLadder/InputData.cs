using System;

namespace WordLadder
{
    public class InputData
    {
        public string StartWord { get; set; }

        public string EndWord { get; set; }

        public string DictionaryFile { get; set; }

        public string ResultFile { get; set; }

        public void ValidateInputs()
        {
            StartWord.ThrowIfNullOrWhiteSpace(nameof(StartWord));
            EndWord.ThrowIfNullOrWhiteSpace(nameof(EndWord));
            DictionaryFile.ThrowIfNullOrWhiteSpace(nameof(DictionaryFile));
            ResultFile.ThrowIfNullOrWhiteSpace(nameof(ResultFile));
            
            if (StartWord.Length != EndWord.Length)
            {
                throw new ArgumentException("Start and end word length should match");
            }
            StartWord = StartWord.ToUpperInvariant();
            EndWord = EndWord.ToUpperInvariant();
        }
    }
}