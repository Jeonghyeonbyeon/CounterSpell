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
    [SerializeField] private GameObject targetObject;
    private Button selectedButton;
    private InputAction controlKeyAction;
    private InputAction iKeyAction;

    private void Awake()
    {
        controlKeyAction = new InputAction("ControlKey", binding: "<Keyboard>/leftCtrl");
        iKeyAction = new InputAction("IKey", binding: "<Keyboard>/i");
    }

    private void OnEnable()
    {
        controlKeyAction.Enable();
        iKeyAction.Enable();
    }

    private void OnDisable()
    {
        controlKeyAction.Disable();
        iKeyAction.Disable();
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
        if (controlKeyAction.ReadValue<float>() == 1 && iKeyAction.ReadValue<float>() == 1)
        {
            if (selectedButton != null)
            {
                ChangeTextToBoldAndItalic(selectedButton);
                AddJumpPadToTarget(targetObject);
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

    private void ChangeTextToBoldAndItalic(Button button)
    {
        Text buttonText = button.GetComponentInChildren<Text>();
        if (buttonText != null)
        {
            buttonText.fontStyle = FontStyle.Bold | FontStyle.Italic;
        }
    }

    private void AddJumpPadToTarget(GameObject target)
    {
        if (target != null)
        {
            JumpPad jumpPad = target.GetComponent<JumpPad>();
            if (jumpPad == null)
            {
                jumpPad = target.AddComponent<JumpPad>();
            }
            jumpPad.jumpPower = 350f;
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