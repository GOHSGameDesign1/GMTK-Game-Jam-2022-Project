using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiverManager : MonoBehaviour
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

        if(attached && DragAndDrop.isDragging)
        {
            if(DragAndDrop.draggedObject == currentDiceAttached)
            {
                attached = false;
                currentDiceAttached = null;
            }
        }
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collide");
        if(collision.gameObject.tag == "Draggable")
        {
            currentDiceAttached = collision.gameObject;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collideRIGREWR");
        if (collision.gameObject.tag == "Draggable" && currentDiceAttached == null)
        {
            currentDiceAttached = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Draggable" && attached == false)
        {
            currentDiceAttached = null;
        }
    } */

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(attached == false)
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


}
