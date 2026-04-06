using UnityEngine;
using UnityEngine.SceneManagement;

public class AvtorsBackToMenu : MonoBehaviour
{
    public void LoadMenuScene()
    {
        SceneManager.LoadScene(Scenes.MAIN_MENU);
    }
}
