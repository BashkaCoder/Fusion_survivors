using System;
using System.Collections;
using NetCode;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class GameMenuUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _connectionStatusText;
        [SerializeField] private TMP_InputField _roomNameInput;
        [SerializeField] private TMP_InputField _nicknameInput;
        [SerializeField] private Button _startGameButton;
        
        [Space]
        [SerializeField] private float _deniedStatusFadeDuration;
        
        //TODO: Action<roomName, nickName> обернуть в свой делегат?
        public Action<string, string> StartGameRequested;

        private Coroutine _deniedRequestRoutine;
        
        [Inject]
        private void Construct() { }

        private void OnEnable()
        {
            _startGameButton.onClick.AddListener(RequestGameStart);

            ResetView();
        }

        private void OnDisable()
        {
            _startGameButton.onClick.RemoveListener(RequestGameStart);

            if (_deniedRequestRoutine != null)
            {
                StopCoroutine(_deniedRequestRoutine);
                _deniedRequestRoutine = null;
            }
        }

        public void HandleDeniedRequest(JoinRequestResult deniedRequestResult)
        {
            if (_deniedRequestRoutine != null)
            {
                StopCoroutine(_deniedRequestRoutine);
            }

            _deniedRequestRoutine = StartCoroutine(ShowDeniedRequestStatus(deniedRequestResult));
        }

        private void ResetView()
        {
            _connectionStatusText.text = "";
            _connectionStatusText.gameObject.SetActive(false);
            _roomNameInput.text = "";
            _nicknameInput.text = "";
            _startGameButton.interactable = true;
        }

        private IEnumerator ShowDeniedRequestStatus(JoinRequestResult deniedRequestResult)
        {
            _startGameButton.interactable = false;
            _connectionStatusText.gameObject.SetActive(true);
            _connectionStatusText.text = GetDeniedRequestStatusText(deniedRequestResult);

            var statusColor = _connectionStatusText.color;
            statusColor.a = 1f;
            _connectionStatusText.color = statusColor;

            if (_deniedStatusFadeDuration <= 0f)
            {
                HideDeniedRequestStatus(statusColor);
                yield break;
            }

            var elapsedTime = 0f;

            while (elapsedTime < _deniedStatusFadeDuration)
            {
                elapsedTime += Time.deltaTime;

                statusColor.a = Mathf.Lerp(1f, 0f, elapsedTime / _deniedStatusFadeDuration);
                _connectionStatusText.color = statusColor;

                yield return null;
            }

            HideDeniedRequestStatus(statusColor);
        }

        private void HideDeniedRequestStatus(Color statusColor)
        {
            statusColor.a = 0f;
            _connectionStatusText.color = statusColor;
            _connectionStatusText.gameObject.SetActive(false);
            _startGameButton.interactable = true;
            _deniedRequestRoutine = null;
        }

        private static string GetDeniedRequestStatusText(JoinRequestResult deniedRequestResult)
        {
            return deniedRequestResult switch
            {
                JoinRequestResult.DeniedFullRoom => "Комната заполнена",
                JoinRequestResult.DeniedNicknameDuplicate => "Игрок с таким ником уже в комнате",
                JoinRequestResult.DeniedNicknameBanned => "Игрок с таким ником заблокирован в этой комнате",
                _ => string.Empty
            };
        }

        private void RequestGameStart()
        {
            StartGameRequested.Invoke(_roomNameInput.text, _nicknameInput.text);
        }
    }
}
