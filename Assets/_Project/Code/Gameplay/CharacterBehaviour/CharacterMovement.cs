using Infrastructure.Input;
using UnityEngine;
using Zenject;

namespace Gameplay.CharacterBehaviour
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterMovement : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;
        
        private IMoveDirectionProvider _moveDirectionProvider;
        
        private float _moveSpeed;

        [Inject]
        private void Construct(IMoveDirectionProvider provider) => _moveDirectionProvider = provider;

        private void Awake() => _rigidbody = GetComponent<Rigidbody2D>();

        private void FixedUpdate()
        {
            var direction = _moveDirectionProvider.GetDirection();
            if (direction.sqrMagnitude > 1f) direction.Normalize();
        
            Move(direction * (_moveSpeed * Time.fixedDeltaTime));
        }

        private void Move(Vector2 moveVector) => _rigidbody.MovePosition(_rigidbody.position + moveVector);
    
        public void SetMoveSpeed(float moveSpeed) => _moveSpeed = moveSpeed;
    }
}