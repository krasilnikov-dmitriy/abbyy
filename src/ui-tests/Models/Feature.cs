using System;
namespace UITests.Models
{
    public enum FeatureType
    {
        Translate,
        Editing,
        ProofSheet,
        ProofReading

    }

    public class Feature
    {
        public FeatureType Type { get; set; }

        public String Title { get; set; }

        public String Description { get; set; }

        public override string ToString()
        {
            return String.Format("Feature:[ Type = {0}, Title = {1}, Description = {2} ]", Type, Title, Description);
        }
    }
}

