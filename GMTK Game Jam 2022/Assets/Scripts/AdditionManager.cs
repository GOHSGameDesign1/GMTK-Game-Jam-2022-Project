using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionManager : MonoBehaviour
{
    private GameObject additionSlot1;
    private GameObject additionSlot2;

    public GameObject dicePrefab;
    private GameObject currentlySpawnedDice;

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
            float sum = addition1.diceValue + addition2.diceValue;
            addition1.currentDiceAttached.SetActive(false);
            addition2.currentDiceAttached.SetActive(false);

            while(sum > 0)
            {
                if(sum > 6)
                {
                    currentlySpawnedDice = Instantiate(dicePrefab, Vector2.zero, Quaternion.identity);
                    currentlySpawnedDice.GetComponent<DiceManager>().diceValue = 5;
                    sum -= 6;
                    Debug.Log("Roll 6");
                } else
                {
                    currentlySpawnedDice = Instantiate(dicePrefab, Vector2.zero, Quaternion.identity);
                    currentlySpawnedDice.GetComponent<DiceManager>().diceValue = (int)sum - 1;
                    Debug.Log("Roll " + sum);
                    sum -= sum;
                }
            }
        }
    }
}
