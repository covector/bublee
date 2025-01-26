using UnityEngine;
using static Utils;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public class DeathScreen : MonoBehaviour
{
    void Start()
    {
        GetComponent<Canvas>().enabled = false;
    }

    public void Retry()
    {
        FindFirstObjectByType<FadeOut>().Play(() => ResetLevel());
        Time.timeScale = 1;
    }

    public void Quit()
    {
        FindFirstObjectByType<FadeOut>().Play(() => SceneManager.LoadScene("Menu"));
        Time.timeScale = 1;
    }

    public void ShowDeathScreen()
    {
        Time.timeScale = 0;
        GetComponent<Canvas>().enabled = true;
    }
}
