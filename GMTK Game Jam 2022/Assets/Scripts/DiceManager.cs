using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour, IDrag
{
    [SerializeField]
    private bool canAttach;
    private GameObject currentReciever;

    public Rigidbody2D rb;
    Vector2 velocity = Vector2.zero;

    public Sprite[] diceSprites;
    public int diceValue;
    private GameObject diceDisplay;

    public void OnEndDrag()
    {
        Debug.Log("Ended Dragging");

        rb.velocity = Vector2.SmoothDamp(rb.velocity, Vector2.zero, ref velocity, 0.01f);

        if (canAttach)
        {
            rb.velocity = Vector2.zero;
            transform.position = currentReciever.transform.position;
        }
    }

    public void OnStartDrag()
    {
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
        //if(collision.gameObject.tag == "Reciever")
        //{
            canAttach = true;
            currentReciever = collision.gameObject;
       // }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Reciever")
        {
            canAttach = false;
        }
    }
}
