using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegetableDrag : MonoBehaviour
{

    private Drag drag;
    private Rigidbody2D rigidbody;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.simulated = false;
       drag = GetComponentInParent<Drag>(); 
    }
    void Start()
    {
        Subscribe(drag);
    }
    void Subscribe(Drag _drag)
    {
        if (drag!= null)
        {
            drag.WhileDrag += OnDrag;
            drag.OnDragFinished += DragFinished;
        }
    }
    private void OnDrag()
    {
        transform.position= transform.parent.position;
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
    }
 

}
