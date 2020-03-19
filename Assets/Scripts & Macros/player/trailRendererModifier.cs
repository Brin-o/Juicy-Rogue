using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trailRendererModifier : MonoBehaviour
{
    
    [SerializeField] TrailRenderer trailRenderer = default;
    [SerializeField] Transform graphicSquisher = default;
    [SerializeField] [Range(0.1f, 2f)] float widthModifier = 0.9f;
    Rigidbody2D rb;

    public float modifiedY;
    

    void Start()
    {
        rb = transform.parent.parent.parent.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update() {
        float rbVelocity = rb.velocity.magnitude;
        if (rbVelocity <= 0.1)
            trailRenderer.emitting = false;
        else{
            trailRenderer.emitting = true;
            modifiedY = graphicSquisher.localScale.x * widthModifier;
            trailRenderer.widthMultiplier = modifiedY;
        }
    }
}
