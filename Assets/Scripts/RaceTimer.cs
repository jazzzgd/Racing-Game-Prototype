using System;
using System.Collections;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UI;
using RaceGame.Utils;

namespace RaceGame
{
    public class RaceTimer : MonoBehaviour
    {
        [SerializeField]
        private Text _text;

        public float Milliseconds { get; private set; } = 0f;
        private bool _started = false;

        public void ClearTimer()
        {
            Milliseconds = 0f;
            _text.text = "00:00:000";
        }

        public void StartTimer()
        {
            _started = true;
            StartCoroutine(TimerRoutine());
        }

        public void StopTimer()
        {
            _started = false;
            StopCoroutine(TimerRoutine());
        }

        private IEnumerator TimerRoutine()
        {
            while (_started)
            {
                Milliseconds += (Time.deltaTime * 1000);
                _text.text = TimerUtils.MillisecondsToRaceTimeString(Milliseconds);
                yield return null;
            }
        }
    }
}