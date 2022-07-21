using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtractionManager : MonoBehaviour
{
    private GameObject subtractionSlot1;
    private GameObject subtractionSlot2;
    private Transform output1;

    public GameObject dicePrefab;
    private GameObject currentlySpawnedDice;

    public GameObject pointVFX;
    GameObject currentspawnedpointVFX;
    public Transform spawnVFXtransform;

    // Start is called before the first frame update
    void Start()
    {
        subtractionSlot1 = transform.GetChild(1).gameObject;
        subtractionSlot2 = transform.GetChild(2).gameObject;
        output1 = transform.GetChild(4);
    }

    // Update is called once per frame
    void Update()
    {
        Calculate(subtractionSlot1.GetComponent<ReceiverManager>(), subtractionSlot2.GetComponent<ReceiverManager>());
    }

    private void Calculate(ReceiverManager sub1, ReceiverManager sub2)
    {
        if (sub1.attached && sub2.attached)
        {
            PointsManager.points += 350;
            currentspawnedpointVFX = Instantiate(pointVFX, spawnVFXtransform.position, Quaternion.identity);
            currentspawnedpointVFX.GetComponent<TextParticlesController>().displayPointValue("+350");

            float difference = Mathf.Abs(sub1.diceValue - sub2.diceValue);

            sub1.attached = false;

            Destroy(sub1.currentDiceAttached);
            Destroy(sub2.currentDiceAttached);

            if(difference <= 0)
            {
                currentlySpawnedDice = Instantiate(dicePrefab, (Vector2)output1.position, Quaternion.identity);
                currentlySpawnedDice.GetComponent<DiceManager>().diceValue = 0;
                DiceRandomizer.dice.Add(currentlySpawnedDice);
            } else
            {
                currentlySpawnedDice = Instantiate(dicePrefab, (Vector2)output1.position, Quaternion.identity);
                currentlySpawnedDice.GetComponent<DiceManager>().diceValue = (int)difference - 1;
                DiceRandomizer.dice.Add(currentlySpawnedDice);
            }
        }
    }


}
