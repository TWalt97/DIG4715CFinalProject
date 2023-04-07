using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class NewPlayerController : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;
    [SerializeField]
    private float rotationSpeed = 5f;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Transform barrelTransform;
    [SerializeField]
    private Transform bulletParent;
    [SerializeField]
    private float bulletMissDistance = 25;
    [SerializeField]
    private Light glowLight;

    private CharacterController controller;
    private PlayerInput playerInput;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Transform cameraTransform;

    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction shootAction;
    private InputAction glowAction;
    private InputAction shrinkAction;

    Animator animator;
    Rigidbody rb;

    public bool aiming;
    public ParticleSystem laserParticle;
    private float playerSize;
    bool lightToggle = false;

    public float mazeTimer;
    private float mazeTimerStart;
    public TextMeshProUGUI mazeTimerText;
    private bool insideOfMaze = false;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform;

        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        shootAction = playerInput.actions["Shoot"];
        glowAction = playerInput.actions["Glow"];
        shrinkAction = playerInput.actions["Shrink"];

        Cursor.lockState = CursorLockMode.Locked;

        mazeTimerStart = mazeTimer;
    }

    private void OnEnable()
    {
        shootAction.performed += _ => ShootGun();
        glowAction.performed += _ => Glow();
        shrinkAction.performed += _ => Shrink();
        shrinkAction.canceled += _ => ShrinkEnd();
    }

    private void OnDisable()
    {
        shootAction.performed -= _ => ShootGun();
        glowAction.performed -= _ => Glow();
        shrinkAction.performed -= _ => Shrink();
        shrinkAction.canceled -= _ => ShrinkEnd();
    }

    private void Shrink()
    {
        if (playerSize == 9f)
        {
            StartCoroutine(ChangeScale.StartFade(this.gameObject, 0.2f, 0.5f));
            animator.SetBool("Shrink", true);
            Invoke("ShrinkAnimCancel", 0.5f);
        }
    }

    private void ShrinkEnd()
    {
        if (playerSize == 4.5f)
        {
            StartCoroutine(ChangeScale.StartFade(this.gameObject, 0.2f, 2f));
            animator.SetBool("Shrink", true);
            Invoke("ShrinkAnimCancel", 0.5f);
        }
    }

    private void ShrinkAnimCancel()
    {
        animator.SetBool("Shrink", false);
    }

    private void Glow()
    {
        lightToggle = !lightToggle;
        if (lightToggle == true)
        {
            StartCoroutine(FadeLightSource.StartFade(glowLight, 2f, 500f));
        }
        if (lightToggle == false)
        {
            StartCoroutine(FadeLightSource.StartFade(glowLight, 2f, 0f));
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
        GameObject bullet = GameObject.Instantiate(bulletPrefab, barrelTransform.position, Quaternion.identity, bulletParent);
        BulletController bulletController = bullet.GetComponent<BulletController>();
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, Mathf.Infinity))
        {
            Vector3 directionToTarget = (hit.point - laserParticle.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
            laserParticle.transform.rotation = lookRotation;
            bulletController.target = hit.point;
            bulletController.hit = true;
            laserParticle.Play();
        }
        else
        {
            bulletController.target = cameraTransform.position + cameraTransform.forward * bulletMissDistance;
            bulletController.hit = false;
        }
    }

    void FixedUpdate()
    {
        playerSize = this.gameObject.transform.localScale.x;
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
            animator.SetBool("Jump", false);
        }

        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);
        move = move.x * cameraTransform.right.normalized + move.z * cameraTransform.forward.normalized;
        move.y = 0f;
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (input.x != 0 || input.y != 0)
        {
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }

        // Changes the height position of the player..
        if (jumpAction.triggered && groundedPlayer)
        {
            animator.SetBool("Jump", true);
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        Quaternion targetRotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
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


    public static class ChangeScale
    {
        public static IEnumerator StartFade(GameObject gameObject, float duration, float scaleMultiplier)
        {
            float currentTime = 0;
            Vector3 start = gameObject.transform.localScale;
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                float newValue = Mathf.Lerp(start.x, start.x * scaleMultiplier, currentTime / duration);
                gameObject.transform.localScale = new Vector3(newValue, newValue, newValue);
                yield return null;
            }
            yield break;
        }
    }

    private void Update()
    {
        if (insideOfMaze == true)
        {
            mazeTimerText.enabled = true;
            string timerStr = mazeTimer.ToString("00.00");
            mazeTimerText.SetText(timerStr);
            mazeTimer -= Time.deltaTime;
        }

        if (mazeTimer <= 0f)
        {
            LoseCondition();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MazeBox"))
        {
            mazeTimer = mazeTimerStart;
            insideOfMaze = true;
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.CompareTag("MazeBox"))
        {
            insideOfMaze = false;
        }
    }

    private void LoseCondition()
    {
        SceneManager.LoadScene("Lose");
    }
}
