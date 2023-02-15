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
        private const int AMPLITUDE=10;
        [SerializeField, Range(-AMPLITUDE, AMPLITUDE)]
        private int _environmental;
        [SerializeField, Range(-AMPLITUDE, AMPLITUDE)]
        private int _social;
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
}


