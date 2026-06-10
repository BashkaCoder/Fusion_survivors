using Infrastructure.Installers;
using UnityEngine.SceneManagement;

namespace Infrastructure.SceneLoader
{
    public class SyncSceneLoader : ISceneLoader
    {
        private readonly ScenesConfig _scenesConfig;

        public SyncSceneLoader(ScenesConfig scenesConfig)
        {
            _scenesConfig = scenesConfig;
        }

        public void LoadScene(SceneKey sceneKey)
        {
            switch (sceneKey)
            {
                case SceneKey.MainMenu:
                    SceneManager.LoadScene(_scenesConfig.MainMenuBuildIndex);
                    break;
                case SceneKey.Gameplay:
                    SceneManager.LoadScene(_scenesConfig.GameplayBuildIndex);
                    break;
            }
        }
    }
}
