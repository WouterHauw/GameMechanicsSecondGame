using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour {
    public float CurrentHealth { get; set; }
    public float MaxHealth { get; set; }
    public LevelManager levelManager;
    public int Lives;
    public Text livesText;

    public Slider HealthBar;

    // Use this for initialization
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        Lives = 10;
        MaxHealth = 20f;
        CurrentHealth = MaxHealth;
        livesText.text = Lives.ToString();
        livesText.gameObject.SetActive(true);
        HealthBar.value = CalculateHealth();
    }

    private void Update()
    {
        if (transform.position.y < -50f)
        {
            Die();
        }
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
        if (Lives <= 0)
        {
            SceneManager.LoadScene("_MainScene");
        }
        else
        {
            levelManager.RespawnPlayer();
            Lives--;
            livesText.text = Lives.ToString();
            CurrentHealth = MaxHealth;
            HealthBar.value = CalculateHealth();
        }
        
    }
}
