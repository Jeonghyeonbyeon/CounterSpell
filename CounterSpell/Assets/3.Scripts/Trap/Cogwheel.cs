using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cogwheel : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;

    void Update()
    {
        transform.Rotate(0f, 0f, -rotationSpeed * Time.deltaTime);
    }
}