using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public void OpenLevel (string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
