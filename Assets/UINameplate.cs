using TMPro;
using UnityEngine;

public class UINameplate : MonoBehaviour
{
    [SerializeField] private TMP_Text _playerName;

    private void Awake()
    {
        _playerName.text = string.Empty;
    }

    public void Setup(string nickname)
    {
        _playerName.text = nickname;
    }
}
