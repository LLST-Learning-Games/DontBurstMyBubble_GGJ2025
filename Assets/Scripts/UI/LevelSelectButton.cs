using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectButton : MonoBehaviour
{
    public string SceneName;

    public void LoadLevel()
    {
        SceneManager.LoadScene(SceneName);
    }
}
