using Infrastructure.Input;
using UnityEngine;
using Zenject;

namespace Gameplay.CharacterBehaviour
{
    public class CharacterAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        
        private static readonly int XAxisHash = Animator.StringToHash("Axis_x");
        private static readonly int YAxisHash = Animator.StringToHash("Axis_y");
        
        private IMoveDirectionProvider _moveDirectionProvider;
        
        private Vector2 _lastNonZeroDirection;
        
        [Inject]
        private void Construct(IMoveDirectionProvider provider) => _moveDirectionProvider = provider;

        private void Update()
        {
            Vector2 direction = _moveDirectionProvider.GetDirection();
            if (direction != Vector2.zero) _lastNonZeroDirection = direction;
            
            _animator.SetFloat(XAxisHash, _lastNonZeroDirection.x);
            _animator.SetFloat(YAxisHash, _lastNonZeroDirection.y);
        }
    }
}