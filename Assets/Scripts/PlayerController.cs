using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Speed")]
    public float speed = 15f;
    [Header("Speed After Lose")]
    public float newSpeed = 15f;
    public float rotationSmoothTime;

    [Header("Gravity")]
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private bool jumpPressed = false;
    private float gravityValue = -9.81f;
    private float charWidth = 0.9f;

    [Header("Jump Height")]
    public float jumpHeight = 3f;

    [Header("Reference the CharacterController on Player")]
    public CharacterController controller;

    [Header("Reference the Camera")]
    public Camera cam;
    public GameObject zoomCam;
    public LayerMask aimColliderLayerMask;

    float currentAngle;
    float currentAngleVelocity;

    [Header("Laser")]
    public GameObject laserParticle;

    public int winObject = 0;
    public TextMeshProUGUI winObjectText;

    [Header("Game Over State")]
    public bool gameOver = false;
    public bool dead;

    [Header("Timer")]
    public TextMeshProUGUI timeText;
    public float timer;
    [Header("Timer After Lose")]
    public float newTime;
    bool timerActive = true;

    [Header("Lose State")]
    public GameObject loseText;
    [Header("Win State")]
    public GameObject winText;
    [Header("How Long Lose is Displayed")]
    public float LoseTime;

    [Header("Movement Controller")]
    //public InputAction playerControls;

    private PlayerControls playerControls;


    Animator animator;

    private void Awake()
    {
        //getting reference for components on the Player
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        cam = Camera.main;
        playerControls = new PlayerControls();
    }

    void Start()
    {
        winObjectText.text = "Win: " + winObject.ToString();
        loseText.SetActive(false);
        winText.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
    }

    void OnJump()
    {
        Debug.Log("Jump pressed");
        if(controller.velocity.y == 0)
        {
            jumpPressed = true;
        }
    }

    void MovementJump()
    {
        groundedPlayer = controller.isGrounded;
        if(groundedPlayer)
        {
            playerVelocity.y = 0.0f;
            animator.SetBool("Jump", false);
        }

        if(jumpPressed && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -1 * gravityValue);
            animator.SetBool("Jump", true);
            jumpPressed = false;
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    void OnEscape()
    {
        Application.Quit();
    }

    void OnGlow()
    {
        //Glow goes here
    }

    void OnShrink()
    {
        //Shrink goes here
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void FixedUpdate()
    {
        MovementJump();
        HandleMovement();
        HandleGravityAndJump();
    }
    private void Update()
    {


        if (timerActive == true)
        {
            timer -= Time.deltaTime;
        }
        if (timer < 0)
        {
            timer = 0;
        }
        // timeText.text = timer.ToString("F2");

        CenterText();

        // win
        /*if (winObject == 1)
        {
            WinCondition();
        }*/
        // win
        //If timer is above zero, win object collected and timer is active teleport the player, activate win text, run WinCondition after 0.5s
        if ((timer > 0) && (winObject == 1) && (timerActive == true))
        {
            AudioManager.Instance.PlaySFX("WinSound");
            //transform.position = new Vector3(-0.6300001f, 2.7f, -0.3499999f);
            //winText.SetActive(true);
            Invoke("WinCondition", 0.5f);
        }

        // lose
        if (((timer == 0) && (winObject == 0) && (timerActive == true)) || dead == true)
        {
            //transform.position = new Vector3(-0.6300001f, 2.7f, -0.3499999f);
            //loseText.SetActive(true);
            AudioManager.Instance.PlaySFX("LoseSound");
            Invoke("LoseCondition", 0.5f);
            //StartCoroutine(LoseState(LoseTime));
        }
    }

    void CenterText()
    {
        string timerStr = timer.ToString("00.00");
        timeText.SetText($"<mspace={charWidth}em>{timerStr}");
    }

    //Method to disable timer
    //Also starts coroutine to remove text after delay
    void WinCondition()
    {
        SceneManager.LoadScene("Win");
        // timerActive = false;
        // StartCoroutine(TextRemove(winText, 4f));
    }

    void LoseCondition()
    {
        SceneManager.LoadScene("Lose");
        // timerActive = false;
        // StartCoroutine(LoseReset(loseText, 4f));
    }

    // IEnumerator LoseReset(GameObject text, float delay)
    // {
    //     // yield return new WaitForSeconds(delay);
    //     // text.SetActive(false);
    //     // timerActive = true;
    //     // timer = newTime;
    // }

    //Sets specified gameobject to inactive after specified delay
    // IEnumerator TextRemove(GameObject text, float delay)
    // {
    //     // yield return new WaitForSeconds(delay);
    //     // text.SetActive(false);
    // }

    // IEnumerator LoseState(float LoseTime)
    // {
    //     // gameOver = true;
    //     // loseText.SetActive(true);
    //     // speed = 0;
    //     // yield return new WaitForSeconds(LoseTime);
    //     // transform.position = new Vector3(-0.6300001f, 2.7f, -0.3499999f);
    //     // speed = newSpeed;
    //     // loseText.SetActive(false);
    //     // timer = newTime;
    // }

    private void HandleMovement()
    {

        Vector3 direction = playerControls.Movement.Move.ReadValue<Vector2>();

        if (direction.x != 0 || direction.y != 0)
        {
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }

        if (direction.magnitude >= 0.1f)
        {
            //compute rotation
            float targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
            currentAngle = Mathf.SmoothDampAngle(currentAngle, targetAngle, ref currentAngleVelocity, rotationSmoothTime);
            transform.rotation = Quaternion.Euler(0, currentAngle, 0);

            //move in direction of rotation
            Vector3 rotatedMovement = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            controller.Move(rotatedMovement * speed * Time.deltaTime);
        }

        Vector3 mouseWorldPosition = Vector3.zero;
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {
            Vector3 directionToTarget = (raycastHit.point - laserParticle.transform.position);
            laserParticle.transform.forward = directionToTarget;
            mouseWorldPosition = raycastHit.point;
        }

        if (zoomCam.gameObject.activeSelf == true)
        {
            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            //transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
        }
    }

    void HandleGravityAndJump()
    {
        /*
        //apply groundedGravity when the Player is Grounded
        if (controller.isGrounded && velocityY < 0f)
            velocityY = groundedGravity;
            animator.SetBool("Jump", false);

        //When Grounded and Jump Button is Pressed, set veloctiyY with the formula below
        if (controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            velocityY = Mathf.Sqrt(jumpHeight * 2f * gravity);
            animator.SetBool("Jump", true);
        }

        //applying gravity when Player is not grounded
        velocityY -= gravity * gravityMultiplier * Time.deltaTime;
        controller.Move(Vector3.up * velocityY * Time.deltaTime);
        */
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("winObject"))
        {
            Destroy(collider.gameObject);
            winObject += 1;
            winObjectText.text = "Win: " + winObject.ToString();
        }
    }

}
