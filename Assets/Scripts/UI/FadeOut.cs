using UnityEngine;
using System;

[DisallowMultipleComponent]
public class FadeOut : MonoBehaviour
{
    Action currentCallback;
    public GameObject fadeObj;

    public void Awake()
    {
        fadeObj.SetActive(false);
    }
    public void Play(Action callback)
    {
        currentCallback = callback;
        fadeObj.SetActive(true);
    }
    public void CallCallback()
    {
        currentCallback();
    }
}
