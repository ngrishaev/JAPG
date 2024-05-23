using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2;
    [SerializeField] private GroundDetector _groundDetector;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    
    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        HandleMovement();
        HandleJump();
        //HandleShoot();
    }

    private void HandleShoot()
    {
        throw new System.NotImplementedException();
    }

    private void HandleJump()
    {
        if (!Input.GetKeyDown(KeyCode.Space))
            return;
        if (!_groundDetector.CheckIsGrounded())
            return;
        
        _rigidbody2.AddForce(Vector2.up * _jumpForce);
    }

    private void HandleMovement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        _rigidbody2.velocity = new Vector2(horizontalInput * _speed, _rigidbody2.velocity.y);
    }
}
