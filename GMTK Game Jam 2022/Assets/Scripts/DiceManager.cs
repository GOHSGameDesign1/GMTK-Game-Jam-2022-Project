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


    public Sprite[] diceSprites;
    public int diceValue;
    private SpriteRenderer diceDisplay;
    private SpriteRenderer diceBaseDisplay;

    public void OnEndDrag()
    {

        diceBaseDisplay.sortingOrder = 0;
        diceDisplay.sortingOrder = 1;

        rb.velocity = Vector2.zero;

        if (canAttach && !currentReceiver.GetComponent<ReceiverManager>().attached)
        {
            rb.velocity = Vector2.zero;
            transform.position = (Vector2)currentReceiver.transform.position;
            currentReceiver.GetComponent<ReceiverManager>().currentDiceAttached = gameObject;
            attached = true;
        }
    }

    public void OnStartDrag()
    {
        attached = false;

        diceBaseDisplay.sortingOrder = 2;
        diceDisplay.sortingOrder = 3;
    }

    // Start is called before the first frame update
    void Awake()
    {
        canAttach = false;
        diceValue = Random.Range(0, 6);
        diceDisplay = transform.GetChild(0).GetComponent<SpriteRenderer>();
        diceBaseDisplay = gameObject.GetComponent<SpriteRenderer>();
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
            canAttach = true;
       }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Reciever")
        {
            canAttach = false;
        }
    }

    private void OnDestroy()
    {
        attached = false;
    }
}
