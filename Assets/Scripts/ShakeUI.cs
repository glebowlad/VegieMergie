using UnityEngine;
using System.Collections;

public class ShakeUI : MonoBehaviour
{
    public RectTransform targetImage;

    public float duration = 0.2f; 
    public float magnitude = 10f; 

    private Vector3 originalPosition;

    void Start()
    {
        if (targetImage != null)
            originalPosition = targetImage.localPosition;
    }

    
    public void StartShake()
    {
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

            targetImage.localPosition = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);

            elapsed += Time.deltaTime;
            yield return null; 
        }

        
        targetImage.localPosition = originalPosition;
    }
}
