using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageBG : MonoBehaviour
{

    [SerializeField] private float rotateSpeed;
    [SerializeField] private Vector2 pivotPoint;
    [SerializeField] private float pulseFreq;
    [SerializeField] private float minBrightness;

    private Transform tran;
    private SpriteRenderer rend;

    private void Awake()
    {
        tran = GetComponent<Transform>();
        rend = GetComponent<SpriteRenderer>();
    }


    private void FixedUpdate()
    {
        tran.RotateAround(new Vector3(pivotPoint.x, pivotPoint.y, transform.position.z), Vector3.forward, rotateSpeed);

        float brightness = oscillate(Time.timeSinceLevelLoad, pulseFreq, minBrightness, 1);
        rend.color = new Color(brightness, brightness, brightness, 1);
    }

    private float oscillate(float time, float speed, float min, float max)
    {
        return Mathf.Lerp(min, max, (Mathf.Cos(time * speed / Mathf.PI) + 1f) * 0.5f);
    }
}
