using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inheritLaserSize : MonoBehaviour
{
    Transform laser;

    void Start()
    {
        var parent = transform.parent;
        laser = parent.GetChild(2).GetComponent<Transform>();

        var indicatorX = laser.transform.localScale.x;
        var indicatorY = laser.transform.localScale.y;
        float floatX = (float) indicatorX;
        float floatY = (float) indicatorY;

        transform.GetComponent<SpriteRenderer>().size = new Vector2( floatX , floatY );
    
    }

    
    /*private void OnDrawGizmos() {
        switch (tweenTarget)
        {
            case whatAmITweening.enemyLaser:
                Gizmos.color = new Color(1,0,0,0.8f);
                Gizmos.DrawCube (new Vector3(transform.position.x + (transform.localScale.x * 0.5f), transform.position.y, 1f), transform.localScale);
                break;
            default:
                break;
        }
    }*/
}
