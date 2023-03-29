using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Speed")]
    public float speed = 15f;
    [Header("Speed After Lose")]
    public float newSpeed = 15f;
    public float rotationSmoothTime;

    [Header("Gravity")]
    public float gravity = 9.8f;
    public float gravityMultiplier = 2;
    public float groundedGravity = -0.5f;

    [Header("Jump Height")]
    public float jumpHeight = 3f;
    float velocityY;

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

    [Header("Timer")]
    public TextMeshProUGUI timeText;
    public float timer;
    [Header("Timer After Lose")]
    public float newTime;

    [Header("Lose State")]
    public GameObject loseText;
    [Header("Win State")]
    public GameObject winText;
    [Header("How Long Lose is Displayed")]
    public float LoseTime;

    bool timerActive = true;

    private void Awake()
    {
        //getting reference for components on the Player
        controller = GetComponent<CharacterController>();
        cam = Camera.main;
    }

    void Start()
    {
        winObjectText.text = "Win: " + winObject.ToString();
        loseText.SetActive(false);
        winText.SetActive(false);
    }

    private void Update()
    {
        HandleMovement();
        HandleGravityAndJump();

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
        //bool to activate and deactive timer
        //only runs while timerActive == true
        if (timerActive == true)
        {
            timer -= Time.deltaTime;
        }
        if (timer < 0)
        {
            timer = 0;
        }
        timeText.text = timer.ToString("F2");

        // win
        /*if (winObject == 1)
        {
            WinCondition();
        }*/
        // win
        //If timer is above zero, win object collected and timer is active teleport the player, activate win text, run WinCondition after 0.5s
        if ((timer > 0) && (winObject == 1) && (timerActive == true))
        {
            transform.position = new Vector3(-0.6300001f, 2.7f, -0.3499999f);
            winText.SetActive(true);
            Invoke("WinCondition", 0.5f);
        }

        // lose
        if ((timer == 0) && (winObject == 0) && (timerActive == true))
        {
            transform.position = new Vector3(-0.6300001f, 2.7f, -0.3499999f);
            loseText.SetActive(true);
            Invoke("LoseCondition", 0.5f);
            //StartCoroutine(LoseState(LoseTime));
        }
    }

    //Method to disable timer
    //Also starts coroutine to remove text after delay
    void WinCondition()
    {
        timerActive = false;
        StartCoroutine(TextRemove(winText, 4f));
    }

    void LoseCondition()
    {
        timerActive = false;
        StartCoroutine(LoseReset(loseText, 4f));
    }

    IEnumerator LoseReset(GameObject text, float delay)
    {
        yield return new WaitForSeconds(delay);
        text.SetActive(false);
        timerActive = true;
        timer = newTime;
    }

    //Sets specified gameobject to inactive after specified delay
    IEnumerator TextRemove(GameObject text, float delay)
    {
        yield return new WaitForSeconds(delay);
        text.SetActive(false);
    }

    IEnumerator LoseState(float LoseTime)
    {
        gameOver = true;
        loseText.SetActive(true);
        speed = 0;
        yield return new WaitForSeconds(LoseTime);
        transform.position = new Vector3(-0.6300001f, 2.7f, -0.3499999f);
        speed = newSpeed;
        loseText.SetActive(false);
        timer = newTime;
    }

    private void HandleMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            //compute rotation
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
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

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
        }
    }

    void HandleGravityAndJump()
    {
        //apply groundedGravity when the Player is Grounded
        if (controller.isGrounded && velocityY < 0f)
            velocityY = groundedGravity;

        //When Grounded and Jump Button is Pressed, set veloctiyY with the formula below
        if (controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            velocityY = Mathf.Sqrt(jumpHeight * 2f * gravity);
        }

        //applying gravity when Player is not grounded
        velocityY -= gravity * gravityMultiplier * Time.deltaTime;
        controller.Move(Vector3.up * velocityY * Time.deltaTime);
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
