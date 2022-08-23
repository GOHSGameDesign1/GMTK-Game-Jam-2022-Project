using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointsVFXManager : MonoBehaviour
{
    private Animator anim;
    private Vector3 velocity = Vector3.zero;
    private TMP_Text tmp;
    // Start is called before the first frame update
    void Awake()
    {
        tmp = transform.GetChild(0).GetComponent<TMP_Text>();
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, 0.5f * Time.deltaTime);

        if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            Destroy(gameObject);
        }
    }

    public void displayPointValue(string points)
    {
        tmp.text = points;
    }
}
