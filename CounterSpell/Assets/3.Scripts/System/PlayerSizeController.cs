using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSizeController : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Vector3 defaultScale = Vector3.one;
    [SerializeField] private Vector3 halfScale = new Vector3(0.5f, 0.5f, 1f);
    [SerializeField] private float defaultJumpPower = 10f;
    [SerializeField] private float halfJumpPower = 5f;

    public void OnSizeIncrease(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            playerController.SetScale(defaultScale);
            playerController.SetJumpPower(defaultJumpPower);
        }
    }

    public void OnSizeDecrease(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            playerController.SetScale(halfScale);
            playerController.SetJumpPower(halfJumpPower);
        }
    }
}