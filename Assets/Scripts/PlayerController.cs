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
    private Scene currentScene;
    private string sceneName;

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

    // private bool isDefault;
    // private bool isMaze;
    // private bool isArena;
    // private bool isVent;

    [Header("Timer")]
    [Header("Maze")]
    public TextMeshProUGUI timeText;
    [Header("Maze")]
    public float timer;
    [Header("Timer After Lose")]
    [Header("Maze")]
    public float newTime;
    [Header("Coliseum")]
    public float newTime2;
    [Header("Maze")]
    bool timerActive = false;

    [Header("Colosseum")]
    public float colosseumTimer;
    [SerializeField]
    private GameObject colosseumTimerDisplay;
    [SerializeField]
    private TextMeshProUGUI colosseumTimerText;
    [SerializeField]
    private GameObject colosseumDoor;
    [SerializeField]
    private GameObject colosseumLevelBlocker;
    [SerializeField]
    private GameObject colosseumSpawners;
    [SerializeField]
    private GameObject colosseumTrigger;
    [SerializeField]
    private Transform colosseumSpawn;
    [SerializeField]
    private GameObject colosseumWinObject;

    private bool colWinObjectSpawned;

    [Header("Maze")]
    public GameObject Timer1;
    [SerializeField]
    private GameObject mazeBlockingDoor;
    // [Header("Default")]
    // public GameObject default;

    [Header("Vent")]
    public GameObject ventCounterDisplay;
    [SerializeField]
    private TextMeshProUGUI ventCounterText;
    [SerializeField]
    private GameObject platformerWinObject;

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
    private bool laserCooldown = false;
    [SerializeField]
    private LayerMask shootableMasks;

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
    [SerializeField]
    private Transform tutorialPos;


    Animator animator;

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

    [SerializeField]
    private HUD hud;
    [SerializeField]
    private UIFadeOut uiFade;

    bool deathCol = false;



    GameObject[] directionLight;

    public int platformerCount;

    bool deathPlat = false;

    [SerializeField]
    private Transform platformerSpawnPos;

    public int platformerGoal;

    public GameObject platformerWin;

    public GameObject mazeDoor;
    public GameObject platformerDoor;
    public GameObject colDoor;

    public PauseUi pauseUi;

    private Vector3 startPos;
    private Vector3 cameraStartPos;

    [SerializeField]
    private Transform mazeSpawn;

    [SerializeField]
    private GameObject pickupParticle;

    private DestructibleObject[] destructibleObject;
    private GameObject[] cheese;

    [Header("DisplayWinObjects")]
    [SerializeField]
    private GameObject displayMazeWin;
    [SerializeField]
    private GameObject displayArenaWin;
    [SerializeField]
    private GameObject displayVentWin;

    private GameObject button;
    [SerializeField]
    private GameObject primaryFan;

    [Header("EscapeCheckpoints")]
    [SerializeField]
    private Transform escapeCheckpoint1;
    [SerializeField]
    private Transform escapeCheckpoint2;
    [SerializeField]
    private Transform escapeCheckpoint3;
    private Transform curCheckpoint;

    // new Vector3(185.8f, 194.87f, -527.3f);
    // new Vector3(-99.79999f, 199.09f, 100.1f);
    // new Vector3(391.7f, 386.5f, -618.3f);

    bool deathEscape = false;

    public Vector3 escapeCheckpoint;

    private void Awake()
    {
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

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
        startPos = transform.position;

        // AudioManager.Instance.musicSource.Stop();
        // AudioManager.Instance.PlayMusic("HubMusic");

        hud.isDefault = true;
        hud.isMaze = false;
        hud.isArena = false;
        hud.isVent = false;

        cheese = GameObject.FindGameObjectsWithTag("PlatformPickUp");
        destructibleObject = (DestructibleObject[])FindObjectsOfType(typeof(DestructibleObject));

        displayMazeWin.SetActive(false);
        displayArenaWin.SetActive(false);
        displayVentWin.SetActive(false);

        button = GameObject.FindWithTag("FanButton");

        escapeCheckpoint = this.transform.position;
    }

    private void OnEnable()
    {
        playerControls.Enable();
        shootAction.performed += _ => ShootGun();
        glowAction.performed += _ => Glow();
        glowAction.Enable();
        shrinkAction.performed += _ => Shrink();
        shrinkAction.canceled += _ => ShrinkEnd();
        interactAction.performed += _ => Interact();
    }

    private void OnDisable()
    {
        playerControls.Disable();
        shootAction.performed -= _ => ShootGun();
        glowAction.performed -= _ => Glow();
        glowAction.Disable();
        shrinkAction.performed -= _ => Shrink();
        shrinkAction.canceled -= _ => ShrinkEnd();
        interactAction.performed -= _ => Interact();
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

                    AudioManager.Instance.PlaySFX("TVZoomIn");
                }
            }
        }
        else
        {
            interacting = !interacting;
            TVCinemachine.GetComponent<CinemachineVirtualCamera>().Priority += 10;
            Invoke("ExitTutorialLevel", 1.5f);

            AudioManager.Instance.PlaySFX("TVZoomOut");
        }
    }

    private void EnterTutorialLevel()
    {
        startLocation = this.gameObject.transform.position;
        this.gameObject.transform.position = tutorialPos.position;

        tutorialCamera.GetComponent<Camera>().enabled = true;
        tutorialCamera.GetComponent<AudioListener>().enabled = true;
        tutorialCamera.GetComponent<CinemachineBrain>().enabled = true;

        gameCamera.SetActive(false);
        cameraTransform = tutorialCamera.transform;

        TVCinemachine.GetComponent<CinemachineVirtualCamera>().Priority -= 10;

        foreach (GameObject go in directionLight)
        {
            go.SetActive(false);
        }

        // disable script
        GetComponent<LightScript>().enabled = false;

        //Respawn target dummies
        foreach (DestructibleObject plank in destructibleObject)
        {
            plank.gameObject.SetActive(true);
        }
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

        GetComponent<LightScript>().enabled = true;

        hud.glowing = false;
        glowLight.intensity = 0f;
        lightToggle = false;
    }
    private void Shrink()
    {
        if (this != null)
        {
            if (playerSize == startSize)
            {
                AudioManager.Instance.PlaySFX("Shrink");

                hud.shrinking = true;
                StartCoroutine(ChangeScale.StartFade(this.gameObject, 0.1f, shrinkSize));
                animator.SetBool("Shrink", true);
                Invoke("ShrinkAnimCancel", 0.5f);
            }
        }   
    }

    private void ShrinkEnd()
    {
        if (this != null)
        {
            if (playerSize == shrinkSize)
            {
                AudioManager.Instance.PlaySFX("Grow");

                hud.shrinking = false;
                StartCoroutine(ChangeScale.StartFade(this.gameObject, 0.1f, startSize));
                animator.SetBool("Shrink", true);
                Invoke("ShrinkAnimCancel", 0.5f);
            }
        }  
    }

    private void ShootGun()
    {
        if (this != null)
        {
            if (aiming == true && laserCooldown == false)
            {
                hud.shooting = true;
                animator.SetBool("Shoot", true);
                Invoke("Shoot", 0.5f);
                laserCooldown = true;
            }
        }   
    }

    private void Shoot()
    {
        if (this != null)
        {
            AudioManager.Instance.PlaySFX("LaserFire");

            animator.SetBool("Shoot", false);
            RaycastHit hit;
            hud.shooting = false;
            laserCooldown = false;
            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, Mathf.Infinity, shootableMasks))
            {
                Vector3 directionToTarget = (hit.point - particlePos.transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
                ParticleSystem laser = ParticleSystem.Instantiate(laserParticle, particlePos.position, lookRotation, particleParent);
            }
        }    
    }

    private void Glow()
    {
        if (this != null)
        {
            lightToggle = !lightToggle;
            if (lightToggle == true)
            {
                AudioManager.Instance.PlaySFX("Glow");

                hud.glowing = true;
                StartCoroutine(FadeLightSource.StartFade(glowLight, 2f, maxLightIntensity));
            }
            if (lightToggle == false)
            {
                AudioManager.Instance.PlaySFX("GlowFade");

                hud.glowing = false;
                StartCoroutine(FadeLightSource.StartFade(glowLight, 2f, 0f));
            }
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
        directionLight = GameObject.FindGameObjectsWithTag("light");

        colWinObjectSpawned = false;

        // Testing: works!
        // VoicelinesController.Instance.PlayVoiceline("StartingVoiceline");
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

        if (pauseUi.hud == true)
        {
            ResetColosseum();
            ResetVent();
            mazeBlockingDoor.SetActive(false);
            colosseumLevelBlocker.SetActive(false);

            if (sceneName == "Lab Rat")
            {
                AudioManager.Instance.musicSource.Stop();
                AudioManager.Instance.PlayMusic("HubMusic");
            }

            if (sceneName == "New Escape")
            {
                AudioManager.Instance.musicSource.Stop();
                AudioManager.Instance.PlayMusic("EscapeMusic");
            }

            hud.isDefault = true;
            hud.isMaze = false;
            hud.isArena = false;
            hud.isVent = false;

            transform.position = startPos;
            timerActive = false;
            // default.SetActive(false);
            Timer1.SetActive(false);
            pauseUi.hud = false;
            pauseUi.Resume();

            GetComponent<LightScript>().enabled = true;

            foreach (GameObject pickup in cheese)
            {
                pickup.SetActive(true);
            }

            button.SetActive(true);
            primaryFan.GetComponent<FanScript>().enabled = true;
            primaryFan.GetComponent<AudioSource>().enabled = true;

            foreach (DestructibleObject plank in destructibleObject)
            {
                plank.gameObject.SetActive(true);
            }

            platformerCount = 0;
            ventCounterText.text = ": " + platformerCount + "/3";
        }
        if (Physics.CheckSphere(transform.position + (Vector3.up * 5), distanceToGround, groundedLayer))
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

        // colosseum
        if (colosseumTimerDisplay.activeSelf == true)
        {
            colosseumTimer -= Time.deltaTime;
            colosseumTimerText.text = colosseumTimer.ToString("F2");
        }

        if (colosseumTimer <= 0 && colWinObjectSpawned == false)
        {
            colosseumDoor.SetActive(false);
            colosseumSpawners.SetActive(false);
            colosseumWinObject.SetActive(true);
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            AudioManager.Instance.PlaySFX("CollectibleSpawn");

            colWinObjectSpawned = true;
            //foreach (GameObject enemy in enemies)
            //GameObject.Destroy(enemy);

            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<EnemyController>().Hit();
            }
        }

        // Force timer to zero (even after win object spawned)
        if ((colosseumTimer <= 0))
        {
            colosseumTimer = 0;

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
        if (((timer == 0) && (timerActive == true) || (dead == true)))
        {
            transform.position = mazeSpawn.position;
            // loseText.SetActive(true);
            AudioManager.Instance.PlaySFX("LoseSound");
            // timer = newTime;
            timerActive = false;
            dead = false;
            // Invoke("LoseCondition", 0.5f);
            // StartCoroutine(LoseState(LoseTime));
        }

        // lose coliseum
        if (deathCol == true)
        {
            transform.position = colosseumSpawn.position;
            AudioManager.Instance.PlaySFX("LoseSound");
            colosseumTimerDisplay.SetActive(false);
            // default.SetActive(true);
            colosseumDoor.SetActive(false);
            deathCol = false;
            colosseumSpawners.SetActive(false);
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            //foreach (GameObject enemy in enemies)
            //GameObject.Destroy(enemy);

            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<EnemyController>().Hit();
            }
        }

        // lose platformer
        if (deathPlat == true)
        {
            transform.position = platformerSpawnPos.position;
            deathPlat = false;
            foreach (GameObject pickup in cheese)
            {
                pickup.SetActive(true);
            }
            button.SetActive(true);
            primaryFan.GetComponent<FanScript>().enabled = true;
            primaryFan.GetComponent<AudioSource>().enabled = true;
            platformerCount = 0;
            ventCounterText.text = ": " + platformerCount + "/3";
            AudioManager.Instance.PlaySFX("LoseSound");
        }

        // fall off during escape
        if (deathEscape == true)
        {
            AudioManager.Instance.PlaySFX("LoseSound");
            deathEscape = false;
        }

        // win platformer
        if (platformerCount == platformerGoal)
        {
            //platformerWin.SetActive(true);
            // AudioManager.Instance.PlaySFX("CollectibleSpawn");
        }

        // For testing New Escape
        if (Input.GetKeyDown(";"))
        {
            EscapeState();
        }
    }

    void CenterText()
    {
        // maze
        string timerStr = timer.ToString("00.00");
        // coliseum
        string timerStr2 = colosseumTimer.ToString("00.00");
        // maze
        timeText.SetText($"<mspace={charWidth}em>{timerStr}");
        colosseumTimerText.SetText($"<mspace={charWidth}em>{timerStr2}");
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
        if (collider.CompareTag("EscapeDeathZone"))
        {
            transform.position = escapeCheckpoint;
            AudioManager.Instance.PlaySFX("LoseSound");
        }
        if (collider.CompareTag("TutorialCheese"))
        {
            interacting = !interacting;
            TVCinemachine.GetComponent<CinemachineVirtualCamera>().Priority += 10;
            AudioManager.Instance.PlaySFX("TVZoomOut");
            AudioManager.Instance.PlaySFX("CheeseCollect");
            Invoke("ExitTutorialLevel", 1.5f);
        }
        if (collider.CompareTag("winMaze"))
        {
            if (winObject == 3)
            {
                EscapeState();
            }
        
            AudioManager.Instance.musicSource.Stop();
            AudioManager.Instance.PlayMusic("HubMusic");

            hud.isMaze = false;
            hud.isDefault = true;

            winObject += 1;
            Debug.Log("Win Object: " + winObject);
            timerActive = false;
            AudioManager.Instance.PlaySFX("WinSound");
            transform.position = startPos;
            Timer1.SetActive(false);
            mazeBlockingDoor.SetActive(false);
            mazeDoor.transform.position = new Vector3(-319f, 207.2345f, -622f);
            mazeDoor.transform.rotation = Quaternion.Euler(0, 0, 0);

            displayMazeWin.SetActive(true);
        }

        if (collider.CompareTag("winColiseum"))
        {
            if (winObject == 3)
            {
                EscapeState();
            }

            AudioManager.Instance.musicSource.Stop();
            AudioManager.Instance.PlayMusic("HubMusic");

            hud.isArena = false;
            hud.isDefault = true;

            winObject += 1;
            Debug.Log("Win Object: " + winObject);
            AudioManager.Instance.PlaySFX("WinSound");
            transform.position = startPos;
            colosseumLevelBlocker.SetActive(false);
            colosseumTimerDisplay.SetActive(false);
            // default.SetActive(true);
            colDoor.transform.position = new Vector3(-244f, 207.2345f, -622f);
            colDoor.transform.rotation = Quaternion.Euler(0, 0, 0);

            displayArenaWin.SetActive(true);
        }

        if (collider.CompareTag("winPlatformer"))
        {
            if (winObject == 3)
            {
                EscapeState();
            }

            AudioManager.Instance.musicSource.Stop();
            AudioManager.Instance.PlayMusic("HubMusic");

            hud.isVent = false;
            hud.isDefault = true;

            winObject += 1;
            Debug.Log("Win Object: " + winObject);
            AudioManager.Instance.PlaySFX("WinSound");
            transform.position = startPos;
            ventCounterDisplay.SetActive(false);
            GetComponent<LightScript>().enabled = true;
            foreach (GameObject go in directionLight)
            {
                go.SetActive(true);
            }
            platformerDoor.transform.position = new Vector3(-281f, 207.2345f, -659f);
            platformerDoor.transform.rotation = Quaternion.Euler(0, 0, 0);

            displayVentWin.SetActive(true);
        }

        if (collider.CompareTag("startMaze"))
        {
            if (timerActive == false)
            {
                hud.isDefault = false;
                hud.isMaze = true;
                AudioManager.Instance.musicSource.Stop();
                AudioManager.Instance.PlayMusic("MazeMusic");
                timerActive = true;
                mazeBlockingDoor.SetActive(true);
                Timer1.SetActive(true);
                timer = newTime;
                foreach (DestructibleObject plank in destructibleObject)
                {
                    plank.gameObject.SetActive(true);
                }
            }
        }

        if (collider.CompareTag("startColosseum"))
        {
            StartColosseum();

            // default.SetActive(false);
            // HUD.Instance.SetDefault(isDefault);
            // HUD.Instance.SetArena(isArena);
        }

        if (collider.CompareTag("startPlatformer"))
        {
            AudioManager.Instance.musicSource.Stop();
            AudioManager.Instance.PlayMusic("VentMusic");

            hud.isDefault = false;
            hud.isVent = true;

            transform.position = platformerSpawnPos.position;
            ventCounterDisplay.SetActive(true);
            ventCounterText.text = ": " + platformerCount + "/3";

            foreach (GameObject go in directionLight)
            {
                go.SetActive(false);
            }

            // disable script
            GetComponent<LightScript>().enabled = false;
        }

        if (collider.CompareTag("PlatformPickUp"))
        {
            GameObject pickup = GameObject.Instantiate(pickupParticle, collider.gameObject.transform.position, Quaternion.identity);

            AudioManager.Instance.PlaySFX("CheeseCollect");

            platformerCount += 1;
            ventCounterText.text = ": " + platformerCount + "/3";

            Debug.Log("Platformer Object: " + platformerCount);
            collider.gameObject.SetActive(false);
            if (platformerCount == 3)
            {
                AudioManager.Instance.PlaySFX("CollectibleSpawn");
                platformerWinObject.SetActive(true);
            }
        }

        if (collider.CompareTag("Trap"))
        {
            deathPlat = true;
        }

        if (collider.CompareTag("PlatformerDeathZone"))
        {
            deathPlat = true;
        }

        if (collider.CompareTag("WinState"))
        {
            uiFade.FadeUI(5f);
        }

        // if (collider.CompareTag("hubMusic"))
        // {
        //     AudioManager.Instance.musicSource.Stop();
        //     AudioManager.Instance.PlayMusic("HubMusic");
        // }

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
            colosseumTrigger.SetActive(true);
            //Debug.Log("Enemy");
        }
    }

    private void StartColosseum()
    {
        if (colWinObjectSpawned == false)
        {
            colosseumTimerDisplay.SetActive(true);
            colosseumTrigger.SetActive(false);
            colosseumDoor.SetActive(true);
            colosseumLevelBlocker.SetActive(true);
            colosseumSpawners.SetActive(true);
            colosseumTimer = 60;
            AudioManager.Instance.musicSource.Stop();
            AudioManager.Instance.PlayMusic("ArenaMusic");
            hud.isDefault = false;
            hud.isArena = true;
        }
    }

    private void ResetColosseum()
    {
        colosseumTimerDisplay.SetActive(false);
        colosseumTrigger.SetActive(true);
        colosseumDoor.SetActive(false);
        colosseumSpawners.SetActive(false);
        AudioManager.Instance.musicSource.Stop();
        AudioManager.Instance.PlayMusic("HubMusic");
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        //foreach (GameObject enemy in enemies)
        //GameObject.Destroy(enemy);

        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<EnemyController>().Hit();
        }
    }

    private void ResetVent()
    {
        ventCounterDisplay.SetActive(false);
        // ventCounterText.text = 
        AudioManager.Instance.musicSource.Stop();
        AudioManager.Instance.PlayMusic("HubMusic");
    }

    private void EscapeState()
    {
        // SceneManager.LoadScene("Win");

        // Cursor.lockState = CursorLockMode.None;
        // Cursor.visible = true;

        AudioManager.Instance.PlaySFX("WinSound");
        // There should probably be some sort of delay here, from switching to Lab Rat
        // scene to New Escape (blackout scene swap?)
        SceneManager.LoadScene("New Escape");

        AudioManager.Instance.musicSource.Stop();
        AudioManager.Instance.PlayMusic("EscapeMusic");
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
