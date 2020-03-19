using UnityEngine;
using DG.Tweening;
public class ScaleByVelocity : MonoBehaviour
{
	public enum Axis { X, Y }

	public float bias = 1f;
	public float strength = 1f;
	public Axis axis = Axis.Y;

	public new Rigidbody2D rigidbody;
	[Range(0, 1)]
	public float velocityGate = 0.2f;
	[SerializeField] float tweenTimer = 0.5f;
	[SerializeField] float jumpToOriginalTimer = 0.1f;
	[SerializeField] float yScaleLimit = 1.2f;

	private Vector2 startScale;
	private Vector3 targetTween;
	private Vector3 originalScale;


	public float scaleInverseAmount;
	public float scaleAmount;

	
	//Camera tweening variables
	Camera cam;
	float orgCamSize;


	private void Awake() {
		Application.targetFrameRate = 70;
	}
	private void Start ()
	{
		startScale = transform.localScale;
		originalScale = transform.localScale;
		
		cam = Camera.main;
		orgCamSize = cam.orthographicSize;

		
	}

	private void FixedUpdate ()
	{
		ScalePlayer();
	}

	private void ScalePlayer()
	{
		var velocity = rigidbody.velocity.magnitude;

		//if (Mathf.Approximately (velocity, 0f))
		if (velocity < velocityGate && transform.localScale != Vector3.one)
			transform.DOScale(originalScale, jumpToOriginalTimer);
			//TODO: Ko se speeda up hočem animacijo potegnit iz 1,1 v karkoli bi moralo biti

		scaleAmount = velocity * strength + bias;
		scaleInverseAmount = (1f / scaleAmount) * startScale.magnitude;


		if (velocity > velocityGate)
		{
		
			if (scaleInverseAmount > yScaleLimit)
				scaleInverseAmount = yScaleLimit;
			if (scaleAmount > 1.2f)
				scaleAmount = 1.2f;
			
			Sequence tweenSequence = DOTween.Sequence();
			
			if(tweenSequence.IsActive() == false)
				tweenSequence.Play();

			switch (axis)
			{
				case Axis.X:
					targetTween = new Vector3 (scaleAmount, scaleInverseAmount, 1f);
					//transform.DOScale(targetTween,tweenTimer);
					transform.localScale = targetTween;
					return;
				case Axis.Y:
					targetTween = new Vector3 (scaleInverseAmount, scaleAmount, 1f);
					//tweenSequence.Append(transform.DOScale(targetTween,tweenTimer));
					
					transform.localScale = targetTween;
					return;
			}
		}
		else
			return;
	}
}