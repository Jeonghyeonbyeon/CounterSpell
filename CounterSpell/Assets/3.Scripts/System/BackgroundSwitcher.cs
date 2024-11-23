using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BackgroundSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject[] whiteBackgroundObjects;
    [SerializeField] private GameObject[] blackBackgroundObjects;
    [SerializeField] private Sprite whiteBackgroundSprite;
    [SerializeField] private Sprite blackBackgroundSprite;
    [SerializeField] private Image backgroundImage;
    private InputAction controlKeyAction;
    private InputAction shiftKeyAction;
    private InputAction lKeyAction;
    private bool isWhiteBackground = true;
    private bool isKeyPressed = false;

    private void Awake()
    {
        controlKeyAction = new InputAction("ControlKey", binding: "<Keyboard>/leftCtrl");
        shiftKeyAction = new InputAction("ShiftKey", binding: "<Keyboard>/leftShift");
        lKeyAction = new InputAction("LKey", binding: "<Keyboard>/L");
    }

    private void OnEnable()
    {
        controlKeyAction.Enable();
        shiftKeyAction.Enable();
        lKeyAction.Enable();
    }

    private void OnDisable()
    {
        controlKeyAction.Disable();
        shiftKeyAction.Disable();
        lKeyAction.Disable();
    }

    private void Update()
    {
        if (controlKeyAction.ReadValue<float>() == 1 && shiftKeyAction.ReadValue<float>() == 1 && lKeyAction.ReadValue<float>() == 1)
        {
            if (!isKeyPressed)
            {
                SwitchBackground();
                isKeyPressed = true;
            }
        }
        else
        {
            isKeyPressed = false;
        }
    }

    private void SwitchBackground()
    {
        if (isWhiteBackground)
        {
            backgroundImage.sprite = blackBackgroundSprite;
            SwitchBackgroundObjects(blackBackgroundObjects, whiteBackgroundObjects);
        }
        else
        {
            backgroundImage.sprite = whiteBackgroundSprite;
            SwitchBackgroundObjects(whiteBackgroundObjects, blackBackgroundObjects);
        }
        isWhiteBackground = !isWhiteBackground;
    }

    private void SwitchBackgroundObjects(GameObject[] objectsToActivate, GameObject[] objectsToDeactivate)
    {
        foreach (var obj in objectsToDeactivate)
        {
            obj.SetActive(false);
        }

        foreach (var obj in objectsToActivate)
        {
            obj.SetActive(true);
        }
    }
}