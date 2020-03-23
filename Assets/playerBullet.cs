using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class playerBullet : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigidbody2d = default;

    public void ShootPlayerBullet(float bulletSpeed)
    {
        rigidbody2d.AddForce(transform.up * bulletSpeed, ForceMode2D.Impulse);
    }
}
