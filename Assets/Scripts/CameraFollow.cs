using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	[SerializeField] private Transform followTransform;
	[SerializeField] private float lerpConstant = 0.05f;

	[Header("Directions to Follow")]
	[SerializeField] private bool Vertical = true;
	[SerializeField] private bool Horizontal = true;

	void FixedUpdate() 
	{
			Vector3 followPosition = new Vector3(
				Horizontal ? followTransform.position.x : transform.position.x,
				Vertical ? followTransform.position.y : transform.position.y,
				-10f
			);
			transform.position = Vector3.Lerp(transform.position, followPosition, lerpConstant);
	}
}
