using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    private Vector2 moveDirection;

    private Vector2 startingScale;
    [SerializeField] private bool canScale;

    [SerializeField] private bool useOldMovement;

    [SerializeField] private float moveForce;
    [SerializeField] private float movementSpeed;
    
    private bool isGrounded = false;

    private bool jumpKeyDown = false;
    [SerializeField] private float jumpVelocity;

    private Rigidbody2D rb;

    private GameObject sizeGun;
    private Transform firePoint;

    private Camera cam;
    private Vector2 mouseWorldPos;

    private bool leftClickDown;
    private bool rightClickDown;

    [SerializeField] private float storedSize;
    [SerializeField] private float sizeDelta;


    [SerializeField] private float playerMinSize;
    [SerializeField] private float playerMaxSize;

    private Animator anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;

        sizeGun = transform.Find("Size Gun").gameObject;
        firePoint = sizeGun.transform.Find("Fire Point");

        startingScale = new Vector2(1, 1);

        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        HandleMovementInput();
        HandleAimingInput();
        HandleShootingInput();
    }

    private void FixedUpdate()
    {
        HandleMovement();
        HandleAiming();
        HandleShooting();

        if (canScale)
        {
            transform.localScale = new Vector2(storedSize * startingScale.x, storedSize * startingScale.y);
        }
    }

    private void HandleShootingInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            leftClickDown = true;

        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            leftClickDown = false;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
           rightClickDown = true;
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            rightClickDown = false;
        }
    }

    private void HandleMovementInput()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        moveDirection = new Vector2(moveX, 0);

        if (moveDirection.x != 0)
        {
            anim.SetBool("IsRunning", true);
        }
        else 
        {
            anim.SetBool("IsRunning", false);
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            jumpKeyDown = true;
        }
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.W))
        {
            jumpKeyDown = false;
        }
    }

    private void HandleAimingInput()
    {
        Vector2 mousePos = Input.mousePosition;
        mouseWorldPos = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));
    }

    private void HandleMovement()
    {
        if (moveDirection.x < 0) {
            transform.localScale = new Vector2(-1 * Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
        else if (moveDirection.x > 0) {
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }

        if (!canScale)
        {
            if (useOldMovement)
            {
                rb.velocity = new Vector2(moveDirection.x * movementSpeed, rb.velocity.y);
            }
            else
            {
                float maxSpeed = movementSpeed;
                rb.AddForce(moveForce * moveDirection);
                if (rb.velocity.x > maxSpeed)
                {
                    rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
                }
                if (rb.velocity.x < -maxSpeed)
                {
                    rb.velocity = new Vector2(-maxSpeed, rb.velocity.y);
                }
            }

            if (isGrounded && jumpKeyDown)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
                anim.SetTrigger("Jump");
            }
        }
        else
        {
            if (useOldMovement)
            {
                rb.velocity = new Vector2(moveDirection.x * movementSpeed / storedSize, rb.velocity.y);
            }
            else
            {
                float maxSpeed = movementSpeed / storedSize;
                rb.AddForce(moveForce * moveDirection);
                if (rb.velocity.x > maxSpeed)
                {
                    rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
                }
                if (rb.velocity.x < -maxSpeed)
                {
                    rb.velocity = new Vector2(-maxSpeed, rb.velocity.y);
                }
            }

            if (isGrounded && jumpKeyDown)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpVelocity / storedSize);
                anim.SetTrigger("Jump");
            }
        }
    }

    private void HandleAiming()
    {
        if (transform.localScale.x < 0) {
            sizeGun.transform.right = -1 * (mouseWorldPos - (Vector2) transform.position).normalized;
        }
        else {
            sizeGun.transform.right = (mouseWorldPos - (Vector2) transform.position).normalized;
        }
    }

    private void HandleShooting() {
        if (!(leftClickDown || rightClickDown))
        {
            return;
        }

        RaycastHit2D hit;

        if (transform.localScale.x < 0) {
            hit = Physics2D.Raycast(firePoint.position, -1 * firePoint.right);
        }
        else
        {
            hit = Physics2D.Raycast(firePoint.position, firePoint.right);
        }

        if (hit.collider)
        {
            Debug.DrawLine(firePoint.position, hit.point, UnityEngine.Color.red);

            SizeManager hitSizeManager = hit.transform.GetComponent<SizeManager>();
            if (hitSizeManager != null)
            {
                if (leftClickDown)
                {
                    if (storedSize > 0)
                    {
                        float currentSize = hitSizeManager.GetSize();
                        float maximumSize = hitSizeManager.GetMaxSize();

                        if (storedSize - sizeDelta <= playerMinSize)
                        {
                            hitSizeManager.ChangeSize(storedSize - playerMinSize);
                            storedSize = playerMinSize;
                        }
                        else if (currentSize + sizeDelta >= maximumSize)
                        {
                            storedSize -= maximumSize - currentSize;
                            hitSizeManager.SetSize(maximumSize);
                        }
                        else
                        {
                            hitSizeManager.ChangeSize(sizeDelta);
                            storedSize -= sizeDelta;
                        }
                    }
                }
                else if (rightClickDown)
                {
                    float currentSize = hitSizeManager.GetSize();
                    float minimumSize = hitSizeManager.GetMinSize();

                    if (storedSize + sizeDelta >= playerMaxSize)
                    {
                        hitSizeManager.ChangeSize(- 1 * storedSize + playerMaxSize);
                        storedSize = playerMaxSize;
                    }
                    else if (currentSize - sizeDelta <= minimumSize)
                    {
                        storedSize += currentSize - minimumSize;
                        hitSizeManager.SetSize(minimumSize);
                    }
                    else
                    {
                        hitSizeManager.ChangeSize(-1 * sizeDelta);
                        storedSize += sizeDelta;

                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        isGrounded = true;
        anim.SetBool("IsGrounded", true);
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        isGrounded = false;
        anim.SetBool("IsGrounded", false);
    }
}
