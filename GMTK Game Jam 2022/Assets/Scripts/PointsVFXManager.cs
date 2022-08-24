using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointsVFXManager : MonoBehaviour
{
    private Animator anim;
    private Vector3 velocity = Vector3.zero;
    private TMP_Text tmp;
    public Vector2 targetPosition;
    // Start is called before the first frame update
    void Awake()
    {
        tmp = transform.GetChild(0).GetComponent<TMP_Text>();
        anim = transform.GetChild(0).GetComponent<Animator>();
        targetPosition = (Vector2)transform.position + new Vector2(0,-1);
    }

    // Update is called once per frame
    void Update()
    {

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, 0.5f * Time.deltaTime);

        if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            Destroy(gameObject);
        }

            lerpUp();
    }

    public void displayPointValue(string points)
    {
        tmp.text = points;
    }

    public void lerpUp()
    {
        float t = Time.deltaTime * 10;
        transform.position = Vector2.Lerp(transform.position, targetPosition, t);
        
    }

    private void OnDestroy()
    {
        if (PointsManager.bulletVFX.Contains(gameObject))
        {
            PointsManager.bulletVFX.Remove(gameObject);
            PointsManager.bulletCount--;
        }
    }
}
