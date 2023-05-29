using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RaceGame
{
    public class CarController : MonoBehaviour
    {
        [SerializeField] public Rigidbody _rigidBody;
        [SerializeField] public PlayerInput _playerInput;
        [SerializeField, Range(10f, 100f)] private float _speed = 50f;
        [SerializeField, Range(0.05f, 0.3f)] private float _stopPower = 0.1f;

        private Vector3 rotationRight = new Vector3(0, 30, 0);
        private Vector3 rotationLeft = new Vector3(0, -30, 0);
        private Vector3? _initialStopVelocity = null;

        private void OnEnable()
        {
            _initialStopVelocity = null;
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
            var stopPressed = _playerInput.actions["Stop"].IsPressed();
            
            MoveUpdate(moveVector);
           
            if (stopPressed)
            {
                StopUpdate();
            }

        }

        private void MoveUpdate(Vector2 vector)
        {
            _initialStopVelocity = null;
            var velocity = _rigidBody.velocity;
            
            if (vector != Vector2.zero)
            {
                if (vector.y != 0 && velocity.magnitude < _speed)
                {
                    if (vector.y > 0)
                    {
                        _rigidBody.velocity += transform.forward * 1.1f;
                    }
                        
                    else
                    {
                        _rigidBody.velocity -= transform.forward * 1.1f;
                    }
                }
                
                if (velocity.magnitude > 0.1f && vector.x != 0)
                {
                    var rotation = vector.x > 0 ? rotationRight : rotationLeft;
                    var deltaRotationRight = Quaternion.Euler(rotation * Time.fixedDeltaTime);
                    _rigidBody.MoveRotation(_rigidBody.rotation * deltaRotationRight);
                }
            }
            else if (velocity.magnitude > 0.3)
            {
                _rigidBody.velocity -= velocity * 0.01f;
            }
            else
            {
                _rigidBody.velocity = Vector3.zero;
            }
                
        }

        private void StopUpdate()
        {
            var velocity = _rigidBody.velocity;
            if (_initialStopVelocity == null)
            {
                _initialStopVelocity = velocity;
            }
                
            if (velocity.magnitude > 0.2)
            {
                _rigidBody.velocity -= _initialStopVelocity.Value * _stopPower;
            }
            else
            {
                _rigidBody.velocity = Vector3.zero;
                _initialStopVelocity = null;
            }
        }
    }
}