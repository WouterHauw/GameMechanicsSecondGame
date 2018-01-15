using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour {
    public float CurrentHealth { get; set; }
    public float MaxHealth { get; set; }
    public LevelManager levelManager;
    private int _lives;
    public Text livesText;

    public Slider HealthBar;

    // Use this for initialization
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        _lives = 10;
        MaxHealth = 20f;
        CurrentHealth = MaxHealth;
        livesText.text = _lives.ToString();
        livesText.gameObject.SetActive(true);
        HealthBar.value = CalculateHealth();
    }

    public void AddTry()
    {
        _lives++;
        livesText.text = _lives.ToString();
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
        if (_lives <= 0)
        {
            SceneManager.LoadScene("_MainScene");
        }
        else
        {
            levelManager.RespawnPlayer();
            _lives--;
            livesText.text = _lives.ToString();
            CurrentHealth = MaxHealth;
            HealthBar.value = CalculateHealth();
        }
        
    }
}
