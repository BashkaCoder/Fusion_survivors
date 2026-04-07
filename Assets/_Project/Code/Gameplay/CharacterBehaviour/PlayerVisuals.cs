using UnityEngine;

namespace Gameplay.CharacterBehaviour
{
    public class PlayerVisuals : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private RuntimeAnimatorController _hostAnimatorController;
        [SerializeField] private RuntimeAnimatorController _clientAnimatorController;
        
        public void Setup(bool isHost)
        {
            _animator.runtimeAnimatorController = isHost ? _hostAnimatorController : _clientAnimatorController;

            _animator.Rebind();
            _animator.Update(0f);
        }
    }
}