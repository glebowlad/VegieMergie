using UnityEngine;
using UnityEngine.SceneManagement;
using YG;
public class RestartGame : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene(0);
        YG2.InterstitialAdvShow();
    }
}
