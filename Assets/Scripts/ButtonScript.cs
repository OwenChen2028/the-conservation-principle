using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
	[SerializeField] public List<Rigidbody2D> rigidBodies;
	
	void Awake() {
		rb = GetComponent<Rigidbody2D>();
	}
	
	private void OnTriggerStay2D(Collider2D other) 
	{
		Rigidbody2D otherRb = other.GetComponent<Rigidbody2D>();
		
		if (otherRb != null && otherRb.mass >= minimumMassToActivate) {
			if (!rigidBodies.Contains(otherRb)) 
			{
				rigidBodies.Add(otherRb);
			}
		}
	}
	
	private async void OnTriggerExit2D(Collider2D other) 
	{
		Rigidbody2D otherRb = other.GetComponent<Rigidbody2D>();
		
		if (rigidBodies.Contains(otherRb)) 
		{
			await Task.Delay(300);
			rigidBodies.Remove(otherRb);
		}
	}
	
	private void FixedUpdate() 
	{
		if (rigidBodies == null) 
			return;
		
		active = false;
		
		foreach (Rigidbody2D largeRb in rigidBodies)
		{
			if (largeRb != null && largeRb.mass >= minimumMassToActivate)
			{
				active = true;
			} else 
			{
				rigidBodies.Remove(largeRb);
			}
		}
		
		if (active) 
		{
			activatorFunction.Invoke(true);
			active = true;
			rb.transform.localPosition = Vector3.Lerp(rb.transform.localPosition, downPosition, lerpConstant);
		} else 
		{
			activatorFunction.Invoke(false);
			active = false;
			rb.transform.localPosition = Vector3.Lerp(rb.transform.localPosition, upPosition, lerpConstant);
		}
	}
}
