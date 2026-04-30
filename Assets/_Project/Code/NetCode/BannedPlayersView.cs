using UnityEngine;

namespace NetCode
{
    public class BannedPlayersView : MonoBehaviour
    {
        [SerializeField] private Transform _nicknamesParent;
        [SerializeField] private UINameplate _uiNameplatePrefab;
        
        public void HandleBannedPlayedAdded(string nickname)
        {
            var newNickname = Instantiate(_uiNameplatePrefab, _nicknamesParent);
            newNickname.GetComponent<UINameplate>().Setup(nickname);
        }
    }
}
