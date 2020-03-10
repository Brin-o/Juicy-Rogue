using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ludiq;
using Bolt;
using DG.Tweening;

public class playerRotationTween : MonoBehaviour
{

    Vector3 inValue;

    void Update()
    {
        inValue = (Vector2)Variables.Object(gameObject).Get("goalVelocity");
        Debug.Log(inValue);
    }
}
