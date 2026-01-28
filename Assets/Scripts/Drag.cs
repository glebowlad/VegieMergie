using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Drag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    
    private Transform line;
    private RectTransform rectTransform;
    private float spawnerWidth;
    private float minX = 220f;
    private float maxX= 1700f;
    public event Action WhileDrag;
    public event Action DragBegined;
    public event Action OnDragFinished;
    private void Awake()
    {
        
        rectTransform= GetComponent<RectTransform>();
        spawnerWidth= rectTransform.rect.width;
        line = transform.GetChild(0);
        line.gameObject.SetActive(false);
        
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        DragBegined?.Invoke();
        line.gameObject.SetActive(true);
    }
    public void OnDrag(PointerEventData eventData)
    {
        WhileDrag?.Invoke();
        var pos=transform.position;
        if (Input.mousePosition.x < minX)
        { pos.x = minX; }
        else if (Input.mousePosition.x > maxX)
        { pos.x = maxX; }
        else
        { pos.x = Input.mousePosition.x;}
        
        transform.position = pos;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        line.gameObject.SetActive(false);
        OnDragFinished?.Invoke();
    }
}
