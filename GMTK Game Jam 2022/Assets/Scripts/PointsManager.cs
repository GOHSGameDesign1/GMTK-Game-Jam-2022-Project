using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PointsManager : MonoBehaviour
{
    public static float points;
    public TMP_Text pointText;
    // Start is called before the first frame update
    void Start()
    {
        points = 0;
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
}
