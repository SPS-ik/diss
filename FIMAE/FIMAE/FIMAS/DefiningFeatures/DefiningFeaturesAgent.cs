using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIMAE.FIMAS.DefiningFeatures
{
    class DefiningFeaturesAgent
    {
        private List<DefiningFeature> _definingFeaturesList;
        private bool[,] _dependencyMap;
        private double [,][,] _valuesCompatibilityMap;

        public List<DefiningFeature> DefiningFeaturesList
        {
            get { return _definingFeaturesList; }
        }

        public bool[,] DependencyMap
        {
            get { return _dependencyMap; }
        }

        public DefiningFeaturesAgent(List<DefiningFeature> definingFeaturesList)
        {
            _definingFeaturesList = definingFeaturesList;
            _dependencyMap = new bool[_definingFeaturesList.Count, _definingFeaturesList.Count];
            _valuesCompatibilityMap = new double[_definingFeaturesList.Count, _definingFeaturesList.Count][,];
        }

        private void initValuesCompatibilityMap()
        {
            for (int i = 0; i<_definingFeaturesList.Count; i++)
            {
                _valuesCompatibilityMap[i, i] = null;
                for (int j = i; j<_definingFeaturesList.Count; j++)
                {
                    _valuesCompatibilityMap[i, j] = new double[_definingFeaturesList[i].Values.Count, _definingFeaturesList[j].Values.Count];
                    _valuesCompatibilityMap[j, i] = new double[_definingFeaturesList[j].Values.Count, _definingFeaturesList[i].Values.Count];
                }
            }

        }

        public void SetDependency(DefiningFeature feature1, DefiningFeature feature2)
        {
            if (_definingFeaturesList.Contains(feature1) && _definingFeaturesList.Contains(feature2))
            {
                _dependencyMap[_definingFeaturesList.IndexOf(feature1), _definingFeaturesList.IndexOf(feature2)] = true;
            }
        }

        public bool GetDependency(DefiningFeature feature1, DefiningFeature feature2)
        {
            if (_definingFeaturesList.Contains(feature1) && _definingFeaturesList.Contains(feature2))
            {
                return _dependencyMap[_definingFeaturesList.IndexOf(feature1), _definingFeaturesList.IndexOf(feature2)];
            }
            return false;
        }

        public void SetValuesCompatibility(DefiningFeature feature1, string value1, DefiningFeature feature2, string value2, double value)
        {
            if (_definingFeaturesList.Contains(feature1) && feature1.Values.Contains(value1) &&
                _definingFeaturesList.Contains(feature2) && feature1.Values.Contains(value2))
            {
                _valuesCompatibilityMap[_definingFeaturesList.IndexOf(feature1), _definingFeaturesList.IndexOf(feature2)]
                    [feature1.Values.IndexOf(value1), feature2.Values.IndexOf(value2)] = value;
            }
        }

        public double GetValuesCompatibility(DefiningFeature feature1, string value1, DefiningFeature feature2, string value2)
        {
            if (_definingFeaturesList.Contains(feature1) && feature1.Values.Contains(value1) &&
                _definingFeaturesList.Contains(feature2) && feature1.Values.Contains(value2))
            {
                return _valuesCompatibilityMap[_definingFeaturesList.IndexOf(feature1), _definingFeaturesList.IndexOf(feature2)]
                    [feature1.Values.IndexOf(value1), feature2.Values.IndexOf(value2)];
            }
            return 0;
        }

    }
}
