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
    public enum Results { LocalShops=1, Pollution=2, Biodiversity=4, Traffic=8 }
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
    public class Mission : ScriptableObject
    {
        [SerializeField]
        private List<Choice> _sequence;
        private VisualNovelManager _manager;
        private void Awake()
        {
            _manager = Instantiate<VisualNovelManager>(null);
            _manager.enabled = false;
        }
        public void ProcessSequence(int branchIndex)
        {
            _sequence = _sequence[branchIndex].postChoiceSequence.Where((l)=>l.available).Select((l)=>l.choice).ToList();
        }
        public bool IsFinalNode
        {
            get
            {
                return _sequence.Count == 0 || (_sequence.Count == 1 && _sequence[0] is FinalChoice);
            }
        }
    }

}
