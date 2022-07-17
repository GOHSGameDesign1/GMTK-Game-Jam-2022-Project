using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DiceRandomizer : MonoBehaviour



{
    public GameObject dicePrefab;
    public GameObject[] spawnPoints;
    private GameObject[] spawnedDice = new GameObject[3];
    private int counter;
    bool canRoll;

    int randomNumber;

    private void Start()
    {
        canRoll = true;
    }

    private void Update()
    {
        CheckIfRollable();
        if (canRoll)
        {
            RollDice();
        }
    }

    void RollTheDice()
    {
        randomNumber = Random.Range(1, 6);
    }


    public void RollDice()
    {


        for (int i = 0; i < 3; i++)
        {
            if (spawnedDice[i] != null)
            {
                Destroy(spawnedDice[i]);
            }
            spawnedDice[i] = Instantiate(dicePrefab, (Vector2)spawnPoints[i].transform.position, Quaternion.identity);
        }
    }

    private void CheckIfRollable()
    {
        counter = 0;
        if (spawnedDice[0] == null) counter++;
        if (spawnedDice[1] == null) counter++;
        if (spawnedDice[2] == null) counter++;

        if (counter >= 3)
        {
            //Debug.Log("Use at least 2 dice first!");
            canRoll = true;
            return;
        }

        canRoll = false;
    }

}