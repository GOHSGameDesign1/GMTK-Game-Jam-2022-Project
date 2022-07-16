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

        Calculate(additionSlot1.GetComponent<ReceiverManager>(), additionSlot2.GetComponent<ReceiverManager>());

    }

    private void Calculate(ReceiverManager addition1, ReceiverManager addition2)
    {
        if(addition1.attached && addition2.attached)
        {
            float sum = additionSlot1.GetComponent<ReceiverManager>().diceValue + additionSlot2.GetComponent<ReceiverManager>().diceValue;
            
        }
    }
}
