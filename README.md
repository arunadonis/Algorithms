# Word ladder algorithm
Word Ladder Algorithm:
The program should calculate the shortest list of four letter words, starting with StartWord, and ending with EndWord, with a number of intermediate words that must appear in the
DictionaryFile file where each word differs from the previous word by one letter only. The result should be written to the destination specified by the ResultFile argument.

For example, if StartWord = Spin, EndWord = Spot and DictionaryFile file contains

Spin

Spit

Spat

Spot

Span

then ResultFile should contain

Spin

Spit

Spot

Two examples of incorrect results:

Spin, Span, Spat, Spot (incorrect as it takes 3 changes rather than 2)

Spin, Spon, Spot (incorrect as spon is not a word)

My solution uses C#.NET console application that takes the following command-line arguments:

**DictionaryFile** - the file name of a text file containing four letter words (included in the test pack)

**StartWord** - a four letter word (that you can assume is found in the DictionaryFile file)

**EndWord** - a four letter word (that you can assume is found in the DictionaryFile file)

**ResultFile** - the file name of a text file that will contain the result

# Solution:
We can solve this problem using a graph algorithm. Here is an outline of where we are going:

1. Represent the relationships between the words as a graph.
2. Use the graph algorithm known as breadth first search to find an efficient path from the starting word to the ending word.

# Steps:
1. Load dictionary file and buildup the dictionary (sort if required)
2. Get list of words that match the length of given word
3. Build graph to have an edge from one word to another if the two words are only different by a single letter
      **a.** To figure out how to connect the words, we could compare each word in the list with every other. This is not efficient O(n^2).
      **b.** Arranging data in a bucket and then building graph would be better solution. Like bucket labelled "_pin" will have all words differ by 1st letter similarly     "s_in" will have words differ only by 2nd letter.
4. Use **breadth first search (BFS)** to find the shortest path.

