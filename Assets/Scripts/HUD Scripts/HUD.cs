using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
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

    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.LeftControl))
        // {
        //     DoTheThing(shrinkButtonBorder);
        //     DoTheThing(shrinkButtonBackground);
        // }
        // else if (Input.GetKeyUp(KeyCode.LeftControl))
        // {
        //     FadeToColor(shrinkButtonBorder.colors.normalColor, shrinkButtonBorder);
        //     FadeToColor(shrinkButtonBackground.colors.normalColor, shrinkButtonBackground);
        // }

        // if (Input.GetKeyDown(KeyCode.Mouse1))
        // {
        //     DoTheThing(aimButtonBorder);
        //     DoTheThing(aimButtonBackground);
        // }
        // else if (Input.GetKeyUp(KeyCode.Mouse1))
        // {
        //     FadeToColor(aimButtonBorder.colors.normalColor, aimButtonBorder);
        //     FadeToColor(aimButtonBackground.colors.normalColor, aimButtonBackground);
        // }

        // if (Input.GetKeyDown(KeyCode.Mouse0))
        // {
        //     DoTheThing(laserButtonBorder);
        //     DoTheThing(laserButtonBackground);
        // }
        // else if (Input.GetKeyUp(KeyCode.Mouse0))
        // {
        //     FadeToColor(laserButtonBorder.colors.normalColor, aimButtonBorder);
        //     FadeToColor(laserButtonBackground.colors.normalColor, aimButtonBackground);
        // }
    }

    void DoTheThing(Button button)
    {
        FadeToColor(button.colors.pressedColor, button);
        button.onClick.Invoke();
    }

    void FadeToColor(Color color, Button button)
    {
        Graphic graphic = button.gameObject.GetComponent<Graphic>();
        graphic.CrossFadeColor(color, button.colors.fadeDuration, true, true);
    }

}