using UnityEngine.SceneManagement;

namespace Infrastructure.SceneLoader
{
    public class SyncSceneLoader : ISceneLoader
    {
        public void LoadScene(SceneKey sceneKey)
        {
            switch (sceneKey)
            {
                case SceneKey.MainMenu:
                    SceneManager.LoadScene("MainMenu");
                    break;
                case SceneKey.Gameplay:
                    SceneManager.LoadScene("Gameplay");
                    break;
            }
        }
    }
}