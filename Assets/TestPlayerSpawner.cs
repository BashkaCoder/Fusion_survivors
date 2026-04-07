using Infrastructure.Spawners;
using UnityEngine;
using Zenject;

public class TestPlayerSpawner : MonoBehaviour
{
    private PlayerSpawner _playerSpawner;
    
    [Inject]
    public void Construct(PlayerSpawner playerSpawner)
    {
        _playerSpawner = playerSpawner;
    }

    private void Start()
    {
        _playerSpawner.SpawnPlayer("Bashka", true);
        _playerSpawner.SpawnPlayer("Shinlyan", false);
    }
}
