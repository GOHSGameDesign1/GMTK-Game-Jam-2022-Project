using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour, IDrag
{
    [SerializeField]
    private bool canAttach;
    public bool attached;
    public GameObject currentReceiver;

    public Rigidbody2D rb;
    public GameObject cannonReceiver;


    public Sprite[] diceSprites;
    public int diceValue;
    private SpriteRenderer diceDisplay;
    private SpriteRenderer diceBaseDisplay;

    public bool shaking;

    public void OnEndDrag()
    {

        diceBaseDisplay.sortingOrder = 2 + DragAndDrop.orderLayer;
        diceDisplay.sortingOrder = 3 + DragAndDrop.orderLayer;

        rb.velocity = Vector2.zero;

        if (canAttach && !currentReceiver.GetComponent<ReceiverManager>().attached)
        {
            rb.velocity = Vector2.zero;
            transform.position = (Vector2)currentReceiver.transform.position;
            return;
        }

        if(canAttach && currentReceiver.GetComponent<ReceiverManager>().attached)
        {
            rb.velocity = Vector2.zero;
            //StartCoroutine(Shake());
        }
    }

    public void OnStartDrag()
    {
        attached = false;

        diceBaseDisplay.sortingOrder = 10 + DragAndDrop.orderLayer;
        diceDisplay.sortingOrder = 11 + DragAndDrop.orderLayer;
    }

    public void OnRightClick()
    {
        transform.position = (Vector2)cannonReceiver.transform.position;
    }

    // Start is called before the first frame update
    void Awake()
    {
        canAttach = false;
        diceValue = Random.Range(0, 6);
        diceDisplay = transform.GetChild(0).GetComponent<SpriteRenderer>();
        diceBaseDisplay = gameObject.GetComponent<SpriteRenderer>();
        diceDisplay.enabled = false;
        shaking = false;
        cannonReceiver = GameObject.Find("Cannon Receiver");

        diceBaseDisplay.sortingOrder += DragAndDrop.orderLayer;
        diceDisplay.sortingOrder += DragAndDrop.orderLayer;
    }

    // Update is called once per frame
    void Update()
    {
        diceDisplay.enabled = true;
        diceDisplay.GetComponent<SpriteRenderer>().sprite = diceSprites[diceValue];

        //CheckSorting(DragAndDrop.draggedObject);
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Reciever")
        {
            currentReceiver = collision.gameObject;
            canAttach = true;
        }
    }*/

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Reciever" && DragAndDrop.isDragging)
        {
            canAttach = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Reciever" && DragAndDrop.isDragging)
        {
            currentReceiver = collision.gameObject;
            canAttach = true;
        }

        /*if(collision.gameObject.tag == "Reciever" && !DragAndDrop.isDragging)
        {
            if (currentReceiver.GetComponent<ReceiverManager>().currentDiceAttached != null)
            {
                if(currentReceiver.GetComponent<ReceiverManager>().currentDiceAttached = gameObject)
                {
                    rb.velocity = Vector2.zero;
                    transform.position = (Vector2)currentReceiver.transform.position;
                    return;
                }
            }
        }*/
    }

    private void OnDestroy()
    {
        attached = false;
    }

    void CheckSorting(GameObject draggedObject)
    {
        if(draggedObject != null)
        {
           if(draggedObject != gameObject)
            {
                diceBaseDisplay.sortingOrder = 2;
                diceDisplay.sortingOrder = 3;
                return;
            }
            diceBaseDisplay.sortingOrder = 10;
            diceDisplay.sortingOrder = 11;

        }
    }

    IEnumerator Shake()
    {
        Debug.Log("Can't Place dice!");
        shaking = true;
        Vector2 oldPosition = rb.position;
        for(int i = 0; i < 50; i++)
        {
            rb.position += Random.insideUnitCircle.normalized * 0.025f;
            yield return new WaitForEndOfFrame();
        }
        shaking = false;
         rb.position = oldPosition;
    }
}
