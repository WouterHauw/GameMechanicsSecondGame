using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour {
    public float CurrentHealth { get; set; }
    public float MaxHealth { get; set; }

    public Slider HealthBar;

    // Use this for initialization
    void Start()
    {
        MaxHealth = 20f;
        CurrentHealth = MaxHealth;

        HealthBar.value = CalculateHealth();
    }


    public void DealDamage(float damageValue)
    {
        CurrentHealth -= damageValue;
        HealthBar.value = CalculateHealth();
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void AddHealth(float healhtValue)
    {
        CurrentHealth += healhtValue;
        if (CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
        HealthBar.value = CalculateHealth();
    }

    float CalculateHealth()
    {
        return CurrentHealth / MaxHealth;
    }

    private void Die()
    {
        SceneManager.LoadScene("_MainScene");
    }
}
