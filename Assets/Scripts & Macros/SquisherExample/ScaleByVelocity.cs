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
	[SerializeField] float jumpToOriginalTimer = 0.1f;
	[SerializeField] float yScaleLimit = 1.2f;

	private Vector2 startScale;
	private Vector3 targetTween;
	private Vector3 originalScale;


	public float scaleInverseAmount;
	public float scaleAmount;


	private void Start ()
	{
		startScale = transform.localScale;
		originalScale = transform.localScale;	
	}

	private void Update ()
	{
		ScaleObejct();
	}

	private void ScaleObejct()
	{
		var velocity = rigidbody.velocity.magnitude;

		if (velocity < velocityGate && transform.localScale != Vector3.one)
			transform.DOScale(originalScale, jumpToOriginalTimer);

		scaleAmount = velocity * strength + bias;
		scaleInverseAmount = (1f / scaleAmount) * startScale.magnitude;


		if (velocity > velocityGate)
		{
		
			if (scaleInverseAmount > yScaleLimit)
				scaleInverseAmount = yScaleLimit;
			if (scaleAmount > 1.2f)
				scaleAmount = 1.2f;

			switch (axis)
			{
				case Axis.X:
					targetTween = new Vector3 (scaleAmount, scaleInverseAmount, 1f);
					transform.localScale = targetTween;
					return;
				
				case Axis.Y:
					targetTween = new Vector3 (scaleInverseAmount, scaleAmount, 1f);
					transform.localScale = targetTween;
					return;
			}
		}
		else
			return;
	}
}