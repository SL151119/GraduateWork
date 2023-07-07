using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.PlayerBehaviour
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float movementSpeed;
        [SerializeField] private float movementForce;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private float distanceBetweenGround;
        [SerializeField] private Rigidbody rigidBody;
        [SerializeField] private Collider colider;
        [SerializeField] private PlayerAnimator playerAnimator;
        
        
        public void Move(Vector2 axises)
        {
            ApplyMovement(axises);
            playerAnimator.SetRunAnimation(1);

        }
        
        private void ApplyMovement(Vector2 axises)
        {
            var velocity = GetYVelocity();
            Vector3 direction = new Vector3(axises.x, 0f, axises.y);
            rigidBody.velocity = direction * (Time.deltaTime * movementSpeed) * movementForce;
            UpdateRotation(rigidBody.velocity);
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, -3f, rigidBody.velocity.z);
        }

        public bool IsGrounded()
        {
            return Physics.Raycast(transform.position, Vector3.down, colider.bounds.size.y + distanceBetweenGround);
        }
        
        public void ResetVelocity(Vector2 direction)
        {
            playerAnimator.SetRunAnimation(0);
            rigidBody.velocity = new Vector3(0f, 0f, 0f);
        }

        private void UpdateRotation(Vector3 direction)
        {
            if (direction == Vector3.zero)
            {
                return;
            }
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction),
                rotationSpeed * Time.deltaTime);
        }


        private float GetYVelocity()
        {
            var velocity = rigidBody.velocity.y;
            
            if (!IsGrounded() && velocity < 0)
            {
                var gravity = Physics.gravity.y;
                velocity -= gravity * Time.deltaTime;
            }

            return velocity;
        }
        
        
    }
}
