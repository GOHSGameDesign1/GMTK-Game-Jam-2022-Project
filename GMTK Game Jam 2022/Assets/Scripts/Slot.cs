using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{

    [SerializeField]
    public GameObject currentDiceAttached;
    public bool attached;
    public int diceValue;

    public Canvas canvas;

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Drop");
        if (!currentDiceAttached)
        {
            Dice.diceDragged.transform.SetParent(transform);
            Dice.diceDragged.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        attached = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(currentDiceAttached.name);
        CheckForDice();

        if(currentDiceAttached != null)
        {
            diceValue = currentDiceAttached.GetComponent<Dice>().diceValue;
        }
    }

    void CheckForDice()
    {
        if (Dice.isDragging)
        {
            if (currentDiceAttached != null)
            {
                if (currentDiceAttached == Dice.diceDragged)
                {
                    Dice.diceDragged.transform.SetParent(canvas.transform);
                }
            }

        }

        if (transform.childCount > 0)
        {
            currentDiceAttached = transform.GetChild(0).gameObject;
            GetComponent<CanvasGroup>().blocksRaycasts = false;
            diceValue = currentDiceAttached.GetComponent<Dice>().diceValue;
            attached = true;
            return;
        }
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        attached = false;
        currentDiceAttached = null;
    }
}
