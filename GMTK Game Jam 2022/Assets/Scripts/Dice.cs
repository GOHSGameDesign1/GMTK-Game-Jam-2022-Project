using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Dice : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{

    private RectTransform diceRectTransform;
    public static GameObject diceDragged;
    public static bool isDragging;

    public int diceValue;
    public Texture[] diceSprites;
    private RawImage diceDisplay;

    public void OnBeginDrag(PointerEventData eventData)
    {
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        isDragging = true;
        diceDragged = gameObject;
    }

    public void OnDrag(PointerEventData eventData)
    {
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

    // Start is called before the first frame update
    void Awake()
    {
        diceRectTransform = transform as RectTransform;
        isDragging = false;
        diceValue = Random.Range(1, 7);
        diceDisplay = transform.GetChild(0).GetComponent<RawImage>();
        diceDisplay.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        diceDisplay.enabled = true;
        diceDisplay.texture = diceSprites[diceValue - 1];
    }
}
