using UnityEngine;

namespace RaceGame
{
    public class FinishWall : MonoBehaviour
    {
        [SerializeField]
        private GameManager _gameManager;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name == "Car") {
                _gameManager.FinishRace();
            }
        }
    }
}