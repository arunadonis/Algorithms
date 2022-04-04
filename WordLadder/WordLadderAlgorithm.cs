using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WordLadder
{
    public interface IWordLadderAlgorithm
    {
        /// <summary>
        /// Finds shortest path from <paramref name="startWord"/> to <paramref name="endWord"/> in the given
        /// <paramref name="listOfWords"/>
        /// </summary>
        /// <param name="listOfWords">List of words to search. <see cref="IReadOnlyCollection{T}"/></param>
        /// <param name="startWord">Starting word. <see cref="string"/></param>
        /// <param name="endWord">Ending word. <see cref="string"/></param>
        /// <returns>Path to the <paramref name="endWord"/> from <paramref name="startWord"/>. <see cref="IReadOnlyCollection{T}"/></returns>
        Task<IReadOnlyCollection<string>> FindShortestPath(IReadOnlyCollection<string> listOfWords, string startWord, string endWord);
    }
    public class WordLadderAlgorithm : IWordLadderAlgorithm
    {
        public Task<IReadOnlyCollection<string>> FindShortestPath(IReadOnlyCollection<string> listOfWords, string startWord, string endWord)
        {
            listOfWords.ThrowIfNull(nameof(listOfWords));
            startWord.ThrowIfNullOrWhiteSpace(nameof(startWord));
            endWord.ThrowIfNullOrWhiteSpace(nameof(endWord));
            
            var graph = BuildGraph(listOfWords);
            return Task.FromResult(Traverse(graph, startWord, endWord));
        }

        private static Dictionary<string, HashSet<string>> BuildGraph(IReadOnlyCollection<string> words)
        {
            var buckets = new Dictionary<string, List<string>>();
            var graph = new Dictionary<string, HashSet<string>>();

            foreach (var word in words)
            {
                for (var i = 0; i < word.Length; i++)
                {
                    // create buckets with for words differing by one letter
                    var bucket = word.Remove(i,1).Insert(i,"_");
                    if (!buckets.Keys.Contains(bucket))
                    {
                        buckets.Add(bucket, new List<string>());
                    }

                    buckets[bucket].Add(word);
                }
            }
            
            // add vertices and edges for words in the same bucket
            foreach (var word in from bucket in buckets 
                     let neighbors = bucket 
                     select bucket.Value.SelectMany(x => neighbors.Value, (x, y) => new {x, y}) 
                     into prod from word in prod where !word.x.Equals(word.y) select word)
            {
                if (!graph.Keys.Contains(word.x))
                {
                    graph[word.x] = new HashSet<string>();
                }
                graph[word.x].Add(word.y);
                        
                if (!graph.Keys.Contains(word.y))
                {
                    graph[word.y] = new HashSet<string>();
                }
                graph[word.y].Add(word.x);
            }
            
            return graph;
        }

        private static IReadOnlyCollection<string> Traverse(
            IReadOnlyDictionary<string, HashSet<string>> graph, 
            string startWord, 
            string endWord)
        {
            var visited = new HashSet<string>();
            var queue = new Queue<List<string>>();
            queue.Enqueue(new List<string>(){startWord});

            while (queue.Count > 0)
            {
                var path = queue.Dequeue();
                var vertex = path[^1];

                // check if we have reached the destination
                if (vertex.Equals(endWord))
                {
                    return path;
                }
                
                var neighbors = graph[vertex].Except(visited).ToList();
                foreach (var neighbor in neighbors)
                {
                    visited.Add(neighbor);
                    var data = new List<string>(path) {neighbor};
                    queue.Enqueue(data);
                }
            }

            return new List<string>();
        }
    }
}