using RaceGame.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace RaceGame
{
    public class ResultsTable : MonoBehaviour
    {
        [SerializeField]
        private Transform _rowsContent;
        [SerializeField]
        private Text _rowPrefab;

        public void SetResults(float[] results)
        {
            foreach (Transform child in _rowsContent)
                Destroy(child.gameObject);
                
            var count = 1;
            foreach (var result in results)
            {
                var row = Instantiate(_rowPrefab, _rowsContent);
                var time = TimerUtils.MillisecondsToRaceTimeString(result);
                row.text = $"{count}. {time}";
                count++;
            }
        }
    }
}