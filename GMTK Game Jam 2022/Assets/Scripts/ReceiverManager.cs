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
            attached = currentDiceAttached.GetComponent<DiceManager>().attached;
        }

        if(attached == false)
        {
            currentDiceAttached = null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collide");
        if(collision.gameObject.tag == "Draggable")
        {
            currentDiceAttached = collision.gameObject;
        }
    }


}
