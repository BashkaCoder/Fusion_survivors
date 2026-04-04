using UnityEngine;

namespace Infrastructure.Input
{
    public class MobileMoveDirectionProvider : IMoveDirectionProvider
    {
        public Vector2 GetDirection()
        {
            return new Vector2(SimpleInput.GetAxisRaw("Horizontal"), SimpleInput.GetAxisRaw("Vertical"));
        }
    }
}