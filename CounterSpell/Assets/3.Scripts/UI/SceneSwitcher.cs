using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] private Button switchSceneButton;
    [SerializeField] private string sceneName;

    private void Awake()
    {
        if (switchSceneButton == null)
        {
            switchSceneButton = FindObjectOfType<Button>();
        }
    }

    private void Start()
    {
        if (switchSceneButton != null)
        {
            switchSceneButton.onClick.AddListener(() => SwitchScene());
        }
    }

    private void SwitchScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}