using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionManager : MonoBehaviour
{
    private GameObject additionSlot1;
    private GameObject additionSlot2;
    private Transform output1;
    private Transform output2;

    public GameObject dicePrefab;
    private GameObject currentlySpawnedDice;

    public GameObject pointVFX;
    GameObject currentspawnedpointVFX;
    public Transform spawnVFXtransform;

    // Start is called before the first frame update
    void Start()
    {
        additionSlot1 = transform.GetChild(1).gameObject;
        additionSlot2 = transform.GetChild(2).gameObject;
        output1 = transform.GetChild(4);
        output2 = transform.GetChild(5);
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


            if(addition1.diceValue == 6  || addition2.diceValue == 6)
            {
                SpawnPoints(0);
            } else
            {
                SpawnPoints(200);
            }

            addition1.attached = false;
            Destroy(addition1.currentDiceAttached);
            Destroy(addition2.currentDiceAttached);


            while(sum > 0)
            {
                if(sum > 6)
                {
                    currentlySpawnedDice = Instantiate(dicePrefab, (Vector2)output2.position, Quaternion.identity);
                    currentlySpawnedDice.GetComponent<DiceManager>().diceValue = 5;
                    DiceRandomizer.dice.Add(currentlySpawnedDice);
                    sum -= 6;
                } else
                {
                    currentlySpawnedDice = Instantiate(dicePrefab, (Vector2)output1.position, Quaternion.identity);
                    currentlySpawnedDice.GetComponent<DiceManager>().diceValue = (int)sum - 1;
                    DiceRandomizer.dice.Add(currentlySpawnedDice);
                    sum -= sum;
                }
            }
        }
    }

    void SpawnPoints(float pointValue)
    {
        PointsManager.points += pointValue;
        currentspawnedpointVFX = Instantiate(pointVFX, spawnVFXtransform.position, Quaternion.identity);
        currentspawnedpointVFX.GetComponent<TextParticlesController>().displayPointValue("+" + pointValue.ToString());
    }
}
