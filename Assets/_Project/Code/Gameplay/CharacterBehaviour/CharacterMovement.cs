using Fusion;
using Infrastructure.Input;
using UnityEngine;

namespace Gameplay.CharacterBehaviour
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterMovement : NetworkBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;

        [Networked] public Vector2 LookDirection { get; private set; }

        private float _moveSpeed;

        public override void Spawned()
        {
            if (Object.HasStateAuthority && LookDirection == Vector2.zero)
            {
                LookDirection = Vector2.down;
            }
        }

        public override void FixedUpdateNetwork()
        {
            if (!GetInput<GameplayInputData>(out var inputData))
            {
                Stop();
                return;
            }

            var direction = inputData.MoveDirection;
            if (direction.sqrMagnitude > 1f)
            {
                direction.Normalize();
            }

            if (direction != Vector2.zero)
            {
                LookDirection = direction;
            }

            Move(direction * _moveSpeed);
        }

        private void Move(Vector2 velocity)
        {
            _rigidbody.linearVelocity = velocity;
        }

        private void Stop()
        {
            _rigidbody.linearVelocity = Vector2.zero;
        }

        public void SetMoveSpeed(float moveSpeed)
        {
            _moveSpeed = moveSpeed;
        }
    }
}
