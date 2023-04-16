using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float rotationSpeed = 5f;
    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private LayerMask groundedLayer;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    [SerializeField]
    private float groundDrag;
    [SerializeField]
    private float distanceToGround;

    [Header("Speed After Lose")]
    public float newSpeed = 15f;
    public float rotationSmoothTime;

    private float charWidth = 0.9f;


    [Header("Reference the CharacterController on Player")]
    public CharacterController controller;


    public int winObject = 0;
    public TextMeshProUGUI winObjectText;

    [Header("Game Over State")]
    public bool gameOver = false;
    public bool dead;

    [Header("Timer")]
    [Header("Maze")]
    public TextMeshProUGUI timeText;
    [Header("Coliseum")]
    public TextMeshProUGUI timeText2;
    [Header("Maze")]
    public float timer;
    [Header("Coliseum")]
    public float timer2;
    [Header("Timer After Lose")]
    [Header("Maze")]
    public float newTime;
    [Header("Coliseum")]
    public float newTime2;
    [Header("Maze")]
    bool timerActive = false;
    [Header("Coliseum")]
    bool timerActive2 = false;
    [Header("Maze")]
    public GameObject Timer1;
    [Header("Coliseum")]
    public GameObject Timer2;

    [Header("Lose State")]
    public GameObject loseText;
    [Header("Win State")]
    public GameObject winText;
    [Header("How Long Lose is Displayed")]
    public float LoseTime;


    [Header("Movement Controller")]
    //public InputAction playerControls;
    private PlayerInput playerInput;
    private PlayerControls playerControls;
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction shootAction;
    private InputAction glowAction;
    private InputAction shrinkAction;
    private InputAction interactAction;
    

    [Header("Glow")]
    bool lightToggle = false;
    public float maxLightIntensity;
    [SerializeField]
    private Light glowLight;

    [Header("Laser")]
    [SerializeField]
    private Transform barrelTransform;
    [SerializeField]
    private Transform particlePos;
    [SerializeField]
    private Transform particleParent;
    public bool aiming;
    public ParticleSystem laserParticle;

    [Header("Shrink")]
    [SerializeField]
    private float shrinkSize = 3f;
    private float startSize;
    private float playerSize;

    [Header("Interact")]
    [SerializeField]
    private float interactRange = 5f;

    [Header("TutorialLevel")]
    public GameObject tutorialCamera;
    public GameObject gameCamera;


    Animator animator;

    bool deathCol = false;
    
    public GameObject cheese;

    private Transform cameraTransform;
    [SerializeField]
    private GameObject thirdPersonCamera;
    [SerializeField]
    private GameObject aimCamera;
    [SerializeField]
    private GameObject TVCinemachine;

    bool interacting = false;

    private Vector3 startLocation;

    [SerializeField]
    private GameObject interactUI;

    Rigidbody rb;

    private void Awake()
    {
        //getting reference for components on the Player
        animator = GetComponentInChildren<Animator>();
        playerInput = GetComponent<PlayerInput>();
        cameraTransform = Camera.main.transform;
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody>();

        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        shootAction = playerInput.actions["Shoot"];
        glowAction = playerInput.actions["Glow"];
        shrinkAction = playerInput.actions["Shrink"];
        interactAction = playerInput.actions["Interact"];

        startSize = transform.localScale.x;
    }

    private void OnEnable()
    {
        playerControls.Enable();
        shootAction.performed += _ => ShootGun();
        glowAction.performed += _ => Glow();
        shrinkAction.performed += _ => Shrink();
        shrinkAction.canceled += _ => ShrinkEnd();
        interactAction.performed += _ => Interact();
    }

    private void OnDisable() 
    {
        playerControls.Disable();
    }

    public TutorialTeleporter GetInteractableObject()
    {
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
        foreach (Collider collider in colliderArray)
        {
            if (collider.TryGetComponent(out TutorialTeleporter tutorialTeleporter))
            {
                return tutorialTeleporter;
            }
        }
        return null;
    }

    private void Interact()
    {
        if (interacting == false)
        {
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
            foreach (Collider collider in colliderArray)
            {
                if (collider.TryGetComponent(out TutorialTeleporter tutorialTeleporter))
                {
                    interacting = !interacting;
                    TVCinemachine.GetComponent<CinemachineVirtualCamera>().Priority += 10;
                    interactUI.transform.localScale = new Vector3(0, 0, 0);
                    Invoke("EnterTutorialLevel", 1.5f);
                }
            }
        }
        else
        {
            interacting = !interacting;
            TVCinemachine.GetComponent<CinemachineVirtualCamera>().Priority += 10;
            Invoke("ExitTutorialLevel", 1.5f);
        }
    }

    private void EnterTutorialLevel()
    {
        startLocation = this.gameObject.transform.position;
        this.gameObject.transform.position = new Vector3(0, 1000, 0);

        tutorialCamera.GetComponent<Camera>().enabled = true;
        tutorialCamera.GetComponent<AudioListener>().enabled = true;
        tutorialCamera.GetComponent<CinemachineBrain>().enabled = true;

        gameCamera.SetActive(false);
        cameraTransform = tutorialCamera.transform;

        TVCinemachine.GetComponent<CinemachineVirtualCamera>().Priority -= 10;    
    }

    private void ExitTutorialLevel()
    {
        this.gameObject.transform.position = startLocation;

        tutorialCamera.GetComponent<Camera>().enabled = false;
        tutorialCamera.GetComponent<AudioListener>().enabled = false;
        tutorialCamera.GetComponent<CinemachineBrain>().enabled = false;

        gameCamera.SetActive(true);
        cameraTransform = gameCamera.transform;

        TVCinemachine.GetComponent<CinemachineVirtualCamera>().Priority -= 10;
        interactUI.transform.localScale = new Vector3(1, 1, 1);
    }
    private void Shrink()
    {
        if (playerSize == startSize)
        {
            StartCoroutine(ChangeScale.StartFade(this.gameObject, 0.2f, shrinkSize));
            animator.SetBool("Shrink", true);
            Invoke("ShrinkAnimCancel", 0.5f);
        }
    }

    private void ShrinkEnd()
    {
        if (playerSize == shrinkSize)
        {
            StartCoroutine(ChangeScale.StartFade(this.gameObject, 0.2f, startSize));
            animator.SetBool("Shrink", true);
            Invoke("ShrinkAnimCancel", 0.5f);
        }
    }

    private void ShootGun()
    {
        if (aiming == true)
        {
            animator.SetBool("Shoot", true);
            Invoke("Shoot", 0.5f);
        }
    }

    private void Shoot()
    {
        animator.SetBool("Shoot", false);
        RaycastHit hit;
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, Mathf.Infinity))
        {
            Vector3 directionToTarget = (hit.point - particlePos.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
            ParticleSystem laser = ParticleSystem.Instantiate(laserParticle, particlePos.position, lookRotation, particleParent);
        }
    }

    private void Glow()
    {
        lightToggle = !lightToggle;
        if (lightToggle == true)
        {
            StartCoroutine(FadeLightSource.StartFade(glowLight, 2f, maxLightIntensity));
        }
        if (lightToggle == false)
        {
            StartCoroutine(FadeLightSource.StartFade(glowLight, 2f, 0f));
        }
    }

    private void ShrinkAnimCancel()
    {
        animator.SetBool("Shrink", false);
    }



    void Start()
    {
        winObjectText.text = "Win: " + winObject.ToString();
        loseText.SetActive(false);
        winText.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
    }

    void OnEscape()
    {
        Application.Quit();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }
    private void Update()
    {
        if (Physics.CheckSphere(transform.position, distanceToGround, groundedLayer))
        {
            groundedPlayer = true;
        }
        else
        {
            groundedPlayer = false;
        }

        if (groundedPlayer)
        {
            rb.drag = groundDrag;
            animator.SetBool("Jump", false);
        }
        else
        {
            rb.drag = 0;
        }

        if (jumpAction.triggered && groundedPlayer)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            animator.SetBool("Jump", true);
            rb.AddForce(transform.up * jumpHeight, ForceMode.Impulse);
        }

        if (GetInteractableObject() != null)
        {
            interactUI.SetActive(true);
        }
        else
        {
            interactUI.SetActive(false);
        }
        // maze
        if (timerActive == true)
        {
            timer -= Time.deltaTime;
        }
        if (timer < 0)
        {
            timer = 0;
        }

        // coliseum
        if (timerActive2 == true)
        {
            timer2 -= Time.deltaTime;
        }
        if (timer2 < 0)
        {
            timer2 = 0;
        }

        // timeText.text = timer.ToString("F2");

        CenterText();

        // win
        //If timer is above zero, win object collected and timer is active teleport the player, activate win text, run WinCondition after 0.5s
        // if ((winObject == 1) && (timerActive == false) && (hasRun == false))
        // {
        //     AudioManager.Instance.PlaySFX("WinSound");
        //     // Invoke("Reset", 0.5f);
        //     hasRun = true;
        //     transform.position = new Vector3(37.69f, 195.81f, -824.1f);
        //     // winText.SetActive(true);
        //     // Invoke("WinCondition", 0.5f);
        // }

        // lose maze
        if (((timer == 0) && (winObject == 0) && (timerActive == true) || (dead == true)))
        {
            transform.position = new Vector3(-24.55f, 196.08f, -806.58f);
            // loseText.SetActive(true);
            //AudioManager.Instance.PlaySFX("LoseSound");
            // timer = newTime;
            timerActive = false;
            dead = false;
            // Invoke("LoseCondition", 0.5f);
            // StartCoroutine(LoseState(LoseTime));
        }

        // lose coliseum
        if (deathCol == true)
        {
            transform.position = new Vector3(99.91f, 194.3172f, -823.0043f);
            //AudioManager.Instance.PlaySFX("LoseSound");
            // timer2 = newTime2;
            timerActive2 = false;
            deathCol = false;
        }

        // win coliseum
        if ((timer2 == 0))
        {
            timerActive2 = false;
            cheese.SetActive(true); 
        }
    }

    void CenterText()
    {
        // maze
        string timerStr = timer.ToString("00.00");
        // coliseum
        string timerStr2 = timer2.ToString("00.00");
        // maze
        timeText.SetText($"<mspace={charWidth}em>{timerStr}");
        // coliseum
        //timeText2.SetText($"<mspace={charWidth}em>{timerStr2}");

    }

    // void Reset()
    // {
    //     hasRun = true;
    // }

    //Method to disable timer
    //Also starts coroutine to remove text after delay
    // void WinCondition()
    // {
    //     // SceneManager.LoadScene("Win");
    //     StartCoroutine(TextRemove(winText, 4f));
    // }

    // void LoseCondition()
    // {
    //     SceneManager.LoadScene("Lose");
    //     // timerActive = false;
    //     // StartCoroutine(LoseReset(loseText, 4f));
    // }

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
    //     yield return new WaitForSeconds(delay);
    //     text.SetActive(false);
    //     //transform.position = new Vector3(37.69f, 195.81f, -824.1f);
    // }

    // IEnumerator LoseState(float LoseTime)
    // {
    //     gameOver = true;
    //     loseText.SetActive(true);
    //     speed = 0;
    //     yield return new WaitForSeconds(LoseTime);
    //     transform.position = new Vector3(-0.6300001f, 2.7f, -0.3499999f);
    //     speed = newSpeed;
    //     loseText.SetActive(false);
    //     timer = newTime;
    // }

    private void HandleMovement()
    {
        playerSize = this.gameObject.transform.localScale.x;

        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);
        move = move.x * cameraTransform.right.normalized + move.z * cameraTransform.forward.normalized;
        move.y = 0f;
        rb.AddForce(move.normalized * playerSpeed * 10, ForceMode.Force);
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > playerSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * playerSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }

        if ((move.x == 0) && (move.z == 0))
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }

        if (input.x != 0 || input.y != 0)
        {
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }

        // Changes the height position of the player..

        Quaternion targetRotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        /*Vector3 direction = playerControls.Movement.Move.ReadValue<Vector2>();

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

        }*/
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
            winObject += 1;
            Debug.Log("Win Object: " + winObject);
            timerActive = false;
            timerActive2 = false;
            //AudioManager.Instance.PlaySFX("WinSound");
            transform.position = new Vector3(37.69f, 195.81f, -824.1f);
            Timer1.SetActive(false);
            Timer2.SetActive(false);
        }

        if (collider.CompareTag("startMaze"))
        {
            timerActive = true;
            Timer1.SetActive(true);
            timer = newTime;
        }

        if (collider.CompareTag("startColiseum"))
        {
            timerActive2 = true;
            Timer2.SetActive(true);
            timer2 = newTime2;
        }

        // if (collider.CompareTag("Enemy"))
        // {
        //     Debug.Log("Enemy");
        //     deathCol = true;
        //     Debug.Log("Death Bool: " + deathCol);
        //     Destroy(collider.gameObject);
        // }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            deathCol = true;
            Destroy(collision.gameObject);
            Debug.Log("Enemy");
        }
    }


    public static class ChangeScale
    {
        public static IEnumerator StartFade(GameObject gameObject, float duration, float targetSize)
        {
            float currentTime = 0;
            Vector3 start = gameObject.transform.localScale;
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                float newValue = Mathf.Lerp(start.x, targetSize, currentTime / duration);
                gameObject.transform.localScale = new Vector3(newValue, newValue, newValue);
                yield return null;
            }
            yield break;
        }
    }

    public static class FadeLightSource
    {
        public static IEnumerator StartFade(Light light, float duration, float targetIntensity)
        {
            float currentTime = 0;
            float start = light.intensity;
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                light.intensity = Mathf.Lerp(start, targetIntensity, currentTime / duration);
                yield return null;
            }
            yield break;
        }
    }
}
