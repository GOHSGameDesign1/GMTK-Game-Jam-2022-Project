using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionManager : MonoBehaviour
{
    private GameObject additionSlot1;
    private GameObject additionSlot2;

    // Start is called before the first frame update
    void Start()
    {
        additionSlot1 = transform.GetChild(0).gameObject;
        additionSlot2 = transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(additionSlot1.GetComponent<ReceiverManager>().attached && additionSlot2.GetComponent<ReceiverManager>().attached)
        {
            float sum = additionSlot1.GetComponent<ReceiverManager>().diceValue + additionSlot2.GetComponent<ReceiverManager>().diceValue;
            Debug.Log(sum);
        }
    }
}
