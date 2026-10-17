using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadSceneAdditive(string sceneName)
    {
        Scene scene = SceneManager.GetSceneByName(sceneName);

        if (scene.IsValid() && scene.isLoaded)
        {
            return;
        }


        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    public void UnloadSceneByName(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }
}
