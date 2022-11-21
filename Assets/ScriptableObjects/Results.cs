using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace RaceGame
{
    [CreateAssetMenu(fileName = "Results", menuName = "Results SO", order = 10)]
    public class Results : ScriptableObject
    {
        [SerializeField]
        private List<float> _results;

        private void Start()
        {
            if (_results == null)
                _results = new List<float>();
        }

        public float[] GetBestNineResults()
            => _results.OrderBy(result => result).Take(9).ToArray();

        public void AddNewResult(float milliseconds)
            => _results.Add(milliseconds);
    }
}
