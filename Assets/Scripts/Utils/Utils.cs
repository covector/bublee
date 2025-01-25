using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Utils
{
    //public static void RunDelay(Action action, float delay, bool unscaledTime = false)
    //{
    //    GameManager.instance.StartCoroutine(_RunDelay(action, delay, unscaledTime));
    //}
    public static IEnumerator RunDelay(MonoBehaviour mb, Action action, float delay, bool unscaledTime = false)
    {
        IEnumerator coroutine = _RunDelay(action, delay, unscaledTime);
        mb.StartCoroutine(coroutine);
        return coroutine;
    }
    public static IEnumerator _RunDelay(Action action, float delay, bool unscaledTime = false)
    {
        yield return unscaledTime ? new WaitForSecondsRealtime(delay) : new WaitForSeconds(delay);
        action();
    }

    public static void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}