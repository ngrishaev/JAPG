using UnityEngine;

namespace Game.Hero
{
    public class GroundDetector : MonoBehaviour
    {
        [SerializeField] private Collider2D _detector = null!;
        [SerializeField] private LayerMask _groundMask;
        [SerializeField] private float _boxCastYOffset = -0.1f;
        [SerializeField] private float _boxCastXOffset = 0.1f;
        [SerializeField] private float _boxCastWidth = 1f;
        [SerializeField] private float _boxCastHeight = 1f;
        [SerializeField] private Color _gizmosColorNotGrounded = Color.red;
        [SerializeField] private Color _gizmosColorGrounded = Color.green;
        
        public bool GroundedDetected { get; private set; }

        private void OnDrawGizmos()
        {
            Gizmos.color = GroundedDetected ? _gizmosColorGrounded : _gizmosColorNotGrounded;
            Gizmos.DrawWireCube(_detector.bounds.center + new Vector3(_boxCastXOffset, _boxCastYOffset, 0),
                new Vector3(_boxCastWidth, _boxCastHeight, 0));
        }

        private void Update()
        {
            UpdateGroundedState();
        }

        private void UpdateGroundedState()
        {
            var raycastHit2D = Physics2D.BoxCast(
                _detector.bounds.center + new Vector3(_boxCastXOffset, _boxCastYOffset, 0),
                new Vector2(_boxCastWidth, _boxCastHeight), 0, Vector2.down, 0, _groundMask);

            GroundedDetected = raycastHit2D.collider != null;
        }
    }
}
