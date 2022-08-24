using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointsManager : MonoBehaviour
{
    public static float points;
    public TMP_Text pointText;
    public TMP_Text highScoreText;

    public static GameObject pointsVFX;
    public static GameObject currentSpawnedPointsVFX = null;
    private static Vector3 velocity = Vector3.zero;
    public static List<GameObject> bulletVFX = new List<GameObject>();
    // Start is called before the first frame update
    void Awake()
    {
        points = 0;

        pointsVFX = (GameObject)Resources.Load("Point VFX");

        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScoreText.text = new string("Hi: " + PlayerPrefs.GetFloat("HighScore").ToString());
        } else
        {
            highScoreText.text = "Hi: 0";
        }

    }

    // Update is called once per frame
    void Update()
    {

        if(points < 0)
        {
            points = 0;
        }

        pointText.text = new string("Points: " + points.ToString());
    }

    public static void SpawnPoints(float pointsValue, Vector2 spawnVFXPosition, int listName)
    {
        points += pointsValue;
        string plusOrMinus = "";

        Debug.Log(currentSpawnedPointsVFX);

        if(pointsValue >= 0)
        {
            plusOrMinus = "+";
        }

        currentSpawnedPointsVFX = Instantiate(pointsVFX, spawnVFXPosition, Quaternion.Euler(0, 0, Random.Range(-15f, 15f)));
        currentSpawnedPointsVFX.GetComponent<PointsVFXManager>().displayPointValue(plusOrMinus + pointsValue.ToString());

        switch (listName)
        {
            case 0:
                break;
            case 1:
                bulletVFX.Add(currentSpawnedPointsVFX);
                Debug.Log(bulletVFX.Count);
                break;
        }

        if (true)
        {
            foreach(GameObject p in bulletVFX)
            {
                p.transform.position += new Vector3(0, 0.7f, 0);
                //p.GetComponent<PointsVFXManager>().StopAllCoroutines();
                //p.GetComponent<PointsVFXManager>().StartCoroutine(p.GetComponent<PointsVFXManager>().coroutine);
            }

        }




    }

    private void OnDestroy()
    {
        //Checks if Highscore is null
        if (!PlayerPrefs.HasKey("HighScore"))
        {
            //Make HighScore
            PlayerPrefs.SetFloat("HighScore", points);
            return;
        }

        //Checks if points is higer than HighScore
        if(points > PlayerPrefs.GetFloat("HighScore"))
        {
            PlayerPrefs.SetFloat("HighScore", points);
        }

    }
}
