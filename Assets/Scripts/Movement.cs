using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    
    private Animator _animator;
    
    private int _currentState;
    
    private static readonly int WalkRight = Animator.StringToHash("WalkRight");
    private static readonly int WalkLeft = Animator.StringToHash("WalkLeft");
    private static readonly int WalkUp = Animator.StringToHash("WalkUp");
    private static readonly int WalkDown = Animator.StringToHash("WalkDown");
    
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

        transform.position += direction * speed * Time.deltaTime;
        
        var newState = GetAnimationState(horizontal, vertical);
        if (newState != _currentState)
        {
            _animator.CrossFade(newState, 0.1f);
            _currentState = newState;
        }
    }
    
    private int GetAnimationState(float horizontal, float vertical)
    {
        if (horizontal > 0) return WalkRight;
        if (horizontal < 0) return WalkLeft;
        if (vertical > 0) return WalkUp;
        if (vertical < 0) return WalkDown;

        return 0; // Default to idle if no movement
    }
    
}
