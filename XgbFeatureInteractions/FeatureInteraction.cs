﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XgbFeatureInteractions
{
    public class FeatureInteraction : IComparable
    {
        public string Name { get; set; }
        public int Depth { get; set; }
        public double Gain { get; set; }
        public double Cover { get; set; }
        public double FScore { get; set; }
        public double FScoreWeighted { get; set; }
        public double AverageFScoreWeighted { get; set; }
        public double AverageGain { get; set; }
        public double ExpectedGain { get; set; }

        public bool HasLeafStatistics { get; set; }
        public double SumLeafValuesLeft { get; set; }
        public double SumLeafCoversLeft { get; set; }
        public double SumLeafValuesRight { get; set; }
        public double SumLeafCoversRight { get; set; }

        public FeatureInteraction(HashSet<XgbTreeNode> interaction, double gain, double cover, double pathProbability, double fScore = 1)
        {
            List<string> features = interaction.OrderBy(x => x.Feature).Select(y => y.Feature).ToList();
            Name = string.Join("|", features);
            Depth = interaction.Count - 1;
            Gain = gain;
            Cover = cover;
            FScore = fScore;
            FScoreWeighted = pathProbability;
            AverageFScoreWeighted = FScoreWeighted / FScore;
            AverageGain = Gain / FScore;
            ExpectedGain = Gain * pathProbability;
            HasLeafStatistics = false;
        }
        
        public int CompareTo(object obj)
        {
            var featInteraction = obj as FeatureInteraction;
            return FIScoreComparer.Compare(this, featInteraction);
        }

        public override string ToString()
        {
            return String.Format("{0}:{1}", Name, Gain);
        }

        public override bool Equals(object obj)
        {
            var featInteraction = obj as FeatureInteraction;
            return this.Name.Equals(featInteraction.Name);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

    }
}
