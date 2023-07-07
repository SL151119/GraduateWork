using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void SetRunAnimation(float speed)
    {
        animator.SetFloat("Speed", speed);
    }
}
