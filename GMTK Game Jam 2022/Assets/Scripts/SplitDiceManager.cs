using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitDiceManager : MonoBehaviour
{
    public bool isSplitObject;
    // Start is called before the first frame update
    void Awake()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        
    }

    public void checkForSplit()
    {
        if (PointsManager.points > 100)
        {
            gameObject.SetActive(true);
        } else
        {
            Destroy(gameObject);
        }
    }
}
