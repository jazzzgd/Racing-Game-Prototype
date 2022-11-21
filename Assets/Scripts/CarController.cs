using UnityEngine;
using UnityEngine.InputSystem;

namespace RaceGame
{
    public class CarController : MonoBehaviour
    {
        [SerializeField]
        public Rigidbody _rigidBody;
        [SerializeField]
        public PlayerInput _playerInput;
        [SerializeField, Range(10f, 100f)]
        public float speed = 50;
        [SerializeField, Range(0.05f, 0.3f)]
        public float stopPower = 0.1f;

        private Vector3 rotationRight = new Vector3(0, 30, 0);
        private Vector3 rotationLeft = new Vector3(0, -30, 0);
        private Vector3? initialStopVelocity = null;

        private void OnEnable()
        {
            initialStopVelocity = null;
            _rigidBody.velocity = Vector3.zero;
            _rigidBody.angularVelocity = Vector3.zero;
        }

        private void OnDisable()
        {
            _rigidBody.velocity = Vector3.zero;
        }

        private void FixedUpdate()
        {
            var moveVector = _playerInput.actions["Move"].ReadValue<Vector2>();
            MoveUpdate(moveVector);
            var stopPressed = _playerInput.actions["Stop"].IsPressed();
            if (stopPressed)
                StopUpdate();
        }

        private void MoveUpdate(Vector2 vector)
        {
            initialStopVelocity = null;
            var velocity = _rigidBody.velocity;
            if (vector != Vector2.zero)
            {
                if (vector.y != 0 && velocity.magnitude < speed)
                {
                    if (vector.y > 0)
                        _rigidBody.velocity += transform.forward * 1.1f;
                    else
                        _rigidBody.velocity -= transform.forward * 1.1f;
                }
                if (velocity.magnitude > 0.1f && vector.x != 0)
                {
                    var rotation = vector.x > 0 ? rotationRight : rotationLeft;
                    var deltaRotationRight = Quaternion.Euler(rotation * Time.fixedDeltaTime);
                    _rigidBody.MoveRotation(_rigidBody.rotation * deltaRotationRight);
                }
            }
            else if (velocity.magnitude > 0.3)
                _rigidBody.velocity -= velocity * 0.01f;
            else
                _rigidBody.velocity = Vector3.zero;
        }

        private void StopUpdate()
        {
            var velocity = _rigidBody.velocity;
            if (initialStopVelocity == null)
                initialStopVelocity = velocity;
            if (velocity.magnitude > 0.2)
            {
                _rigidBody.velocity -= initialStopVelocity.Value * stopPower;
            }
            else
            {
                _rigidBody.velocity = Vector3.zero;
                initialStopVelocity = null;
            }
        }
    }
}