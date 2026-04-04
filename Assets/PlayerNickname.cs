using TMPro;
using UnityEngine;

public class PlayerNickname : MonoBehaviour
{
    [SerializeField] private TMP_Text _playerName;
    
    public void Setup(string nickname)
    {
        _playerName.text = nickname;
    }
}
