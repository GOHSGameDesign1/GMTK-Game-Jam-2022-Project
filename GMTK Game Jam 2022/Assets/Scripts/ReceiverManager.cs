using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ReceiverManager : MonoBehaviour, IDropHandler
{
    public GameObject currentDiceAttached;
    public bool attached;
    public float diceValue;
    // Start is called before the first frame update
    void Start()
    {
        attached = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentDiceAttached != null)
        {
            diceValue = currentDiceAttached.GetComponent<DiceManager>().diceValue + 1;
            //attached = currentDiceAttached.GetComponent<DiceManager>().attached;
        } else
        {
            attached=false;
        }

        if(attached && DiceManager.isDragging)
        {
            if(DragAndDrop.draggedObject == currentDiceAttached)
            {
                attached = false;
                currentDiceAttached = null;
            }
        }

        CheckForDice();
    }

    


    private void OnTriggerStay2D(Collider2D collision)
    {
        if(attached == false && collision.tag == "Draggable")
        {
            if(DragAndDrop.isDragging == false) //check to see if dice was dropped into receiver
            {
                currentDiceAttached = collision.gameObject;
                diceValue = currentDiceAttached.GetComponent<DiceManager>().diceValue + 1;
                if (!currentDiceAttached.GetComponent<DiceManager>().shaking)
                {
                    attached = true;
                }
            }
        } else
        {
            if(currentDiceAttached != collision.gameObject && DragAndDrop.isDragging == false) //Check if a different dice was dropped into receiver. If so, replace currentDice
            {
               // currentDiceAttached = collision.gameObject;
            }
        }
    }

    void CheckForDice()
    {
        if (DiceManager.isDragging)
        {
            if(currentDiceAttached != null)
            {
                if(currentDiceAttached == DiceManager.draggedDice)
                {
                    transform.DetachChildren();
                }
            }

        }

        if(transform.childCount > 0)
        {
            currentDiceAttached = transform.GetChild(0).gameObject;
            return;
        }
        //currentDiceAttached = null;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (!currentDiceAttached)
        {
            DiceManager.draggedDice.transform.SetParent(transform);
        }
    }
}
