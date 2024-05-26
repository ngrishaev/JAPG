using Services.Input;
using UnityEngine;

namespace Game.Hero
{
    public class HeroMover : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2 = null!;
        [SerializeField] private HeroAnimations _heroAnimations = null!;
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

            if (horizontalInput != 0)
                _heroAnimations.PlayRunAnimation();
            else
                _heroAnimations.PlayIdleAnimation();

            if (horizontalInput < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (horizontalInput > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }
}
