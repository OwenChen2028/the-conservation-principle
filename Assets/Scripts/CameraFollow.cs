using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	[SerializeField] private Transform followTransform;
	[SerializeField] private float lerpConstant = 0.05f;

    [SerializeField] private bool horizontalScrolling = true;
    [SerializeField] private bool verticalScrolling = true;

	void FixedUpdate() 
	{
			Vector3 followPosition = new Vector3(
                horizontalScrolling ? followTransform.position.x : transform.position.x,
                verticalScrolling ? followTransform.position.y : transform.position.y,
				-10f
			);
			transform.position = Vector3.Lerp(transform.position, followPosition, lerpConstant);
	}
}
