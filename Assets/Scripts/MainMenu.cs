using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame(int level)
    {
        SceneManager.LoadSceneAsync(level);
    }
}
