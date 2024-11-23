using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStepEffect : MonoBehaviour
{
    [SerializeField] private GameObject[] ground;
    private bool isEffect;

    private void Update()
    {
        if (isEffect)
        {
            ApplyEffect();
            isEffect = false;
        }
    }

    private void ApplyEffect()
    {
        Color effectColor;
        ColorUtility.TryParseHtmlString("#9B9A97", out effectColor);

        foreach (GameObject obj in ground)
        {
            Text text = obj.GetComponent<Text>();
            if (text != null)
            {
                text.color = effectColor;
            }

            Collider2D collider = obj.transform.GetChild(0).GetComponent<Collider2D>();
            if (collider != null)
            {
                collider.enabled = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            isEffect = true;
        }
    }
}