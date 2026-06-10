using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.Installers
{
    [CreateAssetMenu(fileName = "ScenesConfig", menuName = "ScriptableObjects/ScenesConfig")]
    public class ScenesConfig : ScriptableObject
    {
        [SerializeField, ScenePath] private string _mainMenuScenePath;
        [SerializeField, ScenePath] private string _gameplayScenePath;

        public SceneRef MainMenuSceneRef => CreateSceneRef(_mainMenuScenePath);
        public SceneRef GameplaySceneRef => CreateSceneRef(_gameplayScenePath);

        public int MainMenuBuildIndex => GetBuildIndex(_mainMenuScenePath);
        public int GameplayBuildIndex => GetBuildIndex(_gameplayScenePath);

        private static SceneRef CreateSceneRef(string scenePath)
        {
            return SceneRef.FromIndex(GetBuildIndex(scenePath));
        }

        private static int GetBuildIndex(string scenePath)
        {
            return SceneUtility.GetBuildIndexByScenePath(scenePath);
        }
    }
}