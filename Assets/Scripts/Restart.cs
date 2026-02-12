using UnityEngine;
using UnityEngine.SceneManagement; 

public class RestartGame : MonoBehaviour
{
    public void Restart()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        // как я понял,если в игре есть таймер,это его скидывает
        Time.timeScale = 1f;
    }
}
