using Fusion;
using UnityEngine;

namespace Infrastructure.Input
{
    public struct GameplayInputData : INetworkInput
    {
        public Vector2 MoveDirection;
    }
}
