using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private float _gravity = 1.0f;
    [SerializeField] private float _jumpHeight = 15.0f;
    [SerializeField] private int _coins;
    [SerializeField] private int _lives = 3;
    [SerializeField] private float pushPower = 1.5f;
    private CharacterController _controller;
    private float _yVelocity;
    private bool _canDoubleJump = false;
    private UIManager _uiManager;
    private Vector3 _direction;
    private Vector3 _velocity;
    private bool _canWallJump = false;
    private Vector3 _wallSurfaceNormal;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_uiManager == null)
        {
            Debug.LogError("The UI Manager is NULL."); 
        }

        _uiManager.UpdateLivesDisplay(_lives);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (_controller.isGrounded == true)
        {
            _canWallJump = false;
            _direction = new Vector3(horizontalInput, 0, 0);
            _velocity = _direction * _speed;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _yVelocity = _jumpHeight;
                _canDoubleJump = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && !_canWallJump)
            {
                if (_canDoubleJump == true)
                {
                    _yVelocity += _jumpHeight;
                    _canDoubleJump = false;
                }
            }

            if(Input.GetKeyDown(KeyCode.Space) && _canWallJump)
            {
                _yVelocity = _jumpHeight;
                _velocity = _wallSurfaceNormal * _speed;
            }

            _yVelocity -= _gravity;
        }

        _velocity.y = _yVelocity;

        _controller.Move(_velocity * Time.deltaTime);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.transform.CompareTag("Moving Box"))
        {
            Rigidbody rigidbody = hit.transform.GetComponent<Rigidbody>();

            if(rigidbody != null)
            {
                Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, 0);

                rigidbody.velocity = pushDir * pushPower;
            }
        }

        if(!_controller.isGrounded && hit.collider.CompareTag("Wall"))
        {
            Debug.DrawRay(hit.point, hit.normal, Color.red);
            _wallSurfaceNormal = hit.normal;
            _canWallJump = true;
        }
    }

    public void AddCoins()
    {
        _coins++;

        _uiManager.UpdateCoinDisplay(_coins);
    }

    public int GetCoins()
    {
        return _coins;
    }

    public void Damage()
    {
        _lives--;

        _uiManager.UpdateLivesDisplay(_lives);

        if (_lives < 1)
        {
            SceneManager.LoadScene(0);
        }
    }
}
