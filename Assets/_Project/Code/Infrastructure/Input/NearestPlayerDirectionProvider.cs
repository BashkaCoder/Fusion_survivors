using Gameplay;
using UnityEngine;

namespace Infrastructure.Input
{
    public class NearestPlayerDirectionProvider : IMoveDirectionProvider
    {
        private readonly PlayerControllerProvider _playerControllerProvider;
        private Transform _enemySelfTransform;
        
        public NearestPlayerDirectionProvider(PlayerControllerProvider playerControllerProvider)
        {
            _playerControllerProvider = playerControllerProvider;
        }

        public void SetEnemyTransform(Transform enemySelfTransform)
        {
            _enemySelfTransform = enemySelfTransform;
        }
        
        private Vector2 GetDirectionToNearestPlayer()
        {
            PlayerController nearestPlayer = null;
            var nearestDistance = float.MaxValue;
            Vector2 direction = Vector2.zero;
            
            foreach (var player in _playerControllerProvider.Players)
            {
                var distance = Vector3.Distance(_enemySelfTransform.position, player.transform.position);
                if (distance >= nearestDistance) continue;

                nearestDistance = distance;
                nearestPlayer = player;
            }

            if (nearestPlayer != null)
            {
                direction = new Vector2(
                    (nearestPlayer.transform.position - _enemySelfTransform.transform.position).x, 
                    (nearestPlayer.transform.position - _enemySelfTransform.transform.position).y
                    );
            }
                
            return direction;
        }

        public Vector2 GetDirection()
        {
            return GetDirectionToNearestPlayer();
        }
    }
}