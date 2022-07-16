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
    Vector2 velocity;

    public Sprite[] diceSprites;
    public int diceValue;
    private GameObject diceDisplay;

    public void OnEndDrag()
    {
        Debug.Log("Ended Dragging");

        rb.velocity = Vector2.zero;

        if (canAttach)
        {
            rb.velocity = Vector2.zero;
            transform.position = (Vector2)currentReceiver.transform.position;
            // currentReceiver.GetComponent<ReceiverManager>().currentDiceAttached = gameObject;
            attached = true;
        }
    }

    public void OnStartDrag()
    {
        attached = false;
        Debug.Log("Started Dragging");
    }

    // Start is called before the first frame update
    void Start()
    {
        canAttach = false;
        diceDisplay = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        diceDisplay.GetComponent<SpriteRenderer>().sprite = diceSprites[diceValue];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Reciever")
        {
            currentReceiver = collision.gameObject;

            if (!currentReceiver.GetComponent<ReceiverManager>().attached)
            {
                canAttach = true;
            }
       }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Reciever")
        {
            canAttach = false;
        }
    }
}
