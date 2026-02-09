using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestDrag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    Vector3 pos;
    Vector3 mousePos;
    private Camera mainCamera;
    private Vector3 offset;
    void Start()
    {
        mainCamera=Camera.main;
        pos = transform.position;
       
    }
    public void OnDrag(PointerEventData eventData)
    {

        Vector3 currentMouseWorldPos = mainCamera.ScreenToWorldPoint(eventData.position);

        // Обновляем только X координату, сохраняя Y и Z
        transform.position = new Vector3(
            currentMouseWorldPos.x + offset.x,
            transform.position.y,
            transform.position.z
        );
        //mousePos = Input.mousePosition;

        //transform.position = new Vector3(mousePos.x, pos.y,pos.z) ;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Vector3 currentMouseWorldPos = mainCamera.ScreenToWorldPoint(eventData.position);
        offset = transform.position - currentMouseWorldPos;
        Debug.Log($"{currentMouseWorldPos}");
        Debug.Log("Начали перетаскивание");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
    }
}
