using UnityEngine;
using UnityEngine.UI;

public class SoundImage : MonoBehaviour
{
    [SerializeField] private Sprite soundOnSprite;
    [SerializeField] private Sprite soundOffSprite;
    [SerializeField] private Image buttonImage;

    void Start()
    {
        UpdateAppearance();
        AudioManager.Muted += UpdateAppearance;

    }

    private void OnDestroy()
    {
        AudioManager.Muted -= UpdateAppearance;
    }

    void UpdateAppearance()
    {
        if (AudioManager.isMuted)
        {
            buttonImage.sprite = soundOffSprite;
        }
        else
        {
            buttonImage.sprite = soundOnSprite;
        }
    }
}
