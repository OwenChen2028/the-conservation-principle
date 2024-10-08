using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
	private Vector2 moveDirection;

	private float startingActualMass;

	[SerializeField] private bool useOldMovement;

	[SerializeField] private float moveForce;
	[SerializeField] private float movementSpeed;
    [SerializeField] private float decelerationSpeed;

    private bool isGrounded = false;

	private bool jumpKeyDown = false;
	[SerializeField] private float jumpVelocity;

	private Rigidbody2D rb;
    private Animator anim;

	private GameObject playerUI;
	private TMP_Text massText; 

    private GameObject sizeGun;
	private Transform firePoint;
    private GameObject gunEffect;

    private Camera cam;
	private Vector2 mouseWorldPos;

	private bool leftClickDown;
	private bool rightClickDown;


	[SerializeField] private float storedSize;
	[SerializeField] private float sizeDelta;

	[SerializeField] private float playerMinSize;
	[SerializeField] private float playerMaxSize;

	[SerializeField] private int maxBounces;

	[SerializeField] private float soundEffectVolume;

	private AudioSource makeBigAudioSource;
	private AudioSource makeSmallAudioSource;
	private AudioSource jumpAudioSource;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        cam = Camera.main;

		sizeGun = transform.Find("Size Gun").gameObject;
		firePoint = sizeGun.transform.Find("Fire Point");

		makeBigAudioSource = transform.Find("MakeBigSound").GetComponent<AudioSource>();
		makeSmallAudioSource = transform.Find("MakeSmallSound").GetComponent<AudioSource>();
		jumpAudioSource = transform.Find("JumpSound").GetComponentInChildren<AudioSource>();

		startingActualMass = rb.mass;

		gunEffect = sizeGun.transform.Find("Gun Effect").gameObject;
		gunEffect.SetActive(false);

		playerUI = transform.Find("Player UI").gameObject;
		massText = playerUI.transform.Find("Mass Text").GetComponent<TMP_Text>();
    }

	private void Update()
	{
		HandleMovementInput();
		HandleAimingInput();
		HandleShootingInput();
        HandleSoundEffects();

        if (Input.GetKeyDown(KeyCode.R))
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}

		gunEffect.GetComponent<LineRenderer>().SetPosition(0, firePoint.position);

		float massPercent = storedSize / playerMaxSize * 100;
		massText.text = (Mathf.Round(massPercent)).ToString() + "%";
		
		playerUI.transform.localScale = new Vector2(
			Mathf.Abs(playerUI.transform.localScale.x) * Mathf.Sign(transform.localScale.x),
			playerUI.transform.localScale.y
		);
    }

	private void FixedUpdate()
	{
		HandleMovement();
		HandleAiming();
		HandleShooting();
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

		if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
		{
			jumpKeyDown = true;
		}
		if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
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
				rb.velocity = new Vector2(-1 * maxSpeed, rb.velocity.y);
			}
				
			if (moveDirection.x == 0) 
			{
				rb.velocity = Vector2.Lerp(rb.velocity, new Vector2(0, rb.velocity.y), decelerationSpeed);
			}
		}

		if (isGrounded && jumpKeyDown)
		{
			rb.velocity = new Vector2(rb.velocity.x, 0);
			rb.AddForce(startingActualMass * new Vector2(0, jumpVelocity), ForceMode2D.Impulse);
			anim.SetTrigger("Jump");
				
			isGrounded = false;
			anim.SetBool("IsGrounded", false);

			/*
            jumpAudioSource.volume = soundEffectVolume;
            jumpAudioSource.Play();
			*/
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
			gunEffect.SetActive(false);
			return;
		}

		RaycastHit2D hit;
		LayerMask ignorePlayer = ~(1 << 8); // ignores player

		if (transform.localScale.x < 0) {
			hit = Physics2D.Raycast(firePoint.position, -1 * firePoint.right, Mathf.Infinity, ignorePlayer);
		}
		else
		{
			hit = Physics2D.Raycast(firePoint.position, firePoint.right, Mathf.Infinity, ignorePlayer);
		}

		if (hit.collider)
		{
			gunEffect.SetActive(true);

			if (leftClickDown)
			{
                gunEffect.GetComponent<LineRenderer>().startColor = UnityEngine.Color.magenta;
                gunEffect.GetComponent<LineRenderer>().endColor = UnityEngine.Color.red;

            }
			else if (rightClickDown)
			{
                gunEffect.GetComponent<LineRenderer>().startColor = UnityEngine.Color.magenta;
                gunEffect.GetComponent<LineRenderer>().endColor = UnityEngine.Color.blue;

            }

			gunEffect.GetComponent<LineRenderer>().positionCount = 2;
            gunEffect.GetComponent<LineRenderer>().SetPosition(1, hit.point);

			Vector2 startDirection = firePoint.right;

			for (int i = 0; i < maxBounces; i++)
			{
				if (hit.collider.CompareTag("HorizMirror") || hit.collider.CompareTag("VertMirror"))
				{
					Vector2 reflectDirection;

					if (hit.collider.CompareTag("HorizMirror"))
					{
						reflectDirection = new Vector2(startDirection.x, -1 * startDirection.y);
					}
					else
					{
						reflectDirection = new Vector2(-1 * startDirection.x, startDirection.y);
					}

					if (transform.localScale.x < 0)
					{
						hit = Physics2D.Raycast(hit.point - 0.1f * reflectDirection, -1 * reflectDirection);
					}
					else
					{
						hit = Physics2D.Raycast(hit.point + 0.1f * reflectDirection, reflectDirection);
					}

					if (hit.collider)
					{
						startDirection = reflectDirection;

						gunEffect.GetComponent<LineRenderer>().positionCount += 1;
						gunEffect.GetComponent<LineRenderer>().SetPosition(gunEffect.GetComponent<LineRenderer>().positionCount - 1 , hit.point);
					}
				}
			}

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

	private void HandleSoundEffects()
	{
        if (Time.timeScale == 0)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			makeBigAudioSource.volume = soundEffectVolume;
			makeBigAudioSource.Play();
		}
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
			makeSmallAudioSource.volume = soundEffectVolume;
            makeSmallAudioSource.Play();
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
			makeBigAudioSource.volume = 0;
        }
		if (Input.GetKeyUp(KeyCode.Mouse1))
		{
			makeSmallAudioSource.volume = 0;
		}

    }

	private void OnTriggerStay2D(Collider2D col)
	{
		isGrounded = true;
		anim.SetBool("IsGrounded", true);
	}

	private async void OnTriggerExit2D(Collider2D col)
	{
 		if (isGrounded) {
			await Task.Delay(100);
			
			if (isGrounded)
			{
				isGrounded = false;
			}
		}
    }
}
