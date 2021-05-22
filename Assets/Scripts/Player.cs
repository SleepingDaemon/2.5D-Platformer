using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _gravity = 1.0f;
    [SerializeField] private float _jumpHeight = 8f;
    [SerializeField] private bool _canDoubleJump = false;
    private float _yVelocity;

    private int _coins = 0;
    
    private CharacterController _controller = null;
    private Vector3 _movement;

    public int Coins
    {
        get => _coins;
        set => _coins = value;
    }

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        _movement = new Vector3(x, 0, 0);
        _movement.Normalize();
        Vector3 velocity = _movement * _speed;

        if (_controller.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _yVelocity = _jumpHeight;
                _canDoubleJump = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (_canDoubleJump)
                {
                    _yVelocity += _jumpHeight;
                }
                
                _canDoubleJump = false;  
            }

            _yVelocity -= _gravity;
        }

        velocity.y = _yVelocity;

        _controller.Move(velocity * Time.deltaTime);
    }

    public void AddCoin(int amount)
    {
        _coins += amount;
    }

    public int GetCoinAmount()
    {
        return Coins;
    }
}
