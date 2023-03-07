using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _jumpPower;

    [SerializeField]
    private float _gravityMultiplier = 1;
    
    
    private Vector2 _movementInput;
    private CharacterController _characterController;

    private float _verticalVelocity;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    public void Move(Vector2 direction)
    {
        _movementInput += direction;
    }

    private void Update()
    {
        var v3 = new Vector3(_movementInput.x, 0, _movementInput.y);
        var transformedInput = transform.TransformDirection(v3);
        
        
        _characterController.Move(transformedInput * _speed * Time.deltaTime + Vector3.up * _verticalVelocity * Time.deltaTime);
        
        if (_characterController.isGrounded)
        {
            _verticalVelocity = Physics.gravity.y * _gravityMultiplier * Time.deltaTime;
        }
        else
        {
            _verticalVelocity += Physics.gravity.y * _gravityMultiplier * Time.deltaTime;
        }
        
        _movementInput = Vector3.zero;
    }

    public void Rotate(float xDelta)
    {
        transform.Rotate(Vector3.up, xDelta * Time.deltaTime);
    }

    public void Jump()
    {
        if (_characterController.isGrounded)
        {
            _verticalVelocity = _jumpPower;
        }
    }
}
