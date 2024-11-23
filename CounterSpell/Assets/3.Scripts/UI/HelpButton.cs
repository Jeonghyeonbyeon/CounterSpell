using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HelpButton : MonoBehaviour
{
    [SerializeField] private GameObject helpImage;
    private Button helpButton;

    void Start()
    {
        helpButton = GetComponent<Button>();
        helpButton.onClick.AddListener(HandleHelpButtonClick);
    }

    private void HandleHelpButtonClick()
    {
        ToggleHelpImage();
        UnfocusButton();
    }

    private void ToggleHelpImage()
    {
        if (helpImage == null) return;

        if (!helpImage.activeSelf)
        {
            helpImage.SetActive(true);
        }

        int stage = GameManager.Instance.Stage;

        if (stage >= 0 && stage < helpImage.transform.childCount)
        {
            Transform currentHelp = helpImage.transform.GetChild(stage);

            bool isActive = !currentHelp.gameObject.activeSelf;

            foreach (Transform child in helpImage.transform)
            {
                child.gameObject.SetActive(false);
            }

            currentHelp.gameObject.SetActive(isActive);

            if (!isActive)
            {
                helpImage.SetActive(false);
            }
        }
    }

    private void UnfocusButton()
    {
        EventSystem.current.SetSelectedGameObject(null);
    }
}