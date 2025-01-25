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
    }

    public void Quit()
    {
        FindFirstObjectByType<FadeOut>().Play(() => SceneManager.LoadScene("Menu"));
    }

    public void ShowDeathScreen()
    {
        GetComponent<Canvas>().enabled = true;
    }
}
