using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace JoystickStuff
{
    [RequireComponent(typeof(Image))]
    public class Joystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private RectTransform touchArea;

        private Vector3 _currentPosition;
        private float _dragRadius;

        private bool _active;

        public event Action<Vector2> OnDragStarted;
        public event Action<Vector2> OnDragEnded;
        public event Action<Vector2> OnDragged;

        private void Awake()
        {
            _dragRadius = touchArea.rect.width / 2;
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector3 dragPosition = eventData.position;
            _currentPosition = Vector3.ClampMagnitude(dragPosition - touchArea.transform.position, _dragRadius);
            OnDragged?.Invoke(_currentPosition);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnDragStarted?.Invoke(_currentPosition);
            _active = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _currentPosition = Vector3.zero;
            OnDragEnded?.Invoke(_currentPosition);
            _active = false;
        }

        public bool IsActive()
        {
            return _active;
        }

        public float GetHorizontalAxis()
        {
            return _currentPosition.normalized.x;
        }

        public float GetVerticalAxis()
        {
            return _currentPosition.normalized.y;
        }
    }
}