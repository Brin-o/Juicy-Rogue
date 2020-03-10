using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class tweenListHolder : MonoBehaviour
{

    public enum  whatAmITweening
    {
        enemyBox,
        enemyLaser
    };

    SpriteMask myMask = default;
    SpriteRenderer myIndicator = default;

    public whatAmITweening tweenTarget;
    

    SpriteRenderer spriteRenderer;

    [SerializeField] bool laserOn = default;
    public float rotation;

    //Variables for idle rotation
    public bool rotated = false;

    string invalidTweenTar = ("Invalid tween target, please check the dropdown on the object you are tweening.");


    private void Start() {

        switch (tweenTarget)
        {
            case whatAmITweening.enemyLaser:
                var parent = transform.parent;
                myMask = parent.GetChild(0).GetComponent<SpriteMask>();
                myIndicator = parent.GetChild(1).GetComponent<SpriteRenderer>();   
                break;

            case whatAmITweening.enemyBox:
                myMask = null;
                myIndicator = null;
                break;
            
            default:
                break;
        }


        spriteRenderer = GetComponent<SpriteRenderer>();       
    }

    public void LaserOnOff(float castTime){

        switch (tweenTarget)
        {
            case whatAmITweening.enemyLaser:
                spriteRenderer.DOKill();
                //Preveri če je laser aktiviran (1 = aktiviran)
                if (spriteRenderer.color.a < 1)  //fada in laser ter naredi punch animacijo

                {    
                    laserOn = true;
                    myIndicator.transform.DOScale(Vector3.one, 0.25f); //nnaredi indikator
                    myMask.transform.DOScaleX(transform.localScale.x + 2, castTime); //maska inkator
                    DOTween.Sequence()
                        .Append(spriteRenderer.DOFade (0.9f, (castTime * 0.9f) ) )
                        .Append(transform.DOPunchScale ( new Vector3 ( transform.localScale.x, 0.8f,1 ) , 0.1f , 1,0) )
                        .Insert(castTime*0.9f, spriteRenderer.DOFade(1,castTime * 0.1f))
                        ;
                }
                else //naredi shareindikator ter začne fadati laser
                {
                    laserOn = false;
                    DOTween.Sequence()
                        .Append(transform.DOShakeScale(0.5f,1,1,1,true))
                        .Insert(0f, myIndicator.transform.DOScale(Vector3.zero, 0.25f))
                        .Append(spriteRenderer.DOFade(0,0.5f))
                        .Append(myMask.transform.DOScaleX(1, 0.75f))
                        ;
                }
                break;

            default:
                Debug.Log(invalidTweenTar);
                return;
        }

    }

    public void LaserPunch(){
        switch (tweenTarget)
        {
            case whatAmITweening.enemyLaser:
                DOTween.Sequence()
                    .Append(transform.DOPunchScale(new Vector3(transform.localScale.x, 1f,1), 0.3f,2,1))
                    ;
                break;

            default:
                Debug.Log(invalidTweenTar);
                return;
        }

    }


    public void IdleRotation(float rotateTimer, float rotateToAndBack){    
        
        rotation = rotateToAndBack;
        if (rotated)
            rotation = rotation * -1;
        DOTween.Sequence()
            .Append(transform.DOLocalRotate(new Vector3(0,0, rotation), rotateTimer, RotateMode.FastBeyond360))
            ;
        if (rotated)
            rotated = false;
        else
            rotated = true;
        

    }
    private void FixedUpdate() {
        switch (tweenTarget)
        {
            
            //ce ni skatla potem delamo boxcollision enable disable based on opacity
            case whatAmITweening.enemyLaser:
                
                if(spriteRenderer.color.a >= 0.9 && laserOn)
                    GetComponent<BoxCollider2D>().enabled = true;
                else
                    GetComponent<BoxCollider2D>().enabled = false;
                
                break;
    
            default:
                return;
        }
    }

    private void OnDrawGizmos() {
        switch (tweenTarget)
        {
            case whatAmITweening.enemyLaser:
                Gizmos.color = new Color(1,0,0,0.8f);
                Gizmos.DrawCube (new Vector3(transform.position.x + (transform.localScale.x * 0.5f), transform.position.y, 1f), transform.localScale);
                break;
            default:
                break;
        }

    }
}
