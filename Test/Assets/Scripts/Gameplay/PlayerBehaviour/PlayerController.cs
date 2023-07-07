using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay.PlayerBehaviour.InventorySystem;
using JoystickStuff;
using UnityEngine;

namespace Gameplay.PlayerBehaviour
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float grabDuration;
        [SerializeField] private Joystick joystick;
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private BackPackView backPackView;

        public BackPackView BackPackView => backPackView;
        public float GrabDuration => grabDuration;
        
        private void Awake()
        {
            joystick.OnDragEnded += playerMovement.ResetVelocity;
        }
        
        private void OnDestroy()
        {
            joystick.OnDragEnded -= playerMovement.ResetVelocity;
        }

        private void FixedUpdate()
        {
            MovementTick();
        }

        private void MovementTick()
        {
            if (joystick.IsActive())
            {
                playerMovement.Move(new Vector2(joystick.GetHorizontalAxis(), joystick.GetVerticalAxis()));
            }
        }
    }
}
