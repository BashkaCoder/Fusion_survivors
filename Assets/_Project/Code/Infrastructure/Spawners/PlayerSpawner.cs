using Gameplay.CharacterBehaviour;
using Infrastructure.Pools;
using UnityEngine;

namespace Infrastructure.Spawners
{
    public class PlayerSpawner
    {
        private readonly PlayerPool _playerPool;
        private readonly BoxCollider2D _spawnCollider;
        
        public  PlayerSpawner(PlayerPool playerPool, BoxCollider2D spawnCollider)
        {
            _playerPool = playerPool;
            _spawnCollider = spawnCollider;
        }

        public void SpawnPlayer(string nickname, bool isHost)
        {
            var spawnData = new PlayerSpawnData
            {
                SpawnPosition = GetSpawnPosition(),
                IsHost= isHost ? PlayerVisualType.Host : PlayerVisualType.Client,
                Nickname = nickname,
            };

            _playerPool.Spawn(spawnData);
        }
        
        private Vector3 GetSpawnPosition()
        {
            var bounds = _spawnCollider.bounds;

            return bounds.center + new Vector3(
                (Random.value - 0.5f) * bounds.size.x,
                (Random.value - 0.5f) * bounds.size.y,
                0f);
        }
    }
}