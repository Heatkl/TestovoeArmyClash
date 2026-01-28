using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonExecutor : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button mainMenuButton;

    [Header("Scene Names")]
    [SerializeField] private string mainMenuSceneName;
    [SerializeField] private string gameSceneName;

    private void OnEnable()
    {
        if (startGameButton != null)
        {
            startGameButton.onClick.AddListener(LoadGame);
        }
        if (exitButton != null)
        {
            exitButton.onClick.AddListener(ExitGame);
        }
        if (mainMenuButton != null)
        {
            mainMenuButton.onClick.AddListener(LoadMenu);
        }
    }

    private void OnDisable()
    {
        if (startGameButton != null)
        {
            startGameButton.onClick.RemoveAllListeners();
        }
        if (exitButton != null)
        {
            exitButton.onClick.RemoveAllListeners();
        }
        if (mainMenuButton != null)
        {
            mainMenuButton.onClick.RemoveAllListeners();
        }
    }
    private void LoadGame()
    {
        LoadSceneByName(gameSceneName);
    }

    private void LoadMenu()
    {
        LoadSceneByName(mainMenuSceneName);
    }

    private void ExitGame()
    {
        Application.Quit();
    }
    private void LoadSceneByName(string name)
    {
        SceneManager.LoadScene(name);
    }
}
