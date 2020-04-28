using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public static Health Instance;
    public GameObject insertPlayerScore;
    public int startingHealth = 100;
    public static float currentHealth;
    public Slider healthSlider;
    public int attackDamage = 20;
    public bool triggerAnimation;
    private void Awake()
    {
        currentHealth = startingHealth;
        healthSlider.maxValue = currentHealth;
        healthSlider.value = currentHealth;
    }
    
    void Start()
    {
        Instance = this;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        healthSlider.value = currentHealth;

        if (currentHealth <= 0)
        {
            insertPlayerScore.SetActive(true);
            triggerAnimation = true;
            Energy.Instance.destroyPlayer = true;
        }
    }
}
