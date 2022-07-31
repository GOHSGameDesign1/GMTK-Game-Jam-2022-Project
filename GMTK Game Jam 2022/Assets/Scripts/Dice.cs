using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Dice : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerDownHandler
{

    private RectTransform diceRectTransform;
    public static GameObject diceDragged;
    public static bool isDragging;

    public int diceValue;
    public Texture[] diceSprites;
    public ParticleSystem explodeParticles;
    private RawImage diceDisplay;

    private GameObject cannonSlot;

    public void OnBeginDrag(PointerEventData eventData)
    {
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        if(eventData.button != 0)
        {
            return;
        }

        if(DragAndDrop.shiftHeldDown == true)
        {
            return;
        }

        isDragging = true;
        diceDragged = gameObject;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(!isDragging)
        {
            return;
        }


        if(RectTransformUtility.ScreenPointToWorldPointInRectangle(diceRectTransform, eventData.position, eventData.pressEventCamera, out var globalMousePosition))
        {
            diceRectTransform.position = (Vector2)globalMousePosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        diceDragged = null;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        isDragging=false;
    }

    // Start is called before the first frame update
    void Awake()
    {
        cannonSlot = GameObject.Find("Cannon Slot");
        diceRectTransform = transform as RectTransform;
        isDragging = false;
        diceValue = Random.Range(1, 7);
        diceDisplay = transform.GetChild(0).GetComponent<RawImage>();
        diceDisplay.enabled = false;

        if (explodeParticles != null)
        {
            Instantiate(explodeParticles, transform.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        diceDisplay.enabled = true;
        diceDisplay.texture = diceSprites[diceValue - 1];

        if(diceDragged != null)
        {
            if (diceDragged != gameObject)
            {
                GetComponent<CanvasGroup>().blocksRaycasts = false;
            }
        } else
        {
            GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Slot[] addSlots = new Slot[2];
        Slot[] minusSlots = new Slot[2];

        addSlots[0] = GameObject.Find("Add Slot 1").GetComponent<Slot>();
        addSlots[1] = GameObject.Find("Add Slot 2").GetComponent<Slot>();
        minusSlots[0] = GameObject.Find("Minus Slot 1").GetComponent<Slot>();
        minusSlots[1] = GameObject.Find("Minus Slot 2").GetComponent<Slot>();

        if (DragAndDrop.shiftHeldDown)
        {

            if (eventData.button == PointerEventData.InputButton.Left)
            {
                if (!addSlots[0].attached)
                {
                    transform.SetParent(addSlots[0].transform);
                    GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                    return;
                }
                if (!addSlots[1].attached)
                {
                    transform.SetParent(addSlots[1].transform);
                    GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                    return;
                }

            }

            if (eventData.button == PointerEventData.InputButton.Right)
            {
                if (!minusSlots[0].attached)
                {
                    transform.SetParent(minusSlots[0].transform);
                    GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                    return;
                }
                if (!minusSlots[1].attached)
                {
                    transform.SetParent(minusSlots[1].transform);
                    GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                    return;
                }
            }
            return;
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (!cannonSlot.GetComponent<Slot>().attached)
            {
                transform.SetParent(cannonSlot.transform);
                GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            }
        }

    }

    private void OnDestroy()
    {
        if (explodeParticles != null)
        {
            //Instantiate(explodeParticles, transform.position, Quaternion.identity);
        }
    }

    private void OnApplicationQuit()
    {
        explodeParticles = null;
    }
}
