using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class PauseMenu : MonoBehaviour
{
    Canvas canvas;
    public bool isFrozen { get; private set; } = false;
    public bool pauseLock { get; private set; } = false;
    public bool canPause { get; set; } = true;

    private void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
    }

    public void Resume()
    {
        canvas.enabled = false;
        isFrozen = false;
        Time.timeScale = 1.0f;
        AudioListener.pause = false;
        StartCoroutine(PauseUnlock());
        //FindFirstObjectByType<ConfirmBox>().Hide();
    }

    public void Pause()
    {
        if (!canPause) { return; }
        canvas.enabled = true;
        isFrozen = true;
        Time.timeScale = 0.0f;
        AudioListener.pause = true;
        pauseLock = true;
    }

    private IEnumerator PauseUnlock()
    {
        yield return new WaitForFixedUpdate();
        pauseLock = false;
    }

    //public void Exit()
    //{

    //    FindFirstObjectByType<ConfirmBox>().Show("To Menu?", (bool yes) =>
    //    {
    //        if (yes)
    //        {
    //            canPause = false;
    //            FindFirstObjectByType<MainMenu>().Back();
    //            FindFirstObjectByType<GameManager>().ForceEnd();
    //            Time.timeScale = 1;
    //            canvas.enabled = false;
    //        }
    //    });
    //}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        //if (Input.GetKeyDown(KeyCode.Escape) && FindFirstObjectByType<UIState>().IsEmpty())
        {
            if (isFrozen) { Resume(); } else { Pause(); }
        }
    }
}