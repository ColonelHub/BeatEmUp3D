using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelText;

    private LevelSelector _levelSelector;
    private Button _button;
    private int _level;

    public void Init(int level, bool isLocked, LevelSelector levelSelector)
    {
        _button = GetComponent<Button>();

        levelText.text = level.ToString();
        _level = level;
        _levelSelector = levelSelector;

        if (isLocked)
        {
            _button.interactable = false;
        }
        else
        {
            _button.interactable = true;
            _button.onClick.AddListener(LoadLevel);
        }
    }

    private void LoadLevel()
    {
        _levelSelector.LoadLevel(_level);
    }
}
