using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DiceRandomizer : MonoBehaviour



{
    public GameObject dicePrefab;
    public GameObject[] spawnPoints;
    private GameObject[] spawnedDice = new GameObject[3];

    int randomNumber;



    void RollTheDice()
    {
        randomNumber = Random.Range(1,6);
    }

    public void RollDice()
    {
        if (spawnedDice[0] != null && spawnedDice[1] != null && spawnedDice[2] != null)
        {
            Debug.Log("Use all first 3 dice first!");
            return;
        }

        for (int i = 0; i < 3; i++)
        {
            spawnedDice[i] = Instantiate(dicePrefab, (Vector2)spawnPoints[i].transform.position, Quaternion.identity);
        }
    }

}