using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI enemiesLeftText;

    [Header("UI Victory")]
    [SerializeField] private GameObject victoryScreen;
    [SerializeField] private Button nextLevelButton;

    [Header("UI Defeat")]
    [SerializeField] private GameObject defeatScreen;

    [Header("Pause")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Button resumeButton;

    [Space]
    [SerializeField] private Button[] restartButtons;
    [SerializeField] private Button[] mmenuButtons;

    [Space]
    [SerializeField] private GameObject loadingScreen;

    private void Start()
    {
        levelText.text = $"Level {PlayerPrefs.GetInt("CurrentLevel", 1)}";

        InitButtonsEvents();
    }
    private void InitButtonsEvents()
    {
        nextLevelButton.onClick.AddListener(NextLevel);
        resumeButton.onClick.AddListener(ResumeGame);

        foreach (var button in restartButtons)
        {
            button.onClick.AddListener(RestartLevel);
        }
        foreach (var button in mmenuButtons)
        {
            button.onClick.AddListener(MainMenu);
        }

        if (PlayerPrefs.GetInt("CurrentLevel", 1) == 9)
            nextLevelButton.gameObject.SetActive(false);
    }
    public void ShowVictoryScreen()
    {
        victoryScreen.SetActive(true);
    }

    public void ShowDefeatScreen()
    {
        defeatScreen.SetActive(true);
    }
    public void UpdateEnemiesLeft(int enemiesLeft)
    {
        enemiesLeftText.text = $"Enemies Left: {enemiesLeft}";
    }
    public void ShowLoadingScreen()
    {
        loadingScreen.SetActive(true);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu.activeSelf)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
    private void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    private void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    private void NextLevel()
    {
        ShowLoadingScreen();
        SceneManager.LoadScene(PlayerPrefs.GetInt("CurrentLevel", 1) + 1);
    }
    private void RestartLevel()
    {
        ShowLoadingScreen();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void MainMenu()
    {
        ShowLoadingScreen();
        SceneManager.LoadScene("Menu");
    }
}
