using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypewriterEffect : MonoBehaviour
{
    [SerializeField] private float typingSpeed = 0.05f;
    private Text text;
    private Coroutine typingCoroutine;
    private string fullText;

    private void Start()
    {
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
        foreach (char letter in fullText)
        {
            text.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}