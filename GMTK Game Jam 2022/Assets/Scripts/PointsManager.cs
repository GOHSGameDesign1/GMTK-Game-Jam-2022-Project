using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointsManager : MonoBehaviour
{
    public static float points;
    public TMP_Text pointText;
    public TMP_Text highScoreText;
    // Start is called before the first frame update
    void Awake()
    {
        points = 0;

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
