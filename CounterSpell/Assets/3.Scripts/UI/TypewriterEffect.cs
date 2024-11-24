using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TypewriterEffect : MonoBehaviour
{
    [SerializeField] private float typingSpeed = 0.05f;
    [SerializeField] private Button resetButton;
    private Text text;
    private Coroutine typingCoroutine;
    private string fullText;

    private void Start()
    {
        resetButton.onClick.AddListener(OnResetButtonClick);
        resetButton.gameObject.SetActive(false);

        text = GetComponent<Text>();
        fullText = text.text;
        text.text = "";
        StartTyping();
    }

    public void StartTyping()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        typingCoroutine = StartCoroutine(TypingCoroutine());
    }

    private IEnumerator TypingCoroutine()
    {
        text.text = "";
        yield return new WaitForSeconds(1f);
        foreach (char letter in fullText)
        {
            text.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        yield return new WaitForSeconds(1f);
        resetButton.gameObject.SetActive(true);
    }

    private void OnResetButtonClick()
    {
        SceneManager.LoadScene("InGame");
    }
}