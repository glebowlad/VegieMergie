using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vegetable : MonoBehaviour
{

    private Drag drag;
    private Rigidbody2D rigidbody;
    private GameObject gameOverLine;
    public float radiusOffset = 0f; // Положительное число отодвинет от стенки, отрицательное — приблизит


    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.simulated = false;
      
    }
    public void Initialize(Drag _drag, GameObject _gameOverLine)
    {
        gameOverLine = _gameOverLine;
        drag = _drag;
        if (drag!= null)
        {
            drag.WhileDrag += OnDrag;
            drag.OnDragFinished += DragFinished;
        }
    }
    private void OnDrag()
    {
        transform.position= transform.parent.position;
        StopAllCoroutines();
    }
    private void DragFinished()
    {
        
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        rigidbody.simulated = true;
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
        }
    }
}
