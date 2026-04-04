using UnityEngine;

namespace Infrastructure.Input
{
    public class KeyboardMoveDirectionProvider : IMoveDirectionProvider
    {
        public Vector2 GetDirection()
        {
            return new Vector2(UnityEngine.Input.GetAxisRaw("Horizontal"), UnityEngine.Input.GetAxisRaw("Vertical"));
        } 
    }
}