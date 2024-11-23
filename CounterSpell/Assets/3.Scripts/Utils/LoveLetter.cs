using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoveLetter : MonoBehaviour
{
    [SerializeField] private float moveAmount = 0.2f;
    [SerializeField] private float moveSpeed = 1.0f;
    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
        StartCoroutine(MoveUpDown());
    }

    private IEnumerator MoveUpDown()
    {
        while (true)
        {
            float newY = Mathf.Sin(Time.time * moveSpeed) * moveAmount;
            transform.position = initialPosition + new Vector3(0, newY, 0);
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            GameManager.Instance.UpdateStage();
            Destroy(gameObject);
        }
    }
}