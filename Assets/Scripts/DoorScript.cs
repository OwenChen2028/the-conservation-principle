using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorScript : MonoBehaviour
{
	[SerializeField] private Vector3 upPosition;	
	[SerializeField] private Vector3 downPosition;
	[SerializeField] private float lerpConstant;
	private Rigidbody2D rb;
	// public UnityEvent<bool> activatorFunction;
	private bool doorUp = false;
	
	public void Move(bool active) 
	{
		doorUp = active;
	}
	
	private void FixedUpdate() 
	{
		if (doorUp) 
		{
			rb.transform.localPosition = Vector3.Lerp(rb.transform.localPosition, upPosition, lerpConstant);
		} else 
		{
			rb.transform.localPosition = Vector3.Lerp(rb.transform.localPosition, downPosition, lerpConstant);
			
		}
	}
	
	private void Awake() 
	{
		rb = transform.GetComponent<Rigidbody2D>();
	}
}
