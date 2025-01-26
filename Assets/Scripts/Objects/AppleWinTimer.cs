using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppleWinTimer : MonoBehaviour
{
    public float winTime =28;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Win());
    }

    IEnumerator Win()
    {
        yield return new WaitForSeconds(winTime);
        FindFirstObjectByType<FadeOut>().Play(() => SceneManager.LoadScene("Level2"));
    }
}
