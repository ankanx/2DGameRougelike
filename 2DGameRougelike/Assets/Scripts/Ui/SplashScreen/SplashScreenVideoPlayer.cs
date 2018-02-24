// Examples of VideoPlayer function

using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenVideoPlayer : MonoBehaviour
{
    public Camera camera;
    private UnityEngine.Video.VideoPlayer videoPlayer;

    public int sceneIndex = 0;
    void Awake()
    {
         videoPlayer = camera.GetComponent<UnityEngine.Video.VideoPlayer>();
         videoPlayer.loopPointReached += EndReached;
    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
      
        SceneManager.LoadScene(sceneIndex);
    }
}