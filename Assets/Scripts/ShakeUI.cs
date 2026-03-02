using UnityEngine;
using System.Collections;
using TMPro;

public class ShakeUI : MonoBehaviour
{
    public float duration = 0.2f;
    public float magnitude = 10f;
    public int maxClicks = 5;
    public TextMeshProUGUI counterText;

    private int currentClicks;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        currentClicks = maxClicks;

        if (rectTransform != null)
        {
            originalPosition = rectTransform.localPosition;
            originalRotation = rectTransform.localRotation;
        }

        UpdateUI();
    }

    public void OnButtonClick()
    {
        if (currentClicks > 0)
        {
            currentClicks--;
            StartShake();
        }
        else
        {
            currentClicks = maxClicks;
        }
        
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (counterText != null)
            counterText.text = (currentClicks > 0) ? currentClicks.ToString() : "W";
    }

    public void StartShake()
    {
        if (rectTransform == null) return;
        StopAllCoroutines();
        StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            // Твоя прошлая формула перемещения
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            rectTransform.localPosition = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);

            // Добавленное вращение (половина от силы magnitude для мягкости)
            float z = Random.Range(-1f, 1f) * (magnitude * 0.5f);
            rectTransform.localRotation = Quaternion.Euler(0, 0, originalRotation.eulerAngles.z + z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        rectTransform.localPosition = originalPosition;
        rectTransform.localRotation = originalRotation;
    }
}
