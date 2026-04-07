using UnityEngine;

namespace Gameplay.CharacterBehaviour
{
    public class PlayerVisuals : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private RuntimeAnimatorController _hostAnimatorController;
        [SerializeField] private RuntimeAnimatorController _clientAnimatorController;
        
        public void Setup(PlayerVisualType visualType)
        {
            _animator.runtimeAnimatorController = visualType switch
            {
                PlayerVisualType.Host => _hostAnimatorController,
                _ => _clientAnimatorController
            };

            _animator.Rebind();
            _animator.Update(0f);
        }
    }
}