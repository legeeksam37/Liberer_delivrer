using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sequence = VNSequence;
using System.Linq;

namespace ScenarioStructures
{
    [Flags, Obsolete("", true)]
    public enum PositiveResults { LocalShops, Biodiversity, ClearSky, Traffic, HappyPeoples }
    [Flags, Obsolete("", true)]
    public enum NegativeResults { LocalShops, Pollution, Biodiversity, Traffic, TruckForOnePackage }
    [Flags]
    public enum Results { LocalShops = 1, Pollution = 2, Biodiversity = 4, Traffic = 8 }
    [Serializable]
    public class Result
    {
        [SerializeField]
        private Results _positives;
        [SerializeField]
        private Results _negatives;
        /// <summary>
        /// Nb of positive outcomes - nb of positives, to get the "global" impact of the choices
        /// </summary>
        public int GlobalValue
        {
            get
            {
                return CountSetBits(_positives) - CountSetBits(_negatives);
            }
        }
        int CountSetBits(Results flags)
        {
            int count = 0;
            int flagValue = (int)flags;
            while (flagValue != 0)
            {
                count += flagValue & 1;
                flagValue >>= 1;
            }
            return count;
        }
    }
    [Serializable]
    [CreateAssetMenu(fileName = "New mission", menuName = "Scenarisation/Mission")]
    public class Mission
    {
        [SerializeField]
        private List<Choice> _sequence;
        [SerializeField]
        private List<EnabledChoice> _leeves;
        public void UpdateLeaves()
        {
            _leeves = new List<EnabledChoice>();
            var visited = new HashSet<Choice>();
            Visit(_sequence[0], visited);
            _leeves.RemoveAll(x => !visited.Contains(x.Item2));
        }

        private void Visit(Choice choice, HashSet<Choice> visited)
        {
            visited.Add(choice);
            _leeves.Add((true, choice));
            foreach (var subChoice in choice.postChoiceSequence)
            {
                if (!visited.Contains(subChoice))
                    Visit(subChoice, visited);
                /*var inLeeves = _leeves.FirstOrDefault(x => x.Item2 == c);
                if (inLeeves == default(EnabledChoice))
                {
                }*/
            }

        }

        private VisualNovelManager _manager;
        private void Awake()
        {
            _manager = GameObject.Instantiate<VisualNovelManager>(null);
            _manager.enabled = false;
        }
        public void ProcessSequence(int branchIndex)
        {
            _sequence = _sequence[branchIndex].postChoiceSequence
                .Select((l, i) => (i, l))
                .Where(il => il.l.choices[il.i])
                .Select(l => l.l).ToList();
        }
        public bool IsFinalNode
        {
            get
            {
                return _sequence.Count == 0 || (_sequence.Count == 1 && _sequence[0] is FinalChoice);
            }
        }

    }
    public class Graph<T>
    {
        public Graph() { }
        public Graph(IEnumerable<T> vertices, IEnumerable<Tuple<T, T>> edges)
        {
            foreach (var vertex in vertices)
                AddVertex(vertex);

            foreach (var edge in edges)
                AddEdge(edge);
        }

        public Dictionary<T, HashSet<T>> AdjacencyList { get; } = new Dictionary<T, HashSet<T>>();

        public void AddVertex(T vertex)
        {
            AdjacencyList[vertex] = new HashSet<T>();
        }

        public void AddEdge(Tuple<T, T> edge)
        {
            if (AdjacencyList.ContainsKey(edge.Item1) && AdjacencyList.ContainsKey(edge.Item2))
            {
                AdjacencyList[edge.Item1].Add(edge.Item2);
                AdjacencyList[edge.Item2].Add(edge.Item1);
            }
        }
    }
}


