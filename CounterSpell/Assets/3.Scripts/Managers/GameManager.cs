using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private GameObject[] stageUI;
    [SerializeField] private StageManager stageManager;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform startPos;
    [SerializeField] private Image transitionImage;
    [SerializeField] private float transitionDuration = 1.0f;
    [SerializeField] private GameObject helpImage;

    public int Stage { get; private set; } = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            stageManager.UpdateStage(Stage);
            SetPlayerPos();
            DisableAllHelp();
        }
    }

    public void UpdateStage()
    {
        StartCoroutine(StageTransition());
    }

    private IEnumerator StageTransition()
    {
        player.SetActive(false);

        yield return StartCoroutine(CloseTransition());

        if (Stage < stageUI.Length)
        {
            stageUI[Stage].SetActive(false);
        }

        Stage++;

        if (Stage < stageUI.Length)
        {
            stageUI[Stage].SetActive(true);
            stageManager.UpdateStage(Stage);
            SetPlayerPos();
            DisableAllHelp();
        }

        yield return StartCoroutine(OpenTransition());

        player.SetActive(true);
    }

    private IEnumerator CloseTransition()
    {
        if (transitionImage != null)
        {
            transitionImage.gameObject.SetActive(true);
            transitionImage.fillAmount = 0;

            float elapsedTime = 0f;
            while (elapsedTime < transitionDuration)
            {
                elapsedTime += Time.deltaTime;
                transitionImage.fillAmount = Mathf.Lerp(0, 1, elapsedTime / transitionDuration);
                yield return null;
            }

            transitionImage.fillAmount = 1;
        }
    }

    private IEnumerator OpenTransition()
    {
        if (transitionImage != null)
        {
            float elapsedTime = 0f;
            while (elapsedTime < transitionDuration)
            {
                elapsedTime += Time.deltaTime;
                transitionImage.fillAmount = Mathf.Lerp(1, 0, elapsedTime / transitionDuration);
                yield return null;
            }

            transitionImage.fillAmount = 0;
            transitionImage.gameObject.SetActive(false);
        }
    }

    private void SetPlayerPos()
    {
        if (startPos != null)
        {
            player.transform.position = startPos.position;
        }
    }

    private void DisableAllHelp()
    {
        if (helpImage != null)
        {
            foreach (Transform child in helpImage.transform)
            {
                child.gameObject.SetActive(false);
            }
            helpImage.SetActive(false);
        }
    }
}
