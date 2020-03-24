using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class gameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        //nastavimo target framerate
        Application.targetFrameRate = 70;
        DOTween.SetTweensCapacity(350, 100);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
