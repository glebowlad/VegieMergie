using UnityEngine;

public class AppSettings : MonoBehaviour
{
    void Awake()
    {
        // Этот блок кода сработает ТОЛЬКО на iOS или Android
#if UNITY_IOS || UNITY_ANDROID 
        Application.targetFrameRate = 60;
            QualitySettings.vSyncCount = 0;
            Debug.Log("Мобильные настройки FPS применены");
#else
        // На ПК (в редакторе или билде) можно оставить настройки по умолчанию 
        // или задать другие
        Application.targetFrameRate = -1; 
#endif

        DontDestroyOnLoad(gameObject);
    }
}