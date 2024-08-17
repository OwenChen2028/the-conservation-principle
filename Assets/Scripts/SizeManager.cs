using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeManager : MonoBehaviour
{
    private Rigidbody2D rb;

    private Vector2 startingScale;
    [SerializeField] private float size;

    [SerializeField] private float minimumSize;
    [SerializeField] private float maximumSize;

    [SerializeField] private bool scalesHorizontally;
    [SerializeField] private bool scalesVertically;

    [SerializeField] private bool isStatic;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        startingScale = new Vector2(1, 1);

        if (isStatic)
        {
            rb.bodyType = RigidbodyType2D.Static;
        }
        else
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    private void Update()
    {
        if (scalesHorizontally && scalesVertically)
        {
            transform.localScale = new Vector2(size * startingScale.x, size * startingScale.y);
        }
        else if (scalesHorizontally)
        {
            transform.localScale = new Vector2(size * startingScale.x, startingScale.y);
        }
        else if (scalesVertically)
        {
            transform.localScale = new Vector2(startingScale.x, size * startingScale.y);
        }
    }

    public float GetSize()
    {
        return size;
    }

    public void SetSize(float newSize)
    {
        size = newSize;
    }

    public void ChangeSize(float delta)
    {
        size += delta;
    }

    public float GetMinSize()
    {
        return minimumSize;
    }

    public float GetMaxSize()
    {
        return maximumSize;
    }
}
