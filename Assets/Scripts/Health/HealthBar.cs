using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image currentHealthBar;

    void Start()
    {
        UpdateHealthBar();
    }

    void Update()
    {
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        //Cambio de salud a porcentaje total
        currentHealthBar.fillAmount = playerHealth.CurrentHealth() / playerHealth.StartingHealth();
    }
}
