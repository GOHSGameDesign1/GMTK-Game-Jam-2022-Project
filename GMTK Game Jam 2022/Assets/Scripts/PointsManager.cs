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
    public static int bulletCount = 0;
    public static List<GameObject> addVFX = new List<GameObject>();
    public static int AddVFXCount = 0;
    public static List<GameObject> subVFX = new List<GameObject>();
    public static int subVFXCount = 0;
    public static List<GameObject> rerollVFX = new List<GameObject>();
    public static int rerollVFXCount = 0;
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
                break;
            case 2:
                addVFX.Add(currentSpawnedPointsVFX);
                break;
            case 3:
                subVFX.Add(currentSpawnedPointsVFX);
                break;
            case 4:
                rerollVFX.Add(currentSpawnedPointsVFX);
                break;
        }

        if (bulletVFX.Count != bulletCount)
        {
            foreach(GameObject p in bulletVFX)
            {
                p.GetComponent<PointsVFXManager>().targetPosition += new Vector2(0, 1f);
                bulletCount = bulletVFX.Count;
            }

        }

        if(addVFX.Count != AddVFXCount)
        {
            foreach (GameObject p in addVFX)
            {
                p.GetComponent<PointsVFXManager>().targetPosition += new Vector2(0, 1f);
                AddVFXCount = addVFX.Count;
            }
        }

        if (subVFX.Count != subVFXCount)
        {
            foreach (GameObject p in subVFX)
            {
                p.GetComponent<PointsVFXManager>().targetPosition += new Vector2(0, 1f);
                subVFXCount = subVFX.Count;
            }
        }

        if (rerollVFX.Count != rerollVFXCount)
        {
            foreach (GameObject p in rerollVFX)
            {
                p.GetComponent<PointsVFXManager>().targetPosition += new Vector2(0, 1f);
                rerollVFXCount = rerollVFX.Count;
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
