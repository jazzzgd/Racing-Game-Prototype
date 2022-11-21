using UnityEngine;
using UnityEngine.UI;

namespace RaceGame
{
    public class StartTimer : MonoBehaviour
    {
        [SerializeField]
        private Text _text;
        [SerializeField]
        private GameManager _gameManager;

        private bool _growing = true;

        private void OnEnable() {
            _text.text = "3";
        }

        private void FixedUpdate()
        {
            if (int.Parse(_text.text) >= 1)
            {
                var initialScale = transform.localScale.x;
                var newScale = _growing ?
                    initialScale + Time.fixedDeltaTime :
                    initialScale - Time.deltaTime;
                transform.localScale = new Vector3(newScale, newScale, 1);
                if (newScale > 1.6f)
                    _growing = false;
                else if (newScale <= 1f)
                {
                    _growing = true;
                    _text.text = (int.Parse(_text.text) - 1).ToString();
                }
            } else {
                _gameManager.OnStartTimerFired();
            }
        }
    }
}