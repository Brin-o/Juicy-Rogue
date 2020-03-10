using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class tweenScale : MonoBehaviour
{
    Vector3 vektor;
    void Start()
    {        
        vektor = new Vector3 (5,5,5);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            transform.DOPunchScale(vektor, 2f,10,1f);
        }
    }
}
