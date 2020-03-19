using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class playerIndicatorRotation : MonoBehaviour
{

    [SerializeField] Transform target;
    public Vector2 mouseDirection;

    private void FixedUpdate() {
        RotateBasedOnMouse();
    }

    void RotateBasedOnMouse()
    {
        Vector2 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

       mouseDirection = new Vector2(
            mousePosition.x - transform.position.x, 
            mousePosition.y - transform.position.y);
        transform.up = mouseDirection;
    }

    /*void RotateBasedOnTarget()
    {
        Vector2 direction = new Vector2(
            target.position.x - transform.position.x, 
            target.position.y - transform.position.y);
        transform.up = direction;
    }*/

}
