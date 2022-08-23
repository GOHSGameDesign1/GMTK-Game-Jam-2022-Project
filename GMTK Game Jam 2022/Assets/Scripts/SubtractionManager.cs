using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class SubtractionManager : MonoBehaviour
{
    private GameObject subtractionSlot1;
    private GameObject subtractionSlot2;
    private Transform output1;

    public GameObject dicePrefab;
    public Canvas canvas;
    private GameObject currentlySpawnedDice;

    public GameObject pointVFX;
    GameObject currentspawnedpointVFX;
    public Transform spawnVFXTransform;

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
        Calculate(subtractionSlot1.GetComponent<Slot>(), subtractionSlot2.GetComponent<Slot>());
    }

    private void Calculate(Slot sub1, Slot sub2)
    {
        if (sub1.attached && sub2.attached)
        {
            SpawnPoints(350);

            float difference = Mathf.Abs(sub1.diceValue - sub2.diceValue);
            Debug.Log(difference);
            GetComponent<AudioSource>().Play();
            GetComponent<CinemachineImpulseSource>().GenerateImpulse();

            sub1.attached = false;
            sub1.transform.DetachChildren();
            Destroy(sub1.currentDiceAttached);
            Destroy(sub2.currentDiceAttached);

            if(difference <= 0)
            {
                currentlySpawnedDice = Instantiate(dicePrefab, (Vector2)output1.position, Quaternion.identity, canvas.transform);
                currentlySpawnedDice.GetComponent<Dice>().diceValue = 1;
                DiceRandomizer.dice.Add(currentlySpawnedDice);
            } else
            {
                currentlySpawnedDice = Instantiate(dicePrefab, (Vector2)output1.position, Quaternion.identity, canvas.transform);
                currentlySpawnedDice.GetComponent<Dice>().diceValue = (int)difference;
                DiceRandomizer.dice.Add(currentlySpawnedDice);
            }
        }
    }

    void SpawnPoints(float pointValue)
    {
        PointsManager.points += pointValue;
        currentspawnedpointVFX = Instantiate(pointVFX, spawnVFXTransform.position, Quaternion.Euler(0,0, Random.Range(-10f, 10f)) );
        currentspawnedpointVFX.GetComponent<PointsVFXManager>().displayPointValue("+" + pointValue.ToString());
    }


}
