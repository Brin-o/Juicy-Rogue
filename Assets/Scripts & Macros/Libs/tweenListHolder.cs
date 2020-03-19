    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class tweenListHolder : MonoBehaviour
{

    public enum  whatAmITweening
    {
        enemyBox,
        enemyLaser,
        cursor
    };

    //references
    SpriteMask myMask = default;
    SpriteRenderer myIndicator = default;

    public whatAmITweening tweenTarget;
    
    public Transform pointer;
    public Transform pointerEndPoint;

    SpriteRenderer spriteRenderer;

    [SerializeField] bool laserOn = default;
    public Transform pointerOrigin;
    public float rotation;

    //Variables for idle rotation
    public bool rotated = false;


    Color evilBlue = new Color (0f, 0.99f, 1f, 1f);
    Color evilRed = new Color (0.909804f, 0.1490196f, 0.3529412f ,1f);


    private void Start() {

        switch (tweenTarget)
        {
            case whatAmITweening.enemyLaser:
                var parent = transform.parent;
                myMask = parent.GetChild(0).GetComponent<SpriteMask>();
                myIndicator = parent.GetChild(1).GetComponent<SpriteRenderer>();   
                myMask.transform.localScale = new Vector3 (1,3,1);

                pointerEndPoint = transform.GetChild(0).GetComponent<Transform>();
                pointer = transform.parent.GetChild(3);

                spriteRenderer = GetComponent<SpriteRenderer>();
                spriteRenderer.color = new Color (1f,1f,1f, 0f);
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
                    myIndicator.transform.DOScale(Vector3.one, castTime * 0.1f); //nnaredi indikator
                    DOTween.Sequence()
                        .Append(myMask.transform.DOScaleX(transform.localScale.x, castTime))
                        .Append(myMask.transform.DOScaleX(transform.localScale.x + 2, 0.01f))
                        //.Insert(castTime + 2.6f, myMask.transform.DOScaleX(new Vector3()))
                        ;

                    DOTween.Sequence()
                        .Append(spriteRenderer.DOFade (0.9f, (castTime) ) )
                        
                        //spremeni pointer barvo -> v modro
                        .Append(pointer.GetComponent<SpriteRenderer>().DOColor(evilBlue, 0.1f))

                        .Append(pointer.DOMove(pointerEndPoint.position, 0.5f))
                        //Na end točki sem z pointerjem tako, da ga zarotiram naokoli.
                        .Append(pointer.DORotate(new Vector3(0,0, 180f), 0.2f, RotateMode.LocalAxisAdd))
                        .Append(pointer.DOMove(pointerOrigin.position, 0.5f))
                        .Append(pointer.DORotate(new Vector3(0,0, 180f), 0.2f, RotateMode.LocalAxisAdd))


                        .Insert(castTime*0.9f, spriteRenderer.DOFade(1,castTime * 0.1f))
                        //insertaj color transition med rotatcijo #2
                        .Insert(castTime + 1.5f, pointer.GetComponent<SpriteRenderer>().DOColor(evilRed, 0.1f))
                        .Insert(castTime + 1.5f, myIndicator.transform.DOScale(Vector3.zero, 0.25f))
                        
                        .Append(spriteRenderer.DOFade(0,0.5f))
                        .Append(myMask.transform.DOScaleX(1, 0.75f))
                        ;
                }
                /*else //naredi shareindikator ter začne fadati laser
                {
                    laserOn = false;
                    DOTween.Sequence()
                        .Insert(0.5f, myIndicator.transform.DOScale(Vector3.zero, 0.25f))
                        .Append(spriteRenderer.DOFade(0,0.5f))
                        .Append(myMask.transform.DOScaleX(1, 0.75f))
                        ;
                }*/
                break;

            default:
                WrongTweenTarget();
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
                WrongTweenTarget();
                return;
        }

    }


    public void IdleRotation(float rotateTimer, float rotateToAndBack){    
        
        rotation = rotateToAndBack;
        if (rotated)
            rotation = rotation * -1;
        DOTween.Sequence()
            .Append(transform.DOLocalRotate(new Vector3(0,0, rotation), rotateTimer, RotateMode.LocalAxisAdd))
            ;
            
        if (rotated)
            rotated = false;
        else
            rotated = true;
        

    }
    

    //CURSOR TWEENS
    public void cursorPunch()
    {
        switch (tweenTarget)
        {
            case whatAmITweening.cursor:
                if(transform.localScale.x == 1)
                    transform.DOPunchScale(new Vector3(1.2f, 1.2f, 1.2f), 0.1f, 1, 0 );
                else
                    transform.DOScale(Vector3.one, 0.05f);
                break;
            default:
                WrongTweenTarget();
                return;
        }
    }
    public void cursorUnFade()
    {
        switch (tweenTarget)
        {
            case whatAmITweening.cursor:
                    spriteRenderer.DOKill();
                    spriteRenderer.DOFade(1f, 0.1f);
                
                break;
            default:
                WrongTweenTarget();
                return;
        }
    }    
    public void cursorFade()
    {
        switch (tweenTarget)
        {
            case whatAmITweening.cursor:
                    spriteRenderer.DOKill();
                    spriteRenderer.DOFade(0.35f, 0.1f);
                
                break;
            default:
                WrongTweenTarget();
                return;
        }
    }
    
    
    public void CameraShakeDash()
    {
        Camera.main.DOShakePosition(0.1f, 2f, 2, 10);
    }

    
    private void FixedUpdate() {
        switch (tweenTarget)
        {
            
            //ce ni skatla potem delamo boxcollision enable disable based on opacity
            case whatAmITweening.enemyLaser:
                
                if(spriteRenderer.color.a >= 0.9 && laserOn)
                    pointer.GetComponent<Collider2D>().enabled = true;
                else
                     pointer.GetComponent<Collider2D>().enabled = false;
                
                break;
    
            default:
                return;
        }
    }


    
    
    
    private void WrongTweenTarget(){
        Debug.Log("Invalid tween target, please check the dropdown on the object you are tweening.");
        return;
    }
}