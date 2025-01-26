using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class CutScene : MonoBehaviour
{
    VideoPlayer videoPlayer;
    bool started = false;
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += OnVideoEnd;
        Utils.RunDelay(this, () =>
        {
            videoPlayer.Play();
            started = true;
        }, 1.4f);
    }
    void OnVideoEnd(VideoPlayer vp)
    {
        Utils.RunDelay(this, () =>
        {
            Debug.Log("Video ended");
            FindFirstObjectByType<FadeOut>().Play(() => SceneManager.LoadScene("Level1"));
        }, 1.1f);
    }
}
