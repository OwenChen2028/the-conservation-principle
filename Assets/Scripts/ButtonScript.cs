using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class ButtonScript : MonoBehaviour
{
	private Rigidbody2D rb;
	[SerializeField] private UnityEvent<bool> activatorFunction;
	[SerializeField] private float minimumMassToActivate;
	[SerializeField] private Vector3 upPosition;	
	[SerializeField] private Vector3 downPosition;	
	[SerializeField] private float lerpConstant;
	[SerializeField] private bool active = false;
	
	void Awake() {
		rb = GetComponent<Rigidbody2D>();
	}
	
	private void OnTriggerStay2D(Collider2D other) 
	{
		Rigidbody2D otherRb = other.GetComponent<Rigidbody2D>();
		
		if (otherRb != null && otherRb.mass >= minimumMassToActivate) {
			activatorFunction.Invoke(true);
			active = true;
		}
	}
	
	private void OnTriggerExit2D(Collider2D other) 
	{
		Rigidbody2D otherRb = other.GetComponent<Rigidbody2D>();
		
		if (otherRb != null && otherRb.mass >= minimumMassToActivate) {
			activatorFunction.Invoke(false);
			active = false;
		}
	}
	
	private void FixedUpdate() 
	{
		if (active) 
		{
			rb.transform.localPosition = Vector3.Lerp(rb.transform.localPosition, downPosition, lerpConstant);
		} else 
		{
			rb.transform.localPosition = Vector3.Lerp(rb.transform.localPosition, upPosition, lerpConstant);
		}
	}
}
