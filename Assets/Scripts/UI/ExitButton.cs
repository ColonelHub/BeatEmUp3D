using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    private Button _button;
    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnExitButtonClicked);
    }

    private void OnExitButtonClicked()
    {
        Application.Quit();
    }
}
