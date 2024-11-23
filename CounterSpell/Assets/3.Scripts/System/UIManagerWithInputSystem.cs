using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class UIManagerWithEvents : MonoBehaviour
{
    [SerializeField] private List<Button> textButtons;
    [SerializeField] private List<GameObject> selectionObjects;
    private Button selectedButton;
    private InputAction controlKeyAction;
    private InputAction shiftKeyAction;
    private InputAction hKeyAction;

    private void Awake()
    {
        controlKeyAction = new InputAction("ControlKey", binding: "<Keyboard>/leftCtrl");
        shiftKeyAction = new InputAction("ShiftKey", binding: "<Keyboard>/leftShift");
        hKeyAction = new InputAction("HKey", binding: "<Keyboard>/h");
    }

    private void OnEnable()
    {
        controlKeyAction.Enable();
        shiftKeyAction.Enable();
        hKeyAction.Enable();
    }

    private void OnDisable()
    {
        controlKeyAction.Disable();
        shiftKeyAction.Disable();
        hKeyAction.Disable();
    }

    private void Start()
    {
        foreach (var obj in selectionObjects)
        {
            obj.SetActive(false);
        }

        for (int i = 0; i < textButtons.Count; i++)
        {
            int index = i;
            textButtons[i].onClick.AddListener(() => OnTextButtonClick(index));
        }
    }

    private void Update()
    {
        if (controlKeyAction.ReadValue<float>() == 1 && shiftKeyAction.ReadValue<float>() == 1 && hKeyAction.ReadValue<float>() == 1)
        {
            if (selectedButton != null)
            {
                ChangeTextColorAndToggleCollider(selectedButton);
            }
        }

        CheckClickOutside();
    }

    private void OnTextButtonClick(int index)
    {
        foreach (var obj in selectionObjects)
        {
            obj.SetActive(false);
        }

        if (index < selectionObjects.Count)
        {
            selectionObjects[index].SetActive(true);
        }

        selectedButton = textButtons[index];
    }

    private void ChangeTextColorAndToggleCollider(Button button)
    {
        Text buttonText = button.GetComponentInChildren<Text>();
        if (buttonText != null)
        {
            buttonText.color = Color.black;
        }

        Collider2D collider = button.GetComponentInChildren<Collider2D>();
        if (collider != null && !collider.enabled)
        {
            collider.enabled = true;
        }
    }

    private void CheckClickOutside()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current)
            {
                position = Mouse.current.position.ReadValue()
            };

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);

            bool clickedOnUI = false;
            foreach (RaycastResult result in results)
            {
                if (result.gameObject.GetComponent<Button>() != null)
                {
                    clickedOnUI = true;
                    break;
                }
            }

            if (!clickedOnUI)
            {
                foreach (var obj in selectionObjects)
                {
                    obj.SetActive(false);
                }
            }
        }
    }
}