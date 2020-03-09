using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class laserTween : MonoBehaviour
{
    [SerializeField] bool checkIfBox = false;
    [SerializeField] SpriteMask myMask;
    /* TODO: Replace bool with enum
    public enum  whatAmITweening
    {
        enemyBox,
        enemyLaser
    };

    public whatAmITweening tweenTarget;
    */

    SpriteRenderer spriteRenderer;
    float castTime = 1f;
    bool amLaserSystem = false;

    [SerializeField] bool laserOn = default;

    //Variables for idle rotation
    bool rotated = false;


    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();       
    }

    public void LaserOnOff(){
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.DOKill();

        //Preveri če je laser aktiviran (1 = aktiviran)
        if (spriteRenderer.color.a < 1)
            {
                //fada in laser ter naredi punch animacijo

                laserOn = true;
                myMask.transform.DOScaleX(transform.localScale.x + 2, castTime * 0.25f);
                DOTween.Sequence()
                    .Append(spriteRenderer.DOFade(1,castTime))
                    .Append(transform.DOPunchScale(new Vector3(transform.localScale.x, 0.8f,1), 0.2f,2,1))
                    //.Append(transform.DOShakeScale(0.3f,1,1,30,true))
                    ;
            }
        else
            {
                //naredi shareindikator ter začne fadati laser
                laserOn = false;
                DOTween.Sequence()
                    .Append(transform.DOShakeScale(0.5f,1,1,1,true))
                    //.Append(transform.DOShakeRotation(0.5f, 1,1,30, true))
                    .Append(spriteRenderer.DOFade(0,0.5f))
                    ;
            }
    }

    public void LaserPunch(){
        DOTween.Sequence()
            .Append(transform.DOPunchScale(new Vector3(transform.localScale.x, 1f,1), 0.3f,2,1))
            ;
    }
    public void IdleRotation(float rotateTimer){    
        float rotation = 360;
        if (rotated)
            rotation = rotation * -1;
        DOTween.Sequence()
            .Append(transform.DORotate(new Vector3(0,0,rotation), rotateTimer, RotateMode.FastBeyond360))
            ;
        if (rotated)
            rotated = false;
        else
            rotated = true;

    }
    private void FixedUpdate() {
        switch (checkIfBox)
        {
            
            //ce ni skatla potem delamo boxcollision enable disable based on opacity
            case false:
                if(spriteRenderer.color.a >= 1 && laserOn)
                    GetComponent<BoxCollider2D>().enabled = true;
                else
                    GetComponent<BoxCollider2D>().enabled = false;
                
            break;
    
            case true:
                break;
        }
    }


}
