using UnityEngine;

namespace Gameplay.CharacterBehaviour
{
    public class CharacterAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private CharacterMovement _characterMovement;

        private static readonly int XAxisHash = Animator.StringToHash("Axis_x");
        private static readonly int YAxisHash = Animator.StringToHash("Axis_y");

        private Vector2 _lastNonZeroDirection = Vector2.down;

        private void Update()
        {   
            var direction = _characterMovement.LookDirection;
            if (direction != Vector2.zero)
            {
                _lastNonZeroDirection = direction;
            }

            _animator.SetFloat(XAxisHash, _lastNonZeroDirection.x);
            _animator.SetFloat(YAxisHash, _lastNonZeroDirection.y);
        }
    }
}
