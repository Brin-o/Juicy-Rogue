using UnityEngine;
using Bolt;
using Ludiq;
using DG.Tweening;

public class LookAlongVelocity : MonoBehaviour
{
	public float minVelocity = 0.01f;
	public new Rigidbody2D rigidbody;
	private Vector2 moveDirection;

	private void Update ()
	{
		if (rigidbody == null)
			return;

		if (rigidbody.velocity.magnitude < minVelocity)
			return;

		var rotation = transform.eulerAngles;
		
		var angle = Vector2.SignedAngle (Vector2.up, rigidbody.velocity);
		rotation.z = angle;

		/*
		TODO
		Rotate
		DORotate(float toAngle, float duration)
   		Rotates the target to the given value.*/

		transform.eulerAngles = rotation;
	}
}