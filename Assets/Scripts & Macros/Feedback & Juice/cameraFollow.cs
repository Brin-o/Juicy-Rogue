using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    [Header("Fetchers")]
    [SerializeField] GameObject playerPosition;
    [SerializeField] GameObject playerInFront;
    [SerializeField] Rigidbody2D playerRigidbody;
    [Header("Camera follow options")]
    [SerializeField]  float speed = 2.0f;
    [SerializeField] float velocityGateToSwitch = 4;

    public float infrontFor = 4f;
    
    /*void FixedUpdate () {
        float interpolation = speed * Time.deltaTime;

        
        //lerp to player position
        Vector3 target;
        if(playerRigidbody.velocity.magnitude > velocityGateToSwitch)
        {
            target = playerInFront.transform.position;
        }
        else
        {
            target = playerPosition.transform.position;
        }
        

        Vector3 position = this.transform.position;
        position.y = Mathf.Lerp(this.transform.position.y, target.y, interpolation);
        position.x = Mathf.Lerp(this.transform.position.x, target.x, interpolation);
        

        
        this.transform.position = position;
    }*/

    private void FixedUpdate() {
        transform.position = new Vector3 (playerPosition.transform.position.x + (playerPosition.transform.up.x * 4), playerPosition.transform.position.y + (playerPosition.transform.up.y * 4), transform.position.z );

    }
}
