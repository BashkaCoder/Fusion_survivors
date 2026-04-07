using Gameplay.CharacterBehaviour;
using UnityEngine;

namespace Infrastructure
{
    public struct PlayerSpawnData
    {
        public Vector3 SpawnPosition;
        public PlayerVisualType IsHost;
        public string Nickname;
    }
}