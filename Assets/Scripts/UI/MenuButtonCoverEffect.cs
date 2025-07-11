using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButtonCoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TextMeshProUGUI _textMeshPro;
    private string _startText;

    private void Start()
    {
        _textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
        _startText = _textMeshPro.text;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _textMeshPro.text = $"->{_startText}<-";
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _textMeshPro.text = _startText;
    }

    private void OnDisable()
    {
        _textMeshPro.text = _startText;
    }
}
