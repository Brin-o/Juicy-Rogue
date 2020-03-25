using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class cameraFollow : MonoBehaviour
{
    [Header("Fetchers")]
    [SerializeField] GameObject playerPosition = default;
    [SerializeField] Rigidbody2D playerRigidbody = default;

    [Header("Camera follow options")]
    [SerializeField]  float speed = 2.0f;
    [Range(0.1f,1f)][SerializeField] float magnitudeModifier = 0.5f;
    [SerializeField] float distanceClamp  = 5f;
    float orgSpeed;
    

    private void Start() {
        orgSpeed = speed;
    }

    private void Update() {
        
        float magMod = playerRigidbody.velocity.magnitude;
        if (magMod > 10)
            speed = Mathf.Lerp(speed, magMod, 1f * Time.deltaTime);
            //magMod = 10;
        else
        {
            speed = orgSpeed;
        }
            
        
        float modifiedMagnitude = playerRigidbody.velocity.magnitude * magnitudeModifier;
        float interpolation = (Mathf.Lerp(speed, (speed + modifiedMagnitude), 0.5f)) * Time.deltaTime;
        

        Vector3 position = this.transform.position;
        Vector3 playerUp = playerPosition.transform.up;
        Vector3 targetPos;

        targetPos.x = playerPosition.transform.position.x + (playerUp.x * modifiedMagnitude);
        targetPos.y = playerPosition.transform.position.y + (playerUp.y * modifiedMagnitude);
        targetPos.z = transform.position.z;

        //clamp to maximum of 5 distance
        if ((targetPos.x - transform.position.x) > distanceClamp)
            targetPos.x = transform.position.x + distanceClamp;
        if ((targetPos.x - transform.position.x) < -distanceClamp)
            targetPos.x = transform.position.x - distanceClamp;
        if ((targetPos.y - transform.position.y) > distanceClamp)
            targetPos.y = transform.position.y + (distanceClamp * 0.66f);
        if ((targetPos.y - transform.position.y) < - distanceClamp)
            targetPos.y = transform.position.y - (distanceClamp * 0.66f);
        //clamp ending


        //lerping magic
        position.y = Mathf.Lerp(this.transform.position.y, targetPos.y, interpolation);
        position.x = Mathf.Lerp(this.transform.position.x, targetPos.x, interpolation);

        this.transform.position = position;

    }
}
