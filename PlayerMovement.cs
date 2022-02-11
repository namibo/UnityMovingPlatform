using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody2D _rb2D;
    public float _jumpForce;
    public float _movementAcceleration;
    public float _maxMoveSpeed;

    private float _horizontalDirection;
    private float _VerticalDirection;

    public LayerMask _movingPlatformMask;
    public float _groundRaycastLength;

    public SpriteRenderer _playerSprite;
    private Color oldColor;
    public Color highlightColor;

    private bool _isOnPlatform;
    private Rigidbody2D _platformRB2D;

    // This is just a basic Rigidbody movement. No Fallmultipler, Airdrag etc....
    // No real groundcheck etc...

    void Awake()
    {
        oldColor = _playerSprite.color;
    }

    void Update()
    {
        _horizontalDirection = Input.GetAxis("Horizontal");
        _VerticalDirection = Input.GetAxis("Vertical");

        if(Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, _groundRaycastLength, _movingPlatformMask);
        if(hit)
        {
            _isOnPlatform = true;
            _playerSprite.color = highlightColor;
            _platformRB2D = hit.collider.gameObject.GetComponent<Rigidbody2D>();
        }
        else
        {
            _isOnPlatform = false;
            _playerSprite.color = oldColor;
            _platformRB2D = null;
        }
        if(_isOnPlatform)
            _rb2D.velocity = _platformRB2D.velocity;

        MoveCharacter();
    }

    void MoveCharacter()
    {
        _rb2D.AddForce(new Vector2(_horizontalDirection,0f) * _movementAcceleration);
        if (Mathf.Abs(_rb2D.velocity.x) > _maxMoveSpeed)
            _rb2D.velocity = new Vector2(Mathf.Sign(_rb2D.velocity.x) * _maxMoveSpeed, _rb2D.velocity.y);
    }

    void Jump()
    {
        _rb2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        //Ground Check
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * _groundRaycastLength);
    }
}
