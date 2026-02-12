using UnityEngine;
using System.Collections;

public class ShakeUI : MonoBehaviour
{
    public float duration = 0.2f;
    public float magnitude = 10f;

    private Vector3 originalPosition;
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        if (rectTransform != null)
            originalPosition = rectTransform.localPosition;
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
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            rectTransform.localPosition = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        rectTransform.localPosition = originalPosition;
    }
}
