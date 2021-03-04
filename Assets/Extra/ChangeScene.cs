using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeScene : MonoBehaviour
{
    public bool useSceneName;
    public int sceneIndex;
    public string sceneName;

    public void Change()
    {
        if (useSceneName)
            SceneManager.LoadScene(sceneName);
        else
            SceneManager.LoadScene(sceneIndex);
    }
}