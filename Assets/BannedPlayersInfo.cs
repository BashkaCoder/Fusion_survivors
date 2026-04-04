using System.Collections.Generic;
using UnityEngine;

public class BannedPlayersInfo : MonoBehaviour
{
    [SerializeField] private Transform _nicknamesParent;
    [SerializeField] private PlayerNickname _playerNicknamePrefab;

    private readonly HashSet<string> _playerNicknames = new();

    public bool Contains(string nickname)
    {
        return _playerNicknames.Contains(nickname);
    }
    
    public void Add(string nickname)
    {
        var newNickname = Instantiate(_playerNicknamePrefab, _nicknamesParent);
        _playerNicknames.Add(nickname);
        
        newNickname.GetComponent<PlayerNickname>().Setup(nickname);
    }
}
