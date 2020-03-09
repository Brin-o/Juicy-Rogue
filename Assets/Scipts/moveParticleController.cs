using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveParticleController : MonoBehaviour
{
    // Start is called before the first frame update
    ParticleSystem particle;
    Rigidbody2D playerRB;
    [SerializeField] float playerVelocity = default;
    void Start()
    {
        particle = GetComponent<ParticleSystem>();   
        playerRB = transform.parent.parent.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        playerVelocity = playerRB.velocity.magnitude;
        Debug.Log(particle.emission.rateOverTime);

    }
}
