using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorScript : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private Vector3 upPosition;	
	[SerializeField] private Vector3 downPosition;

	[SerializeField] private float lerpConstant;

	private bool doorUp = false;

    private void Awake()
    {
        rb = transform.GetComponent<Rigidbody2D>();
    }
	
	private void FixedUpdate() 
	{
		if (doorUp) 
		{
			rb.transform.localPosition = Vector3.Lerp(rb.transform.localPosition, upPosition, lerpConstant);
		}
		else 
		{
			rb.transform.localPosition = Vector3.Lerp(rb.transform.localPosition, downPosition, lerpConstant);
			
		}
	}

    public void MoveDoor(bool moveUp)
    {
        doorUp = moveUp;
    }
}
