using UnityEngine;
using UnityEngine.UIElements;

namespace JoystickStuff
{
    public class JoystickView : MonoBehaviour
    {
        [SerializeField] private Transform handler;
        [SerializeField] private Joystick joystick;

        private Vector3 _defaultDirectionButtonPosition;
        private Vector3 _currentPosition;
        private float _dragRadius;
        private float _fixedVariable = 45f;

        private void Awake()
        {
            _defaultDirectionButtonPosition = handler.transform.localPosition;
            joystick.OnDragStarted += OnDragStarted;
            joystick.OnDragEnded += OnDragEnded;
            joystick.OnDragged += OnDragged;
        }

        private void OnDestroy()
        {
            joystick.OnDragStarted -= OnDragStarted;
            joystick.OnDragEnded -= OnDragEnded;
            joystick.OnDragged -= OnDragged;
        }

        private void OnDragged(Vector2 position)
        {
            var currentDirection = position;
            handler.transform.position = transform.TransformPoint(currentDirection);
        }

        private void OnDragStarted(Vector2 position)
        {
            joystick.transform.position = position;
        }
            

        private void OnDragEnded(Vector2 position)
        {
            handler.transform.localPosition = _defaultDirectionButtonPosition;
        }
    }
}