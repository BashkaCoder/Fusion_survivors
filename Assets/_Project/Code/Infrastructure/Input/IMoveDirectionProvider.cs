using UnityEngine;

namespace Infrastructure.Input
{
    public interface IMoveDirectionProvider
    {
        Vector2 GetDirection();
    }
}