using Services.Input;
using UnityEngine;

namespace Game.Hero
{
    public class HeroMover : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2 = null!;
        [SerializeField] private float _speed;
        private IInput _input = null!;

        private void Awake()
        {
            _input = Infrastructure.Game.Input;
        }

        private void Update()
        {
            ProcessMovement();
        }

        private void ProcessMovement()
        {
            float horizontalInput = _input.HorizontalMovement;
            _rigidbody2.velocity = new Vector2(horizontalInput * _speed, _rigidbody2.velocity.y);
        }
    }
}
