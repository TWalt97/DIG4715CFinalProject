using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    // Testing
    public bool isHub;
    public bool isMaze;
    public bool isArena;
    public bool isVent;
    public bool isGlow;

    private bool isAiming = false;

    // Old Glow
    // private bool isCooldown = false;

    // public float glowCooldownTime = 10.0f; 
    // public float abilityCooldownTime = 20.0f;

    // private float glowCooldownTimer = 0.0f;
    // private float abilityCooldownTimer = 0.0f;

    // [SerializeField]
    // private TextMeshProUGUI glowAbilityTimerText;
    // [SerializeField]
    // private TextMeshProUGUI glowCooldownTimerText;

    public GameObject mazeObjective;
    public GameObject arenaObjective;
    public GameObject ventObjective;
    public GameObject hubObjective;
    public GameObject escapeObjective;

    public Button shrinkButtonBorder;
    public Button shrinkButtonBackground;

    public Button aimButtonBorder;
    public Button aimButtonBackground;

    public Button laserButtonBorder;
    public Button laserButtonBackground;

    public Button glowButtonBorder;
    public Button glowButtonBackground;

    private ColorBlock shrinkBorderColors;
    private ColorBlock shrinkBackgroundColors;
    private ColorBlock aimBorderColors;
    private ColorBlock aimBackgroundColors;
    private ColorBlock laserBorderColors;
    private ColorBlock laserBackgroundColors;
    private ColorBlock glowBorderColors;
    private ColorBlock glowBackgroundColors;

    private Color defaultBorderButtonBaseColor = new Color(0.4603736f, 0.2856444f, 0.4622642f, 1.0f);
    private Color defaultBorderButtonPressedColor = new Color(0.4603736f, 0.2856444f, 0.4622642f, 0.5882353f);
    private Color defaultBackgroundButtonBaseColor = new Color(0.6352941f, 0.3540406f, 0.6431373f, 1.0f);
    private Color defaultBackgroundButtonPressedColor = new Color(0.6352941f, 0.3540406f, 0.6431373f,  0.5882353f);

    private Color mazeBorderButtonBaseColor = new Color(0.2470588f, 0.2392157f, 0.5882353f, 1.0f);
    private Color mazeBorderButtonPressedColor = new Color(0.2470588f, 0.2392157f, 0.5882353f, 0.5882353f);
    private Color mazeBackgroundButtonBaseColor = new Color (0.6196079f, 0.7301583f, 0.8980392f, 1.0f);
    private Color mazeBackgroundButtonPressedColor = new Color (0.6196079f, 0.7301583f, 0.8980392f, 0.5882353f);

    private Color arenaBorderButtonBaseColor = new Color(0.5843138f, 0.2352941f, 0.2388183f, 1.0f);
    private Color arenaBorderButtonPressedColor = new Color(0.5843138f, 0.2352941f, 0.2388183f, 0.5882353f);
    private Color arenaBackgroundButtonBaseColor = new Color(0.8980392f, 0.6875842f, 0.6196079f, 1.0f);
    private Color arenaBackgroundButtonPressedColor = new Color(0.8980392f, 0.6875842f, 0.6196079f, 0.5882353f);

    private Color ventBorderButtonBaseColor = new Color(0.8901961f, 0.8980393f, 0.6196079f, 1.0f);
    private Color ventBorderButtonPressedColor = new Color(0.8901961f, 0.8980393f, 0.6196079f, 0.5882353f);
    private Color ventBackgroundButtonBaseColor = new Color(0.8161573f, 0.8301887f, 0.3320754f, 1.0f);
    private Color ventBackgroundButtonPressedColor = new Color(0.8161573f, 0.8301887f, 0.3320754f, 0.5882353f);

    // Start is called before the first frame update
    void Awake()
    {
        shrinkBorderColors = shrinkButtonBorder.colors;
        shrinkBackgroundColors = shrinkButtonBackground.colors;
        aimBorderColors = aimButtonBorder.colors;
        aimBackgroundColors = aimButtonBackground.colors;
        laserBorderColors = laserButtonBorder.colors;
        laserBackgroundColors = laserButtonBackground.colors;
        glowBorderColors = glowButtonBorder.colors;
        glowBackgroundColors = glowButtonBackground.colors;
        // player = GetComponent<PlayerController>();

        mazeObjective.SetActive(false);
        arenaObjective.SetActive(false);
        ventObjective.SetActive(false);
        hubObjective.SetActive(true);
        escapeObjective.SetActive(false);

        laserButtonBorder.interactable = false;
        laserButtonBackground.interactable = false;

        glowButtonBorder.interactable = true;
        glowButtonBackground.interactable = true;

        // Set default color
        SetDefault();
    }

    void SetDefault ()
    {
        SetColor(shrinkBorderColors, defaultBorderButtonBaseColor, defaultBorderButtonPressedColor);
        SetColor(shrinkBackgroundColors, defaultBackgroundButtonBaseColor, defaultBackgroundButtonPressedColor);
        SetColor(aimBorderColors, defaultBorderButtonBaseColor, defaultBorderButtonPressedColor);
        SetColor(aimBackgroundColors, defaultBackgroundButtonBaseColor, defaultBackgroundButtonPressedColor);
        SetColor(laserBorderColors, defaultBorderButtonBaseColor, defaultBorderButtonPressedColor);
        SetColor(laserBackgroundColors, defaultBackgroundButtonBaseColor, defaultBackgroundButtonPressedColor);
        SetColor(glowBorderColors, defaultBorderButtonBaseColor, defaultBorderButtonPressedColor);
        SetColor(glowBackgroundColors, defaultBackgroundButtonBaseColor, defaultBackgroundButtonPressedColor);
    }

    void SetMaze ()
    {
        SetColor(shrinkBorderColors, mazeBorderButtonBaseColor, mazeBorderButtonPressedColor);
        SetColor(shrinkBackgroundColors, mazeBackgroundButtonBaseColor, mazeBackgroundButtonPressedColor);
        SetColor(aimBorderColors, mazeBorderButtonBaseColor, mazeBorderButtonPressedColor);
        SetColor(aimBackgroundColors, mazeBackgroundButtonBaseColor, mazeBackgroundButtonPressedColor);
        SetColor(laserBorderColors, mazeBorderButtonBaseColor, mazeBorderButtonPressedColor);
        SetColor(laserBackgroundColors, mazeBackgroundButtonBaseColor, mazeBackgroundButtonPressedColor);
        SetColor(glowBorderColors, mazeBorderButtonBaseColor, mazeBorderButtonPressedColor);
        SetColor(glowBackgroundColors, mazeBackgroundButtonBaseColor, mazeBackgroundButtonPressedColor);
    }

    void SetArena ()
    {
        SetColor(shrinkBorderColors, arenaBorderButtonBaseColor, arenaBorderButtonPressedColor);
        SetColor(shrinkBackgroundColors, arenaBackgroundButtonBaseColor, arenaBackgroundButtonPressedColor);
        SetColor(aimBorderColors, arenaBorderButtonBaseColor, arenaBorderButtonPressedColor);
        SetColor(aimBackgroundColors, arenaBackgroundButtonBaseColor, arenaBackgroundButtonPressedColor);
        SetColor(laserBorderColors, arenaBorderButtonBaseColor, arenaBorderButtonPressedColor);
        SetColor(laserBackgroundColors, arenaBackgroundButtonBaseColor, arenaBackgroundButtonPressedColor);
        SetColor(glowBorderColors, arenaBorderButtonBaseColor, arenaBorderButtonPressedColor);
        SetColor(glowBackgroundColors, arenaBackgroundButtonBaseColor, arenaBackgroundButtonPressedColor);
    }

    void SetVent ()
    {
        SetColor(shrinkBorderColors, ventBorderButtonBaseColor, ventBorderButtonPressedColor);
        SetColor(shrinkBackgroundColors, ventBackgroundButtonBaseColor, ventBackgroundButtonPressedColor);
        SetColor(aimBorderColors, ventBorderButtonBaseColor, ventBorderButtonPressedColor);
        SetColor(aimBackgroundColors, ventBackgroundButtonBaseColor, ventBackgroundButtonPressedColor);
        SetColor(laserBorderColors, ventBorderButtonBaseColor, ventBorderButtonPressedColor);
        SetColor(laserBackgroundColors, ventBackgroundButtonBaseColor, ventBackgroundButtonPressedColor);
        SetColor(glowBorderColors, ventBorderButtonBaseColor, ventBorderButtonPressedColor);
        SetColor(glowBackgroundColors, ventBackgroundButtonBaseColor, ventBackgroundButtonPressedColor);
    }

    void SetColor (ColorBlock colors, Color baseColor, Color pressedColor)
    {
        colors.normalColor = baseColor;
        colors.pressedColor = pressedColor;
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

        isGlowing(isGlow);

        // Old Glowing
        // if (Input.GetKeyDown(KeyCode.E))
        // {
        //     UseGlow();
        // }

        // if (isCooldown)
        // {
        //     ApplyCooldown();
        // }
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

    public void isGlowing (bool isGlow)
    {
        if (isGlow)
        {
            glowButtonBorder.interactable = false;
            glowButtonBackground.interactable = false;
        }
        else
        {
            glowButtonBorder.interactable = true;
            glowButtonBackground.interactable = true;
        }
    }

    // public void ChangeHUDDefault (bool isHub)
    // {

    // }

    // public void ChangeObjective (String level)
    // {
    //     if (isMaze)
    //     {

    //     }
    //     else
    //     {
            
    //     }
    // }


    // Old Glowing
    // void UseGlow()
    // {
    //     if (!isCooldown)
    //     {
    //         isCooldown = true;
    //         glowAbilityTimerText.gameObject.SetActive(true);
    //         glowButtonBorder.interactable = false;
    //         glowButtonBackground.interactable = false;
    //         glowCooldownTimer = glowCooldownTime;
    //         abilityCooldownTimer = abilityCooldownTime;
    //     }
    // }

    // void ApplyCooldown()
    // {
    //     abilityCooldownTimer -= Time.deltaTime;
    //     glowCooldownTimer -= Time.deltaTime;

    //     if (abilityCooldownTimer < 0.0f)
    //     {
    //         glowAbilityTimerText.gameObject.SetActive(false);
    //         glowCooldownTimerText.gameObject.SetActive(true);
            
    //         if (glowCooldownTimer < 0.0f)
    //         {
    //             isCooldown = false;
    //             glowCooldownTimerText.gameObject.SetActive(false);
    //             glowButtonBorder.interactable = true;
    //             glowButtonBackground.interactable = true;
    //         }
    //         else
    //         {
    //             glowCooldownTimerText.text = Mathf.RoundToInt(glowCooldownTimer).ToString();
    //         }
    //     }
    //     else
    //     {
    //         glowAbilityTimerText.text = Mathf.RoundToInt(abilityCooldownTimer).ToString();
    //     }   
    // }
}