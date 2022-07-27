using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dice : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerDownHandler
{

    private RectTransform diceRectTransform;
    public static GameObject diceDragged;
    public static bool isDragging;

    public void OnBeginDrag(PointerEventData eventData)
    {
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        isDragging = true;
        diceDragged = gameObject;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging");
        if(RectTransformUtility.ScreenPointToWorldPointInRectangle(diceRectTransform, eventData.position, eventData.pressEventCamera, out var globalMousePosition))
        {
            diceRectTransform.position = (Vector2)globalMousePosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        isDragging=false;
        diceDragged = null;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(diceRectTransform, eventData.position, eventData.pressEventCamera, out var globalMousePosition))
        {
            diceRectTransform.position = (Vector2)globalMousePosition;
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        diceRectTransform = transform as RectTransform;
        isDragging = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
