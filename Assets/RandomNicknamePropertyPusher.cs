using System.Collections;
using Fusion;
using NetCode;
using UnityEngine;
using Zenject;

//TODO: Удалить
public class RandomNicknamePropertyPusher : NetworkBehaviour
{
    private RoomNicknamesService _roomNicknamesService;
    
    private readonly YieldInstruction _delay =  new WaitForSeconds(2.5f);

    private static readonly string[] NicknamePrefixes =
    {
        "Swift", "Lucky", "Silent", "Brave", "Crazy", "Dark", "Red", "Wild", "Iron", "Shadow"
    };

    private static readonly string[] NicknameNames =
    {
        "Wolf", "Fox", "Bear", "Raven", "Tiger", "Hunter", "Ghost", "Storm", "Viper", "Falcon"
    };

    [Inject]
    private void Construct(RoomNicknamesService roomNicknamesService)
    {
        _roomNicknamesService = roomNicknamesService;
    }
    
    private void Start()
    {
        StartCoroutine(PushRandomNickname());
    }

    private IEnumerator PushRandomNickname()
    {
        while (true)
        {
            yield return _delay;
            UpdateNicknameProperties();
        }
    }

    private void UpdateNicknameProperties()
    {
        if (Runner == null || Runner.SessionInfo == null) return;
        
        var randomNickname = GenerateRandomNickname();
        if (Random.value > 0.5f)
        {
            _roomNicknamesService.AddNicknameAlive(Runner.SessionInfo, randomNickname);
        }
        else
        {
            _roomNicknamesService.AddNicknameBanned(Runner.SessionInfo, randomNickname);
        }
    }
    
    private string GenerateRandomNickname()
    {
        var prefix = NicknamePrefixes[Random.Range(0, NicknamePrefixes.Length)];
        var root = NicknameNames[Random.Range(0, NicknameNames.Length)];
        var number = Random.Range(100, 1000);

        return $"{prefix}{root}{number}";
    }
}