using UnityEngine;
using UnityEngine.UI;

public class SoundToggle : MonoBehaviour
{
    [SerializeField] private Sprite soundOnSprite;
    [SerializeField] private Sprite soundOffSprite;
    [SerializeField] private Image buttonImage;

    private bool isMuted = false;

    void Start()
    {
        UpdateAppearance();
    }

    public void ToggleSound()
    {
        isMuted = !isMuted;

        AudioListener.pause = isMuted;

        UpdateAppearance();
    }

    void UpdateAppearance()
    {
        buttonImage.sprite = isMuted ? soundOffSprite : soundOnSprite;
    }
}
