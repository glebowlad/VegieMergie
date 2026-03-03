using System;
using System.Collections;
using UnityEngine;
using YG;
public class Vegetable : MonoBehaviour
{
    private Drag drag;
    private Rigidbody2D rb; 
    private GameObject gameOverLine;
    public float radiusOffset = 0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.simulated = false;
    }

    public void Initialize(Drag _drag, GameObject _gameOverLine)
    {
        gameOverLine = _gameOverLine;
        drag = _drag;
        if (drag != null)
        {
            drag.WhileDrag += OnDrag;
            drag.OnDragFinished += DragFinished;
        }
    }

    private void OnDrag()
    {
        transform.position = transform.parent.position;
        StopAllCoroutines();
    }

    private void DragFinished()
    {
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        
        rb.simulated = true; 

        if (drag != null)
        {
            drag.WhileDrag -= OnDrag;
            drag.OnDragFinished -= DragFinished;
        }
        this.enabled = false;
        StartCoroutine(CheckGameOver());
    }

    private IEnumerator CheckGameOver()
    {
        yield return new WaitForSeconds(2f);
        if (gameObject.transform.position.y > gameOverLine.transform.position.y)
        {
            Debug.Log("GAME OVER");
            YG2.InterstitialAdvShow();
        }
    }
}
