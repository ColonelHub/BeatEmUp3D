using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthbar : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private Slider healthSlider;

    private void Start()
    {
        Init();
    }
    private void Init()
    {
        playerHealth.OnTakeDamage += UpdateSlider;

        healthSlider.maxValue = playerHealth.MaxHealth;
        healthSlider.value = playerHealth.MaxHealth;
    }

    private void UpdateSlider()
    {
        healthSlider.value = playerHealth.Health;
    }
}
