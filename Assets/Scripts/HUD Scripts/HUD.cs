using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    public PlayerController player;

    private bool isAiming = false;
    private bool isCooldown = false;

    public float glowCooldownTime = 10.0f; 
    public float abilityCooldownTime = 20.0f;

    private float glowCooldownTimer = 0.0f;
    private float abilityCooldownTimer = 0.0f;

    [SerializeField]
    private TextMeshProUGUI glowAbilityTimerText;
    [SerializeField]
    private TextMeshProUGUI glowCooldownTimerText;

    public Button shrinkButtonBorder;
    public Button shrinkButtonBackground;

    public Button aimButtonBorder;
    public Button aimButtonBackground;

    public Button laserButtonBorder;
    public Button laserButtonBackground;

    public Button glowButtonBorder;
    public Button glowButtonBackground;

    // Start is called before the first frame update
    void Awake()
    {
        player = GetComponent<PlayerController>();

        laserButtonBorder.interactable = false;
        laserButtonBackground.interactable = false;

        glowButtonBorder.interactable = true;
        glowButtonBackground.interactable = true;
        glowAbilityTimerText.gameObject.SetActive(false);
        glowCooldownTimerText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Shrinking
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            UseAbility(shrinkButtonBorder);
            UseAbility(shrinkButtonBackground);
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            FadeToColor(shrinkButtonBorder.colors.normalColor, shrinkButtonBorder);
            FadeToColor(shrinkButtonBackground.colors.normalColor, shrinkButtonBackground);
        }

        // Aiming
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            UseAbility(aimButtonBorder);
            UseAbility(aimButtonBackground);
            isAiming = true;
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            FadeToColor(aimButtonBorder.colors.normalColor, aimButtonBorder);
            FadeToColor(aimButtonBackground.colors.normalColor, aimButtonBackground);
            isAiming = false;
        }

        if (isAiming)
        {
            laserButtonBorder.interactable = true;
            laserButtonBackground.interactable = true;
        }
        else
        {
            laserButtonBorder.interactable = false;
            laserButtonBackground.interactable = false;
        }

        // Fire Laser
        if (isAiming && Input.GetKeyDown(KeyCode.Mouse0))
        {
            UseAbility(laserButtonBorder);
            UseAbility(laserButtonBackground);
        }
        else if (isAiming && Input.GetKeyUp(KeyCode.Mouse0))
        {
            FadeToColor(laserButtonBorder.colors.normalColor, laserButtonBorder);
            FadeToColor(laserButtonBackground.colors.normalColor, laserButtonBackground);
        }

        // Glowing
        if (Input.GetKeyDown(KeyCode.E))
        {
            UseGlow();
        }

        if (isCooldown)
        {
            ApplyCooldown();
        }
    }

    void UseAbility(Button button)
    {
        FadeToColor(button.colors.pressedColor, button);
        button.onClick.Invoke();
    }

    void FadeToColor(Color color, Button button)
    {
        Graphic graphic = button.gameObject.GetComponent<Graphic>();
        graphic.CrossFadeColor(color, button.colors.fadeDuration, true, true);
    }

    void UseGlow()
    {
        if (!isCooldown)
        {
            isCooldown = true;
            glowAbilityTimerText.gameObject.SetActive(true);
            glowButtonBorder.interactable = false;
            glowButtonBackground.interactable = false;
            glowCooldownTimer = glowCooldownTime;
            abilityCooldownTimer = abilityCooldownTime;
        }

    }

    void ApplyCooldown()
    {
        abilityCooldownTimer -= Time.deltaTime;
        glowCooldownTimer -= Time.deltaTime;

        if (abilityCooldownTimer < 0.0f)
        {
            glowAbilityTimerText.gameObject.SetActive(false);
            glowCooldownTimerText.gameObject.SetActive(true);
            
            if (glowCooldownTimer < 0.0f)
            {
                isCooldown = false;
                glowCooldownTimerText.gameObject.SetActive(false);
                glowButtonBorder.interactable = true;
                glowButtonBackground.interactable = true;
            }
            else
            {
                glowCooldownTimerText.text = Mathf.RoundToInt(glowCooldownTimer).ToString();
            }
        }
        else
        {
            glowAbilityTimerText.text = Mathf.RoundToInt(abilityCooldownTimer).ToString();
        }
        
        
    }
}