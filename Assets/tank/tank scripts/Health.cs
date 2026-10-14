using UnityEngine;
using UnityEngine.UI;
using TMPro; // ќЅя«ј“≈Ћ№Ќќ добавл€ем эту строчку дл€ работы с TextMeshPro!

public class Health : MonoBehaviour
{
    [Header("ѕараметры здоровь€")]
    public int maxHealth = 100;
    private int currentHealth;

    [Header("»нтерфейс (“олько дл€ »грока)")]
    public Slider healthSlider;
    public TextMeshProUGUI hpText; 

    void Start()
    {
        currentHealth = maxHealth;

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = maxHealth;
        }

        
        UpdateHPUI();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

   
        UpdateHPUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

   
    void UpdateHPUI()
    {
        if (hpText != null)
        {
            
            hpText.text = currentHealth + " / " + maxHealth;
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " был уничтожен!");
        Destroy(gameObject);
    }
}
