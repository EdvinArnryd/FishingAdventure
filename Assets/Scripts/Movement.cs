using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    private Animator _animator;

    private int _currentState;
    private Vector2 _lastDirection = Vector2.down; // Default to IdleDown initially

    private static readonly int IdleRight = Animator.StringToHash("IdleRight");
    private static readonly int IdleLeft = Animator.StringToHash("IdleLeft");
    private static readonly int IdleUp = Animator.StringToHash("IdleUp");
    private static readonly int IdleDown = Animator.StringToHash("IdleDown");
    private static readonly int WalkRight = Animator.StringToHash("WalkRight");
    private static readonly int WalkLeft = Animator.StringToHash("WalkLeft");
    private static readonly int WalkUp = Animator.StringToHash("WalkUp");
    private static readonly int WalkDown = Animator.StringToHash("WalkDown");
    
    private static readonly int NewWalkDown = Animator.StringToHash("NewWalkDown");
    private static readonly int NewWalkUp = Animator.StringToHash("NewWalkUp");
    private static readonly int NewWalkRight = Animator.StringToHash("NewWalkRight");
    private static readonly int NewWalkLeft = Animator.StringToHash("NewWalkLeft");
    
    private static readonly int NewIdleRight = Animator.StringToHash("NewIdleRight");
    private static readonly int NewIdleLeft = Animator.StringToHash("NewIdleLeft");
    private static readonly int NewIdleUp = Animator.StringToHash("NewIdleUp");
    private static readonly int NewIdleDown = Animator.StringToHash("NewIdleDown");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        if (_animator == null)
        {
            Debug.LogError("Animator component is missing.");
        }
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector2(horizontal, vertical);

        // Move the player
        transform.position += direction * speed * Time.deltaTime;

        // Update last direction only if there is movement
        if (direction != Vector3.zero)
        {
            _lastDirection = new Vector2(horizontal, vertical).normalized;
        }

        // Get the new animation state
        var newState = GetAnimationState(horizontal, vertical);
        if (newState != _currentState)
        {
            _animator.CrossFade(newState, 0.1f);
            _currentState = newState;
        }
    }

    private int GetAnimationState(float horizontal, float vertical)
    {
        // If no input, return an idle state based on last direction
        if (horizontal == 0 && vertical == 0)
        {
            return GetIdleState();
        }

        // Determine walking state
        if (horizontal > 0) return NewWalkRight;
        if (horizontal < 0) return NewWalkLeft;
        if (vertical > 0) return NewWalkUp;
        if (vertical < 0) return NewWalkDown;

        // Default to idle if no direction matches
        return GetIdleState();
    }

    private int GetIdleState()
    {
        // Determine idle animation based on last direction
        if (_lastDirection.x > 0) return NewIdleRight;
        if (_lastDirection.x < 0) return NewIdleLeft;
        if (_lastDirection.y > 0) return NewIdleUp;
        if (_lastDirection.y < 0) return NewIdleDown;

        // Default to IdleDown for safety
        return NewIdleDown;
    }
}
