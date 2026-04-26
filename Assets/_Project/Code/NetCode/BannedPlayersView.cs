using UnityEngine;

namespace NetCode
{
    public class BannedPlayersView : MonoBehaviour
    {
        [SerializeField] private Transform _nicknamesParent;
        [SerializeField] private PlayerNickname _playerNicknamePrefab;
        
        public void HandleBannedPlayedAdded(string nickname)
        {
            var newNickname = Instantiate(_playerNicknamePrefab, _nicknamesParent);
            newNickname.GetComponent<PlayerNickname>().Setup(nickname);
        }
    }
}
