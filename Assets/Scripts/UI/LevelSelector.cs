using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] private LevelsData levelsData;
    [SerializeField] private LevelButton[] levelButtons;

    [Space]
    [SerializeField] private GameObject loadingScreen;

    private void Start()
    {
        InitButtons();
        Time.timeScale = 1f;
    }
    private void InitButtons()
    {
        for (int i = 1; i <= levelButtons.Length; i++)
        {
            bool isLocked = i > PlayerPrefs.GetInt("LastAvailableLevel", 1);
            levelButtons[i - 1].Init(i, isLocked, this);
        }
    }
    public void LoadLevel(int level)
    {
        loadingScreen.SetActive(true);

        LevelData data = levelsData.Data[level - 1];

        PlayerPrefs.SetInt("CurrentLevel", level);
        SceneManager.LoadScene("Scenery " + data.SceneryIndex);
    }
}
