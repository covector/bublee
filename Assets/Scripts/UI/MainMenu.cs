using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        FindFirstObjectByType<FadeOut>().Play(() => SceneManager.LoadScene("Level1"));
    }
}
