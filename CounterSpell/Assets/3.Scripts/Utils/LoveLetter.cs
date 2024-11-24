using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoveLetter : MonoBehaviour
{
    [SerializeField] private Sprite whiteBackgroundSprite;
    [SerializeField] private float moveAmount = 0.2f;
    [SerializeField] private float moveSpeed = 1.0f;
    private Image backgroundImage;
    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
        StartCoroutine(MoveUpDown());

        backgroundImage = GameObject.Find("UI/Background").GetComponent<Image>();
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
            if (GameManager.Instance.Stage < 5)
            {
                GameManager.Instance.UpdateStage();
                Destroy(gameObject);
            }
            else if (GameManager.Instance.Stage < 6)
            {
                backgroundImage.sprite = whiteBackgroundSprite;
                GameManager.Instance.UpdateStage();
                Destroy(gameObject);
            }
            else
            {
                SceneManager.LoadScene("InGame");
            }
        }
    }
}