using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void LoadGameScene()
    {
        SceneManager.LoadScene("MainLevel");
    }

    public void LoadTitleScreen()
    {
        SceneManager.LoadScene("StartScene");
    }

}
