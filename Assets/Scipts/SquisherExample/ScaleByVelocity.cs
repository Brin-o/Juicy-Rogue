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

	private void Start ()
	{
		startScale = transform.localScale;
		originalScale = transform.localScale;
		
	}

	private void FixedUpdate ()
	{
		var velocity = rigidbody.velocity.magnitude;

		//if (Mathf.Approximately (velocity, 0f))
		if (velocity < velocityGate)
			transform.DOScale(originalScale, jumpToOriginalTimer);
			//TODO: Ko se speeda up hočem animacijo potegnit iz 1,1 v karkoli bi moralo biti

		var amount = velocity * strength + bias;
		var inverseAmount = (1f / amount) * startScale.magnitude;


		if (velocity > velocityGate)
		{
		
			if (inverseAmount > yScaleLimit)
				inverseAmount = yScaleLimit;
			if (amount > 1.2f)
				amount = 1.2f;
			
			switch (axis)
			{
				case Axis.X:
					targetTween = new Vector3 (amount, inverseAmount, 1f);
					transform.DOScale(targetTween,tweenTimer);
					return;
				case Axis.Y:
					targetTween = new Vector3 (inverseAmount, amount, 1f);
					transform.DOScale(targetTween,tweenTimer);
					return;
			}
		}
		
	}
}