using System.Linq;
using UnityEngine;

namespace RaceGame
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private StartTimer _startTimer;
        [SerializeField]
        private RaceTimer _raceTimer;
        [SerializeField]
        private CarController _carController;
        [SerializeField]
        private ResultsTable _resultsTable;
        [SerializeField]
        private Results _resultsSO;

        private Vector3 _carStartPosition = default;
        private Quaternion _carStartRotation = default;

        private void Start()
        {
            _carStartPosition = _carController.transform.localPosition;
            _carStartRotation = _carController.transform.localRotation;
            _carController.enabled = false;
            _startTimer.gameObject.SetActive(true);
        }

        public void OnStartTimerFired()
        {
            _startTimer.gameObject.SetActive(false);
            _raceTimer.StartTimer();
            _carController.enabled = true;
        }

        public void FinishRace()
        {
            _carController.enabled = false;
            _raceTimer.StopTimer();
            _resultsSO.AddNewResult(_raceTimer.Milliseconds);
            var bestResults = _resultsSO.GetBestNineResults();
            _resultsTable.SetResults(bestResults);
            _resultsTable.gameObject.SetActive(true);
        }

        public void StartNewRace()
        {
            _carController.enabled = true;
            _carController.transform.localPosition = _carStartPosition;
            _carController.transform.localRotation = _carStartRotation;
            _carController.enabled = false;
            _resultsTable.gameObject.SetActive(false);
            _raceTimer.ClearTimer();
            _startTimer.gameObject.SetActive(true);
        }
    }
}