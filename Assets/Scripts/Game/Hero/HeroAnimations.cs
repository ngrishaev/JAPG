using UnityEngine;

namespace Game.Hero
{
    public class HeroAnimations : MonoBehaviour
    {
        [SerializeField] private Animator _animator = null!;

        private static readonly int IdleAnimation = Animator.StringToHash("Idle");
        private static readonly int RunAnimation = Animator.StringToHash("Run");
        private static readonly int JumpAnimation = Animator.StringToHash("Jump");
        

        public void PlayRunAnimation()
        {
            _animator.Play(RunAnimation);
        }
        
        public void PlayIdleAnimation()
        {
            _animator.Play(IdleAnimation);
        }
        
        public void PlayJumpAnimation()
        {
            _animator.Play(JumpAnimation);
        }
    }
}
