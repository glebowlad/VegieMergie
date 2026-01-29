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
    private Canvas canvas;
    public RectTransform leftWall;
    public RectTransform rightWall;
    private float minX;
    private float maxX;
    public event Action WhileDrag;
    public event Action DragBegined;
    public event Action OnDragFinished;
    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        rectTransform= GetComponent<RectTransform>();
        spawnerWidth= rectTransform.rect.width;
        line = transform.GetChild(0);
        line.gameObject.SetActive(false);
        CalculateLimits();


    }

    private void CalculateLimits()
    {
        Vector2 leftWallScreenPos = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera,leftWall.position);
        Vector2 rightWallScreenPos = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera,rightWall.position);
        Vector2 selfScreenPos = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera,transform.position);

       float Width = (rectTransform.rect.width * canvas.scaleFactor)/2f;
        minX = leftWallScreenPos.x + Width;
        maxX = rightWallScreenPos.x - Width;

    
    }
public void OnBeginDrag(PointerEventData eventData)
    {
        DragBegined?.Invoke();
        line.gameObject.SetActive(true);
    }
    public void OnDrag(PointerEventData eventData)
    {
        WhileDrag?.Invoke();

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
        canvas.transform as RectTransform,
        eventData.position,
        canvas.worldCamera,
        out Vector2 localPoint
    );

        // Преобразуем позиции стен в локальные координаты канваса
        Vector2 leftWallLocal = canvas.transform.InverseTransformPoint(leftWall.position);
        Vector2 rightWallLocal = canvas.transform.InverseTransformPoint(rightWall.position);

        // Учитываем половину ширины объекта
        float halfWidth = rectTransform.rect.width / 2f;

        // Ограничиваем движение
        float clampedX = Mathf.Clamp(
            localPoint.x,
            leftWallLocal.x + halfWidth,
            rightWallLocal.x - halfWidth
        );

        // Устанавливаем позицию
        rectTransform.localPosition = new Vector3(
            clampedX,
            rectTransform.localPosition.y,
            rectTransform.localPosition.z
        );



        //float mouseX = Input.mousePosition.x;
        //float clampedX = Mathf.Clamp(mouseX, minX, maxX);
        //RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform,
        //    new Vector2(clampedX, Input.mousePosition.y),
        //    canvas.worldCamera,
        //    out Vector3 worldPos
        //);

        //worldPos.y = transform.position.y;
        //worldPos.z = transform.position.z;

        //transform.position = worldPos;




        //var pos=transform.position;
        //if (Input.mousePosition.x < minX)
        //{ pos.x = minX; }
        //else if (Input.mousePosition.x > maxX)
        //{ pos.x = maxX; }
        //else
        //{ pos.x = Input.mousePosition.x;}

        //transform.position = pos;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        line.gameObject.SetActive(false);
        OnDragFinished?.Invoke();
    }
}
